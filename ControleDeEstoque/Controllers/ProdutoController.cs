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
    public class ProdutoController : Controller
    {
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(ILogger<ProdutoController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroProduto()
        {
            ProdutoViewModel produto = new ProdutoViewModel();
            return View("CadastroProduto", produto);
        }

        public IActionResult ListagemProduto()
        {
            return View("ListagemProduto");
        }
    }
}