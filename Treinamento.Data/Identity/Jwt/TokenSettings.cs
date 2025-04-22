using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Treinamento.Shared.ViewModels;

namespace Treinamento.Data.Identity.Jwt;

public class TokenSettings(UserManager<ApplicationUser> userManager)
{
    public async Task<LoginResponseViewModel> GerarJwt(string email, AppSettings appSettings)
    {
        var user = await userManager.FindByEmailAsync(email)
                   ?? throw new Exception("Usuário não encontrado.");

        var claims = await ObterClaimsAsync(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret!));

        var enumerable = claims as Claim[] ?? claims.ToArray();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(enumerable),
            Expires = DateTime.UtcNow.AddHours(appSettings.ExpiracaoHoras),
            Issuer = appSettings.Emissor,
            Audience = appSettings.ValidoEm,
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encodedToken = tokenHandler.WriteToken(token);

        return new LoginResponseViewModel
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(appSettings.ExpiracaoHoras).TotalSeconds,
            UserToken = new UserTokenViewModel
            {
                Id = user.Id,
                Email = user.Email,
                NomeUsuario = user.Nome,
                SobreNome = user.Sobrenome,
                TipoUsuario = user.Tipo,
                Bloqueado = user.Bloqueado,
                Ativo = user.Ativo,
                Claims = enumerable.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value }).ToList()
            }
        };
    }

    private async Task<IEnumerable<Claim>> ObterClaimsAsync(ApplicationUser user)
    {
        var claims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
            new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
        };

        jwtClaims.AddRange(claims);
        jwtClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return jwtClaims;
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

}