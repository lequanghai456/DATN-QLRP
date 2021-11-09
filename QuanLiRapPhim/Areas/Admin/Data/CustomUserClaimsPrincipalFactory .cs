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
        public CustomUserClaimsPrincipalFactory(UserManager<Staff> userManager, IdentityContext context, RoleManager<Role> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _context = context;
        }
        private readonly IdentityContext _context;
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Staff user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("FullName", user.FullName));
            identity.AddClaim(new Claim("Img", user.Img));
            identity.AddClaim(new Claim("RoleId", user.RoleId.ToString()));
            var roles = _context.Roles.FirstOrDefault(x=>x.Id == user.RoleId);

            identity.AddClaim(new Claim(ClaimTypes.Role, roles.Name));
            identity.AddClaim(new Claim("Role", roles.Name));

            return identity;
        }
    }
}
