using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TutorpalveluDBContext _context;


        public HomeController(ILogger<HomeController> logger, TutorpalveluDBContext context)
        {
            _logger = logger;
            _context = context;


        }


        public IActionResult Index()
        {
            //DataAccess da = new DataAccess(_context);
            //var palvelut = da.haepalvelut().Where(p => p.TutorId == null).ToList();
            //foreach(var pal in palvelut)
            //{
            //    da.PoistaPalvelu(pal);
            //}
            return View();
        }

        /// <summary>
        /// Palauttaa kirjautumisnäkymän
        /// </summary>
        /// <param name="username">käyttäjän syöttämä käyttäjänimi</param>
        /// <param name="password">käyttäjän syöttämä salasana</param>
        /// <returns>kirjautumisnäkymä</returns>
        [HttpPost]
        public IActionResult Index(string username, string password)
        {

            Person käyttäjä = _context.People.Where(p => p.Username == username && p.Password == password).FirstOrDefault();
            if (käyttäjä != null)
            {
                HttpContext.Session.SetInt32("id", käyttäjä.PersonId);
                var id = HttpContext.Session.GetInt32("id");
                HttpContext.Session.SetString("nimi", käyttäjä.Etunimi);
                //RedirectToAction("Testi", "Muutos");
                ViewBag.AuthOK = true;
                return RedirectToAction("Index", "Henkilö");
            }
            else
            {
                ViewBag.AuthOK = false;
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Virhe()
        {
            return View();
        }
    }
}
