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

    public class KasaHareketleriController : Controller
    {
        private readonly DepoStokBitirmeProjesiContext _context;

        public KasaHareketleriController(DepoStokBitirmeProjesiContext context)
        {
            _context = context;
        }

        // GET: KasaHareketleri
        public async Task<IActionResult> Index()
        {
            

            var depoStokBitirmeProjesiContext = _context.KasaHareketleri.Include(k => k.Urun);
            return View(await depoStokBitirmeProjesiContext.ToListAsync());
        }

        // GET: KasaHareketleri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KasaHareketleri == null)
            {
                return NotFound();
            }

            var kasaHareketleri = await _context.KasaHareketleri
                .Include(k => k.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kasaHareketleri == null)
            {
                return NotFound();
            }

            return View(kasaHareketleri);
        }

        // GET: KasaHareketleri/Create
        public IActionResult Create()
        {
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Aciklama");
            return View();
        }

        // POST: KasaHareketleri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AlisFiyati,SatisFiyati,Kar,IslemTuru,UrunId")] KasaHareketleri kasaHareketleri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kasaHareketleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Aciklama", kasaHareketleri.UrunId);
            return View(kasaHareketleri);
        }

        // GET: KasaHareketleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KasaHareketleri == null)
            {
                return NotFound();
            }

            var kasaHareketleri = await _context.KasaHareketleri.FindAsync(id);
            if (kasaHareketleri == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Aciklama", kasaHareketleri.UrunId);
            return View(kasaHareketleri);
        }

        // POST: KasaHareketleri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AlisFiyati,SatisFiyati,Kar,IslemTuru,UrunId")] KasaHareketleri kasaHareketleri)
        {
            if (id != kasaHareketleri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kasaHareketleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KasaHareketleriExists(kasaHareketleri.Id))
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
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Aciklama", kasaHareketleri.UrunId);
            return View(kasaHareketleri);
        }

        // GET: KasaHareketleri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KasaHareketleri == null)
            {
                return NotFound();
            }

            var kasaHareketleri = await _context.KasaHareketleri
                .Include(k => k.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kasaHareketleri == null)
            {
                return NotFound();
            }

            return View(kasaHareketleri);
        }

        // POST: KasaHareketleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KasaHareketleri == null)
            {
                return Problem("Entity set 'DepoStokBitirmeProjesiContext.KasaHareketleri'  is null.");
            }
            var kasaHareketleri = await _context.KasaHareketleri.FindAsync(id);
            if (kasaHareketleri != null)
            {
                _context.KasaHareketleri.Remove(kasaHareketleri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KasaHareketleriExists(int id)
        {
          return (_context.KasaHareketleri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
