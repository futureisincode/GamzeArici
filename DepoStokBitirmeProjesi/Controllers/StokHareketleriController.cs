using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStokBitirmeProjesi.Entities;
using DepoStokBitirmeProjesi.Identity;
using DepoStokBitirmeProjesi.Services;
using Microsoft.AspNetCore.Authorization;

namespace DepoStokBitirmeProjesi.Controllers
{
    [Authorize(Roles = "Yönetici")]
    public class StokHareketleriController : Controller
    {
        private readonly DepoStokBitirmeProjesiContext _context;

        public StokHareketleriController(DepoStokBitirmeProjesiContext context)
        {
            _context = context;
        }

        // GET: StokHareketleri
        public async Task<IActionResult> Index()
        {
            var depoStokBitirmeProjesiContext = _context.StokHareketleri.Include(s => s.Depo).Include(s => s.Urun);
            return View(await depoStokBitirmeProjesiContext.ToListAsync());
        }

        // GET: StokHareketleri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StokHareketleri == null)
            {
                return NotFound();
            }

            var stokHareketleri = await _context.StokHareketleri
                .Include(s => s.Depo)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stokHareketleri == null)
            {
                return NotFound();
            }

            return View(stokHareketleri);
        }

        // GET: StokHareketleri/Create
        public IActionResult Create()
        {
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "DepoAdi");
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Aciklama");
            return View();
        }

        // POST: StokHareketleri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adet,Fiyat,Tarih,IslemTuru,DepoId,UrunId")] StokHareketleri stokHareketleri)
        {
            if (ModelState.IsValid)
            {

                //Alış  1
                var sonuc = stokHareketleri.Adet * stokHareketleri.Fiyat;

                stokHareketleri.ToplamFiyat = sonuc;

                _context.Add(stokHareketleri);
                await _context.SaveChangesAsync();


                if (stokHareketleri.IslemTuru.ToString() == "1")
                {
                   
                    var urunId = stokHareketleri.UrunId;

                    KasaHareketleri kasaHareketleri = new KasaHareketleri
                    {
                        AlisFiyati = sonuc,
                        IslemTuru = 1,
                        UrunId = Convert.ToInt32(stokHareketleri.UrunId),
                    };

                    _context.KasaHareketleri.Add(kasaHareketleri);
                    await _context.SaveChangesAsync();
                }
                else
                {

                    //Satış  0
                    KasaHareketleri kasaHareketleri = new KasaHareketleri
                    {
                        SatisFiyati = sonuc,
                        IslemTuru = 0,
                        UrunId = Convert.ToInt32(stokHareketleri.UrunId),
                    };

                    _context.KasaHareketleri.Add(kasaHareketleri);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "DepoAdi", stokHareketleri.DepoId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Aciklama", stokHareketleri.UrunId);
            return View(stokHareketleri);
        }

        // GET: StokHareketleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StokHareketleri == null)
            {
                return NotFound();
            }

            var stokHareketleri = await _context.StokHareketleri.FindAsync(id);
            if (stokHareketleri == null)
            {
                return NotFound();
            }
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "DepoAdi", stokHareketleri.DepoId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Aciklama", stokHareketleri.UrunId);
            return View(stokHareketleri);
        }

        // POST: StokHareketleri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adet,Fiyat,Tarih,IslemTuru,DepoId,UrunId")] StokHareketleri stokHareketleri)
        {
            if (id != stokHareketleri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                

                try
                {
                    _context.Update(stokHareketleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StokHareketleriExists(stokHareketleri.Id))
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
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "DepoAdi", stokHareketleri.DepoId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Aciklama", stokHareketleri.UrunId);
            return View(stokHareketleri);
        }

        // GET: StokHareketleri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StokHareketleri == null)
            {
                return NotFound();
            }

            var stokHareketleri = await _context.StokHareketleri
                .Include(s => s.Depo)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stokHareketleri == null)
            {
                return NotFound();
            }

            return View(stokHareketleri);
        }

        // POST: StokHareketleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StokHareketleri == null)
            {
                return Problem("Entity set 'DepoStokBitirmeProjesiContext.StokHareketleri'  is null.");
            }
            var stokHareketleri = await _context.StokHareketleri.FindAsync(id);
            if (stokHareketleri != null)
            {
                _context.StokHareketleri.Remove(stokHareketleri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StokHareketleriExists(int id)
        {
          return (_context.StokHareketleri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
