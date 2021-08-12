using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Chat.Services
{
    public static class Authentication
    {
        public static string UserId(this IIdentity Identity)
        {
            ClaimsIdentity claimsIdentity = Identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("userId");

            return claim?.Value;
        }

    }
}
