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
            public string FullName { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Mật khẩu ít nhất 6 kí tự")]
            public string PasswordHash { get; set; }
            [Compare(otherProperty: "PasswordHash", ErrorMessage = "Mật khẩu không trùng khớp")]
            public string confirmPasswordHash { get; set; }
            [DisplayName("Ngày sinh")]
            [DataType(DataType.Date)]
            [Required]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime DateOfBirth { get; set; }
            public string UserName { get; set; }
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
                ViewData["Img"] = user.Img;
                return View(users);
            }
            catch
            {
                
            }
            return View(users);
        }
        [TempData]
        public String Mess { get; set; }
        public async Task<IActionResult> EditUser(InputUser users, IFormFile ful)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Staff user = _context.Staffs.FirstOrDefault(x => x.UserName == User.Identity.Name);
                    
                    
                    if (users.PasswordHash != null || users.PasswordHash != "capnhatprofile")
                    {
                        await StaffMgr.RemovePasswordAsync(user);
                        IdentityResult result = await StaffMgr.AddPasswordAsync(user, users.PasswordHash);
                    }
                    _context.Update(user);
                    if (ful != null)
                    {
                        if (user.Img.Contains("avatar.png"))
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img",
                            user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await ful.CopyToAsync(stream);
                            }
                            user.Img = user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                            _context.Update(user);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img", user.Img);
                            System.IO.File.Delete(path);

                            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img",
                            user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await ful.CopyToAsync(stream);
                            }
                            user.Img = user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                            _context.Update(user);
                            await _context.SaveChangesAsync();
                        }
                    }
                    await _context.SaveChangesAsync();

                    Mess = "Cập nhật thành công";
                    return RedirectToAction("Index");
                }
                catch (Exception err)
                {
                    Mess = "Cập nhật thất bại";
                    return RedirectToAction("Index");
                }
            }
            Mess = "Cập nhật thất bại";
            return RedirectToAction("Index");
        }
    }
}
