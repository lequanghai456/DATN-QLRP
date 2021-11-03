using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLiRapPhim.Areas.Admin.Data;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [AuthorizeRoles("Admin,Manager")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
