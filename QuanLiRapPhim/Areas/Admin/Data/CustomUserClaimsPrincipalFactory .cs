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
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<Staff, Role>
    {
        public CustomUserClaimsPrincipalFactory(UserManager<Staff> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Staff user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("FullName", user.FullName));
            identity.AddClaim(new Claim("RoleId", user.RoleId.ToString()));
            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                identity.AddClaim(new Claim("Role", role));
            }
            return identity;
        }
    }
}
