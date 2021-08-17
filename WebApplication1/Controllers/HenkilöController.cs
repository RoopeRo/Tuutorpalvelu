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
            var url = @"https://localhost:44325/Tutor/LisääTutor";
            var body = JsonConvert.SerializeObject(person);
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
    }
}
