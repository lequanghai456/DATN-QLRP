using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLiRapPhim.Areas.Admin.Data;
using Microsoft.AspNetCore.Identity;
using QuanLiRapPhim.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IO;
using Microsoft.AspNetCore.Http;
using QuanLiRapPhim.SupportJSON;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles("Admin,Manager")]
    public class HomeController : Controller
    {
        private UserManager<Staff> StaffMgr { get; }
        private SignInManager<Staff> SignInMgr { get; }
        private readonly IdentityContext _context;
        private RoleManager<Role> RoleMgr { get; }
        public HomeController(IdentityContext context, UserManager<Staff> userManager, SignInManager<Staff> signInManager, RoleManager<Role> roleManager)
        {
            StaffMgr = userManager;
            SignInMgr = signInManager;
            RoleMgr = roleManager;
            _context = context;
        }
        public class InputUser
        {
            [DisplayName("Họ tên")]
            public string FullName { get; set; }

            [DisplayName("Ngày sinh")]
            [DataType(DataType.Date)]
            [Required]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime DateOfBirth { get; set; }

            [DisplayName("Tài khoản")]
            public string UserName { get; set; }
            public string Img { get; set; }
        }
        public IActionResult Index()
        {
            InputUser users = new InputUser();
            Staff user = new Staff();
            try
            {
                user = _context.Staffs.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
                users.UserName = User.Identity.Name;
                users.FullName = user.FullName;
                users.DateOfBirth = user.DateOfBirth.Date;
                users.Img = user.Img;
            }
            catch (Exception er)
            {
                Mess = "Có lỗi xảy ra";
            }
            return View(users);
        }
        [TempData]
        public String Mess { get; set; }
        public IActionResult EditUser(InputUser users, IFormFile ful)
        {
            try
            {
                Staff user = _context.Staffs.FirstOrDefault(x => x.UserName == User.Identity.Name);
                user.FullName = users.FullName;
                user.DateOfBirth = users.DateOfBirth;
                
                if (ful != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img", user.Img);
                    System.IO.File.Delete(path);

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img",
                    user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ful.CopyTo(stream);
                    }

                    user.Img = user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                    
                }

                _context.Update(user);
                _context.SaveChanges();

                Mess =  "Cập nhật thành công";
            }
            catch (Exception err)
            {
                Mess = "Cập nhật thất bại";
            }
            return RedirectToAction("Index");
        }
        [AuthorizeRoles("admin")]
        public async Task<JsonResult> ThongKeTheoNgay()
        {
            var a = _context.Users.Select(x => new {
                x.FullName,
                x.Img,
                SL = _context.Tickets.Where(t => t.Username.Equals(x.UserName))
                .Where(x => true).Count()
            }).OrderBy(x => x.SL).Take(5);
            return Json(a);
        }
        [AuthorizeRoles("admin")]
        public async Task<JsonResult> ThongKeTheoThang()
        {
            var a = _context.Users.Select(x => new {
                x.FullName,
                x.Img,
                SL = _context.Tickets.Where(t => t.Username.Equals(x.UserName))
                .Where(x => true).Count()
            }).OrderBy(x => x.SL).Take(5);
            return Json(a);
        }
        [AuthorizeRoles("admin")]
        public async Task<JsonResult> ThongKeTheoQuy()
        {
            var a = _context.Users.Select(x => new
            {
                x.FullName,
                x.Img,
                SL = _context.Tickets.Where(t => t.Username.Equals(x.UserName))
                .Where(x=>true).Count()
            }).OrderBy(x=>x.SL).Take(5) ;
            return Json(a);
        }
    }
}
