using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class PalveluController : Controller
    {  
        [HttpGet]
        public IActionResult HaePalvelut()
        {
            DA haku = new DA();
            var palvelut = haku.HaePalvelut();
            ViewBag.Palvelut = palvelut;
            return View();
        }

        public IActionResult NäytäPalvelu()
        {
            //Detailsit palvelusta
            return View();
        }
    }
}
