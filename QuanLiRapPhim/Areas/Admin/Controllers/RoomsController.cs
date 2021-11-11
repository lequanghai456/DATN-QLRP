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
    [AuthorizeRoles("Admin")]
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
                if (room == null)
                {
                    return NotFound();
                }
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
                            room.Price,
                            FullName = room.Role.Staffs.First().FullName.Length != 0 ?            room.Role.Staffs.First().FullName : ""
                        
                        };
            var query1 = from a in query
                         where (a.FullName.Contains(jTablePara.FullName) || String.IsNullOrEmpty(jTablePara.FullName)) && (String.IsNullOrEmpty(jTablePara.NameRoom) || a.Name.Contains(jTablePara.NameRoom))
                         select a;
            int count = query1.Count();
            var data = query1.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Name", "Row", "Col","Price", "FullName");
            return JsonConvert.SerializeObject(jdata);
        }
        public bool Kiemtradelete(int id)
        {

            var a = _context.ShowTimes
                .Where(x => x.RoomId == id && !x.IsDelete)
                .Where(x => x.DateTime.Date.CompareTo(DateTime.Now) >= 0).ToList();
            
            return a.Count() != 0;
        }
        public async Task<JsonResult> DeleteRoom(int id)
        {
            try
            {
                Room room = new Room();
                Role role = new Role();
                room = _context.Rooms.Include(x => x.Seats).FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (room == null)
                {
                    return Json("Không thể tìm thấy phòng");
                }
                if (!Kiemtradelete(id))
                {
                    room.IsDelete = true;
                    role = _context.Roles.FirstOrDefault(x => x.Id == room.RoleId);
                    role.IsDelete = true;
                    _context.Update(role);
                    _context.Update(room);
                    room.Seats.All(x => x.IsDelete = true);
                    await _context.SaveChangesAsync();
                    return Json("Xóa phòng thành công");
                }
                else
                {
                    return Json("Phòng còn lịch chiếu không thể xóa");
                }
            }
            catch(Exception err)
            {
                return Json("Có lỗi xảy ra");
            }
        }
        public async Task<JsonResult> DeleteRoomList(String Listid)
        {
            try
            {
                String[] List = Listid.Split(',');
                Room room = new Room();
                Role role = new Role();
                foreach (String id in List)
                {
                    if (!Kiemtradelete(int.Parse(id)))
                    {
                        room = _context.Rooms.Include(x => x.Seats).FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                        room.IsDelete = true;
                        role = _context.Roles.FirstOrDefault(x => x.Id == room.RoleId);
                        role.IsDelete = true;
                        room.Seats.All(x => x.IsDelete = true);
                        _context.Update(role);
                        _context.Update(room);
                    }
                    else
                    {
                        return Json("Xóa phòng thất bại do có phòng đã có lịch chiếu");
                    }
                }
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Create([Bind("Id,Name,Row,Col,RoleId,Price")] Room room)
        {
            try
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
            }catch(Exception err)
            {
                Message = "Có lỗi xảy ra trong quá trình tạo";
            }
            ViewData["RoleId"] = new SelectList(_context.Staffs, "Id", "RoleId", room.Role.Name);
            return View(room);
        }

        

        // POST: Admin/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Row,Col,RoleId,Price,IsDelete")] Room room)
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
                    
                    var seats = new List<Seat>();
                    room.Seats.All(x => x.IsDelete = true);
                    for (int j = 0; j < room.Row; j++)
                    {
                        for (int i = 0; i < room.Col; i++)
                        {
                            Seat seat = new Seat();
                            seat.X = char.ConvertFromUtf32(65 + j);
                            seat.Y = i;
                            Seat seat1 = _context.Seats.Include(a => a.Room)
                                .FirstOrDefault(x => x.RoomId == id && x.X == seat.X && x.Y == seat.Y);
                            if (seat1 == null)
                            {
                                seat.Room = room;
                                _context.Add(seat);
                            }
                            else
                            {
                                seat1.IsDelete = false;
                                _context.Update(seat1);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                    Message = "Cập nhật phòng thành công";
                }
                catch (DbUpdateConcurrencyException)
                {
                    Message = "Cập nhật phòng thất bại";
                }
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
