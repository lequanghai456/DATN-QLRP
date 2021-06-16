using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class LoginController : Controller
    {
        private UserManager<User> StaffMgr { get; }
        private SignInManager<User> SignInMgr { get; }
        private readonly IdentityContext _identityConext;
        
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager,IdentityContext identityContext)
        {
            _identityConext = identityContext;
            StaffMgr = userManager;
            SignInMgr = signInManager;
            //_httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> Register()
        {
            var Message = "Fail";
            try
            {
                ViewBag.Message = "Admin already registered";
                User user = await StaffMgr.FindByNameAsync("ChoBao");
                if (user == null)
                {
                    user = new User();
                    user.UserName = "ChoBao";
                    user.FullName = "Hồ Gia Bảo";
                    user.PasswordHash = "abc123";
                    IdentityResult result =  await StaffMgr.CreateAsync(user, "abc123");
                    if (result.Succeeded)
                    {
                        return Json(Message="Succeeded");
                    }
                }
                return Json(Message);

            }
            catch (Exception ex)
            {
                return Json(Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            var result = await SignInMgr.PasswordSignInAsync(obj.UserName, obj.PasswordHash, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","home");
            }
            return RedirectToAction("Index", "home");
        }
        public async Task<IActionResult> Logout()
        {
             
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
