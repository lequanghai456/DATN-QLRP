using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Data
{
    public class CustomUserClaims : UserClaimsPrincipalFactory<User>
    {
        public CustomUserClaims(UserManager<User> userManager, IOptions<IdentityOptions> options) : base(userManager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("FullNameUser", user.FullName));
            identity.AddClaim(new Claim("Id", user.Id.ToString()));
            identity.AddClaim(new Claim("Role", "User"));
            return identity;
        }
    }
}
