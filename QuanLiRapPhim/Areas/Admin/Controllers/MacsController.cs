using System;
using System.Collections.Generic;
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
    [AuthorizeRoles("Admin")]
    public class MacsController : Controller
    {
        private readonly IdentityContext _context;

        public MacsController(IdentityContext context)
        {
            _context = context;
        }
        public class JTableModelCustom : JTableModel
        {
            public string Title { get; set; }
        }
        // GET: Admin/Macs
        public async Task<IActionResult> Index(int? id)
        {
            Mac mac = null;
            if (id != null)
            {
                mac = _context.Macs.FirstOrDefault(s => s.Id == id);
            }
            return View(mac);
        }

       
        // POST: Admin/Macs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Age,Describe,IsDelete")] Mac mac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mac);
                await _context.SaveChangesAsync();
                Message = "Tạo mac thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(mac);
        }

        // POST: Admin/Macs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Age,Describe,IsDelete")] Mac mac)
        {
            if (id != mac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mac);
                    await _context.SaveChangesAsync();
                    Message = "Cập nhật mac thành công";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MacExists(mac.Id))
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
            return View(mac);
        }
        [HttpGet]
        public async Task<String> JtableMacsModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Macs.Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.Title) || x.Title.Contains(jTablePara.Title)));
            int count = query.Count();
            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Title","Age","Describe");
            return JsonConvert.SerializeObject(jdata);
        }
        public async Task<JsonResult> DeleteMac(int? id)
        {
            try
            {
                Mac mac = new Mac();
                mac = _context.Macs.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (mac == null)
                {

                    return Json("Không tìm thấy mac");
                }
              
                mac.IsDelete = true;
                _context.Update(mac);
                await _context.SaveChangesAsync();
                 return Json("Xóa mac thành công");
            }catch(Exception err)
            {
                
                return Json("Có lỗi xảy ra");
            }
        }
        [TempData]
        public string Message { get; set; }
        public async Task<JsonResult> DeleteMacList(String Listid)
        {
            try
            {
                
                String[] List = Listid.Split(',');
                Mac mac = new Mac();
                foreach (String id in List)
                {
                    mac = _context.Macs.FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    mac.IsDelete = true;
                    _context.Update(mac);
                   
                }
                await _context.SaveChangesAsync();
                return Json("Xóa thành công MAC");
            }
            catch (Exception er)
            {

                return Json("Có lỗi xảy ra");
            }
           


        }
        private bool MacExists(int id)
        {
            return _context.Macs.Any(e => e.Id == id);
        }
    }
}
