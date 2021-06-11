using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class RoomsController : Controller
    {
        public class JTableModelCustom : JTableModel
        {
            public string NameRoom { get; set; }
            public string Staff { get; set; }
        }
        private readonly IdentityContext _context;

        public RoomsController(IdentityContext context)
        {
            _context = context;
        }
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
          
        //    ViewBag.List = _context.Rooms.Include(r => r.Staff);
        //    base.OnActionExecuted(context);
        //}
        public IActionResult Index(int? id)
        {
            Room room = null;
            if(id!=null)
            {
                room = _context.Rooms.FirstOrDefault(s => s.Id == id);
            }
            var identityContext = _context.Rooms.Include(r => r.Staff);
            ViewData["IdStaf"] = new SelectList(_context.Staffs, "Id", "FullName");
            return View(room);
        }
        //Danh sach phong json
        //public  JsonResult JsonRoom()
        //{
        //    var data =  _context.Rooms.Include(r=>r.Staff).Where(x=>x.IsDelete == false).ToList();
        //    return Json(new { data = data });
        //}
        [HttpGet]
        public async Task<String> JtableRoomModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Rooms.Include(r => r.Staff).Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.NameRoom) || x.Name.Contains(jTablePara.NameRoom)) && ((String.IsNullOrEmpty(jTablePara.Staff)) || x.Staff.FullName.Contains(jTablePara.Staff)));
            int count = query.Count();
            var data = query.AsQueryable().Select(x=> new { x.Id,x.Name,x.Row,x.Col,x.Staff.FullName})
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Name","Row","Col","FullName");
            return JsonConvert.SerializeObject(jdata);
        }
        public JsonResult DeleteRoom(int? id)
        {
            Room room = new Room();
            room = _context.Rooms.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
            if(room == null)
            {
                return Json("Fail");
            }
            room.IsDelete = true;
            _context.Update(room);
            _context.SaveChangesAsync();
            return Json("Pass");
        }
        public int DeleteRoomAll(String Listid)
        {
            int itam = 0;
            try
            {

            String[] List = Listid.Split(',');
            Room room = new Room();
            foreach (String id in List)
            {
                
                    room = _context.Rooms.FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    room.IsDelete = true;
                    _context.Update(room);
                    itam++;
                
            }
            }
            catch (Exception er)
            {
                _context.SaveChangesAsync();
                return itam;
            }
            _context.SaveChangesAsync();
            return itam;
           
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Row,Col,IdStaftManager")] Room room)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStaftManager"] = new SelectList(_context.Staffs ,"Id", "FullName", room.IdStaftManager);
            return View(room);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Row,Col,IdStaftManager")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["IdStaftManager"] = new SelectList(_context.Staffs, "Id", "FullName", room.IdStaftManager);
            return RedirectToAction(nameof(Index));
        }
        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
       
    }
}
