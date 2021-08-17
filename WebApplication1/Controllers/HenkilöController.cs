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
        public IActionResult LisääHenkilö(Person person)
        {
            DataAccess da = new DataAccess(_context);
            da.lisääkäyttäjä(person);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult LisääTutor(Person person)
        {
            return Content("Uusi tuutori lisätty!");

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
            da.lisääpalvelu(palvelu);
            return RedirectToAction("NäytäPalvelut", palvelu);
        }


        [HttpGet]
        public IActionResult NäytäPalvelut()
        {

            return View();
        }
    }
}
