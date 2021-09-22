using Microsoft.AspNetCore.Mvc;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.SupportJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class ShowtimesController : Controller
    {
        private readonly IdentityContext _context;

        public ShowtimesController(IdentityContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<DateTime> dates=new List<DateTime>();
            for(int i = 0; i < 8; i++)
            {
                dates.Add(DateTime.Now.AddDays(i));
            }
            ViewData["Dates"] = dates;
            return View();
        }

        public JsonResult GetListShowtime(DateTime? date)
        {
            JMessage jmess = new JMessage();
            if (date != null)
            {
                jmess.Object = (from st in _context.ShowTimes
                                where st.DateTime.CompareTo((DateTime)date) == 0
                                select new {
                                    st.Id,
                                    st.Movie.Title,
                                    st.Movie.Poster,
                                    Soghe = st.Tickets.Where(x=>x.IsDelete==false).Count(),
                                    Total=st.Room.Seats.Count(),
                                    st.DateTime,
                                    st.startTime
                                }).ToList();

                jmess.Error= jmess.Object==null;
                if (jmess.Error)
                {
                    jmess.Title = "Không tìm thấy lịch chiếu";
                }
            }
            else
            {
                jmess.Error = true;
                jmess.Title="Bạn chưa chọn ngày";
            }
            return Json(jmess);
        }
    }
}
