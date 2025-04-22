using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Treinamento.Data.Identity;

public class CustomAuthorization
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
            : base(typeof(RequisitoClaimFilter))
        {
            Arguments = [new Claim(claimName, claimValue)];
        }
    }

    public class RequisitoClaimFilter(Claim claim) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity!.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!ValidarClaimsUsuario(context.HttpContext, claim.Type, claim.Value)) 
                context.Result = new StatusCodeResult(403);
        }
    }

    public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
    {
        var list = claimName.Split(',');

        foreach (var item in list)
            if (context.User.Claims.Any(c => c.Type.Contains(item) && c.Value.Contains(claimValue)))
                return context.User.Identity!.IsAuthenticated;

        return false;
    }
}