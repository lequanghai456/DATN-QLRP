using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SevicesController : Controller
    {
        private readonly IdentityContext _context;

        public SevicesController(IdentityContext context)
        {
            _context = context;
        }
        public class JTableModelCustom : JTableModel
        {
            public string Name { get; set; }
            public Decimal Price { get; set; }
        }
        // GET: Admin/Sevices
        public async Task<IActionResult> Index(int? id)
        {
            return View();
        }
        // POST: Admin/Sevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sevice sevice,List<SeviceCategory> ListSeviceCategories)
        {
            try {
            if (ModelState.IsValid)
            {
                    if (ListSeviceCategories.Count() > 0)
                    {
                        foreach (var item in ListSeviceCategories)
                        {
                            item.Sevice = sevice;
                            _context.Add(item);
                        }
                        await _context.SaveChangesAsync();
                        Message = "Tạo dịch vụ thành công";
                        return RedirectToAction(nameof(Index));
                    }
                    Message = "Dịch vụ phải có ít nhất một loại";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception err)
            {
                Message = "Tạo dịch vụ thất bại";
                return View();
            }
            Message = "Tạo dịch vụ thất bại";
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<String> JtableSeviceModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Sevices.Include(x=>x.SeviceCategories).Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.Name) || x.Name.Contains(jTablePara.Name)));
            int count = query.Count();
            var data = query.AsQueryable().Select(x=>new { x.Id,x.Name,Size = JsonConvert.SerializeObject(x.SeviceCategories.ToList<SeviceCategory>()) })
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Name","Size");
            return JsonConvert.SerializeObject(jdata);
        }
        public JsonResult DeleteSevice(int? id)
        {
            try
            {
                Sevice sevice = new Sevice();
                sevice = _context.Sevices.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (sevice == null)
                {

                    return Json("Fail");
                }
                sevice.IsDelete = true;
                _context.Update(sevice);
                _context.SaveChangesAsync();
                
                return Json("Xóa dịch vụ thành công");
            }catch(Exception err)
            {
                
                return Json("Xóa dịch vụ thất bại");
            }
        }
        [TempData]
        public string Message { get; set; }
        public JsonResult DeleteSeviceList(String Listid)
        {
            int itam = 0;
            try
            {

                String[] List = Listid.Split(',');
                Sevice sevice = new Sevice();
                foreach (String id in List)
                {
                    sevice = _context.Sevices.FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    sevice.IsDelete = true;
                    _context.Update(sevice);
                    itam++;

                }
               
                _context.SaveChangesAsync();
                return Json("Xóa dịch vụ thành công");
            }
            catch (Exception er)
            {
                
                return Json("Xóa dịch vụ thất bại");
            }
            


        }


        // POST: Admin/Sevices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,IsDelete")] Sevice sevice)
        {
            if (id != sevice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Message = "Cập nhật dịch vụ thành công";
                    _context.Update(sevice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeviceExists(sevice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sevice);
        }
        
        public class inputSevice
        {
            
            [Display(Name = "Tên dịch vụ")]
            [Required(ErrorMessage ="Tên dịch vụ không được để trống")]
            public string Name { get; set; }
            [Display(Name = "Loại dịch vụ")]
            public bool Isfood { get; set; }
            
            //[Display(Name = "Kích thước")]
            //public string Size { get; set; }
            //[Display(Name = "Giá")]
            //[Required(ErrorMessage = "Giá dịch vụ không được để trống")]
            //public decimal Price { get; set; }
        }
        private bool SeviceExists(int id)
        {
            return _context.Sevices.Any(e => e.Id == id);
        }
    }
}
