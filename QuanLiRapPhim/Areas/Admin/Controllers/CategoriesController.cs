﻿using System;
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
    public class CategoriesController : Controller
    {
        private readonly IdentityContext _context;

        public CategoriesController(IdentityContext context)
        {
            _context = context;
        }
        public class JTableModelCustom : JTableModel
        {
            public string Title { get; set; }
        }
        // GET: Admin/Categories
        public async Task<IActionResult> Index(int ?id)
        {
            Category category = null;
            if(id !=null)
            {
                category = _context.Categories.FirstOrDefault(x => x.Id == id);
            }
            return View(category);
        }
        [HttpGet]
        public async Task<String> JtableCategoryModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Categories.Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.Title) || x.Title.Contains(jTablePara.Title)));
            int count = query.Count();
            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Title");
            return JsonConvert.SerializeObject(jdata);
        }
        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,IsDelete")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                Message = "Successfully create rooms";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        
        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IsDelete")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Message = "Successfully update rooms";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public JsonResult DeleteCategories(int? id)
        {
            try
            {
                Category category = new Category();
                category = _context.Categories.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (category == null)
                {

                    return Json("Fail");
                }
                category.IsDelete = true;
                _context.Update(category);
                _context.SaveChangesAsync();
                Message = "Successfully deleted Categories";
                return Json("");
            }catch(Exception err)
            {
                Message = "Delete fail Categories";
                return Json("");
            }
        }
        [TempData]
        public string Message { get; set; }
        public JsonResult DeleteCategoriesAll(String Listid)
        {
            int itam = 0;
            try
            {

                String[] List = Listid.Split(',');
                Category category = new Category();
                foreach (String id in List)
                {
                    category = _context.Categories.FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    category.IsDelete = true;
                    _context.Update(category);
                    itam++;
                }
                Message = "Successfully deleted " + itam + " Categories";
                _context.SaveChangesAsync();
                return Json("");
            }
            catch (Exception er)
            {
                Message = "Delete failed Categories";
                return Json("");
            }
           


        }
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
