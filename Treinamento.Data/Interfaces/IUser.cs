using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Treinamento.Data.Interfaces;

public interface IUser
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    bool IsAuthenticated();
    bool IsInRole(string role);
    IEnumerable<Claim> GetClaimsIdentity();
}


public class AspNetUser(IHttpContextAccessor accessor) : IUser
{
    public string Name => accessor.HttpContext!.User.Identity!.Name!;

    public Guid GetUserId() => IsAuthenticated() ? Guid.Parse(accessor.HttpContext!.User.GetUserId()) : Guid.Empty;

    public string GetUserEmail() => IsAuthenticated() ? accessor.HttpContext!.User.GetUserEmail() : "";

    public bool IsAuthenticated() => accessor.HttpContext!.User.Identity!.IsAuthenticated;

    public bool IsInRole(string role) => accessor.HttpContext!.User.IsInRole(role);

    public IEnumerable<Claim> GetClaimsIdentity() => accessor.HttpContext!.User.Claims;
}

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentException(nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        return claim?.Value!;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentException(nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Email);
        return claim?.Value!;
    }
}