using AutomatedInvoiceGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using QuanLiRapPhim.SupportJSON;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLiRapPhim.Controllers
{
    public class MovieReviewController : Controller
    {
        private readonly IdentityContext _context;

        public MovieReviewController(IdentityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMovieByName(String Name)
        {
            JMessage jMessage = new JMessage();
            var movie = (from m in  _context.Movies 
                         where (m.Title == Name)
                         select new {
                            m.Id,
                            m.Title,
                            m.Time,
                            Lstcategories=from c in m.Lstcategories select c.Title ,
                            m.Describe,
                            m.Mac,
                            m.Poster,
                            m.Trailer,
                            m.TotalRating,
                            m.TotalReviewers
                         }).FirstOrDefault();
            jMessage.Error = movie == null;
            if (!jMessage.Error)
            {
                jMessage.Object = movie;
            }
            else
            {
                jMessage.Title = "<h1>Không có phim "+Name+"</h1>";
            }
            return Json(jMessage);
        }
        public JsonResult GetItem(int id)
        {
            return Json(_context.Movies.FirstOrDefault(movie=>movie.Id==id));
        }
        public async Task<JsonResult> GetMovie(int year, int idCate)
        {

            return Json(_context.Movies.Where(x=>x.IsDelete==false).ToList());
        }
        [HttpGet]
        public JsonResult GetListShowTime()
        {
            List<ShowTime> data = new List<ShowTime>();
            for (int i = 0; i < 3; i++) {
                ShowTime item = new ShowTime();
                item.DateTime = DateTime.Now;
                item.Id = i;
                data.Add(item);
            }
            return Json(data);
        }
    }
    class movie
    {
        public int Id { get; set; }
        public String Title{ get; set; }
        public String Trailer{ get; set; }
        public String Poster{ get; set; }
        public String Describe{ get; set; }
        public int Status{ get; set; }
        public int Time{ get; set; }
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }
    }
}
