using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private  UserManager<Staff> StaffMgr { get; }
        private  SignInManager<Staff> SignInMgr { get; }
        private  RoleManager<Role> RoleMgr { get; }
        public LoginController(UserManager<Staff> userManager,SignInManager<Staff> signInManager,RoleManager<Role> roleManager)
        {
            StaffMgr = userManager;
            SignInMgr = signInManager;
            RoleMgr = roleManager;
        }
        public IActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Staff obj)
        {
            var result = await SignInMgr.PasswordSignInAsync(obj.UserName, obj.PasswordHash, false, false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "home");
            }
            ViewData["Message"] = "Tài khoản hoặc mật khẩu không chính xát vui lòng đăng nhập lại";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        public List<IdentityRole> roles { set; get; }
        public async Task<IActionResult> RegisterAsync()
        {
            try
            {
                ViewBag.Message = "Admin already registered";
                Staff staff = await StaffMgr.FindByNameAsync("Admin1");

                Role role = await RoleMgr.FindByNameAsync("Admin1");
                if (staff == null)
                {
                    staff = new Staff();
                    staff.UserName = "Admin1";
                    staff.PasswordHash = "abc123";
                    staff.FullName = "Hồ Gia Bảo";
                    staff.RoleId = 1;
                    IdentityResult result = await StaffMgr.CreateAsync(staff, "abc123");
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "home");
                    }
                }
                return View("Login");

            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}
