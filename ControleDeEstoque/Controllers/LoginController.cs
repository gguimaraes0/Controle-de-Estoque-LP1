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


        public IActionResult CadastrarUsuario(UsuarioViewModel usuario)
        {
            try
            {
                MainViewModel mainViewModel = new MainViewModel();
                mainViewModel.usuario = usuario;
                string Operacao = ViewBag.Operacao = "I";
                ValidaDados(usuario);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    return View("../Usuario/CadastroUsuario", mainViewModel);
                }
                else
                {
                    UsuarioDAO dao = new UsuarioDAO();

                    //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                    dao.Insert(usuario);
                    return View("../Home/Index");
                }

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult CadastroUsuario()
        {
            MainViewModel mainViewModel = new MainViewModel();
            return View("../Usuario/CadastroUsuario", mainViewModel);
        }

        private void ValidaDados(UsuarioViewModel usuario)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net
            FornecedorDAO dao = new FornecedorDAO();
            if (string.IsNullOrEmpty(usuario.CEP))
                ModelState.AddModelError("usuario.CEP", "Obrigatório informar um CEP.");

            if (string.IsNullOrEmpty(usuario.Complemento))
                ModelState.AddModelError("usuario.Complemento", "Obrigatório informar Complemento.");

            if (string.IsNullOrEmpty(usuario.Email))
                ModelState.AddModelError("usuario.Email", "Obrigatório informar o Email.");

            if (string.IsNullOrEmpty(usuario.Nome))
                ModelState.AddModelError("usuario.Nome", "Obrigatório informar o Nome.");

            if (string.IsNullOrEmpty(usuario.Numero) || int.Parse(usuario.Numero) < 0)
                ModelState.AddModelError("usuario.Numero", "Obrigatório informar o Numero válido.");

            if (string.IsNullOrEmpty(usuario.Telefone))
                ModelState.AddModelError("usuario.Telefone", "Obrigatório informar um Telefone válido.");

            if (string.IsNullOrEmpty(usuario.Senha))
                ModelState.AddModelError("usuario.Senha", "Obrigatório informar a Senha.");
        }

    }
}
