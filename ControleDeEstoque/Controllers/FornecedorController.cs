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
    public class FornecedorController : DefaultController
    {
        private readonly ILogger<FornecedorController> _logger;

        public FornecedorController(ILogger<FornecedorController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroFornecedor()
        {
            MainViewModel mainViewModel = new MainViewModel();
            return View("CadastroFornecedor", mainViewModel);
        }

        public IActionResult ListagemFornecedor()
        {
            return View("ListagemFornecedor");
        }
    }
}