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
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Controllers
{
    public class HenkilöController : Controller
    {
        //private readonly UserManager<Person> _userManager;
        private readonly TutorpalveluDBContext _context;
        public HenkilöController(TutorpalveluDBContext context)
        {
            _context = context;
            
        }

        /// <summary>
        /// HENKILÖ CRUD: GET, POST (put)
        /// </summary>
        /// <returns></returns>

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
            //return View();
            return RedirectToAction("HaePalvelut", "Palvelu");
        }

        [HttpGet]
        public IActionResult OmatTiedot(int Id)
        {
            var henkilö = new DataAccess(_context).HaeTutor(Id);
            return View(henkilö);
        }

        [HttpGet]
        public IActionResult EditoiHenkilöä(int Id)//siirrytään tiettyyn palveluun uniikin palveluid perusteella, uusi muokkausnäkymä
        {
            var henkilö = new DataAccess(_context).HaeTutor(Id);
            return View(henkilö);
        }

        [HttpPost] //editoidaan henkilöä ja lähetetaan se
        public IActionResult EditoiHenkilöä(Person henkilö)
        {
            DataAccess da = new DataAccess(_context);
            da.EditoiHenkilöä(henkilö);
            return RedirectToAction("OmatTiedot", new {Id = henkilö.PersonId });
        }
        public IActionResult PoistaHenkilö(int Id)
        {
            DataAccess da = new DataAccess(_context);
            var henkilö = da.HaeTutor(Id);
            da.PoistaHenkilö(henkilö);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// PALVELUN CRUD
        /// </summary>
        /// <returns></returns>


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

        [HttpGet/*(Name = "HaeMuokattavaPalvelua")*/] //siirrytään tiettyyn palveluun uniikin palveluid perusteella, uusi muokkausnäkymä
        public IActionResult EditoiPalvelua(int palveluid)
        {
            DataAccess da = new DataAccess(_context);
            var muokattavapalvelu = da.haetuutorinpalvelut(palveluid).FirstOrDefault();
            return View(muokattavapalvelu);
        }

        [HttpPost(Name = "EditoiPalvelua")] //muokataan palvelua ja lähetetään se
        public IActionResult EditoiPalvelua(Palvelu palvelu)
        {
            DataAccess da = new DataAccess(_context);
            da.EditoiPalvelua(palvelu);
            return RedirectToAction("HaeTutorinPalvelut");
        }

        [HttpDelete(Name = "PoistaPalvelu")]//poistetaan palvelu palvelu id perusteella; pelkkä nappi, ohjaa samaan näkymään hakemalla uudestaan tuutorin palvelut
        public IActionResult PoistaPalvelu(int id)
        {
            DataAccess da = new DataAccess(_context);
            var palvelu = da.haepalvelut().Where(p => p.PalveluId == id).FirstOrDefault();
            da.PoistaPalvelu(palvelu);
            return RedirectToAction("HaeTutorinPalvelut");
        }

        /// <summary>
        /// Hakee tuutorin tarjoamat palvelut
        /// </summary>
        /// <returns>haeTutorinpalvelut näkymä</returns>
        [HttpGet]
        public IActionResult HaeTutorinPalvelut()
        {
            DataAccess haku = new DataAccess(_context);
            var id = HttpContext.Session.GetInt32("id");
            ViewBag.ID = id;
            if (id != null)
            {
                var palvelut = haku.haetuutorinpalvelut((int)id);
                ViewBag.palvelut = palvelut.OrderBy(p => p.Tyyppi);
                ViewBag.PalveluidenMäärä = palvelut.Count();
                ViewBag.EriTyyppienMäärä =
                    (from p in palvelut
                     where p.TutorId == id
                     select p.Tyyppi).Distinct().Count();
                ViewBag.Nimi = HttpContext.Session.GetString("nimi");
            }
            return View();
        }
    }
}
