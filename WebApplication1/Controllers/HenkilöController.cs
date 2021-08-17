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
        public IActionResult LisääHenkilö(Person p)
        {
            DataAccess da = new DataAccess(_context);
            da.lisääkäyttäjä(p);
            return RedirectToAction("NäytäPalvelut", p);
        }
        public IActionResult LisääKäyttäjä(Person p)
        {
            
            return View();
        }

        //[HttpPost]
        //public IActionResult LisääTutor(Person person)
        //{
        //    var url = @"https://localhost:44325/Tutor/LisääTutor";
        //    var body = JsonConvert.SerializeObject(person);
        //    string json = "";
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //        var content = new StringContent(body, UTF8Encoding.UTF8, "application/json");
        //        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        var response = client.PostAsync(url, content).Result;
        //        json = response.Content.ReadAsStringAsync().Result;
        //    }
        //    return Content("Uusi tuutori lisätty!");

        //}

        [HttpGet]
        public IActionResult LisääPalvelu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LisääPalvelu(Palvelu palvelu)
        {
            var url = @"https://localhost:44325/Tutor/LisääPalvelu";
            var body = JsonConvert.SerializeObject(palvelu);
            string json = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(body, UTF8Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(url, content).Result;
                json = response.Content.ReadAsStringAsync().Result;
            }
            return Content("Uusi palvelu lisätty!");
        }


        [HttpGet]
        public IActionResult NäytäPalvelut()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OmatPalvelut(int tuutorinID)
        {
            DataAccess da = new DataAccess(_context);
            var palvelut = da.haetuutorinpalvelut(tuutorinID);
            ViewBag.OP = palvelut.OrderBy(p => p.Tyyppi);
            ViewBag.PalveluidenMäärä = palvelut.Count();
            ViewBag.EriTyyppienMäärä =
                (from p in palvelut
                 where p.TutorId == tuutorinID
                 select p.Tyyppi).Distinct().Count();

            return View();
        }
    }
}
