using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;

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

        // GET: Admin/Movies
        public async Task<IActionResult> Index()
        {
            Movie movie = new Movie();
            ViewData["MacId"] = new SelectList(_context.Macs.Where(x=>x.IsDelete==false), "Id", "Title");
            ViewBag.categories = new SelectList(_context.Categories.Where(x => x.IsDelete == false), "Id", "Title");
            return View(movie);
        }


        // POST: Admin/Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie, int[] Lstcategories, IFormFile ful)
        {
            if (ModelState.IsValid)
            {
                foreach (int category in Lstcategories)
                {
                    movie.Lstcategories.Add(_context.Categories.FirstOrDefault(x => x.Id == category));
                }
                _context.Add(movie);
                _context.SaveChangesAsync();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/Poster",
                   movie.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                   ful.CopyToAsync(stream);
                }
                movie.Poster = movie.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                return RedirectToAction(nameof(Index));
            }
            ViewData["MacId"] = new SelectList(_context.Macs, "Id", "Id", movie.MacId);
            return View(movie);
        }



        // POST: Admin/Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie, IFormFile ful)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
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
                        _context.Update(movie);
                        await _context.SaveChangesAsync();
                    }
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
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
