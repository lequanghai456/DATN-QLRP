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
    public class ShowTimesController : Controller
    {
        private readonly IdentityContext _context;

        public ShowTimesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Admin/ShowTimes
        public async Task<IActionResult> Index(int? id)
        {
            ShowTime showTime = null;
            if (id != null)
            {
                showTime = _context.ShowTimes.FirstOrDefault(s => s.Id == id);
               
            }
            ViewBag.ListRooms = _context.Rooms.Where(x => x.IsDelete == false).ToList();
            ViewBag.ListMovies = _context.Movies.Where(x => x.IsDelete == false).ToList();
            return View(showTime);
        }
        public class JTableModelCustom : JTableModel
        {
            public string NameRoom { get; set; }
            public string NameMovie { get; set; }
        }
        public async Task<String> JtableShowTimeModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.ShowTimes.Include(a=>a.Room).Include(b=>b.Movie).Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.NameRoom) || x.Room.Name.Contains(jTablePara.NameRoom)));
            int count = query.Count();
            var data = query.AsQueryable().Select(x=> new { x.Id,DateTime = x.DateTime.ToString("MM/dd/yyyy"),NameRoom = x.Room.Name,NameMovie = x.Movie.Title,StartTime = x.startTime.ToString("HH:mm")})
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "DateTime","NameRoom","NameMovie","StartTime");
            return JsonConvert.SerializeObject(jdata);
        }

        // POST: Admin/ShowTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,Price,startTime,MovieId,RoomId,IsDelete")] ShowTime showTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(showTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ListShowTimes = _context.ShowTimes.Where(x => x.IsDelete == false).ToList();
            ViewBag.ListMovies = _context.Movies.Where(x => x.IsDelete == false).ToList();
            return View(showTime);
        }

      

        // POST: Admin/ShowTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,Price,startTime,MovieId,RoomId,IsDelete")] ShowTime showTime)
        {
            if (id != showTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowTimeExists(showTime.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title");
            ViewData["ShowTimeId"] = new SelectList(_context.ShowTimes, "Id", "Id");
            return View(showTime);
        }
        public JsonResult DeleteShowTime(int? id)
        {
            ShowTime showtime = new ShowTime();
            
            showtime = _context.ShowTimes.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
            if (showtime == null)
            {

                return Json("Fail");
            }
            showtime.IsDelete = true;
            _context.Update(showtime);
            _context.SaveChangesAsync();
            return Json("Success");
        }
        [TempData]
        public string Message { get; set; }
        public JsonResult DeleteShowTimeList(String Listid)
        {
            int itam = 0;
            try
            {
                String[] List = Listid.Split(',');
                ShowTime showtime = new ShowTime();
              
                foreach (String id in List)
                {
                    showtime = _context.ShowTimes.FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    showtime.IsDelete = true;
                    _context.Update(showtime);
                    itam++;
                }
            }
            catch (Exception er)
            {
                Message = "Successfully deleted " + itam + " showtimes";
                _context.SaveChangesAsync();
                return Json("Successfully deleted " + itam + " showtimes");
            }
            Message = "Successfully deleted " + itam + " showtimes";
            _context.SaveChangesAsync();
            return Json("Successfully deleted " + itam + " showtimes");


        }

        private bool ShowTimeExists(int id)
        {
            return _context.ShowTimes.Any(e => e.Id == id);
        }
    }
}
