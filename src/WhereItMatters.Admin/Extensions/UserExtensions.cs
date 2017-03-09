using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WhereItMatters.Admin
{
    public static class GenericPrincipalExtensions
    {
        public static int OrganisationId(this IPrincipal user)
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
