using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CompleteApp.App.Extensions.IdentityAuthorize
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated && context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }
}
