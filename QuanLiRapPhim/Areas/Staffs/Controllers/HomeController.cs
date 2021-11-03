using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Staffs.Controllers
{
    [Area("Staffs")]
    [AuthorizeRoles("Admin,Manager,Staff")]
    public class HomeController : Controller
    {
        private readonly IdentityContext _context;

        public HomeController(IdentityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public string JtableTestModel(JModel jTablePara)
        {
            //Truy vấn lấy ds bill và ticket theo username sắp xếp theo thời gian 
            var Bills = from b in _context.Bills.Where(x=>!x.IsDelete)
                        //where b.User.UserName == User.Identity.Name
                        where (String.IsNullOrEmpty(jTablePara.date) || b.Date.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
                        select new
                        {
                            isTicket = false,
                            b.Id,
                            b.Date,
                            b.TotalPrice,
                            b.BillDetails,
                            b.Status
                        };
            var Tickets = from t in _context.Tickets
                          where t.Username == User.Identity.Name
                          && (String.IsNullOrEmpty(jTablePara.date) || t.ShowTime.DateTime.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
                          select t;


            List<yourOrder> yourOrders = new List<yourOrder>();
            yourOrders.AddRange((from x in Bills
                                 where x.Status == false
                                 select new yourOrder
                                 {
                                     id = "HD" + x.Id,
                                     Objects = x,
                                     Date = x.Date
                                 }).ToList());

            yourOrders.AddRange((from x in Tickets
                                 where x.Status == false
                                 select new yourOrder
                                 {
                                     id = "V" + x.Id,
                                     Objects = new
                                     {
                                         isTicket = true,
                                         x.ShowTime.Movie.Title,
                                         x.Seat,
                                         Time = x.ShowTime.startTime.ToShortTimeString(),
                                         x.Name,
                                         Date = x.ShowTime.DateTime.ToShortDateString(),
                                         Room = x.Seat.Room.Name
                                     },
                                     Date = x.PurchaseDate
                                 }).ToList());
            yourOrders = yourOrders.Where(x => String.IsNullOrEmpty(jTablePara.date) || x.Date.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
                .OrderByDescending(x => x.Date).ToList();

            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;

            var query = from a in yourOrders
                        select new
                        {
                            a.id,
                            Objects = JsonConvert.SerializeObject(a.Objects),
                            Date = a.Date.ToShortDateString()
                        };

            int count = query.Count();

            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "id", "Objects", "Date");
            return JsonConvert.SerializeObject(jdata);
        }
    }
}
