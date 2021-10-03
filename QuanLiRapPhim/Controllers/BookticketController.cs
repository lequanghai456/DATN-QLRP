using Microsoft.AspNetCore.Mvc;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using QuanLiRapPhim.SupportJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class BookticketController : Controller
    {
        private readonly IdentityContext _context;
        
        public BookticketController(IdentityContext context)
        {
            _context = context;
        }
        [AuthorizeRoles("")]
        public IActionResult Index(int id)
        {
            var has = _context.ShowTimes.Any(x => x.Id == id);
            var deleted = _context.ShowTimes.Where(x=>x.IsDelete).Any(x => x.Id == id);
            if (id <= 0 || !has || deleted)
            {
                return NotFound();
            }
            var St = _context.ShowTimes.Find(id);

            return View(St);
        }
        public JsonResult getRoomByIdShowtime(int id)
        {
            JMessage jMessage = new JMessage();
            jMessage.ID = id;
            jMessage.Error = id <= 0 || _context.ShowTimes.Where(x => x.IsDelete == true).Any(x => x.Id == id);
            if (jMessage.Error == true) {
                jMessage.Title = "Không tìm thấy lịch chiếu hoặc phim của bạn";
            }
            else
            {
                jMessage.Object = (from sh in _context.ShowTimes
                                   join r in _context.Rooms on sh.RoomId equals r.Id
                                   join mv in _context.Movies on sh.MovieId equals mv.Id
                                   where sh.Id == id
                                   select new
                                   {
                                       sh.Id,
                                       r.Row,
                                       r.Col,
                                       time=sh.startTime.ToShortTimeString(),
                                       date=sh.DateTime.ToShortDateString(),
                                       r.Name,
                                       mv.Title,
                                       mv.Poster,
                                       User=_context.Users.FirstOrDefault(x=>x.UserName==User.Identity.Name).FullName
                                   }).FirstOrDefault();
            }
            return Json(jMessage);
        }
        public JsonResult getListSeatOfShowtime(int id)
        {
            JMessage jMessage = new JMessage();
            jMessage.ID = id;
            jMessage.Error = id <= 0 || _context.ShowTimes.Where(x => x.IsDelete == true).Any(x => x.Id == id);
            if (jMessage.Error == true)
            {
                jMessage.Title = "Không tìm thấy lịch chiếu hoặc phim của bạn";
            }
            else
            {
                var seats = (from se in _context.Seats
                                   join r in _context.Rooms on se.RoomId equals r.Id
                                   join sh in _context.ShowTimes on r.Id equals sh.RoomId
                                   where sh.Id == id && !se.IsDelete
                                   orderby se.X,se.Y
                                   select new
                                   {
                                       se.Id,
                                       se.X,
                                       se.Y,
                                       se.Status
                                   }).ToList().GroupBy(x => x.X).Select(x => new { name = x.Key,arr=x.ToList().Select(x=> new {x.X, x.Y,x.Id,x.Status}) });
                jMessage.Object = seats;
            }
            return Json(jMessage);
        }
        public JsonResult DsGheDaDat(int id) {
            JMessage jMessage = new JMessage();
            jMessage.ID = id;
            var has = _context.ShowTimes.Where(x => !x.IsDelete).Any(x => x.Id == id);
            jMessage.Error = id <= 0 || !has;
            if (jMessage.Error == true)
            {
                jMessage.Title = "Không tìm thấy lịch chiếu của bạn";
            }
            else
            {
                var seats = (from t in _context.Tickets
                            join sh in _context.ShowTimes
                            on t.ShowTimeId equals sh.Id
                            where sh.Id==id
                            select t.SeatId).ToList();
                jMessage.Object = seats;
            }
            return Json(jMessage);
        }
    }
}

