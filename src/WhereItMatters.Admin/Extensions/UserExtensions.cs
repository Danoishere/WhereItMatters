using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WhereItMatters.Admin.Models;

namespace WhereItMatters.Admin
{
    public static class GenericPrincipalExtensions
    {
        public static Task<T> GetCurrentUser<T>(this UserManager<T> manager, HttpContext httpContext) where T : class
        {
            return manager.GetUserAsync(httpContext.User);
        }

        public static async Task<int?> GetOrganisationId(this UserManager<ApplicationUser> manager, HttpContext httpContext)
        { 
            var user = await manager.GetUserAsync(httpContext.User);
            return user.OrganisationId;
        }

        public static int OrganisationId(this UserManager<ApplicationUser> manager, IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type == "OrganisationId")
                        return int.Parse(claim.Value);
                }
                return 0;
            }
            else
                return 0;
        }
    }
}
