using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLiRapPhim.Areas.Admin.Data;
using QuanLiRapPhim.Areas.Admin.Models;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MacsController : Controller
    {
        private readonly IdentityContext _context;

        public MacsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Admin/Macs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Macs.ToListAsync());
        }

        // GET: Admin/Macs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mac = await _context.Macs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mac == null)
            {
                return NotFound();
            }

            return View(mac);
        }

        // GET: Admin/Macs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Macs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Age,Describe,IsDelete")] Mac mac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mac);
        }

        // GET: Admin/Macs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mac = await _context.Macs.FindAsync(id);
            if (mac == null)
            {
                return NotFound();
            }
            return View(mac);
        }

        // POST: Admin/Macs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Age,Describe,IsDelete")] Mac mac)
        {
            if (id != mac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MacExists(mac.Id))
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
            return View(mac);
        }

        // GET: Admin/Macs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mac = await _context.Macs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mac == null)
            {
                return NotFound();
            }

            return View(mac);
        }

        // POST: Admin/Macs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mac = await _context.Macs.FindAsync(id);
            _context.Macs.Remove(mac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MacExists(int id)
        {
            return _context.Macs.Any(e => e.Id == id);
        }
    }
}
