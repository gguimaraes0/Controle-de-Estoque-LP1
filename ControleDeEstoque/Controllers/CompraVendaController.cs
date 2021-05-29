using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Controllers
{
    public class CompraVendaController : DefaultController
    {
        private readonly ILogger<CompraVendaController> _logger;

        public CompraVendaController(ILogger<CompraVendaController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroCompra()
        {
            MainViewModel mainViewModel = new MainViewModel();
            return View("CadastroCompra", mainViewModel);
        }

        public IActionResult CadastroVenda()
        {
            MainViewModel mainViewModel = new MainViewModel();
            return View("CadastroVenda", mainViewModel);
        }

        public IActionResult ListagemCompraVenda()
        {
            return View("ListagemCompraVenda");
        }
    }
}