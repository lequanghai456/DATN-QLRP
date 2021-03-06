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
using QuanLiRapPhim.SupportJSON;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles("Admin")]
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
            var query = _context.Categories.Include(x=>x.lstMovie).Where(x => x.IsDelete == false && /*x.lstMovie.All(x=>x.IsDelete == false) && */(String.IsNullOrEmpty(jTablePara.Title) || x.Title.Contains(jTablePara.Title)));
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
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    Message = "Tạo thể loại thành công";
                    return RedirectToAction(nameof(Index));
                }
            }catch(Exception err)
            {
                Message = "Có lỗi xảy ra trong quá trình tạo";
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> VerifyTitle(string Title)
        {
            return Json(data: !CheckTitle(Title));
        }
        private bool CheckTitle(string name)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Title==name && x.IsDelete == false);
            return category != null;
        }

        public async Task<JsonResult> CreateAPI(String name)
        {
            try
            {
                Category category = new Category();
                if (!CheckTitle(name))
                {
                    

                    category.Title = name;

                    _context.Add(category);
                    await _context.SaveChangesAsync();

                    return Json(category.Id);
                }
            }
            catch (Exception ex)
            {
            }
            return Json("0");

        }
        public async Task<JsonResult> ListCategories()
        {
            JMessage jMessage = new JMessage();
            try
            {
                var obj = _context.Categories.Include(x=>x.lstMovie).Where(x => x.IsDelete == false).Select(x=> new { x.Id,x.Title,MovieId = x.lstMovie.Select(a=>a.Id)}).ToList();
                jMessage.Error = obj.Count() > 0;
                if (jMessage.Error)
                {
                    jMessage.Object = obj;
                }
                else
                {
                    jMessage.Title = "Không có thể loại";
                }
            }
            catch (Exception er){
                jMessage.Error = true;
                jMessage.Title = "Có lỗi xảy ra";
            }
            return Json(jMessage);
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
                Message = "Cập nhật thể loại thành công ";
                return RedirectToAction(nameof(Index));
            }
            Message = "Có lỗi xảy ra trong quá trình tạo";
            return RedirectToAction(nameof(Index));
        }
        public async Task<JsonResult> DeleteCategories(int? id)
        {
            try
            {
                Category category = new Category();
                category = _context.Categories.Include(x=>x.lstMovie).FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (category == null)
                {

                    return Json("Không tìm thấy thể loại");
                }
                category.IsDelete = true;
                _context.Update(category);
                category.lstMovie.All(a => a.Lstcategories.Remove(category));
                await _context.SaveChangesAsync();        
                return Json("Xóa thể loại thành công");
            }catch(Exception err)
            {
               
                return Json("Xóa thể loại thất bại");
            }
        }
        [TempData]
        public string Message { get; set; }
        public JsonResult DeleteCategoriesAll(String Listid)
        {
            
            try
            {

                String[] List = Listid.Split(',');
                Category category = new Category();
                foreach (String id in List)
                {
                    category = _context.Categories.Include(x=>x.lstMovie).FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    category.IsDelete = true;
                    category.lstMovie.All(a => a.Lstcategories.Remove(category));
                    _context.Update(category);
                }
                
                _context.SaveChangesAsync();
                return Json("Xóa thành công thể loại");
            }
            catch (Exception er)
            {
                
                return Json("Xóa thể loại thất bại");
            }
           


        }
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
