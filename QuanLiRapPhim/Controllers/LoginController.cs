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
        [TempData]
        public string Message { get; set; }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            try
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
            }
            catch (Exception err)
            {
                Message = "Tài khoản hoặc mật khẩu không chính xát vui lòng đăng nhập lại";
                return RedirectToAction("Index", "Login");
            }
            Message = "Tài khoản hoặc mật khẩu không chính xát vui lòng đăng nhập lại";
            return RedirectToAction("Index", "Login");
        }
        public async Task<IActionResult> Logout()
        {
             
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
