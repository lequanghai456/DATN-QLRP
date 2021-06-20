using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;

namespace QuanLiRapPhim.Areas.Admin.Views
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IdentityContext _context;

        public UsersController(IdentityContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            StaffMgr = userManager;
            SignInMgr = signInManager;
        }
        private UserManager<User> StaffMgr { get; }
        private SignInManager<User> SignInMgr { get; }
        public class JTableModelCustom : JTableModel
        {
            public string FullName { get; set; }
        }
        // GET: Admin/Users
        public async Task<IActionResult> Index(int? id)
        {
            User user = null;
            if (id != null)
            {
                user = _context.Users.FirstOrDefault(x => x.Id == id);
            }
            return View(user);
        }


        [HttpGet]
        public async Task<String> JtableUserModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Users.Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.FullName) || x.FullName.Contains(jTablePara.FullName)));
            int count = query.Count();
            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id","FullName","UserName", "PasswordHash" , "Img");
            return JsonConvert.SerializeObject(jdata);
        }
    }
}
