using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using QuanLiRapPhim.App;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using QuanLiRapPhim.Areas.Staffs.Controllers;
using QuanLiRapPhim.SupportJSON;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace QuanLiRapPhim.Controllers
{
    public class BookticketController : Controller
    {
        [TempData]
        public string Message { get; set; }
        private readonly IdentityContext _context;
        public const string ENDPOINT = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
        public const string PARTNER_CODE = "MOMOEQWT20211117";
        public const string ACCESS_KEY = "YRqd4mAHshaRNv6b";
        public const string SECRET_KEY = "AU8eFuLyYzJBKEeTH9p8LWi1xWTlHPc4";

        public const string RETURN_URL = "https://localhost:44350/Bookticket/returnurl";
        public const string NOTIFY_URL = "https://localhost:44350/Bookticket/notifyurl";

        public BookticketController(IdentityContext context)
        {
            _context = context;
        }


        [AuthorizeRoles("User")]
        public IActionResult Index(int id)
        {
            var deleted = _context.ShowTimes.Include(x => x.Room).Where(x => x.IsDelete)
                .Where(x => x.Room.IsDelete)
                .Any(x => x.Id == id);
            if (id <= 0 || deleted)
            {
                return NotFound();
            }
            var St = _context.ShowTimes.Find(id);

            return View(St);
        }

        public class order : STime
        {
            [JsonProperty("orderInfo")]
            public string orderInfo { get; set; }
            [JsonProperty("amout")]
            public long amout { get; set; }
            [JsonProperty("isbill")]
            public bool isbill { get; set; }
            [JsonProperty("billid")]
            public int billid { get; set; }
        }
        
        public IActionResult Thanhtoan(order o)
        {
            string ORDER_ID = Guid.NewGuid().ToString();
            string REQUEST_ID = Guid.NewGuid().ToString();
            string EXTRADATA = JsonConvert.SerializeObject(o);
            string rawHash = "partnerCode=" +
                PARTNER_CODE + "&accessKey=" +
                ACCESS_KEY + "&requestId=" +
                REQUEST_ID + "&amount=" +
                o.amout + "&orderId=" +
                ORDER_ID + "&orderInfo=" +
                o.orderInfo + "&returnUrl=" +
                RETURN_URL + "&notifyUrl=" +
                NOTIFY_URL + "&extraData=" + EXTRADATA;
            MoMoSecurity crypto = new MoMoSecurity();
            string signature = crypto.signSHA256(rawHash, SECRET_KEY);
            JObject message = new JObject
            {
                {"partnerCode", PARTNER_CODE },
                {"accessKey", ACCESS_KEY },
                {"requestId", REQUEST_ID },
                {"amount", o.amout.ToString() },
                {"orderId",ORDER_ID },
                {"orderInfo", o.orderInfo },
                {"returnUrl", RETURN_URL },
                {"notifyUrl", NOTIFY_URL },
                {"requestType", "captureMoMoWallet" },
                {"signature", signature },
                {"extraData", EXTRADATA }
            };
            string url = PaymentRequest.sendPaymentRequest(ENDPOINT, message.ToString());
            JObject jmessage = JObject.Parse(url);
            string a = jmessage.GetValue("errorCode").ToString();
            if (a == "0")
                return Redirect(jmessage.GetValue("payUrl").ToString());
            return NotFound();
            //return url;
        }
        
        

        public IActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString()
                .Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = WebUtility.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string serectKey = SECRET_KEY;
            string signature = crypto.signSHA256(param, serectKey);
            string a = Request.Query["signature"].ToString();
            if (signature != a)
            {
                Message = "Thông tin Request không hợp lệ";
            }

            string err = Request.Query["errorCode"];

            try
            {
                var user = _context.Users.Find(int.Parse(User.FindFirst("Id").Value));
                var ob = JsonConvert.DeserializeObject<order>(Request.Query["extraData"].ToString());
                var obj = _context.Tickets.Where(x => !x.IsDelete)
                    .Where(x => x.IsPurchased == false)
                    .Where(x => x.Username.Equals(user.UserName))
                    .Where(x => x.ShowTimeId == int.Parse(ob.id));

                if (!err.Equals("0"))
                {
                    if (ob.isbill == false)
                    {

                        foreach (int id in ob.idS)
                        {
                            var t = obj.Where(x => x.SeatId == id).FirstOrDefault();
                            if (t != null)
                                t.IsDelete = true;
                            _context.Update(t);
                        }
                        _context.SaveChanges();
                    }
                    else
                    {
                        var b = _context.Bills.Find(ob.billid);
                        b.IsDelete = true;
                        _context.Update(b);
                        _context.SaveChanges();
                    }

                    Message = "Thanh toán thất bại";
                }
                else
                {

                    if (ob.isbill == false)
                    {
                        foreach (int id in ob.idS)
                        {
                            var t = obj.Where(x => x.SeatId == id).FirstOrDefault();
                            if (t != null)
                            {
                                t.IsPurchased = true;
                                _context.Update(t);
                            }
                        }
                        _context.SaveChanges();
                        var maill = SendEmailSuccesOder(user, obj.ToList());
                    }
                    else
                    {
                        var b = _context.Bills.Find(ob.billid);
                        b.IsPurchased = true;
                        _context.Update(b);
                        _context.SaveChanges();
                        var maill = SendEmailSuccesOderBill(user, b);
                    }
                    Message = "Thanh toán thành công";
                }
            }
            catch (Exception er)
            {
                Message = er.ToString();
            }
            return RedirectToAction("Index", "YourOrder");
        }
       
        [HttpPost]
        public IActionResult BuyTicket(STime st)
        {
            JMessage message = new JMessage();
            order a = new order();
            try
            {
                a.id = st.id;
                a.idS = st.idS;
                message.Object = st;
                var ShowTime = _context.ShowTimes.Where(x => !x.IsDelete && x.Id == int.Parse(st.id)).FirstOrDefault();
                message.Error = ShowTime == null;
                if (message.Error)
                {
                    message.Title = "Không tìm thấy lịch chiếu";
                }
                else
                {
                    message.Error = st.idS.Count() == 0;
                    if (message.Error)
                    {
                        message.Title = "Bạn chưa chọn ghế";
                    }
                    else
                    {
                        var s = _context.Tickets.Where(x => !x.IsDelete && x.ShowTimeId == ShowTime.Id);
                        foreach (int id in st.idS)
                        {
                            if (!s.Any(x => x.SeatId == id))
                            {
                                Ticket ticket = new Ticket();
                                ticket.Username = User.Identity.Name;
                                ticket.SeatId = id;
                                ticket.Price = Price(id, int.Parse(st.id));
                                ticket.PurchaseDate = DateTime.Now;
                                ticket.Name = User.FindFirst("FullNameUser").Value;
                                ticket.ShowTimeId = int.Parse(st.id);
                                ticket.IsPurchased = false;
                                a.orderInfo += "S" + id;
                                a.amout = (long)ticket.Price;
                                _context.Add(ticket);
                            }
                            else
                            {
                                message.Error = true;
                                message.Title = "Có ghế dã có người đặt";
                            }
                        }
                        if (!message.Error)
                            _context.SaveChanges();
                        a.isbill = false;
                    }
                }
            }
            catch (Exception err)
            {
                message.Error = true;
                message.Title = "Có lỗi xảy ra " + err.ToString();
            }
            if (message.Error)
            {
                return RedirectToAction("Index", "YourOrder");
            }
            return Thanhtoan(a);
        }

        public bool SendEmailSuccesOder(User user, List<Ticket> ticket)
        {
            String res = "";
            if (ticket == null)
            {
                res = "Có lỗi xảy ra! Vui lòng thông báo lại vào email này.";
            }
            else
            {
                res = "Bạn đã đặt vé có mã:";
                res += String.Join(",",ticket.Select(x => "V" + x.Id).ToList()); 
                res += " Vào ngày: " + DateTime.Now.ToShortDateString();
            }
            return Email.SendMailGoogleSmtp("giabao158357@gmail.com", user.Email, "Lấy mã đơn hàng", res).Result ?
                true : false;
        }


        [AuthorizeRoles("User")]
        //[HttpPost]
        public IActionResult BuySevice(List<BillDetail> Billdetails)
        {
            order a = new order();
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
                    IsPurchased = false,
                    IsDelete=false
                };
                a.amout =(long) bill.TotalPrice;
                _context.Add(bill);
                _context.SaveChanges();
                User u = _context.Users.FirstOrDefault(x => x.UserName == bill.Username);

                a.orderInfo = "HD" + bill.Id;
                a.isbill = true;
                a.billid = bill.Id;
            }
            catch (Exception er)
            {
                Message += "Lỗi";
                return RedirectToAction(nameof(Index));
            }

            return Thanhtoan(a);
        }

        public bool SendEmailSuccesOderBill(User user, Bill bill)
        {
            String res = "";
            if (bill == null)
            {
                res = "Có lỗi xảy ra! Vui lòng thông báo lại vào email này.";
            }
            else
            {
                    res = "Bạn đã đặt hóa đơn có mã: HD" + bill.Id + " Vào ngày: " + bill.Date.Date;
            }
            return Email.SendMailGoogleSmtp("0306181113@caothang.edu.vn", user.Email, "Lấy mã đơn hàng", res).Result ?
                true : false;
        }


        [AuthorizeRoles("User,Staff,Manager,Admin")]
        public JsonResult getRoomByIdShowtime(int id)
        {
            JMessage jMessage = new JMessage();
            jMessage.ID = id;
            bool lichchieuquathoigian = _context.ShowTimes.Where(x => x.IsDelete == true
            || x.DateTime.AddHours(x.startTime.Hour).AddMinutes(x.startTime.Minute)
            .CompareTo(DateTime.Now.AddMinutes(x.Movie.Time)) < 0).Any(x => x.Id == id);

            jMessage.Error = id <= 0 || _context.ShowTimes.Find(id) == null || lichchieuquathoigian;
            if (jMessage.Error == true)
            {
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
                                       time = sh.startTime.ToShortTimeString(),
                                       date = sh.DateTime.ToShortDateString(),
                                       r.Name,
                                       mv.Title,
                                       mv.Poster,
                                       User = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).FullName
                                   }).FirstOrDefault();
            }
            return Json(jMessage);
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

        [AuthorizeRoles("User,Staff,Admin")]
        public async Task<JsonResult> getListSeatOfShowtime(int id)
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
                             orderby se.X, se.Y
                             select new
                             {
                                 se.Id,
                                 se.X,
                                 se.Y,
                                 se.Status,
                             }).ToList().GroupBy(x => x.X).Select(x => new {
                                 name = x.Key,
                                 arr = x.ToList().Select(x => new
                                 {
                                     x.X,
                                     x.Y,
                                     x.Id,
                                     x.Status,
                                     Price = Price(x.Id, id)
                                 })
                             });
                jMessage.Object = seats;
            }
            return Json(jMessage);
        }

        [AuthorizeRoles("User,Staff,Admin")]
        public JsonResult DsGheDaDat(int id)
        {
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
                var seats = (from t in _context.Tickets.Where(x => !x.IsDelete)
                             join sh in _context.ShowTimes
                             on t.ShowTimeId equals sh.Id
                             where sh.Id == id
                             select t.SeatId).ToList();
                jMessage.Object = seats;
            }
            return Json(jMessage);
        }
    }
}

