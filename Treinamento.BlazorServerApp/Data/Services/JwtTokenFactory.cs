using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Treinamento.BlazorServerApp.Data.Services;

public class JwtTokenFactory(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
{
    public string GenerateJwtFromCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        var context = httpContextAccessor.HttpContext;

        if (context?.User == null || !context.User.Identity?.IsAuthenticated == true)
        {
            // Redireciona para a tela de login
            context?.Response.Redirect("/login"); // ou a rota correta da sua aplicação
            return string.Empty; // ou null, dependendo do seu fluxo
        }

        var claims = user!.Claims;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["AppSettings:Emissor"],
            audience: configuration["AppSettings:ValidoEm"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds);

        var result = new JwtSecurityTokenHandler().WriteToken(token);

        var isTokenValid = IsTokenStillValid(result);
        return result;
    }

    private static bool IsTokenStillValid(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.ValidTo > DateTime.UtcNow;
        }
        catch
        {
            return false;
        }
    }
}