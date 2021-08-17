using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HenkilöController : Controller
    {
        [HttpGet]
        public IActionResult LisääHenkilö()
        {
            DataAccess haku = new DataAccess();
            var henkilö = haku.haetuutori();
            return View();
        }

        [HttpPost]
        public IActionResult LisääHenkilö(Person p)
        {
            return RedirectToAction("LisääKäyttäjä", p);
        }

        public IActionResult LisääKäyttäjä(Person p)
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult LisääTutor(Person person)
        {
            return Content("Uusi tuutori lisätty!");

        }

        [HttpGet]
        public IActionResult LisääPalvelu()
        {
            DataAccess haku = new DataAccess();
            var palvelut = haku.haepalvelut();
            ViewBag.palvelut = palvelut;
            return View();
        }

        [HttpPost]
        public IActionResult LisääPalvelu(Palvelu palvelu)
        {
            return Content("Uusi palvelu lisätty!");
        }


        [HttpGet]
        public IActionResult NäytäPalvelut()
        {

            return View();
        }
    }
}
