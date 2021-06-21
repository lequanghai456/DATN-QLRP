using AutomatedInvoiceGenerator.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }
        List<movie> movies;
        public MovieReviewController()
        {
            movies = new List<movie>();
            for (int i = 1; i < 9; i++)
            {
                movie movie = new movie
                {
                    Id = i,
                    Title = "Maleficient " +i,
                    Poster = "thumb-" + i + ".jpg",
                    Describe = "Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.",
                    Status = 0,
                    Time=90,
                    Date=DateTime.Now
                };
                movies.Add(movie);
            }
        }

        [HttpGet]
        public JsonResult GetMovieByName(String Name)
        {
            JMessage jMessage = new JMessage();
            var movie = movies.Where(x => x.Title == Name).FirstOrDefault();
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
            return Json(movies.FirstOrDefault(movie=>movie.Id==id));
        }
        public async Task<JsonResult> GetMovie()
        {            
            return Json(movies);
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
