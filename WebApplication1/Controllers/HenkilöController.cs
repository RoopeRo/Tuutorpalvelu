﻿using Microsoft.AspNetCore.Mvc;
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
            da.lisääpalvelu(palvelu);
            return RedirectToAction("HaeTutorinPalvelut");
        }

        [HttpGet(Name ="HaeMuokattavaPalvelua")] //siirrytään tiettyyn palveluun uniikin palveluid perusteella, uusi muokkausnäkymä
        public IActionResult EditoiPalvelua(Palvelu palvelu)
        {
            DataAccess da = new DataAccess(_context);
            var muokattavapalvelu = da.haetuutorinpalvelut(palvelu); //EditoiPalvelua metodi puuttuu DataAccess palikasta
            return View(muokattavapalvelu);
        }

        [HttpPut(Name = "EditoiPalvelua")] //muokataan palvelua ja lähetetään se
        public IActionResult EditoiPalvelua(Palvelu palvelu)
        {
            DataAccess da = new DataAccess(_context);
            var palvelu = da.EditoiPalvelua(palvelu);
            return RedirectToAction("HaeTutorinPalvelut");
        }

        [HttpDelete(Name="PoistaPalvelu")]//poistetaan palvelu palvelu id perusteella; pelkkä nappi, ohjaa samaan näkymään hakemalla uudestaan tuutorin palvelut
        public IActionResult EditoiPalvelua(int palveluid)
        {
            DataAccess da = new DataAccess(_context);
            da.PoistaPalvelu(palveluid);
            return RedirectToAction("HaeTutorinPalvelut");
        }

        [HttpGet]
        public IActionResult HaeTutorinPalvelut(int tutorid)
        {
            DataAccess haku = new DataAccess(_context);
            var palvelut = haku.haetuutorinpalvelut(tutorid);
            ViewBag.palvelut = palvelut;
            return View();
            //tämän metodin pitää automaattisesti hakea tuutorin id:llä hänen palvelunsa kun käyttäjä ohjataan tähän actioon/sivulle
        }

       



    }
}
