using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLiRapPhim.App;
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
                
                if (user != null && !user.IsDelete)
                {
                    if (!user.ConfirmEmail)
                    {
                        SendEmail(user.Email, user.UserName);
                        MessageLogin = "Bạn vui lòng xác nhận email";
                        return RedirectToAction("ConfirmEmail", "Register");
                    }
                    else
                    {
                        var result = await SignInMgr.PasswordSignInAsync(obj.UserName, obj.PasswordHash, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "home");
                        }
                        else
                        {
                            MessageLogin = "Tài khoản hoặc mật khẩu không chính xác vui lòng đăng nhập lại";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageLogin = "Có lỗi xảy ra";
            }
            return RedirectToAction("Index", "Login");
        }
        public bool SendEmail(string email, string username)
        {
            return Email.SendMailGoogleSmtp("giabao158357@gmail.com", email, "Xác nhận Email cho tài khoản", "Vui lòng bấm vào link để xác nhận Email cho tài khoản https://localhost:44350/Register/confirmEmailUser?username=" + username).Result
                ? true
                : false;
        }
        public async Task<IActionResult> Logout()
        {
             
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
