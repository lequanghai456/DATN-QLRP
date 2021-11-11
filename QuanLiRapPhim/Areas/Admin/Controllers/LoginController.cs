using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private  UserManager<Staff> StaffMgr { get; }
        private  SignInManager<Staff> SignInMgr { get; }
        private readonly IdentityContext _context;
        private  RoleManager<Role> RoleMgr { get; }
        public LoginController(IdentityContext context, UserManager<Staff> userManager,SignInManager<Staff> signInManager,RoleManager<Role> roleManager)
        {
            StaffMgr = userManager;
            SignInMgr = signInManager;
            RoleMgr = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Login");
        }
        
        public async Task<IActionResult> Logout()
        {
            //var demo = User.FindFirstValue("RoleId");
            ////var listRole = RoleMgr.Roles.ToListAsync();
            //var demo1 = User.Claims.ToList();
            //HttpContext.Session.Clear();
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Staff obj)
        {
            var result = await SignInMgr.PasswordSignInAsync(obj.UserName, obj.PasswordHash, false, false);
            if (result.Succeeded && !obj.IsDelete)
            {

                if (_context.Staffs.FirstOrDefault(x => x.UserName.Equals(obj.UserName)).RoleId == 2)
                {
                    var url = Url.RouteUrl("areas", new { controller = "Home", action = "index", area = "Staffs" });
                    return Redirect(url);
                }
                //HttpContext.Session.SetString("Role", RoleMgr.FindByIdAsync(User.FindFirstValue("RoleId")).ToString());
                return RedirectToAction("Index", "home");
            }
            ViewData["Message"] = "Tài khoản hoặc mật khẩu không chính xác vui lòng đăng nhập lại";
            return View();
        }
    }
}
