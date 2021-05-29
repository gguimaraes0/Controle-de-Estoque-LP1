using ControleDeEstoque.DAO;
using Microsoft.AspNetCore.Http;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ControleDeEstoque.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAO UsuarioDAO = new UsuarioDAO();
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
        public IActionResult LogarUsuario(MainViewModel main)
        {
            try
            {
                var usuarioValido = UsuarioDAO.ValidaLogin(main.usuario);
                if (usuarioValido != null)
                {
                    HttpContext.Session.SetString("Logado", "true");
                    ViewBag.Logado = true;
                    // Microsoft.AspNetCore.Http.HttpContext.Session.SetString("Logado", "true");
                    return View("../Home/Index");
                }
                else
                {
                    ViewBag.Logado = false;
                    ViewBag.Erro = "Usuário ou senha inválidos!";
                    return View("../Home/Index");
                    //Mostrar mensagem de erro
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Ocorreu um erro: " + ex.Message;
                return RedirectToAction("HomeView", "Home");
            }
            //Logar(usuario);
            //return null;
        }
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            ViewBag.Logado = false;
            return View("../Home/Index");
        }
    }
}