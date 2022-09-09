using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStokBitirmeProjesi.Entities;
using DepoStokBitirmeProjesi.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DepoStokBitirmeProjesi.Controllers
{
    [Authorize(Roles = "Yönetici")]

    public class DepolarController : Controller
    {
        private readonly DepoStokBitirmeProjesiContext _context;

        public DepolarController(DepoStokBitirmeProjesiContext context)
        {
            _context = context;
        }

        // GET: Depolar
        public async Task<IActionResult> Index()
        {
            var depoStokBitirmeProjesiContext = _context.Depolar.Include(d => d.Bina);
            return View(await depoStokBitirmeProjesiContext.ToListAsync());
        }

        // GET: Depolar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Depolar == null)
            {
                return NotFound();
            }

            var depolar = await _context.Depolar
                .Include(d => d.Bina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depolar == null)
            {
                return NotFound();
            }

            return View(depolar);
        }

        // GET: Depolar/Create
        public IActionResult Create()
        {
            ViewData["BinaId"] = new SelectList(_context.Binalar, "Id", "BinaAdi");
            return View();
        }

        // POST: Depolar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepoAdi,BinaId")] Depolar depolar)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(depolar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BinaId"] = new SelectList(_context.Binalar, "Id", "BinaAdi", depolar.BinaId);
            return View(depolar);
        }

        // GET: Depolar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Depolar == null)
            {
                return NotFound();
            }

            var depolar = await _context.Depolar.FindAsync(id);
            if (depolar == null)
            {
                return NotFound();
            }
            ViewData["BinaId"] = new SelectList(_context.Binalar, "Id", "BinaAdi", depolar.BinaId);
            return View(depolar);
        }

        // POST: Depolar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepoAdi,BinaId")] Depolar depolar)
        {
            if (id != depolar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depolar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepolarExists(depolar.Id))
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
            ViewData["BinaId"] = new SelectList(_context.Binalar, "Id", "BinaAdi", depolar.BinaId);
            return View(depolar);
        }

        // GET: Depolar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Depolar == null)
            {
                return NotFound();
            }

            var depolar = await _context.Depolar
                .Include(d => d.Bina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depolar == null)
            {
                return NotFound();
            }

            return View(depolar);
        }

        // POST: Depolar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Depolar == null)
            {
                return Problem("Entity set 'DepoStokBitirmeProjesiContext.Depolar'  is null.");
            }
            var depolar = await _context.Depolar.FindAsync(id);
            if (depolar != null)
            {
                _context.Depolar.Remove(depolar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepolarExists(int id)
        {
          return (_context.Depolar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
