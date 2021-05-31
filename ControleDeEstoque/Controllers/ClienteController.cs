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
                // ValidaDados(curriculo, Operacao);

                ClienteDAO dao = new ClienteDAO();

                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 

                dao.Insert(cliente);
                return View("../Home/Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}