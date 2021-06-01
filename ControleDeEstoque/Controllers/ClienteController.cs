using ControleDeEstoque.DAO;
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
    public class ClienteController : DefaultController
    {
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroCliente()
        {
            MainViewModel mainViewModel = new MainViewModel();

            // ClienteViewModel cliente = new ClienteViewModel();
            return View("CadastroCliente", mainViewModel);
        }

        public IActionResult ListagemCliente()
        {
            try
            {
                ClienteDAO dao = new ClienteDAO();
                MainViewModel cliente = new MainViewModel();
                cliente.clientes = dao.Listagem();

                return View(cliente);
            }
            catch (Exception erro)
            {
                return View("../Home/Index");
            }
        }

        public IActionResult CadastrarCliente(ClienteViewModel cliente)
        {
            try
            {

                MainViewModel mainViewModel = new MainViewModel();
                mainViewModel.cliente = cliente;
                string Operacao = ViewBag.Operacao = "I";
                ValidaDados(cliente);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    return View("../Cliente/CadastroCliente", mainViewModel);
                }
                else
                {
                    ClienteDAO dao = new ClienteDAO();

                    //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 

                    dao.Insert(cliente);
                    return View("../Home/Index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        private void ValidaDados(ClienteViewModel cliente)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net
            ClienteDAO dao = new ClienteDAO();
            if (string.IsNullOrEmpty(cliente.CEP))
                ModelState.AddModelError("cliente.CEP", "Obrigatório informar um CEP.");

            if (string.IsNullOrEmpty(cliente.Complemento))
                ModelState.AddModelError("cliente.Complemento", "Obrigatório informar o Complemento.");

            if (string.IsNullOrEmpty(cliente.Email))
                ModelState.AddModelError("cliente.Email", "Obrigatório informar o Email.");

            if (string.IsNullOrEmpty(cliente.Nome))
                ModelState.AddModelError("cliente.Nome", "Obrigatório informar o Nome.");

            if (string.IsNullOrEmpty(cliente.Numero) || int.Parse(cliente.Numero) < 0)
                ModelState.AddModelError("cliente.Numero", "Obrigatório informar um número válido.");

            if (string.IsNullOrEmpty(cliente.Telefone))
                ModelState.AddModelError("cliente.Telefone", "Obrigatório informar o Telefone.");

            if (string.IsNullOrEmpty(cliente.DataNascimento))
                ModelState.AddModelError("cliente.DataNascimento", "Obrigatório informar a Data Nascimento.");

            if (string.IsNullOrEmpty(cliente.CNPJ) && string.IsNullOrEmpty(cliente.CPF))
            {
                ModelState.AddModelError("cliente.CNPJ", "Obrigatório informar CPF/CNPJ.");
                ModelState.AddModelError("cliente.CPF", "Obrigatório informar CPF/CNPJ.");
            }

        }

        public IActionResult Delete(string pk)
        {
            try
            {
                ClienteDAO dao = new ClienteDAO();
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