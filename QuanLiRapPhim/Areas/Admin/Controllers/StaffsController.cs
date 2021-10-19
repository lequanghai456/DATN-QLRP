using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AuthorizeRoles("Admin")]
    public class StaffsController : Controller
    {
        private readonly IdentityContext _context;
        private UserManager<Staff> StaffMgr { get; }
        private SignInManager<Staff> SignInMgr { get; }
        private RoleManager<Role> RoleMgr { get; }
        public StaffsController(IdentityContext context, UserManager<Staff> userManager, SignInManager<Staff> signInManager, RoleManager<Role> roleManager)
        {
            _context = context;
            StaffMgr = userManager;
            SignInMgr = signInManager;
            RoleMgr = roleManager;
        }
        [TempData]
        public string Message { get; set; }
        // GET: Admin/Staffs
        public async Task<IActionResult> Index(int? id)
        {
            Staff staff = null;
            if (id != null)
            {
                staff = _context.Staffs.FirstOrDefault(s => s.Id == id);
                ViewData["Img"] = staff.Img; 

            }
            //Danh sách phòng đã có người quản lí
            var ListRoom = from a in _context.Staffs
                           join room in _context.Rooms on a.RoleId equals room.RoleId
                           select a.RoleId;
            ViewBag.Role = _context.Roles.Where(x => x.IsDelete == false && !x.Name.Contains("Admin") && !ListRoom.Contains(x.Id)).ToList();
            return View(staff);
        }
        public JsonResult DeleteStaff(int? id)
        {
            try
            {
                Staff staff = new Staff();
                staff = _context.Staffs.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (staff == null)
                {

                    return Json("Fail");
                }
                staff.IsDelete = true;
                _context.Update(staff);
                _context.SaveChangesAsync();
                return Json("Xóa nhân viên thành công");
            }catch(Exception err)
            {
                
                return Json("Xóa nhân viên thất bại");
            }
        }
        
        public JsonResult DeleteStaffList(String Listid)
        {
            try
            {

                String[] List = Listid.Split(',');
                Staff staff = new Staff();
                foreach (String id in List)
                {
                    staff = _context.Staffs.FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    staff.IsDelete = true;
                    _context.Update(staff);
                

                }
                _context.SaveChangesAsync();
                return Json("Xóa nhân viên thành công");
            }
            catch (Exception err)
            {
         
                return Json("Xóa nhân viên thất bại");
            }
           


        }
        public class JTableModelCustom : JTableModel
        {
            public string FullName { get; set; }
            public int Role { get; set; }
        }
        public JsonResult Role()
        {
            return Json(_context.Roles.Include(x=>x.Staffs).Where(x=>x.IsDelete==false && x.Id != 1).Select(x=>new {x.Id,x.Name}).ToList());
        }
        [HttpGet]
        public async Task<String> JtableStaffModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Staffs.Where(x => x.IsDelete == false && x.Id != 1 && x.UserName != User.Identity.Name &&   (String.IsNullOrEmpty(jTablePara.FullName) || x.FullName.Contains(jTablePara.FullName)) && (jTablePara.Role ==0 || jTablePara.Role == x.RoleId ));
            int count = query.Count();
            var data = query.AsQueryable().Select(x => new { x.Id, x.Img, x.FullName, x.UserName, date = x.DateOfBirth.ToString("MM/dd/yyyy"), RoleName = x.Role.Name})
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "FullName","UserName","Img","date","RoleName");
            return JsonConvert.SerializeObject(jdata);
        }

        // POST: Admin/Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Staff staff, IFormFile ful)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await StaffMgr.CreateAsync(staff, staff.PasswordHash);
                if (ful != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img",
                  staff.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ful.CopyToAsync(stream);
                    }
                    staff.Img = staff.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    staff.Img = "Noimage.png";
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                if (result.Succeeded)
                {
                    Message = "Thêm nhân viên thành công";
                    return RedirectToAction(nameof(Index));
                }
            }
            Message = "Thêm nhân viên thất bại";
            return View(staff);
        }

        

        // POST: Admin/Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Staff staff,IFormFile ful)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }
            Staff staffold = _context.Staffs.FirstOrDefault(x => x.Id == id);
            staffold.FullName = staff.FullName;
            staffold.DateOfBirth = staff.DateOfBirth;
            staffold.RoleId = staff.RoleId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffold); 
                    if (ful != null)
                    {
                        if (staffold.Img.Contains("Noimage.png") || staffold.Img == null)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img",
                            staffold.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await ful.CopyToAsync(stream);
                            }
                            staffold.Img = staffold.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                            _context.Update(staffold);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img", staffold.Img);
                            System.IO.File.Delete(path);

                            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img",
                            staffold.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await ful.CopyToAsync(stream);
                            }
                            staffold.Img = staffold.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                            _context.Update(staffold);
                            await _context.SaveChangesAsync();
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staffold.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Message = "Cập nhật nhân viên thành công";
                return RedirectToAction(nameof(Index));
            }
            Message = "Cập nhật nhân viên thất bại";
            return View(staff);
        }
        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.Id == id);
        }
    }
}
