using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using QuanLiRapPhim.SupportJSON;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShowTimesController : Controller
    {
        private readonly IdentityContext _context;
        private ShowTimes show;

        [TempData]
        public string Message { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewBag.ListRooms = new SelectList(_context.Rooms.Where(x => x.IsDelete == false).ToList(), "Id", "Name");
        }

        public ShowTimesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Admin/ShowTimes
        public async Task<IActionResult> Index()
        {
           return View();
        }

        public class JTableModelCustom : JTableModel
        {
            public String date { get; set; }
            public int RoomId { get; set; }
        }

        public async Task<String> JtableShowTimeModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.ShowTimes.Include(a => a.Room).Include(b => b.Movie).Where(x => x.IsDelete == false
            && (String.IsNullOrEmpty(jTablePara.date) || x.DateTime.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
            && (jTablePara.RoomId == 0 || x.RoomId == jTablePara.RoomId));
            int count = query.Count();
            var data = query.AsQueryable().Select(x => new { x.Id, DateTime = x.DateTime.ToString("MM/dd/yyyy"), NameRoom = x.Room.Name, NameMovie = x.Movie.Title, StartTime = x.startTime.ToString("HH:mm") })
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "DateTime", "NameRoom", "NameMovie", "StartTime");
            return JsonConvert.SerializeObject(jdata);
        }

        // POST: Admin/ShowTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,DateTime,Price,startTime,MovieId,RoomId,IsDelete")] ShowTime showTime)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(showTime);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.ListShowTimes = _context.ShowTimes.Where(x => x.IsDelete == false).ToList();
        //    ViewBag.ListMovies = _context.Movies.Where(x => x.IsDelete == false).ToList();
        //    return View(showTime);
        //}



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
            JMessage jMessage = new JMessage();
            try
            {
                ShowTime showtime = new ShowTime();

                showtime = _context.ShowTimes.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                jMessage.Error = showtime == null;
                if (jMessage.Error)
                {
                    jMessage.Title = "Không tìm thấy lịch chiếu";
                    return Json(jMessage);
                }
                showtime.IsDelete = true;
                _context.Update(showtime);
                _context.SaveChangesAsync();
                jMessage.Title = "Thành công";
            }
            catch(Exception er)
            {
                jMessage.Error = true;
                jMessage.Title = "Có lỗi xãy ra";
            }
            return Json(jMessage);
        }

        public JsonResult DeleteShowTimeList(String Listid)
        {
            int itam = 0;
            JMessage jMessage = new JMessage();
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
                _context.SaveChangesAsync();
                jMessage.Error = false;
                jMessage.Title = "Xóa thành công " + itam + " lịch chiếu";
            }
            catch (Exception er)
            {
                jMessage.Error = true;
                jMessage.Title = "Có lỗi xãy ra";
            }
            return Json(Message);
        }

        private bool ShowTimeExists(int id)
        {
            return _context.ShowTimes.Any(e => e.Id == id);
        }

        public JsonResult ListMovie()
        {
            return Json(_context.Movies.ToList());
        }


        public async Task<IActionResult> CreateTimes(DateTime? Date,int? Roomid)
        {
            if (Date != null)
            {
                if (Roomid == null)
                {
                    Message = "Bạn chưa chọn phòng";
                    return RedirectToAction(nameof(Index));
                }
                ShowTimes showTimes = new ShowTimes();

                showTimes.showTimes = _context.ShowTimes.Include(x => x.Movie)
                    .Where(x => x.DateTime.CompareTo((DateTime)Date) == 0)
                    .Where(x=>!x.IsDelete)
                    .Where(x=>x.RoomId==(int)Roomid)
                    .Where(x=>x.Room.IsDelete==false)
                    .OrderBy(x => x.startTime).ToList();

                showTimes.ListMivie = showTimes.showTimes.Select(s => s.Movie.Id).ToList();

                showTimes.Date = (DateTime)Date;

                ViewBag.ListMovies = new SelectList(_context.Movies.Where(x => x.IsDelete == false), "Id", "Title");
                ViewBag.ListRooms = new SelectList(_context.Rooms.Where(x => x.IsDelete == false).ToList(), "Id", "Name");


                return View("Index", showTimes);
            }
            return View("Index");
        }

        //public IActionResult listshowTime(DateTime date)
        //{
        //    ShowTimes showTimes = new ShowTimes();
        //    var query = _context.ShowTimes.Include(x => x.Movie).Where(x => x.IsDelete == false && x.DateTime.Date.CompareTo(date.Date) == 0).Select(x=> new { DateTime = x.DateTime.ToString("MM/dd/yyyy"), x.Movie.Title, startTime = x.startTime.ToString("HH:mm"),endTime = x.startTime.AddMinutes(x.Movie.Time).ToString("HH:mm") });
        //    return Json(query);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTimes([Bind("TimeStart,Date,TotalTime,ListMivie,RoomId")] ShowTimes showTimes)
        {
            var a = showTimes.TimeStart.AddMinutes(showTimes.TotalTime);
            if (a.Date > showTimes.TimeStart.Date)
            {
                ModelState.AddModelError(showTimes.TimeStart.ToString(), "Quá 12 giờ");
            }
            //showTimes.showTimes = _context.ShowTimes.Where(s => s.DateTime == showTimes.Date).ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    List<Movie> movies = new List<Movie>();
                    showTimes.showTimes = _context.ShowTimes
                        .Where(x => x.DateTime == showTimes.Date)
                        .Where(x=>x.IsDelete==false)
                        .Where(x => x.RoomId == showTimes.RoomId)
                        .Where(x => x.Room.IsDelete == false)
                        .OrderBy(x => x.startTime).ToList();

                    DateTime startTime = showTimes.TimeStart;
                    if (showTimes.showTimes.Count > showTimes.ListMivie.Count)
                    {

                    }
                    foreach (var item in showTimes.ListMivie.Select((value, index) => (index, value)))
                    {
                        var movie = _context.Movies.FirstOrDefault(x => x.Id == item.value);
                        movies.Add(movie);
                        var s = new ShowTime();
                        if (showTimes.showTimes.Count > item.index)
                        {
                            s = _context.ShowTimes.Find(showTimes.showTimes[item.index].Id);
                            s.Movie = movie;
                            s.DateTime = showTimes.Date;
                            s.RoomId = 1;
                            s.startTime = startTime;
                            startTime = startTime.AddMinutes(movie.Time + 30);
                            _context.Update(s);
                        }
                        else
                        {
                            s = new ShowTime();
                            s.Movie = movie;
                            s.DateTime = showTimes.Date;
                            s.RoomId = 1;
                            s.startTime = startTime;
                            startTime = startTime.AddMinutes(movie.Time + 30);

                            _context.Add(s);
                        }

                    }
                    for (int i = showTimes.ListMivie.Count; i < showTimes.showTimes.Count; i++)
                    {
                        var del = _context.ShowTimes.Find(showTimes.showTimes[i].Id);
                        del.IsDelete = true;
                        _context.Update(del);
                    }

                    _context.SaveChanges();
                    Message = "Lưu thành công";
                    return View("Index");
                }
                catch (Exception e)
                {
                    Message = e.ToString();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ListMovies = new SelectList(_context.Movies.Where(x => x.IsDelete == false), "Id", "Title"); ;
            
            return RedirectToAction(nameof(Index));
        }
        public JsonResult GetMovieTitle(int id)
        {
            return Json(_context.Movies.Find(id).Title);
        }

        public IActionResult CoppyShowTimes(DateTime from, DateTime to, int idFrom,int idTo)
        {
            try
            {
                var showtimefrom = _context.ShowTimes
                    .Where(x => x.DateTime.Date == from.Date)
                    .Where(x=>x.RoomId==idFrom)
                    .Where(x=>x.Room.IsDelete==false)
                    .OrderBy(x => x.startTime).ToList();
                var showtimeto = _context.ShowTimes
                    .Where(x => x.DateTime.Date == to.Date)
                    .Where(x => x.RoomId == idTo)
                    .Where(x => x.Room.IsDelete == false)
                    .OrderBy(x => x.startTime).ToList();
                foreach(var item in showtimeto)
                {
                    item.IsDelete = true;
                    _context.Update(item);
                }
                foreach (var item in showtimefrom)
                {
                    item.DateTime = to.Date;
                    item.Id = 0;
                    _context.Add(item);
                }
                _context.SaveChanges();
                Message = "Copy thành công ngày "+from.ToShortDateString()+" đên ngày "+ to.ToShortDateString();
            }
            catch (Exception e)
            {
                Message = "Có lỗi xãy ra";
            }
            return RedirectToAction(nameof(Index));
        }
    }
    public class ShowTimes
    {
        public ShowTimes()
        {
            TimeStart = DateTime.Parse("8:00:00 AM");
            ListMivie = new List<int>();
        }
        public List<ShowTime> showTimesCreated { get; set; }
        public List<int> ListMivie { get; set; }
        [Required(ErrorMessage ="Bạn chưa chọn phòng")]
        public int RoomId { get; set; }
        [DisplayName("Chọn phim")]
        public Movie movie1 { get; set; }
        public List<ShowTime> showTimes { get; set; }
        //[Range(0, 960, ErrorMessage = "Quá giờ")]
        public int TotalTime { get; set; }
        [DisplayName("Giờ bắt đầu")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime TimeStart { get; set; }
        [DisplayName("Chọn ngày")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public String Json()
        {
            return JsonConvert.SerializeObject(ListMivie);
        }
    }
}
