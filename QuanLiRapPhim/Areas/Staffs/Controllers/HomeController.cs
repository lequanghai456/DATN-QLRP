using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using QuanLiRapPhim.Controllers;
using QuanLiRapPhim.SupportJSON;
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
        [TempData]
        public string Message { get; set; }

        public HomeController(IdentityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Purchase(int id,bool IsT)
        {
            try
            {
                if (IsT)
                {
                    var a = _context.Tickets.Find(id);
                    a.IsPurchased = true;
                    _context.Update(a);
                }
                else
                {
                    var a = _context.Bills.Find(id);
                    a.IsPurchased = true;
                    _context.Update(a);
                }
                _context.SaveChanges();
            }
            catch (Exception err)
            {
                return Json(false);
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult BuyTicket([FromBody]STime st) {
            JMessage message = new JMessage();
            try
            {
                message.Object = st;
                var ShowTime = _context.ShowTimes.Where(x => !x.IsDelete && x.Id == int.Parse(st.id)).FirstOrDefault();
                message.Error = ShowTime == null;
                if (message.Error)
                {
                    message.Title = "Không tìm thấy lịch chiếu";
                }
                else{
                    message.Error = st.idS.Count() == 0;
                    if (message.Error)
                    {
                        message.Title = "Bạn chưa chọn ghế";
                    }
                    else
                    {
                        foreach(int id in st.idS)
                        {
                            Ticket ticket = new Ticket();
                            ticket.Username = User.Identity.Name;
                            ticket.SeatId = id;
                            ticket.Price = Price(id, int.Parse(st.id));
                            ticket.PurchaseDate = DateTime.Now;
                            ticket.Name = "--------";
                            ticket.ShowTimeId = int.Parse(st.id);
                            ticket.IsPurchased = true;
                            _context.Add(ticket);
                        }
                        _context.SaveChanges();
                    }
                }
            }
            catch(Exception err)
            {
                message.Error = true;
                message.Title = "Có lỗi xảy ra "+err.ToString();
            }
            return Json(message);
        }

        private decimal Price(int seatId, int showTimeId)
        {
            decimal price = _context.ShowTimes
                .Include(x => x.Room).FirstOrDefault(x => x.Id == showTimeId).Room.Price;
            var s = _context.Seats.Find(seatId);
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

        [HttpGet]
        public string JtableTestModel(JModel jTablePara)
        {
            //Truy vấn lấy ds bill và ticket theo username sắp xếp theo thời gian 
            var Bills = from b in _context.Bills.Where(x=>!x.IsDelete)
                        where (String.IsNullOrEmpty(jTablePara.date) || b.Date.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
                        select new
                        {
                            isTicket = false,
                            b.Id,
                            b.Date,
                            b.TotalPrice,
                            b.BillDetails,
                            b.Status,
                            b.Username,
                            b.IsPurchased
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
                                     Date = x.Date,
                                     Username = x.Username
                                 }).ToList());

            yourOrders.AddRange((from x in Tickets.Where(x=>!x.IsDelete&&x.SeatId!=null)
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
                                     Date = x.PurchaseDate,
                                     Username = x.Username
                                 }).ToList());
            yourOrders = yourOrders.Where(x => String.IsNullOrEmpty(jTablePara.date) || x.Date.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
                .OrderByDescending(x => x.Date).ToList();

            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;

            var query = from a in yourOrders
                        where
                        (jTablePara.idsearch == null || a.Username.Contains(jTablePara.idsearch) || a.id.Contains(jTablePara.idsearch))
                        select new
                        {
                            a.id,
                            Objects = JsonConvert.SerializeObject(a.Objects),
                            Date = a.Date.ToShortDateString(),
                            a.Username
                        };

            int count = query.Count();

            var data = query.AsQueryable()
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "id","Username" ,"Objects", "Date");
            return JsonConvert.SerializeObject(jdata);
        }
       
        [HttpPost]
        public JsonResult BuySevice([FromBody]List<BillDetail> Billdetails)
        {
            JMessage message = new JMessage();
            try
            {
                message.Error = Billdetails == null;
                if (message.Error)
                {
                    message.Title = "Không tìm thấy hóa đơn";
                }
                else
                {
                    foreach (var x in Billdetails)
                    {
                        var seviceSeviceCategories = _context.seviceSeviceCategories.Find(x.idSeviceSeviceCategories);
                        seviceSeviceCategories.Sevice = _context.Sevices.Find(seviceSeviceCategories.IdSevice);
                        seviceSeviceCategories.SeviceCategory = _context.SeviceCategories.Find(seviceSeviceCategories.IdSeviceCategory);

                        x.Name = seviceSeviceCategories.Sevice.Name + "(" + seviceSeviceCategories.SeviceCategory.Name + ")";
                        x.UnitPrice = seviceSeviceCategories.Price;
                        x.Bill = null;
                    }
                    Bill bill = new Bill()
                    {
                        BillDetails = Billdetails,
                        Date = DateTime.Now,
                        TotalPrice = Billdetails.Sum(x => x.UnitPrice * x.Amount),
                        Username = User.Identity.Name,
                        IsPurchased = true
                    };
                    message.Object = JsonConvert.SerializeObject(bill);
                    _context.Add(bill);
                    _context.SaveChanges();
                }
            }
            catch (Exception er)
            {
                message.Error = true;
                message.Title = "Có lỗi xảy ra";

            }

            return Json(message);
        }
    }

    public class STime
    {
        public String id { get; set; }
        public int[] idS { get; set; }
    }
}
