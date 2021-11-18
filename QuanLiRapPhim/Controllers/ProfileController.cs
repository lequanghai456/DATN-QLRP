using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static QuanLiRapPhim.Controllers.RegisterController;

namespace QuanLiRapPhim.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IdentityContext _identityConext;
        private UserManager<User> StaffMgr { get; }
        private SignInManager<User> SignInMgr { get; }
        public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager, IdentityContext identityContext)
        {
            _identityConext = identityContext;
            StaffMgr = userManager;
            SignInMgr = signInManager;
        }
        [AuthorizeRolesAttribute("User")]
        public IActionResult Index()
        {
            try
            {

                InputUser users = new InputUser();
                var user = _identityConext.Users.Where(x => x.IsDelete == false).FirstOrDefault(x => x.UserName == User.Identity.Name);
                users.UserName = User.Identity.Name;
                users.FullName = User.FindFirst("FullNameUser").Value;
                users.DateOfBirth = user.DateOfBirth;
                ViewData["Img"] = user.Img;
                return View(users);

            }catch(Exception err)
            {
                return View("NotFound");
            }
        }
        [TempData]
        public String Mess { get; set; }
        public class InputUser
        {
            public string FullName { get; set; }
            
            [DisplayName("Ngày sinh")]
            [DataType(DataType.Date)]
            [Required]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime DateOfBirth { get; set; }
            public string UserName { get; set; }
        }
        public async Task<IActionResult> EditUser(InputUser users, IFormFile ful)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = _identityConext.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                    user.FullName = users.FullName;
                    user.DateOfBirth = users.DateOfBirth;
                    _identityConext.Update(user);
                    if (ful != null)
                    {
                        if (user.Img.Contains("avatar.png"))
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/pro",
                            user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await ful.CopyToAsync(stream);
                            }
                            user.Img = user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                            _identityConext.Update(user);
                            await _identityConext.SaveChangesAsync();
                        }
                        else
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/pro", user.Img);
                            System.IO.File.Delete(path);

                            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/pro",
                            user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await ful.CopyToAsync(stream);
                            }
                            user.Img = user.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                            _identityConext.Update(user);
                            await _identityConext.SaveChangesAsync();
                        }
                    }
                    await _identityConext.SaveChangesAsync();
 
                    Mess = "Cập nhật thành công";
                    return RedirectToAction("Index");
                }catch(Exception err)
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
