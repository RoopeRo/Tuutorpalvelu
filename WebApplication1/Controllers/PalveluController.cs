using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

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
            DataAccess haku = new DataAccess(_context);
            var palvelut = haku.haepalvelut();
            ViewBag.palvelut = palvelut;
            return View();
        }

        [HttpGet]
        public IActionResult HaeDetails()
        {
            //detailsien näyttämien
            return View();
        }

        [HttpPost]
        public IActionResult HaePalvelutFiltteri()
        {
            return View();
        }
    }
}
