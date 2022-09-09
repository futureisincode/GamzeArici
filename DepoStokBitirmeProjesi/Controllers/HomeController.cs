﻿using DepoStokBitirmeProjesi.Identity;
using DepoStokBitirmeProjesi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace DepoStokBitirmeProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DepoStokBitirmeProjesiContext _context;

        public HomeController(ILogger<HomeController> logger, DepoStokBitirmeProjesiContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeDashboardViewModel homeDashboardViewModel = new HomeDashboardViewModel
            {
                ToplamSatisFiyati = (decimal)_context.KasaHareketleri.Where(k => k.IslemTuru == 0).Sum(k => k.SatisFiyati),
                ToplamAlisFiyati= (decimal)_context.KasaHareketleri.Where(k=>k.IslemTuru==1).Sum(k=>k.AlisFiyati),


                ToplamSatisAdeti = _context.KasaHareketleri.Where(k => k.IslemTuru == 0).Count(),
                ToplamAlisAdeti = _context.KasaHareketleri.Where(k => k.IslemTuru == 1).Count(),


            };
            return View(homeDashboardViewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}