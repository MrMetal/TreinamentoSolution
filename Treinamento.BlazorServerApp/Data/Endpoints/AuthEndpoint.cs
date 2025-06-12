using Treinamento.BlazorServerApp.Data.Services;

namespace Treinamento.BlazorServerApp.Data.Endpoints;

public static class AuthEndpoint
{
    public static void AuthEndpointMap(this WebApplication app)
    {
        var group = app.MapGroup("Auth");
        group.MapGet("SignIn", LoginEndpoint.ExecuteAsync);
        group.MapGet("SignOut", LogoutEndpoint.ExecuteAsync);
    }
}

public class LoginEndpoint
{
    internal static async Task<IResult> ExecuteAsync(string token, SignInUserFromJwtAsyncService signInUserFromJwtAsyncService)
    {
        var result = await signInUserFromJwtAsyncService.SignInUserFromJwtAsync(token);
        return Results.Redirect(result ? "/" : "login");
    }
}

public class LogoutEndpoint
{
    internal static async Task<IResult> ExecuteAsync(SignInUserFromJwtAsyncService signInUserFromJwtAsyncService)
    {
        await signInUserFromJwtAsyncService.SignOutUserAsync();
        return Results.Redirect("/login"); 
    }
}