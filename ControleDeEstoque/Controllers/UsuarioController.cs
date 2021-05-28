using ControleDeEstoque.DAO;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
                usuario.Senha = "admin";
                dao.Insert(usuario);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        //public IActionResult FazLogin(string usuario, string senha)
        //{
        //    ////Este é apenas um exemplo, aqui você deve consultar na sua tabela de usuários
        ////se existe esse usuário e senha
        //using ( dc = new ())
        //{
        //    var v = dc.Usuarios.Where(a => a.NomeUsuario.Equals(u.NomeUsuario) && a.Senha.Equals(u.Senha)).FirstOrDefault();
        //    if (v != null)
        //    {
        //        Session["usuarioLogadoID"] = v.Id.ToString();
        //        Session["nomeUsuarioLogado"] = v.NomeUsuario.ToString();
        //        return RedirectToAction("Index");
        //    }
        //}

        //    foreach (var item in collection)
        //{

        //}
        //if (usuario == "admin" && senha == "1234")
        //{
        //    HttpContext.Session.SetString("Logado", "true");
        //    return RedirectToAction("index", "Home");
        //}
        //else
        //{
        //    ViewBag.Erro = "Usuário ou senha inválidos!";
        //    return View("Index");
        //}
        //  }

        public IActionResult LogarUsuario(UsuarioViewModel usuario)
        {
            try
            {
                var usuarioValido = UsuarioDAO.RetornaUsuarioLogado(usuario);
                if (usuarioValido != null)
                {
                    HttpContext.Session.SetString("Logado", "true");
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ViewBag.Erro = "Usuário ou senha inválidos!";
                    return View("Index");
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
            return RedirectToAction("Index");
        }
    }
}