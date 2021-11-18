using AutomatedInvoiceGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.App;
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
        [AuthorizeRoles("User")]
        [HttpPost]
        public IActionResult BuySevice(List<BillDetail> Billdetails)
        {
            try
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
                };

                _context.Add(bill);
                _context.SaveChanges();
                User u = _context.Users.FirstOrDefault(x=>x.UserName==bill.Username);
                bool a = SendEmailSuccesOder(u,bill,null);
                if (a)
                    Message = "Đặt hàng thành công";
                else
                    Message = u.Email;
            }
            catch (Exception er)
            {
                Message+= "Lỗi";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        public decimal Price(int idseat, int idShowTime)
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

        [AuthorizeRoles("User")]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllServices()
        {
            JMessage jMessage = new JMessage();
            var Object = _context.Sevices.Where(s => !s.IsDelete).Select(x => new
            {
                x.Id,
                x.IsFood,
                x.Name,
                size = x.LstSeviceSeviceCategories.Where(x =>x.isDelete==false).Select(x => new
                {
                    x.SeviceCategory.Name,
                    x.Price,
                    x.Id
                }).ToList()
            }).ToList();

            jMessage.Error = Object.Count == 0;
            if (jMessage.Error)
            {
                jMessage.Title = "Không tìm thấy dịch vụ";
            }
            else
            {
                jMessage.Object = Object;
            }
            return Json(jMessage);
        }



        public List<yourOrder> yourOrders { get; set; }

        public JsonResult GetAll()
        {
            return Json(yourOrders);
        }

        [AuthorizeRoles("User")]
        [HttpGet]
        public string JtableTestModel(JModel jTablePara)
        {
            //Truy vấn lấy ds bill và ticket theo username sắp xếp theo thời gian 
            var Bills = from b in _context.Bills.Where(x => !x.IsDelete).Where(x => x.IsPurchased)
                        where b.Username == User.Identity.Name
                        //&& (String.IsNullOrEmpty(jTablePara.date) || b.Date.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
                        select new
                        {
                            isTicket = false,
                            b.Id,
                            b.Date,
                            b.TotalPrice,
                            b.BillDetails,
                            b.Status,
                            b.IsPurchased
                        };
            var Tickets = from t in _context.Tickets.Where(x=>!x.IsDelete).Where(x=>x.IsPurchased)
                      where t.Username == User.Identity.Name
                          //&& (String.IsNullOrEmpty(jTablePara.date) || t.ShowTime.DateTime.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
                          select t;
            yourOrders = new List<yourOrder>();
            yourOrders.AddRange((from x in Bills
                                 where x.Status == false
                                 select new yourOrder
                                 {
                                     id = "HD" + x.Id,
                                     Objects = x,
                                     Date = x.Date,
                                     
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
                                        Room=x.Seat.Room.Name,
                                        IsPurchased=x.IsPurchased
                                     },
                                     Date = x.PurchaseDate
                                 }).ToList());
            yourOrders = yourOrders.Where(x=> String.IsNullOrEmpty(jTablePara.date) || x.Date.Date.CompareTo(DateTime.Parse(jTablePara.date).Date) == 0)
                .OrderByDescending(x => x.Date).ToList();

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

        [AuthorizeRoles("User")]
        public bool SendEmailSuccesOder(User user, Bill bill,Ticket ticket)
        {
            String res="";
            if (bill == null && ticket == null)
            {
                res = "Có lỗi xảy ra! Vui lòng thông báo lại vào email này.";
            }
            else {
                if (bill == null)
                {
                    res = "Bạn đã đặt vé có mã: V" + ticket.Id + " Vào ngày: " +ticket.PurchaseDate.Date;
                }
                else
                {
                    res = "Bạn đã đặt hóa đơn có mã: HD" + bill.Id+ " Vào ngày: " + bill.Date.Date;
                }
            }
            return Email.SendMailGoogleSmtp("0306181113@caothang.edu.vn",user.Email, "Lấy mã đơn hàng",res).Result?
                true: false;
        }
    }
    public class JModel : JTableModel
    {
        public string date { get; set; }
        public string idsearch { get; set; }
    }
    public class yourOrder
    {
        public String id { get; set; }
        public object Objects { get; set; }
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }
        public bool IsTicket { get; set; }
        public string Username { get; set; }

    }
}


