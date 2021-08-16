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
            TutorpalveluDBContext db = _context;
            var k = db.Käyttäjäs.Where(k => k.Username != null).FirstOrDefault();
            ViewBag.Name = k.Username;
            return View();
        }
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            bool AuthOK = new DataAccess(_context).TarkistaKäyttäjänAuth(username, password);
            if (AuthOK)
            {
                RedirectToAction("Tutor", "Sisälle");
            }
            ViewBag.AuthOK = false;
            return View();
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
    }
}
