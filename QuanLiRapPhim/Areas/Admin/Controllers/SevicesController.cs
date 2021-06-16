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
            Sevice sevice = null;
            if (id != null)
            {
                sevice = _context.Sevices.FirstOrDefault(x => x.Id == id);
            }
            return View(sevice);
        }
        // POST: Admin/Sevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,IsDelete")] Sevice sevice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sevice);
                await _context.SaveChangesAsync();
                Message = "Successfully create sevice";
                return RedirectToAction(nameof(Index));
            }
            return View(sevice);
        }
        [HttpGet]
        public async Task<String> JtableSeviceModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Sevices.Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.Name) || x.Name.Contains(jTablePara.Name)));
            int count = query.Count();
            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Name", "Price");
            return JsonConvert.SerializeObject(jdata);
        }
        public JsonResult DeleteSevice(int? id)
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
            Message = "Successfully deleted sevice";
            return Json("Success");
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
            }
            catch (Exception er)
            {
                Message = "Successfully deleted " + itam + " sevice";
                _context.SaveChangesAsync();
                return Json("Successfully deleted " + itam + " sevice");
            }
            Message = "Successfully deleted " + itam + " sevice";
            _context.SaveChangesAsync();
            return Json("Successfully deleted " + itam + " sevice");


        }
        // GET: Admin/Sevices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sevice = await _context.Sevices.FindAsync(id);
            if (sevice == null)
            {
                return NotFound();
            }
            return View(sevice);
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
       
        private bool SeviceExists(int id)
        {
            return _context.Sevices.Any(e => e.Id == id);
        }
    }
}
