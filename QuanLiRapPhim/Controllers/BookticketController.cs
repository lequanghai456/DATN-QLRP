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
            bool a=User.Identity.Name == null;
            if (id <= 0 || _context.ShowTimes.Where(x => x.IsDelete == true).Any(x => x.Id == id))
            {
                return RedirectToAction("Index","Home");
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
                                       r.Row,
                                       r.Col,
                                       time=sh.startTime.ToShortTimeString(),
                                       r.Name,
                                       mv.Title,
                                       mv.Poster,
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
    }
}

