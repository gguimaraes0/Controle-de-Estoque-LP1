using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Controllers
{
    public class SobreController : DefaultController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View("../Home/Sobre");
        }
    }
}
