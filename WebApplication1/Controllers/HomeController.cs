﻿using Microsoft.AspNetCore.Http;
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
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(string username, string password)
        {

            //bool AuthOK = new DataAccess(_context).TarkistaKäyttäjänAuth(username, password);

            //if (AuthOK)
            //{

            //    RedirectToAction("Testi", "Muutos");
            //}
            //ViewBag.AuthOK = false;
            //return View();

            //Person käyttäjä = new DataAccess(_context).HaeKirjautumistietojaVastaavaHenkilö(username, password);
            Person käyttäjä = _context.People.Where(p => p.Username == username && p.Password == password).FirstOrDefault();
            if (käyttäjä != null)
            {
                HttpContext.Session.SetInt32("id", käyttäjä.PersonId);
                var id = HttpContext.Session.GetInt32("id");
                //HttpContext.Session.SetString("nimi", käyttäjä.Etunimi);
                //RedirectToAction("Testi", "Muutos");
                ViewBag.AuthOK = true;
            }
            else
            {
                ViewBag.AuthOK = false;
            }
            
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
