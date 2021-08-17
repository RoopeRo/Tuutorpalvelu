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
        private readonly TutorpalveluDBContext _context;
        public HenkilöController(TutorpalveluDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LisääHenkilö()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LisääHenkilö(Person person) //tuutorin tai asiakkaan lisääminen
        {
            DataAccess da = new DataAccess(_context);
            da.Lisääkäyttäjä(person);
            var q = da.haetuutorit();
            ViewBag.People = q;
            return View();
            //return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LisääPalvelu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LisääPalvelu(Palvelu palvelu)
        {
            DataAccess da = new DataAccess(_context);
            da.lisääpalvelu(palvelu, tuutorid);
            return RedirectToAction("HaeTutorinPalvelut");
        }

        [HttpGet]
        public IActionResult HaeTutorinPalvelut(int tunniste)
        {
            DataAccess haku = new DataAccess(_context);
            var palvelut = haku.haetuutorinpalvelut(tunniste);
            ViewBag.palvelut = palvelut;
            return View();
            //tämän metodin pitää automaattisesti hakea tuutorin id:llä hänen palvelunsa
        }

    }
}
