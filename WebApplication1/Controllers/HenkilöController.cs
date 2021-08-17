using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HenkilöController : Controller
    {
        [HttpGet]
        public IActionResult LisääHenkilö()
        {
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

        [HttpGet]
        public IActionResult LisääPalvelu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NäytäPalvelut()
        {
            return View();
        }
    }
}
