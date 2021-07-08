using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLiRapPhim.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static QuanLiRapPhim.Controllers.RegisterController;

namespace QuanLiRapPhim.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IdentityContext _identityConext;
        public ProfileController(IdentityContext identityContext)
        {
            _identityConext = identityContext;
            
        }
        [AuthorizeRolesAttribute("")]
        public IActionResult Index()
        {
            try
            {
                
                Users users = new Users();
                var user = _identityConext.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                users.UserName = User.Identity.Name;
                users.FullName = User.FindFirst("FullNameUser").Value;
                users.DateOfBirth = user.DateOfBirth.Date;
                ViewData["Img"] = user.Img;
                return View(users);

            }catch(Exception err)
            {
                return View("NotFound");
            }
        }
    }
}
