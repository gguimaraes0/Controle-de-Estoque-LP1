using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Controllers
{
    public class HomeLogadoController : DefaultController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View("../Home/Index");
        }
    }
}
