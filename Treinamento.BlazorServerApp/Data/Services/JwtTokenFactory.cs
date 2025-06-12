using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Treinamento.BlazorServerApp.Data.Services;

public class JwtTokenFactory(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
{
    public string GenerateJwtFromCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity?.IsAuthenticated == true)
            throw new InvalidOperationException("Usuário não autenticado.");

        var claims = user.Claims;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["AppSettings:Emissor"],
            audience: configuration["AppSettings:ValidoEm"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
