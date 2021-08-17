using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TutorController : Controller
    {
        private readonly TutorpalveluDBContext _context;
        public TutorController(TutorpalveluDBContext context)
        {
            _context = context;


        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

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
