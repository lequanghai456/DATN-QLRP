using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuanLiRapPhim.App;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using QuanLiRapPhim.Models;
using QuanLiRapPhim.SupportJSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IdentityContext _context;

        public HomeController(ILogger<HomeController> logger, IdentityContext context)
        {
            _logger = logger;
            _context = context;
        }
        public String SendEmail(String email)
        {
            return Email.SendMailGoogleSmtp("giabao158357@gmail.com", email, "TestEmail","<h1>Cho Hai</h1>").Result
                ? "true"
                : "false";
        }
        public JsonResult GetJSON()
        {
            var a = _context.BillDetails.Where(x => x.BillId == 1);
            return Json(a);
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public JsonResult GetMovieSelection()
        {
            JMessage jmess = new JMessage();
            try
            {
                var Object = _context.Movies.Where(x => !x.IsDelete).Take(7).ToList();
                jmess.Error = Object.Count == 0;
                if (jmess.Error)
                {
                    jmess.Title = "Không có phim nào";
                }
                else
                {
                    jmess.Object = Object;
                }
            }catch(Exception ex)
            {
                jmess.Error = true;
                jmess.Title = "Có lỗi xảy ra";
            }
            return Json(jmess);
        }
    }
}
