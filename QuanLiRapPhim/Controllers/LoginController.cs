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
                //ViewBag.Message = "Admin already registered";
                //User user = await StaffMgr.FindByNameAsync("User1");
                //if (user == null)
                //{
                //    user = new User();
                //    user.UserName = "User1";
                //    user.FullName = "Hồ Gia Bảo";
                //    user.PasswordHash = "abc1234";
                //    IdentityResult result = await StaffMgr.CreateAsync(user, user.PasswordHash);
                //    if (result.Succeeded)
                //    {
                //        return Json(Message = "Succeeded");
                //    }
                //}
                User user = await StaffMgr.FindByNameAsync("haile123");
                IdentityResult result = await StaffMgr.ChangePasswordAsync(user, "abc123", "abc1234");
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
            User user = new User();
            user = await StaffMgr.FindByNameAsync(obj.UserName);
            if (user != null && user.ConfirmEmail)
            {
                var result = await SignInMgr.PasswordSignInAsync(obj.UserName, obj.PasswordHash, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "home");
                }
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
