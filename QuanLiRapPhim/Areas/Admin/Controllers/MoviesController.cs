﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;
using QuanLiRapPhim.SupportJSON;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesController : Controller
    {
        private readonly IdentityContext _context;

        public MoviesController(IdentityContext context)
        {
            _context = context;
        }
        public class JTableModelCustom : JTableModel
        {
            public string Title { get; set; }
        }
        
        [HttpGet]
        public async Task<String> JtableMovieModel(JTableModelCustom jTablePara)
        {
            
            int intBegin = (jTablePara.CurrentPage - 1) * jTablePara.Length;
            var query = _context.Movies.Include(a=>a.Mac).Include(a=>a.Lstcategories).Where(x => x.IsDelete == false && (String.IsNullOrEmpty(jTablePara.Title) || x.Title.Contains(jTablePara.Title)));

            
            int count = query.Count();
            var data = query.AsQueryable().Select(x => new { x.Id, x.Title,category = JsonConvert.SerializeObject(x.Lstcategories.ToList<Category>()),x.Describe, mac=x.Mac.Title,x.Time,x.TotalRating,x.TotalReviewers,x.Trailer, x.Poster })
                .Skip(intBegin)
                .Take(jTablePara.Length);

            var jdata = JTableHelper.JObjectTable(data.ToList(), jTablePara.Draw, count, "Id", "Title","category","Describe","mac","Time","Trailer","Poster");
            return JsonConvert.SerializeObject(jdata);
        }
        // GET: Admin/Movies
        public async Task<IActionResult> Index(int? id)
        {
            Movie movie = new Movie();
            if (id != null)
            {
                movie = _context.Movies.Include(x=>x.Lstcategories).FirstOrDefault(x => x.Id == id);
                ViewData["Poster"] = movie.Poster;
                ViewData["Trailer"] = movie.Trailer;
                ViewData["MacId"] = new SelectList(_context.Macs.Where(x => x.IsDelete == false), "Id", "Title");
                ViewBag.categories = new SelectList(_context.Categories.Where(x => x.IsDelete == false && !x.lstMovie.Contains(movie)), "Id", "Title");
                return View(movie);

            }
            ViewData["MacId"] = new SelectList(_context.Macs.Where(x=>x.IsDelete==false), "Id", "Title");
           
            ViewBag.categories = new SelectList(_context.Categories.Where(x => x.IsDelete == false), "Id", "Title");
            
            return View(movie);
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    Movie movie = new Movie();
        //    movie = _context.Movies.Include(a=>a.Mac).Include(a=>a.Lstcategories).Single(x => x.Id == id);
        //    movie.Lstcategories = _context.Categories.Where(x => x.Id == id).ToList();
        //    return View(movie);
        //}

        // POST: Admin/Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie, int[] Lstcategories, IFormFile ful, IFormFile video)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    movie.Poster = "Noimage.png";
                    foreach (int category in Lstcategories)
                    {
                        movie.Lstcategories.Add(_context.Categories.FirstOrDefault(x => x.Id == category));
                    }
                    _context.Add(movie);
                    await _context.SaveChangesAsync();
                    if (ful != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/Poster",
                           movie.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await ful.CopyToAsync(stream);

                        }
                        movie.Poster = movie.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                    }
                    if (video != null)
                    {
                        var pathVideo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/Trailer",
                           movie.Id + "." + video.FileName.Split(".")[video.FileName.Split(".").Length - 1]);

                        using (var stream = new FileStream(pathVideo, FileMode.Create))
                        {
                            await video.CopyToAsync(stream);
                        }

                        movie.Trailer = movie.Id + "." + video.FileName.Split(".")[video.FileName.Split(".").Length - 1];
                    }
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["MacId"] = new SelectList(_context.Macs, "Id", "Id", movie.MacId);
                return View(movie);
            }
            catch (Exception err)
            {
                return View(movie);
            }
        }



        // POST: Admin/Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie, IFormFile ful, IFormFile video, int[] Lstcategories)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (ful != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/Poster", movie.Poster);
                        System.IO.File.Delete(path);

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/Poster",
                        movie.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await ful.CopyToAsync(stream);
                        }
                        movie.Poster = movie.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                    }
                    if (video != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/Trailer", movie.Trailer);
                        System.IO.File.Delete(path);

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/Trailer",
                        movie.Id + "." + video.FileName.Split(".")[video.FileName.Split(".").Length - 1]);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await video.CopyToAsync(stream);
                        }
                        movie.Trailer = movie.Id + "." + video.FileName.Split(".")[video.FileName.Split(".").Length - 1];
                    }
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                    movie = _context.Movies.Include(x => x.Lstcategories).FirstOrDefault(x => x.Id == id);
                    movie.Lstcategories.Clear();
                    foreach (int category in Lstcategories)
                    {
                        movie.Lstcategories.Add(_context.Categories.FirstOrDefault(x => x.Id == category));
                    }
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MacId"] = new SelectList(_context.Macs, "Id", "Id", movie.MacId);
            return View(movie);
        }
         
        public JsonResult DeleteMovie(int? id)
        {
            try
            {
                Movie movie = new Movie();
                movie = _context.Movies.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
                if (movie == null)
                {

                    return Json("Fail");
                }
                movie.IsDelete = true;
                _context.Update(movie);
                if(_context.ShowTimes.FirstOrDefault(x=>x.MovieId == id && x.DateTime.Date.CompareTo(DateTime.Now.Date) > 0) != null)
                {
                    return Json("Phim này vẫn còn lịch chiếu");
                }
                _context.SaveChangesAsync();
                return Json("Xóa phim thành công");
            }
            catch (Exception err)
            {

                return Json("Xóa phim thất bại");
            }
        }
        [TempData]
        public string Message { get; set; }
        public JsonResult DeleteMovieList(String Listid)
        {
            try
            {
                String[] List = Listid.Split(',');
                Movie movie = new Movie();
                foreach (String id in List)
                {
                    movie = _context.Movies.FirstOrDefault(x => x.Id == int.Parse(id) && x.IsDelete == false);
                    movie.IsDelete = true;
                    _context.Update(movie);

                }

                _context.SaveChangesAsync();
                return Json("Xóa phim thành công");
            }
            catch (Exception er)
            {

                return Json("Xóa phim thất bại");
            }



        }
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
