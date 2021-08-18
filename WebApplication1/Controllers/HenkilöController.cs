using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Person> _userManager;
        private readonly TutorpalveluDBContext _context;
        public HenkilöController(TutorpalveluDBContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            da.lisääpalvelu(palvelu);
            return RedirectToAction("HaeTutorinPalvelut");
        }

        //[HttpGet(Name ="HaeMuokattavaPalvelua")] //siirrytään tiettyyn palveluun uniikin palveluid perusteella, uusi muokkausnäkymä
        //public IActionResult EditoiPalvelua(Palvelu palvelu)
        //{
        //    DataAccess da = new DataAccess(_context);
        //    var muokattavapalvelu = da.haetuutorinpalvelut(palvelu); //EditoiPalvelua metodi puuttuu DataAccess palikasta
        //    return View(muokattavapalvelu);
        //}

        //[HttpPut(Name = "EditoiPalvelua")] //muokataan palvelua ja lähetetään se
        //public IActionResult EditoiPalvelua(Palvelu palvelu)
        //{
        //    DataAccess da = new DataAccess(_context);
        //    var palvelu = da.EditoiPalvelua(palvelu);
        //    return RedirectToAction("HaeTutorinPalvelut");
        //}

        //[HttpDelete(Name="PoistaPalvelu")]//poistetaan palvelu palvelu id perusteella; pelkkä nappi, ohjaa samaan näkymään hakemalla uudestaan tuutorin palvelut
        //public IActionResult EditoiPalvelua(int palveluid)
        //{
        //    DataAccess da = new DataAccess(_context);
        //    da.PoistaPalvelu(palveluid);
        //    return RedirectToAction("HaeTutorinPalvelut");
        //}

        [HttpGet]
        public IActionResult HaeTutorinPalvelut(int tunniste)
        {
            DataAccess haku = new DataAccess(_context);
            var palvelut = haku.haetuutorinpalvelut(tunniste);
            var id = _userManager.GetUserId(HttpContext.User);
            ViewBag.ID = id;
            ViewBag.palvelut = palvelut.OrderBy(p => p.Tyyppi);
            ViewBag.PalveluidenMäärä = palvelut.Count();
            ViewBag.EriTyyppienMäärä =
                (from p in palvelut
                 where p.TutorId == tunniste
                 select p.Tyyppi).Distinct().Count();
        
            return View();
            //tämän metodin pitää automaattisesti hakea tuutorin id:llä hänen palvelunsa kun käyttäjä ohjataan tähän actioon/sivulle
        }
    }
}
