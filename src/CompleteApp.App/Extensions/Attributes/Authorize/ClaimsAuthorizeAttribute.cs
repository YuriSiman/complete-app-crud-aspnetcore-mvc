using CompleteApp.App.Extensions.IdentityAuthorize;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompleteApp.App.Extensions.Attributes.Authorize
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
