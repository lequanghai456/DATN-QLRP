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

        public class order:STime
        {
            public string orderInfo { get; set; }
            public long amout { get; set; }
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
                NOTIFY_URL+ "&extraData=" + EXTRADATA;
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
            if (a=="0")
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
            if (!Request.Query["errorCode"].Equals("0"))
            {
                Message = "Thanh toán thất bại";
            }
            else
            {
                Message = "Thanh toán thành công";
            }
            return RedirectToAction("Index", "YourOrder");
        }
        [HttpPost("notifyurl")]
        public JsonResult NotifyUrl()
        {
            string param = "partner_code=" + Request.Query["partner_code"] +
            "&access_key=" + Request.Query["access_key"] +
            "&amount=" + Request.Query["amount"] +
            "&order_id=" + Request.Query["order_id"] +
            "&order_info=" + Request.Query["order_info"] +
            "&order_type=" + Request.Query["order_type"] +
            "&transaction_id=" + Request.Query["transaction_id"] +
            "&message=" + Request.Query["message"] +
            "&response_time=" + Request.Query["response_time"] +
            "&status_code=" + Request.Query["status_code"]+
            "&extraData=" + Request.Query["extraData"];

            param = WebUtility.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string serectKey = SECRET_KEY;
            string status_code = Request.Query["status_code"].ToString();

            string signature = crypto.signSHA256(param, serectKey);

            string a = Request.Query["signature"].ToString();
            if (signature != a)
            {
                return Json(a);
            }

            if ((status_code != "0"))
            {
                

            }
            else
            {
                try
                {
                    order ob = (order)JsonConvert.DeserializeObject(Request.Query["extraData"].ToString());
                    var obj = _context.Tickets.Where(x => !x.IsDelete)
                        .Where(x => x.Username.Equals(User.FindFirst("Username")))
                        .Where(x => x.ShowTimeId == int.Parse(ob.id))
                        .Where(x => ob.idS.Any(id => id == x.SeatId));
                    foreach(Ticket t in obj)
                    {
                        t.IsDelete = true;
                        _context.Update(t);
                    }
                    _context.SaveChanges();
                }
                catch
                {

                }
            }
            return Json(a);
            //return new Newtonsoft.Json.JsonSerializerSettings();
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
                        var s = _context.Tickets.Where(x => !x.IsDelete&&x.ShowTimeId==ShowTime.Id);
                        foreach (int id in st.idS)
                        {
                            if (!s.Any(x=>x.SeatId==id))
                            {
                                Ticket ticket = new Ticket();
                                ticket.Username = User.Identity.Name;
                                ticket.SeatId = id;
                                ticket.Price = Price(id, int.Parse(st.id));
                                ticket.PurchaseDate = DateTime.Now;
                                ticket.Name = "--------";
                                ticket.ShowTimeId = int.Parse(st.id);
                                ticket.IsPurchased = false;
                                a.orderInfo += "S" + id;
                                a.amout = 1000;//(long)ticket.Price;
                                _context.Add(ticket);
                            }
                            else
                            {
                                message.Error = true;
                                message.Title = "Có ghế dã có người đặt";
                            }
                        }
                        if(!message.Error)
                        _context.SaveChanges();
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
                return RedirectToAction("Index","YourOrder");
            }
            return Thanhtoan(a);
        }


        [HttpPost]
        public IActionResult BookTicket(Ticket ticket)
        {
            try
            {
                ticket.Username = User.Identity.Name;
                ticket.Price = Price((int)ticket.SeatId, (int)ticket.ShowTimeId);
                ticket.PurchaseDate = DateTime.Now;
                
                var isbooked = (from T in _context.Tickets
                                where T.SeatId == ticket.SeatId && T.IsDelete == false
                                select T).Count() > 0;

                if (ticket.SeatId == null || isbooked || !ModelState.IsValid)
                {
                    Message = "Không tìm thấy ghế";
                    return RedirectToAction("Index/" + ticket.ShowTimeId.Value);
                }
                _context.Add(ticket);
                _context.SaveChanges();
                Message = "Bạn đã mua thành công";

                var a = SendEmailSuccesOder(_context.Users.Where(x => x.UserName == ticket.Username)
                    .FirstOrDefault(), ticket);
            }
            catch (Exception er)
            {
                Message= "Lỗi";
                return RedirectToAction("Index/" + ticket.ShowTimeId.Value);
            }
            return RedirectToAction("Index", "YourOrder");
        }
        
        public bool SendEmailSuccesOder(User user, Ticket ticket)
        {
            String res = "";
            if (ticket == null)
            {
                res = "Có lỗi xảy ra! Vui lòng thông báo lại vào email này.";
            }
            else
            {
                res = "Bạn đã đặt vé có mã: V" + ticket.Id + " Vào ngày: " + ticket.PurchaseDate.Date;
            }
            return Email.SendMailGoogleSmtp("giabao158357@gmail.com", user.Email, "Lấy mã đơn hàng", res).Result ?
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

            jMessage.Error = id <= 0 || _context.ShowTimes.Find(id) == null|| lichchieuquathoigian;
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
                                   orderby se.X,se.Y
                                   select new
                                   {
                                       se.Id,
                                       se.X,
                                       se.Y,
                                       se.Status,
                                   }).ToList().GroupBy(x => x.X).Select(x => new { name = x.Key,arr=x.ToList().Select(x=> new 
                                   {
                                       x.X,
                                       x.Y,
                                       x.Id,
                                       x.Status,
                                       Price=Price(x.Id, id)
                                   }) });
                jMessage.Object = seats;
            }
            return Json(jMessage);
        }

        [AuthorizeRoles("User,Staff,Admin")]
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

