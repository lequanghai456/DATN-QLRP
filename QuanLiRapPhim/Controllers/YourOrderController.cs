using AutomatedInvoiceGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using QuanLiRapPhim.SupportJSON;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class YourOrderController : Controller
    {
        public readonly IdentityContext _context;

        [TempData]
        public string Message { get; set; }

        public YourOrderController(IdentityContext context)
        {
            _context = context;           
        }
        //[HttpPost]
        //public IActionResult BuySevice(List<BillDetail> Billdetails) {

        //    try
        //    {
        //        foreach (var x in Billdetails)
        //        {
        //            var Sevice = _context.SeviceCategories.Find(x.SeviceCatId);
        //            Sevice.Sevice = _context.Sevices.Find(Sevice.IdSevice);
        //            x.Name = Sevice.Sevice.Name + "(" + Sevice.Name + ")";
        //            x.UnitPrice = Sevice.price;
        //            x.Bill = null;
        //        }
        //        Bill bill = new Bill()
        //        {
        //            BillDetails = Billdetails,
        //            Date = DateTime.Now,
        //            TotalPrice = Billdetails.Sum(x => x.UnitPrice * x.Amount),
        //            UserId = _context.Users.Where(x => x.UserName == (string)User.Identity.Name).First().Id,
        //        };

        //        _context.Add(bill);
        //        _context.SaveChanges();
        //        Message = "Thanh toán thành công ";
        //    }
        //    catch(Exception er)
        //    {
        //        Message = "Lỗi";
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult BookTicket(Ticket ticket)
        {
            //var flag = (from sh in _context.ShowTimes
            //            join r in _context.Rooms on sh.RoomId equals r.Id
            //            join se in _context.Seats on r.Id equals se.RoomId
            //            where sh.Id == ticket.ShowTimeId && se.Id == ticket.SeatId
            //            select se).Count() > 0;

            try
            {
                var isbooked = (from T in _context.Tickets
                            where T.SeatId == ticket.SeatId && T.IsDelete==false
                            select T).Count() > 0;

                if (ticket.SeatId == null || isbooked)
                {
                    return NotFound();
                }

                ticket.Username = User.Identity.Name;
                ticket.Price = Price((int)ticket.SeatId, (int)ticket.ShowTimeId);
                ticket.PurchaseDate = DateTime.Now;
                _context.Add(ticket);
                _context.SaveChanges();
                Message = "Bạn đã mua thành công";
            }
            catch (Exception er)
            {
                Message = "Lỗi";
            }
            return RedirectToAction(nameof(Index));
        }

        private decimal Price(int idseat, int idShowTime)
        {
            decimal price = _context.ShowTimes
                .Include(x => x.Room).FirstOrDefault(x => x.Id == idShowTime).Room.Price;
            var s = _context.Seats.Find(idseat);
            switch (s.X)
            {
                case "A":
                case "B":
                case "C":
                    price += price / 10;
                    break;
                case "D":
                case "E":
                case "F":
                    price += price / 5;
                    break;
                default:
                    break;
            }

            return price;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public JsonResult GetAllServices()
        //{
        //    JMessage jMessage = new JMessage();
        //    var Object = _context.Sevices.Where(s => !s.IsDelete).Select(x => new
        //    {
        //        x.Id,
        //        x.IsFood,
        //        x.Name,
        //        size = x.SeviceCategories.Where(x=>!x.IsDelete).Select(x => new
        //        {
        //            x.Name,
        //            x.price,
        //            x.Id
        //        }).ToList()
        //    }).ToList();

        //    jMessage.Error = Object.Count == 0;
        //    if (jMessage.Error)
        //    {
        //        jMessage.Title = "Không tìm thấy dịch vụ";
        //    }
        //    else
        //    {
        //        jMessage.Object = Object;
        //    }
        //    return Json(jMessage);
        //}
        public List<yourOrder> yourOrders { get; set; }

        public JsonResult GetAll()
        {
            return Json(yourOrders);
        }



        [HttpGet]
        public string JtableTestModel(JTableModel jTablePara)
        {
            //Truy vấn lấy ds bill và ticket theo username sắp xếp theo thời gian 
            var Bills = from b in _context.Bills
                        where b.User.UserName==User.Identity.Name
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
                      select t;

            
            yourOrders = new List<yourOrder>();
            yourOrders.AddRange((from x in Bills
                                 where x.Status == false
                                 select new yourOrder
                                 {
                                     id = "HD" + x.Id,
                                     Objects = x,
                                     Date = x.Date
                                 }).ToList());

            yourOrders.AddRange((from x in Tickets
                                 where x.Status==false
                                 select new yourOrder
                                 {
                                     id = "V" + x.Id,
                                     Objects = new {
                                        isTicket=true,
                                        x.ShowTime.Movie.Title,
                                        x.Seat,
                                        Time=x.ShowTime.startTime.ToShortTimeString(),
                                        x.Name,
                                        Date=x.ShowTime.DateTime.ToShortDateString(),
                                        Room=x.Seat.Room.Name
                                     },
                                     Date = x.PurchaseDate
                                 }).ToList());
            yourOrders = yourOrders.OrderByDescending(x => x.Date).ToList();

            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;

            var query = from a in yourOrders
                        select new{
                a.id,
                Objects=JsonConvert.SerializeObject(a.Objects),
                Date=a.Date.ToShortDateString()
            };

            int count = query.Count();

            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "id", "Objects", "Date");
            return JsonConvert.SerializeObject(jdata);
        }

    }
    public class yourOrder
    {
        public String id { get; set; }
        public object Objects { get; set; }
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }
        public bool IsTicket { get; set; }

    }
}


