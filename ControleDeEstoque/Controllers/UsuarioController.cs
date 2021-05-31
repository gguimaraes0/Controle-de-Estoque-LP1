using ControleDeEstoque.DAO;
using Microsoft.AspNetCore.Http;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            MainViewModel mainViewModel = new MainViewModel();
            return View("CadastroUsuario", mainViewModel);
        }

        public IActionResult ListagemUsuario()
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                MainViewModel usuario = new MainViewModel();
                usuario.usuarios = dao.Listagem();
                
                return View(usuario);
            }
            catch (Exception erro)
            {
                return View("../Home/Index");
            }
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

        public IActionResult Delete(string pk)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                dao.Delete(int.Parse(pk));
                return View("../Home/Index");
            }
            catch (Exception erro)
            {
                return RedirectToAction("index");
            }
        }
    }
}