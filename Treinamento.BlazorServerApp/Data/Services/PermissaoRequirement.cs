using Microsoft.AspNetCore.Authorization;

namespace Treinamento.BlazorServerApp.Data.Services;

public class ClaimRequirement(string claimType, params string[] allowedValues) : IAuthorizationRequirement
{
    public string ClaimType { get; } = claimType;
    public string[] AllowedValues { get; } = allowedValues;
}

public class PermissaoHandler : AuthorizationHandler<ClaimRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
    {
        var claim = context.User.FindFirst(requirement.ClaimType);
        if (claim == null)
            return Task.CompletedTask;

        var userValues = claim.Value.Split(", ", StringSplitOptions.RemoveEmptyEntries);

        if (requirement.AllowedValues.Any(v => userValues.Contains(v)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}