using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        private ShowTimes show;

        [TempData]
        public string Message { get; set; }


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
        }

        public async Task<String> JtableShowTimeModel(JTableModelCustom jTablePara)
        {
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.ShowTimes.Include(a=>a.Room).Include(b=>b.Movie).Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.date) ||x.DateTime.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0));
            int count = query.Count();
            var data = query.AsQueryable().Select(x=> new { x.Id,DateTime = x.DateTime.ToString("MM/dd/yyyy"),NameRoom = x.Room.Name,NameMovie = x.Movie.Title,StartTime = x.startTime.ToString("HH:mm"),x.Price})
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Price","DateTime","NameRoom","NameMovie","StartTime");
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

        public JsonResult ListMovie()
        {
            return Json(_context.Movies.ToList());
        }


        public async Task<IActionResult> CreateTimes(DateTime? Date)
        {
            if (Date != null)
            {
                ShowTimes showTimes = new ShowTimes();
                var List = _context.ShowTimes.Include(x=>x.Movie).Where(x => x.DateTime.CompareTo((DateTime)Date)==0).OrderBy(x => x.startTime);

                showTimes.Date = (DateTime)Date;

                if (List.ToList().Count() != 0)
                {
                    showTimes.TotalTime =(int) (List.First().startTime - List.Last().startTime.AddMinutes(List.Last().Movie.Time)).TotalSeconds;
                }

                ViewBag.ListMovies = new SelectList(_context.Movies.Where(x => x.IsDelete == false), "Id", "Title"); ;
                ViewBag.ListRooms = _context.Rooms.Where(x => x.IsDelete == false).ToList();

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
        public async Task<IActionResult> CreateTimes([Bind("TimeStart,Date,TotalTime,ListMivie")] ShowTimes showTimes)
        {
            var a = showTimes.TimeStart.AddMinutes(showTimes.TotalTime);
            if (a.Date > showTimes.TimeStart.Date)
            {
                ModelState.AddModelError(showTimes.TimeStart.ToString(), "Quá 12 giờ");
            }
            //showTimes.showTimes = _context.ShowTimes.Where(s => s.DateTime == showTimes.Date).ToList();
           
            if (ModelState.IsValid)
            {
                ShowTimes s = showTimes;
                List<Movie> movies = new List<Movie>();
                showTimes.showTimes = new List<ShowTime>();
                DateTime startTime = showTimes.TimeStart;
                foreach (int i in showTimes.ListMivie)
                {
                    var movie = _context.Movies.FirstOrDefault(x => x.Id == i);
                    movies.Add(movie);
                    showTimes.showTimes.Add(new ShowTime
                    {
                        Movie = movie, 
                        DateTime = showTimes.Date,
                        Price = 10000,
                        RoomId = 1,
                        startTime = startTime
                    });
                    
                    startTime = startTime.AddMinutes(movie.Time+30);
                }
                try
                {
                    foreach(var item in showTimes.showTimes)
                    {
                        _context.Add(item);
                    }    
                    _context.SaveChanges();
                    Message = "Lưu thành công";
                    return View("Index");
                }
                catch (Exception e)
                {
                    Message = e.ToString();
                }
            }
            ViewBag.ListMovies = new SelectList(_context.Movies.Where(x => x.IsDelete == false), "Id", "Title"); ;
            ViewBag.ListRooms = _context.Rooms.Where(x => x.IsDelete == false).ToList();

            return View("Index", showTimes);
        }
        public JsonResult GetMovieTitle(int id) {
            return Json(_context.Movies.Find(id).Title);
        }
    }
    public class ShowTimes
    {
        public ShowTimes()
        {
            TimeStart = DateTime.Parse("8:00:00 AM");
            ListMivie = new List<int>();
        }
        public List<int> ListMivie { get; set; }
        public int RoomID { get; set; }
        [DisplayName("Chọn phim")]
        public Movie movie1 { get; set; }
        public List<ShowTime> showTimes { get; set; }
        //[Range(0, 960, ErrorMessage = "Quá giờ")]
        public int TotalTime{ get; set; }
        [DisplayName("Giờ bắt đầu")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime TimeStart { get; set; }
        [DisplayName("Chọn ngày")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public String Json() {
            return JsonConvert.SerializeObject(ListMivie);
        }
    }
}
