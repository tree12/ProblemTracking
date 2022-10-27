using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Entity
{
    public static class Extension
    {
        public static UserData AsUserData(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null) return null;
            string id = new[]
                {
                    claimsPrincipal.GetClaim(ClaimTypes.NameIdentifier)?.Value,
                    claimsPrincipal.GetClaim("userId")?.Value
                }
                .FirstOrDefault(s => !string.IsNullOrEmpty(s)) ?? "";

            string name = new[]
            {
                claimsPrincipal.GetClaim(ClaimTypes.Name)?.Value,
                claimsPrincipal.GetClaim("preferred_username")?.Value,
                claimsPrincipal.Identity?.Name
            }.FirstOrDefault(s => !string.IsNullOrEmpty(s)) ?? "";

            return new UserData(id, name, claimsPrincipal.Claims.ToList());
        }
        public static Claim GetClaim(this ClaimsPrincipal user, string claimTypes)
        {
            return user.Claims.FirstOrDefault(w => w.Type.Equals(claimTypes, StringComparison.OrdinalIgnoreCase));
        }
    }


}
