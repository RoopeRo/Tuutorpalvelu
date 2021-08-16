using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class TutorController : Controller
    {
        [HttpPost]
        public IActionResult LisääTutor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LisääPalvelu()
        {
            return View();
        }
    }
}
