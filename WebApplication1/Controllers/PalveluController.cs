using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Controllers
{
    public class PalveluController : Controller
    {
        private readonly TutorpalveluDBContext _context;
        public PalveluController(TutorpalveluDBContext context)
        {
            _context = context;

        }
        [HttpGet]
        public IActionResult HaePalvelut()
        {
            DataAccess DA = new DataAccess(_context);
            var palvelut = DA.haepalvelut();
            foreach(var p in palvelut)
            {
                _context.Entry(p).Reference(r => r.Tutor).Load();
            }

            ViewBag.Palvelut = palvelut;
            return View();
        }

        [HttpGet]
        public IActionResult HaeDetails(int id)
        {
            DataAccess DA = new DataAccess(_context);
            var palvelut = DA.haepalvelut().Where(p => p.PalveluId == id);
            foreach (var p in palvelut)
            {
                _context.Entry(p).Reference(r => r.Tutor).Load();
            }

            ViewBag.Palvelut = palvelut;
            return View();
        }
        [HttpPost]
        public IActionResult HaePalvelutFiltteri()
        {
            return View();
        }

    }
}
