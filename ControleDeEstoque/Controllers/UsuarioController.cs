using ControleDeEstoque.DAO;
using Microsoft.AspNetCore.Http;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControleDeEstoque.Controllers
{
    public class UsuarioController : DefaultController
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

        public IActionResult CadastrarUsuario(UsuarioViewModel usuario)
        {
            try
            {
                // ValidaDados(curriculo, Operacao);

                UsuarioDAO dao = new UsuarioDAO();

                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                usuario.Codigo = usuario.Codigo;
                dao.Insert(usuario);
                return View("../Home/Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }    
    }
}