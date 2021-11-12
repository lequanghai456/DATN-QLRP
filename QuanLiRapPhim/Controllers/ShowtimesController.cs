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
            try
            {
                if (date == null)
                {
                    date = DateTime.Parse(DateTime.Now.ToShortDateString());
                }
                if (date.Value.CompareTo(DateTime.Now.Date.AddDays(7)) <= 0 && date.Value.CompareTo(DateTime.Now.Date) >0 )
                {
                    date = (DateTime)date.Value.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                    var lst = (from st in _context.ShowTimes
                                    where st.DateTime.AddHours(st.startTime.Hour).AddMinutes(st.startTime.Minute)
                                    .CompareTo((DateTime)date)>= 0 && !st.IsDelete
                                    orderby st.startTime.TimeOfDay
                                    select new
                                    {
                                        st.Id,
                                        st.Movie.Title,
                                        st.Movie.Poster,
                                        st.Room.Name,
                                        Soghe = st.Tickets.Where(x => x.IsDelete == false).Count(),
                                        Total = st.Room.Seats.Count(),
                                        st.DateTime,
                                        Time = string.Format("{0:t}", st.startTime),
                                        Day = st.DateTime
                                    }).ToList();
                        
                    jmess.Error = lst.Count()==0;
                    if (jmess.Error)
                    {
                        jmess.Title = "Không tìm thấy lịch chiếu";
                    }
                    else
                    {
                        jmess.Object = lst;
                    }
                }
                else
                {
                    jmess.Error = true;
                    jmess.Title = "Ngày quá 7 ngày";
                }
            }
            catch (Exception er)
            {
                jmess.Error = true;
                jmess.Title = "Có lỗi xãy ra";
            }

            return Json(jmess);
        }
    }
}
