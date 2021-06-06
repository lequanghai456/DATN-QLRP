using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomsController : Controller
    {
        private readonly IdentityContext _context;

        public RoomsController(IdentityContext context)
        {
            _context = context;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
          
            ViewBag.List = _context.Rooms.Include(r => r.Staff);
            base.OnActionExecuted(context);
        }
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
        public  JsonResult JsonRoom()
        {
            var data =  _context.Rooms.Include(r=>r.Staff).ToList();
            return Json(new { data = data });
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
