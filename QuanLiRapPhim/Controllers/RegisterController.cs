using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLiRapPhim.App;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class RegisterController : Controller
    {
        private UserManager<User> StaffMgr { get; }
        private SignInManager<User> SignInMgr { get; }
        private readonly IdentityContext _identityConext;

        public RegisterController(UserManager<User> userManager, SignInManager<User> signInManager, IdentityContext identityContext)
        {
            _identityConext = identityContext;
            StaffMgr = userManager;
            SignInMgr = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        public async Task<IActionResult> ChangePassword(string UserName,string SecurityStamp)
        {
            
            User user = new User();
            Users users = new Users();
            users.UserName = UserName;
            users.SecurityStamp = SecurityStamp;
            user = await StaffMgr.FindByNameAsync(UserName);
            if (user != null && user.SecurityStamp == SecurityStamp)
            {
                return View(users);
            }
            ViewData["MessChangePassword"] = "Liên kết đã hết hiệu lực";
            MessChangePassword = "Liên kết đã hết hiệu lực";
            return View(users);
        }
       
        public bool SendEmailForgotPassword(string email,string username, string SecurityStamp)
        {
            return Email.SendMailGoogleSmtp("giabao158357@gmail.com", email, "Quên mật khẩu", "Vui lòng bấm vào link để đến bước kế https://localhost:44350/Register/ChangePassword?username=" + username+ "&SecurityStamp="+ SecurityStamp).Result
                ? true
                : false;
        }
        [TempData]
        public String MessChangePassword { get; set; }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(Users users)
        {
            User user = new User();        
            try
            {
                user = await StaffMgr.FindByNameAsync(users.UserName);

                        await StaffMgr.RemovePasswordAsync(user);
                        IdentityResult result = await StaffMgr.AddPasswordAsync(user,users.PasswordHash);
                        if (result.Succeeded)
                        { 
                            MessChangePassword = "Thay đổi mật khẩu thành công";
                            return View("ChangePassword");
                        }
                return View();


            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [TempData]
        public String Mess { get; set; }
        
        
        public async Task<IActionResult> ConfrimEmail(string Email)
        {
            User user = new User();
            user = await StaffMgr.FindByEmailAsync(Email);
            if (user != null)
            {
                return Json(data: true);
            }
            return Json(data: false);

        }
        [TempData]
        public string MessForgotPassword { get; set; }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            User user = new User();
            user = await StaffMgr.FindByEmailAsync(Email);
            if (user != null)
            {
                if (SendEmailForgotPassword(Email, user.UserName, user.SecurityStamp))
                {


                    ViewData["MessForgotPassword"] = "Vui lòng vào Email để thực hiện bước kế tiếp";
                    return View("ForgotPassword");
                }
            }
            ViewData["MessForgotPassword"] = "Có lỗi xảy ra trong quá trình gửi email";
            return View();
        }
        public async Task<IActionResult> Register(Users users)
        {
            User user = new User();
            
            try
            {
                if (ModelState.IsValid)
                {
                    
                    user.FullName = users.FullName;
                    user.DateOfBirth = users.DateOfBirth;
                    user.Email = users.Email;
                    user.UserName = users.UserName;
                    user.PasswordHash = users.PasswordHash;
                    IdentityResult result = await StaffMgr.CreateAsync(user, user.PasswordHash);
                    if (result.Succeeded && SendEmail(users.Email, users.UserName))
                    {
                        ViewData["Mess"] = "Tài khoản của bạn đã tạo thành công vui lòng vào Email của bạn để xác nhận tài khoản ";
                        return View("Index");
                    }
                }
                return View("Index");
                

            }
            catch (Exception ex)
            {
                return View("Index");
            }
            
        }
        public async Task<IActionResult> confirmEmail(string username)
        {
            User user = new User();
            user = await StaffMgr.FindByNameAsync(username);
            user.ConfirmEmail = true;
             _identityConext.Update(user);
            await _identityConext.SaveChangesAsync();
            return RedirectToAction("Success","Home");
        }
        public async Task<IActionResult> VerifyUsername(string username)
        {
            User user = new User();
            user = await StaffMgr.FindByNameAsync(username);
            if(user != null)
            {
                return Json(data: false);
            }
            return Json(data: true);

        }
        //
        public async Task<IActionResult> Verifyemail(string email)
        {
            User user = new User();
            user = await StaffMgr.FindByEmailAsync(email);
            if (user != null)
            {
                return Json(data: false);
            }
            return Json(data: true);

        }
        //public JsonResult 
        public bool SendEmail(string email, string username)
        {
            return Email.SendMailGoogleSmtp("giabao158357@gmail.com", email, "Xác nhận Email cho tài khoản", "Vui lòng bấm vào link để xác nhận Email cho tài khoản https://localhost:44350/Register/confirmEmail?username=" + username).Result
                ? true
                : false;
        }
        public class Users
        {
            public string FullName { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập Email")]
            [EmailAddress(ErrorMessage = "Địa chỉ e-mail hợp lệ")]
            [Remote(action: "Verifyemail", controller: "Register", ErrorMessage = "Email đã được sử dụng")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
            [Remote(action: "VerifyUsername", controller: "Register", ErrorMessage = "Tên tài khoản đã có người sử dụng")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập mật khẩu, mật khẩu phải có số và chữ")]
            public string PasswordHash { get; set; }
            [Compare(otherProperty: "PasswordHash", ErrorMessage = "Mật khẩu không trùng khớp")]
            public string confirmPasswordHash { get; set; }
            [DisplayName("Chọn ngày")]
            [DataType(DataType.Date)]
            [Required]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime DateOfBirth { get; set; }
            public string SecurityStamp { get; set; }
        }
        public class inputEmail
        {
            [Required(ErrorMessage = "Vui lòng nhập Email")]
            [EmailAddress(ErrorMessage = "Trường Email không phải là một địa chỉ e-mail hợp lệ")]
            [Remote(action: "ConfrimEmail", controller: "Register", ErrorMessage = "Email chưa được xác thực hoặc không có tài khoản nào sử dụng email này")]
            public string Email { get; set; }
        }
    }
}
