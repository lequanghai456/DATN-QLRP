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
using QuanLiRapPhim.SupportJSON;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeRoles("Admin")]
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
        public class IsFood
        {
            public bool isFood { get; set; }
            public string name { get; set; }
        }
        // GET: Admin/Sevices
        public async Task<IActionResult> Index(int? id)
        {
            Sevice sevice = new Sevice();
            var listIsFood = new[] {
                   new IsFood {isFood = true,name = "Thức ăn"},
                   new IsFood {isFood = false,name = "Nước"}
            };
            if (id != null)
            {
                sevice = _context.Sevices.Where(x => x.IsDelete == false).FirstOrDefault(x => x.Id == id);
                if (sevice == null)
                {
                    return NotFound();
                }
            }
            ViewData["IsFood"] = new SelectList(listIsFood, "isFood", "name");
            return View(sevice);
        }
        // POST: Admin/Sevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sevice sevice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(sevice);
                    await _context.SaveChangesAsync();
                    Message = "Tạo dịch vụ thành công";
                }
            }
            catch (Exception err)
            {
                Message = "Tạo dịch vụ thất bại";

            }
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<String> JtableSeviceModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Sevices.Include(x => x.LstSeviceSeviceCategories).Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.Name) || x.Name.Contains(jTablePara.Name)));
            int count = query.Count();
            var data = query.AsQueryable().Select(x => new { x.Id, x.Name, Size = JsonConvert.SerializeObject(x.LstSeviceSeviceCategories.Where(a => a.isDelete == false).Select(x => new {
                x.SeviceCategory.Name,
                x.Price,
                x.Id
            }).ToList()) })
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Name", "Size");
            return JsonConvert.SerializeObject(jdata);
        }
        public async Task<JsonResult> getSeviceCategory(int id)
        {
            JMessage jMessage = new JMessage();
            try
            {
                var obj = _context.SeviceCategories.Where(x => x.IsDelete == false).ToList();
                var SeviceId = _context.seviceSeviceCategories.Include(x => x.SeviceCategory)
                    .Where(x => x.IdSevice == id).Select(x => x.SeviceCategory.Id);
                var kq = obj.Where(x => !SeviceId.Contains(x.Id));
                jMessage.Error = kq.Count() > 0;
                if (jMessage.Error)
                {
                    jMessage.Object = kq;
                }
                else
                {
                    jMessage.Title = "Không có kích thước";
                }
            }
            catch (Exception er)
            {
                jMessage.Error = true;
                jMessage.Title = "Có lỗi xảy ra";
            }

            return Json(jMessage);
        }
        public async Task<JsonResult> getSeviceCategoryUpdate(int id)
        {
            JMessage jMessage = new JMessage();
            try
            {
                var kq = _context.seviceSeviceCategories.Include(x => x.SeviceCategory)
                    .Where(x => x.IdSevice == id && x.isDelete == false).Select(x => x.SeviceCategory);
                jMessage.Error = kq.Count() > 0;
                if (jMessage.Error)
                {
                    jMessage.Object = kq;
                }
                else
                {
                    jMessage.Title = "Không có kích thước";
                }
            }
            catch (Exception er)
            {
                jMessage.Error = true;
                jMessage.Title = "Có lỗi xảy ra";
            }

            return Json(jMessage);
        }
        public async Task<JsonResult> addCategory(String name)
        {
            JMessage jMessage = new JMessage();
            try
            {
                SeviceCategory seviceCategory = new SeviceCategory();
                seviceCategory.Name = name;
                _context.Add(seviceCategory);
                await _context.SaveChangesAsync();
                jMessage.Error = false;
            }
            catch (Exception ex)
            {
                jMessage.Error = true;
                jMessage.Title = "Thêm thất bại";
            }
            return Json(jMessage);

        }
        public bool checkCategory(string name)
        {
            return _context.SeviceCategories.Where(x => name == x.Name).Where(x => x.IsDelete == false).FirstOrDefault() != null;
        }
        public class Model
        {
            [Required]
            public int IdSeviceCategory { get; set; }
            [Required]
            public Decimal Price { get; set; }
            [Required]
            public int IdSevice { get; set; }
        }

        public async Task<IActionResult> addCategorySevice(Model model)
        {
            SeviceSeviceCategories seviceSeviceCategories = new SeviceSeviceCategories();
            try
            {
                if (!ModelState.IsValid)
                {
                    Message = "Thêm thất bại";
                    return RedirectToAction(nameof(Index));
                }
                seviceSeviceCategories.IdSevice = model.IdSevice;
                seviceSeviceCategories.Price = model.Price;
                seviceSeviceCategories.IdSeviceCategory = model.IdSeviceCategory;
                _context.Add(seviceSeviceCategories);
                await _context.SaveChangesAsync();
                Message = "Thêm giá thành công";
            } catch (Exception err)
            {
                Message = "Có lỗi xảy ra";
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult updateCategorySevice(Model model)
        {
            JMessage jMessage = new JMessage();
            
            try
            {
                
                SeviceSeviceCategories seviceSeviceCategories = _context.seviceSeviceCategories.Where(x => x.IdSevice == model.IdSevice && x.IdSeviceCategory == model.IdSeviceCategory).FirstOrDefault();
                jMessage.Error = model.Price <= 0 || seviceSeviceCategories == null;
                if (jMessage.Error)
                {
                    Message = "Cập nhật thất bại";
                }
                else
                {
                    seviceSeviceCategories.Price = model.Price;
                    _context.Update(seviceSeviceCategories);
                    _context.SaveChanges();
                    jMessage.Error = false;
                    Message = "Cập nhật thành công";
                }
            }
            catch (Exception err)
            {
                jMessage.Error = true;
                Message = "Có lỗi xảy ra";
            }
            return RedirectToAction("Index" ,new{ id= model.IdSevice});
        }
        public bool deleteSeviceCategory(int id)
        {
            try
            {
                SeviceSeviceCategories seviceSeviceCategories = _context.seviceSeviceCategories.Where(x => x.Id == id).FirstOrDefault();
                if (seviceSeviceCategories != null)
                {
                    seviceSeviceCategories.isDelete = true;
                    _context.Update(seviceSeviceCategories);
                    _context.SaveChanges();
                    
                    return true;
                }
            }
            catch (Exception err)
            {
                
            }
            return false;
        }
            public JsonResult DeleteSevice(int? id)
        {
            try
            {
                Sevice sevice = new Sevice();
                sevice = _context.Sevices.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (sevice == null)
                {
                    return Json("Không tìm thấy dịch vụ");
                }
                sevice.IsDelete = true;
                _context.Update(sevice);
                _context.SaveChangesAsync();
                
                return Json("Xóa dịch vụ thành công");
            }catch(Exception err)
            {
                return Json("Có lỗi xảy ra");
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
                    if (sevice == null)
                    {
                        return Json("Không tìm thấy");
                    }
                    sevice.IsDelete = true;
                    _context.Update(sevice);
                    itam++;
                }
               
                _context.SaveChangesAsync();
                return Json("Xóa dịch vụ thành công");
            }
            catch (Exception er)
            {
                return Json("Có lỗi xảy ra");
            }
            


        }
        //[HttpPost]
        //public IActionResult editSevice(int id)
        //{
        //    var data = _context.Sevices.Include(x =>x.SeviceCategories).Where(x => x.Id == id && x.IsDelete == false).Select(x=> new { x.Id,x.Name,x.IsFood, listCategorysevice = x.SeviceCategories.Where(x=>x.IsDelete == false)});
        //    return Json(data);
        //}
        public bool delCategorySevice(int id)
        {
            try
            {
                SeviceCategory seviceCategory = new SeviceCategory();
                seviceCategory = _context.SeviceCategories.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (seviceCategory != null)
                {
                    seviceCategory.IsDelete = true;
                    _context.Update(seviceCategory);
                    _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        //public async Task<int> addCategorySevice([FromBody] ModelCategorySevice sevice)
        //{
        //    try
        //    {
        //        SeviceCategory seviceCategory = new SeviceCategory();
        //        seviceCategory.IdSevice = sevice.idSevice;
        //        seviceCategory.Name = sevice.name;
        //        seviceCategory.price = sevice.price;
        //        if (seviceCategory != null)
        //        {
                    
        //            _context.Add(seviceCategory);
        //            await _context.SaveChangesAsync();
        //            return seviceCategory.Id;
        //        }
        //        return 0;
        //    }
        //    catch (Exception err)
        //    {
        //        return 0;
        //    }
        //}
        public class ModelCategorySevice
        {
            public int id { get; set; }
            
            public String name { get; set; }
            public Decimal price { get; set; }
            public int idSevice { get; set; }
        }
        //[HttpPost]
        //public async Task<bool> editCategorySevice([FromBody]ModelCategorySevice sevice)
        //{
        //    try
        //    {
        //        SeviceCategory seviceCategory = new SeviceCategory();
        //        seviceCategory = _context.SeviceCategories.FirstOrDefault(x => x.Id == sevice.id && x.IsDelete == false);
        //        seviceCategory.Name = sevice.name;
        //        seviceCategory.price = sevice.price;
        //        _context.Update(seviceCategory);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch(Exception err) {
        //        return false;
        //    }
            
        //}
        // POST: Admin/Sevices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sevice sevice)
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
                    Message = "Cập nhật dịch vụ thành công";
                }
                catch (Exception er)
                {
                    Message = "Có lỗi xảy ra";
                }
                return RedirectToAction(nameof(Index));
            }
            Message = "Cập nhật dịch vụ thất bại";
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
