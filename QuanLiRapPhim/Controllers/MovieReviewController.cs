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
        [AuthorizeRoles("")]
        public JsonResult Rate(int id, int star)
        {
            var movie = _context.Movies.Include(x => x.RatedUsers).Where(x => x.Id == id).First();
            JMessage jMessage = new JMessage();
            if (User.Identity.Name != null)
            {
                jMessage.Error = star < 0 || star > 5;

                if (!jMessage.Error)
                {
                    jMessage.Error = movie == null || movie.IsDelete;
                    if (!jMessage.Error)
                    {
                        movie.TotalRating += star;
                        movie.TotalReviewers += 1;
                        var us = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                        if (us != null)
                        {
                            if (movie.RatedUsers == null)
                                movie.RatedUsers = new List<User>();
                            if (movie.RatedUsers.Where(x => x.UserName == User.Identity.Name).Count() == 0)
                            {
                                movie.RatedUsers.Add(us);
                                try
                                {
                                    _context.Update(movie);
                                    _context.SaveChanges();
                                }
                                catch (Exception er)
                                {
                                    jMessage.Error = true;
                                    jMessage.Title = "Lỗi đánh giá";
                                    return Json(jMessage);
                                }


                                jMessage.Object = "Bạn vừa đánh giá phim " + star + " sao";
                            }
                            else
                            {
                                jMessage.Error = true;
                                jMessage.Title = "Bạn đã đánh giá phim này rồi!";

                            }


                        }
                        else
                        {
                            jMessage.Error = true;
                            jMessage.Title = "Không tìm thấy người đang đăng nhập";
                        }
                    }
                    else
                    {
                        jMessage.Title = "Không tìm thấy phim";
                    }
                }
                else
                {
                    jMessage.Title = "Lỗi đánh giá";
                }
            }
            else
            {
                jMessage.Error = true;
                jMessage.Title = "Bạn chưa đăng nhập";
            }
            return Json(jMessage);
        }
        [HttpGet]
        public JsonResult GetMovieByName(String Name)
        {
            JMessage jMess = new JMessage();
            var Object = _context.Movies
                .Include(x => x.RatedUsers)
                .Include(x => x.Lstcategories)
                .Where(x => x.Title==Name).Where(x => x.IsDelete == false)
                .Select(x => new {
                    x.Id,
                    x.Title,
                    x.Time,
                    x.Status,
                    x.TotalReviewers,
                    x.TotalRating,
                    x.Poster,
                    x.Trailer,
                    x.Lstcategories,
                    IsRated = User.Identity.Name == null || x.RatedUsers.Where(x => x.UserName == User.Identity.Name).Count() > 0,
                    x.Describe
                }).First();
            jMess.Error = Object == null;
            if (jMess.Error)
            {
                jMess.Title = "Không tìm thấy phim";
            }
            else
            {
                jMess.Object = Object;
            }
            return Json(jMess);
        }

        public JsonResult GetItem(int id)
        {
            JMessage jMess = new JMessage();
            var Object = _context.Movies
                .Include(x=>x.RatedUsers)
                .Include(x=>x.Lstcategories)
                .Where(x=>x.Id==id).Where(x=>x.IsDelete==false)
                .Select(x=>new {
                    x.Id,
                    x.Title,
                    x.Time,
                    x.Status,
                    x.TotalReviewers,
                    x.TotalRating,
                    x.Poster,
                    x.Trailer,
                    x.Lstcategories,
                    IsRated=User.Identity.Name==null||x.RatedUsers.Where(x=>x.UserName==User.Identity.Name).Count()>0,
                    x.Describe
                }).First();
            jMess.Error = Object == null;
            if (jMess.Error)
            {
                jMess.Title = "Không tìm thấy phim";
            }
            else
            {
                jMess.Object = Object;
            }
            return Json(jMess);
        }
        public async Task<JsonResult> GetMovie(int year, int idCate)
        {
            return Json(_context.Movies.Where(x => x.IsDelete == false).ToList());
        }
        [HttpGet]
        public JsonResult GetListShowTime(int? idmovie, String date)
        {
            JMessage jMessage = new JMessage();
            jMessage.ID = (int)idmovie;
            try
            {
                var list = (from sh in _context.ShowTimes
                            where sh.MovieId == (int)idmovie
                            && (String.IsNullOrEmpty(date) ? sh.DateTime.Date.CompareTo(DateTime.Now) == 0 : sh.DateTime.Date.CompareTo(DateTime.Parse(date).Date) == 0)
                            && !sh.IsDelete
                            select new
                            {
                                sh.Id,
                                Time = sh.startTime.ToShortTimeString() + " - " + sh.startTime.AddMinutes(sh.Movie.Time).ToShortTimeString()
                            }
                           ).ToList();

                jMessage.Error = list.Count() <= 0;
                if (!jMessage.Error)
                {
                    jMessage.Object = list;
                }
                else
                {
                    jMessage.Title = "Không có lịch chiếu";
                }
            }
            catch (Exception er)
            {
                jMessage.Error = true;
                jMessage.Title = "Có lỗi xảy ra";
            }
            return Json(jMessage);
        }
    }
}
