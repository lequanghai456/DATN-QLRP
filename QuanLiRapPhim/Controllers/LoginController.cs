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
        public async Task<IActionResult> Index()
        {
            if (SignInMgr.IsSignedIn(User))
            {
                await SignInMgr.SignOutAsync();
            }
            return View();
        }
        [TempData]
        public string MessageLogin { get; set; }
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
                if(!user.ConfirmEmail)
                {
                    MessageLogin = "Bạn vui lòng xác nhận email";
                }
                else
                    MessageLogin = "Tài khoản hoặc mật khẩu không chính xác vui lòng đăng nhập lại";
            }
            catch (Exception err)
            {
                MessageLogin = "Có lỗi xảy ra";
                
            }
            return RedirectToAction("Index", "Login");
        }
        public async Task<IActionResult> Logout()
        {
             
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
