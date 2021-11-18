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

        public JsonResult GetComment(int id,int numoc)
        {
            JMessage message = new JMessage();
            try
            {
                var a = _context.Comments.Include(x=>x.User).Where(X=>X.MovieId==id&&X.Parent==0).Where(x => x.IsDelete == false).Select(x=>new{
                    x.Id,
                    x.User.FullName,
                    x.User.Img,
                    x.SubmittedDate,
                    x.Content,
                    ListCmt=_context.Comments.Where(x1=>x1.Parent==x.Id).Where(x => x.IsDelete == false).Select(x2 => new
                    {
                        x2.Id,
                        x2.User.FullName,
                        x2.User.Img,
                        x2.SubmittedDate,
                        x2.Content
                    }).OrderByDescending(x2 => x2.SubmittedDate).ToList()
                }).OrderByDescending(x=>x.SubmittedDate);
                message.ID = a.Count();

                message.Object = a.Take(numoc);
                message.Error = false;
            }
            catch (Exception err)
            {
                message.Error = true;
                message.Title = "Có lỗi xảy ra";
            }
            return Json(message);
        }
        public class comment
        {
            [Required]
            public int id { get; set; }
            [Required]
            public String cmt { get; set; }
            public int parentid { get; set; }
        }

        [HttpPost]
        [AuthorizeRoles("User")]
        public JsonResult postComment([FromBody]comment cm)
        {
            JMessage message = new JMessage();
            try
            {
                if (!ModelState.IsValid)
                {
                    message.Error = true;
                    message.Title = "Chưa đủ dữ liệu";
                    message.Object = cm;
                }
                else
                {
                    Comment cmt = new Comment();
                    cmt.MovieId = cm.id;
                    cmt.UserId = int.Parse(User.FindFirst("Id").Value);
                    cmt.Content = cm.cmt;
                    cmt.SubmittedDate = DateTime.Now;
                    cmt.Parent = (int)cm.parentid;
                    _context.Add(cmt);
                    _context.SaveChanges();
                    message.Error = false;
                    message.Object = cmt;
                }
            }
            catch (Exception err)
            {
                message.Error = true;
                message.Title = "Có lỗi xảy ra";
            }
            return Json(message);
        }
        public IActionResult Index()
        {
            return View();
        }
        [AuthorizeRoles("")]
        public JsonResult Rate(int id, int star)
        {
            var movie = _context.Movies.Include(x => x.RatedUsers).Where(x => x.Id == id).Where(x => x.IsDelete == false).First();
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
                        var us = _context.Users.Where(x => x.UserName == User.Identity.Name).Where(x => x.IsDelete == false).FirstOrDefault();
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
                    Lstcategories=x.Lstcategories.Select(x=>x.Title),
                    comment = User.Identity.Name != null&&User.FindFirst("Role").Value.Contains("User"),
                    IsRated = User.Identity.Name == null ||!User.FindFirst("Role").Value.Contains("User")
                    || x.RatedUsers.Where(x => x.UserName == User.Identity.Name).Count() > 0,
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
                    comment= User.Identity.Name != null || User.FindFirst("Role").Value.Contains("User"),
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
        public async Task<JsonResult> GetMovie(String search)
        {
            return Json(_context.Movies.Where(x => x.IsDelete == false).Where(x=>search==null||x.Title.Contains(search)).ToList());
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
