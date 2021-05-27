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
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroUsuario()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            return View("CadastroUsuario", usuario);
        }

        public IActionResult ListagemUsuario()
        {
            return View("ListagemUsuario");
        }
    }
}