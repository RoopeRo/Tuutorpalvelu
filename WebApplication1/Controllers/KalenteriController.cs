using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class KalenteriController : Controller
    {
        private readonly TutorpalveluDBContext _context;

        public KalenteriController(TutorpalveluDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Kalenteri(int? vuosi, int? kuukausi)
        {
            ViewBag.Vuosi = vuosi == null ? DateTime.Now.Year : vuosi;
            ViewBag.Kuukausi = kuukausi == null ? (Kuukausi)DateTime.Now.Month : (Kuukausi)kuukausi;
            ViewBag.KNro = kuukausi == null ? DateTime.Now.Month : kuukausi;
            ViewBag.PaivienLKM = Karkausvuosi.PaivienLkm(ViewBag.Kuukausi.ToString(), ViewBag.Vuosi);
            var Paivakirja = new Dictionary<string, int>() { { "Monday", 1 }, { "Tuesday", 2 }, { "Wednesday", 3 }, { "Thursday", 4 }, { "Friday", 5 }, { "Saturday", 6 }, { "Sunday", 7 } };
            var kuluvakuu = new DateTime((int)(vuosi == null ? DateTime.Now.Year : vuosi), (int)(kuukausi == null ? DateTime.Now.Month : kuukausi), 1);
            ViewBag.EnsimmainenPaiva = Paivakirja[kuluvakuu.DayOfWeek.ToString()];
            DataAccess da = new DataAccess(_context);
            List<Palvelu> palvelut = new List<Palvelu>();
            if (vuosi != null && kuukausi != null)
            {
                palvelut = da.haepalvelut().Where(x => x.Pvm.Year == vuosi && x.Pvm.Month == kuukausi).ToList();
            }
            else
            {
                palvelut = da.haepalvelut().Where(x => x.Pvm.Year == DateTime.Now.Year && x.Pvm.Month == DateTime.Now.Month).ToList();
            }
            var päivätJoillaPalveluita = new List<int>();
            foreach(var p in palvelut)
            {
                if (!päivätJoillaPalveluita.Contains(p.Pvm.Day))
                {
                    päivätJoillaPalveluita.Add(p.Pvm.Day);
                }
            }
            ViewBag.PalveluPäivät = päivätJoillaPalveluita;
            return View();
        }
        public IActionResult Varauskalenteri(int kuukausi, int vuosi, int päivä)
        {
            if (päivä == 0)
            {
                ViewBag.Varaukset = null;
                return View();
            }
            else
            {
                var palvelut = new DataAccess(_context).haepalvelut().Where(p => p.Pvm.Day == päivä).ToList();
                
                ViewBag.Vuosi = vuosi;
                ViewBag.Kuukausi = (Kuukausi)kuukausi;
                ViewBag.Päivä = päivä;
                ViewBag.Varaukset = palvelut.Count();
                return View(palvelut);
            }
        }

    }
    public enum Kuukausi
    {
        Tammikuu = 1,
        Helmikuu = 2,
        Maaliskuu = 3,
        Huhtikuu = 4,
        Toukokuu = 5,
        Kesäkuu = 6,
        Heinäkuu = 7,
        Elokuu = 8,
        Syyskuu = 9,
        Lokakuu = 10,
        Marraskuu = 11,
        Joulukuu = 12
    }
    public enum PaivatKuussa
    {
        Tammikuu = 31,
        Helmikuu = 28,
        Maaliskuu = 31,
        Huhtikuu = 30,
        Toukokuu = 31,
        Kesäkuu = 30,
        Heinäkuu = 31,
        Elokuu = 31,
        Syyskuu = 30,
        Lokakuu = 31,
        Marraskuu = 30,
        Joulukuu = 31
    }
}
