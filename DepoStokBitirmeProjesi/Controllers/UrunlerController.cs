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
    public class UrunlerController : Controller
    {
        private readonly DepoStokBitirmeProjesiContext _context;
        private readonly IWebHostEnvironment _host;

        public UrunlerController(DepoStokBitirmeProjesiContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Urunler
        public async Task<IActionResult> Index()
        {
            var depoStokBitirmeProjesiContext = _context.Urunler.Include(u => u.Depo);
            return View(await depoStokBitirmeProjesiContext.ToListAsync());
        }

        // GET: Urunler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           

            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            Urunler urunler = await _context.Urunler
                .Include(u => u.Depo)
                .FirstOrDefaultAsync(m => m.Id == id);



            List<StokHareketleri> stokHareketleri = _context.StokHareketleri.Include(s=>s.Depo).Where(s => s.UrunId == id).ToList();



            if (urunler == null)
            {
                return NotFound();
            }


            StokDetayViewModel stokDetayViewModel = new StokDetayViewModel
            {
                Urunler = urunler,
                StokHareketleri = stokHareketleri

            };
            return View(stokDetayViewModel);
        }

        // GET: Urunler/Create
        public IActionResult Create()
        {
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "DepoAdi");
            return View();
        }

        // POST: Urunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UrunAdi,ResimDosya,UrunStokAdeti,AlisFiyati,SatisFiyati,Aciklama,DepoId")] Urunler urunler)
        {
            if (!ModelState.IsValid)
            {


                // wwwroot/YuklenenResimler klasörüne resim yükleme işlemi
                string wwwRootPath = _host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(urunler.ResimDosya.FileName);//Dosya Adını Aldık.
                string extension = Path.GetExtension(urunler.ResimDosya.FileName);//Yüklenen Resmin Uzantısını Aldık.
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath, "yuklenenResimler/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await urunler.ResimDosya.CopyToAsync(fileStream);
                }

                urunler.UrunResmi = fileName;
                // wwwroot/YuklenenResimler klasörüne resim yükleme işlemi




                _context.Add(urunler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "DepoAdi", urunler.DepoId);
            return View(urunler);
        }

        // GET: Urunler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urunler = await _context.Urunler.FindAsync(id);
            if (urunler == null)
            {
                return NotFound();
            }
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "DepoAdi", urunler.DepoId);
            return View(urunler);
        }

        // POST: Urunler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UrunAdi,UrunResmi,UrunStokAdeti,AlisFiyati,SatisFiyati,Aciklama,DepoId")] Urunler urunler)
        {
            if (id != urunler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // wwwroot/YuklenenResimler klasörüne resim yükleme işlemi
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(urunler.ResimDosya.FileName);//Dosya Adını Aldık.
                    string extension = Path.GetExtension(urunler.ResimDosya.FileName);//Yüklenen Resmin Uzantısını Aldık.
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "yuklenenResimler/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await urunler.ResimDosya.CopyToAsync(fileStream);
                    }

                    urunler.UrunResmi = fileName;
                    // wwwroot/YuklenenResimler klasörüne resim yükleme işlemi


                    _context.Update(urunler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunlerExists(urunler.Id))
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
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "DepoAdi", urunler.DepoId);
            return View(urunler);
        }

        // GET: Urunler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urunler = await _context.Urunler
                .Include(u => u.Depo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunler == null)
            {
                return NotFound();
            }

            return View(urunler);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Urunler == null)
            {
                return Problem("Entity set 'DepoStokBitirmeProjesiContext.Urunler'  is null.");
            }
            var urunler = await _context.Urunler.FindAsync(id);
            if (urunler != null)
            {
                _context.Urunler.Remove(urunler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunlerExists(int id)
        {
          return (_context.Urunler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
