using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class TutorController : Controller
    {
        [HttpGet]
        public IActionResult LisääTutor()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LisääPalvelu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NäytäPalvelut()
        {
            return View();
        }
    }
}
