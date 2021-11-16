using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using QuanLiRapPhim.App;
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
        [TempData]
        public string Message { get; set; }
        private readonly IdentityContext _context;
        
        public BookticketController(IdentityContext context)
        {
            _context = context;
        }
        [AuthorizeRoles("User")]
        public IActionResult Index(int id)
        {
            var deleted = _context.ShowTimes.Include(x=>x.Room).Where(x=>x.IsDelete)
                .Where(x=>x.Room.IsDelete)
                .Any(x => x.Id == id);
            if (id <= 0 || deleted)
            {
                return NotFound();
            }
            var St = _context.ShowTimes.Find(id);

            return View(St);
        }
        //public IActionResult Thanhtoan()
        //{
        //    string ENDPOINT = "https://payment.momo.vn/gw_payment/transactionProcessor";
        //    string PARTNER_CODE = "MOMOQ3N820211116";
        //    string ACCESS_KEY = "izZSheQQlSA72oKw";
        //    string SECRET_KEY = "z4E3VQKqbTFdwmHDGPOAmyqEvIdH0erl";
        //    string ORDER_ID = Guid.NewGuid().ToString();
        //    string REQUEST_ID = Guid.NewGuid().ToString();
        //    string EXTRADATA = "";
        //    string rawHash = "partnerCode=" +
        //        PARTNER_CODE + "&accessKey=" +
        //        ACCESS_KEY + "&requestId=" +
        //        REQUEST_ID + "&amount=" + "2000" + "&orderId=" +
        //        ORDER_ID + "&orderInfo=" + "HD" + "&returnUrl=" + "https://localhost:44350/#!/" + "&notifyUrl=" + "true" +
        //        "&extraData=" + EXTRADATA;
        //    MoMoSecurity crypto = new MoMoSecurity();
        //    string signature = crypto.signSHA256(rawHash, SECRET_KEY);
        //    JObject message = new JObject
        //    {
        //        {"partnerCode", PARTNER_CODE },
        //        {"accessKey", ACCESS_KEY },
        //        {"requestId", REQUEST_ID },
        //        {"amount", "2000" },
        //        {"orderId",ORDER_ID },
        //        {"orderInfo", "HD" },
        //        {"returnUrl", "https://localhost:44350/#!/" },
        //        {"notifyUrl", "true" },
        //        {"requestType", "captureMoMoWallet" },
        //        {"signature", signature }
        //    };
        //    string url = PaymentRequest.sendPaymentRequest(ENDPOINT, message.ToString());
        //    JObject jmessage = JObject.Parse(url);
        //    return Redirect(jmessage.GetValue("payURL").ToString());
        //    //return url;
        //}

        [HttpPost]
        public IActionResult BookTicket(Ticket ticket)
        {

            try
            {
                string ENDPOINT = "https://payment.momo.vn/gw_payment/transactionProcessor";
                string PARTNER_CODE = "MOMOQ3N820211116";
                string ACCESS_KEY = "izZSheQQlSA72oKw";
                string SECRET_KEY = "z4E3VQKqbTFdwmHDGPOAmyqEvIdH0erl";
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

