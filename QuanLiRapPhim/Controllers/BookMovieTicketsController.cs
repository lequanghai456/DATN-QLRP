using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class BookMovieTicketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
