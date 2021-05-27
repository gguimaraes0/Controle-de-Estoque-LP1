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
    public class CompraVendaController : Controller
    {
        private readonly ILogger<CompraVendaController> _logger;

        public CompraVendaController(ILogger<CompraVendaController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroCompra()
        {
            CompraVendaViewModel compraVenda = new CompraVendaViewModel();
            return View("CadastroCompra", compraVenda);
        }

        public IActionResult CadastroVenda()
        {
            CompraVendaViewModel compraVenda = new CompraVendaViewModel();
            return View("CadastroVenda", compraVenda);
        }

        public IActionResult ListagemCompraVenda()
        {
            return View("ListagemCompraVenda");
        }
    }
}