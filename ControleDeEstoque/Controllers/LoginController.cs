using ControleDeEstoque.DAO;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioDAO UsuarioDAO = new UsuarioDAO();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogarUsuario(MainViewModel main)
        {
            try
            {
                var usuarioValido = UsuarioDAO.ValidaLogin(main.usuario);
                if (usuarioValido != null)
                {
                    HttpContext.Session.SetString("Logado", "true");
                    HttpContext.Session.SetString("nome_usuario", main.usuario.Email);
                    HttpContext.Session.SetString("id_usuario", main.usuario.Senha);
                    ViewBag.Logado = true;
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
