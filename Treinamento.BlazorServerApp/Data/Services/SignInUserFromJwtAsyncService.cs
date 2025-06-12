using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Treinamento.BlazorServerApp.Data.Services;

public class SignInUserFromJwtAsyncService(IHttpContextAccessor httpContextAccessor)
{
    public async Task<bool> SignInUserFromJwtAsync(string tokenResponse)
    {
        if (string.IsNullOrWhiteSpace(tokenResponse)) return false;

        var jwtHandler = new JwtSecurityTokenHandler();
        var token = jwtHandler.ReadToken(tokenResponse) as JwtSecurityToken;
        jwtHandler.ReadToken(tokenResponse);

        var claims = token!.Claims.ToList();

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
        };

        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null) return false;

        await httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProperties);

        return true;
    }

    public async Task SignOutUserAsync()
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext != null) 
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}