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

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AuthorizeRoles("admin")]
    public class RoomsController : Controller
    {
        public class JTableModelCustom : JTableModel
        {
            public string NameRoom { get; set; }
            public string FullName { get; set; }
        }
        private RoleManager<Role> RoleMgr { get; }
        [TempData]
        public string Message { get; set; }
        private readonly IdentityContext _context;

        public RoomsController(IdentityContext context, RoleManager<Role> roleManager)
        {
            _context = context;
            RoleMgr = roleManager;
        }
        public IActionResult Index(int? id)
        {
            Room room = null;
            if (id != null)
            {
                room = _context.Rooms.FirstOrDefault(s => s.Id == id);
            }
            return View(room);
        }
        [HttpGet]
        public String JtableRoomModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = from room in _context.Rooms.Include(x => x.Role)
                        where room.IsDelete == false
                        select new
                        {
                            room.Id,
                            room.Name,
                            room.Row,
                            room.Col,
                            FullName = room.Role.Staffs.First().FullName.Length != 0 ?            room.Role.Staffs.First().FullName : ""
                        
                        };
            var query1 = from a in query
                         where (a.FullName.Contains(jTablePara.FullName) || String.IsNullOrEmpty(jTablePara.FullName)) && (String.IsNullOrEmpty(jTablePara.NameRoom) || a.Name.Contains(jTablePara.NameRoom))
                         select a;
            int count = query1.Count();
            var data = query1.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Name", "Row", "Col", "FullName");
            return JsonConvert.SerializeObject(jdata);
        }
        public JsonResult DeleteRoom(int? id)
        {
            try
            {
                Room room = new Room();
                Role role = new Role();
                room = _context.Rooms.Include(x => x.Seats).FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (room == null)
                {
                    return Json("Xóa phòng thất bại");
                }
                room.IsDelete = true;
                role = _context.Roles.FirstOrDefault(x => x.Id == room.RoleId);
                role.IsDelete = true;
                _context.Update(role);
                _context.Update(room);
                room.Seats.Clear();
                _context.SaveChangesAsync();
                return Json("Xóa phòng thành công");
            }
            catch(Exception err)
            {
                
                return Json("Xóa phòng thất bại");
            }
        }
        public JsonResult DeleteRoomList(String Listid)
        {
            try
            {
                String[] List = Listid.Split(',');
                Room room = new Room();
                Role role = new Role();
                foreach (String id in List)
                {
                    room = _context.Rooms.Include(x=>x.Seats).FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    room.IsDelete = true;
                    role = _context.Roles.FirstOrDefault(x => x.Id == room.RoleId);
                    role.IsDelete = true;
                    room.Seats.Clear();
                    _context.Update(role);
                    _context.Update(room);

                    
                }
                _context.SaveChangesAsync();
                return Json("Xóa phòng thành công");
            }
            catch (Exception err)
            {
                return Json("Xóa thất bại");
            }
           


        }

        // POST: Admin/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Row,Col,RoleId")] Room room)
        {
            if (ModelState.IsValid)
            {
                Role role = new Role();
                int idem = _context.Rooms.Count() + 1;
                room.Name = "Room " + idem;
                role.Name = "Manager " + room.Name;
                await RoleMgr.CreateAsync(role);
                room.RoleId = role.Id;
                _context.Add(room);
                var seats = new List<Seat>();
                for (int j = 0; j < room.Row; j++)
                {
                    for (int i = 0; i < room.Col; i++)
                    {
                        Seat seat = new Seat();
                        seat.X = char.ConvertFromUtf32(65 + j);
                        seat.Y = i;
                        seat.Room = room;
                        _context.Add(seat);
                    }
                }
                await _context.SaveChangesAsync();
                Message = "Tạo phòng thành công";
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Staffs, "Id", "RoleId", room.Role.Name);
            return View(room);
        }

        

        // POST: Admin/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Row,Col,RoleId,IsDelete")] Room room)
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
                    room = _context.Rooms.Include(x => x.Seats).FirstOrDefault(x => x.Id == id);
                    room.Seats.Clear();
                    var seats = new List<Seat>();
                    for (int j = 0; j < room.Row; j++)
                    {
                        for (int i = 0; i < room.Col; i++)
                        {
                            Seat seat = new Seat();
                            seat.X = char.ConvertFromUtf32(65 + j);
                            seat.Y = i;
                            seat.Room = room;
                            _context.Add(seat);
                        }
                    }
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
                Message = "Cập nhật phòng thành công";
                return RedirectToAction(nameof(Index));
            }
            
            return View(room);
        }
        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
