using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Areas.Staffs.Controllers
{
    public class HomeController : Controller
    {
        [Area("Staffs")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
