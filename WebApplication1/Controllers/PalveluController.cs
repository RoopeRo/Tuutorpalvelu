using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class PalveluController : Controller
    {
        public IActionResult HaePalvelutKaikki()
        {
            DataAccess haku = new DataAccess();
            var palvelut = haku.haepalvelut();
            ViewBag.palvelut = palvelut;
            return View();
        }

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
