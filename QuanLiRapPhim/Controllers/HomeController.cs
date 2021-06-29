using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLiRapPhim.App;
using QuanLiRapPhim.Models;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public String SendEmail(String email)
        {
            return Email.SendMailGoogleSmtp("giabao158357@gmail.com", email, "TestEmail","<h1>Cho Hai</h1>").Result
                ? "true"
                : "false";
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
    }
}
