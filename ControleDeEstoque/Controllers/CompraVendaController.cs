using ControleDeEstoque.DAO;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Controllers
{
    public class CompraVendaController : DefaultController
    {
        private readonly ILogger<CompraVendaController> _logger;

        public CompraVendaController(ILogger<CompraVendaController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroCompra()
        {
            MainViewModel mainViewModel = new MainViewModel();
            PreparaListaFornecedorParaCombo();
            return View("CadastroCompra", mainViewModel);
        }

        public IActionResult CadastroVenda()
        {
            MainViewModel mainViewModel = new MainViewModel();
            PreparaListaFornecedorParaCombo();
            return View("CadastroVenda", mainViewModel);
        }

        public IActionResult ListagemCompraVenda()
        {
            try
            {
                CompraVendaDAO dao = new CompraVendaDAO();
                MainViewModel compraVenda = new MainViewModel();
                compraVenda.compraVendas = dao.Listagem();

                return View(compraVenda);
            }
            catch (Exception erro)
            {
                return View("../Home/Index");
            }
        }

        public IActionResult CadastrarCompra(MainViewModel compra)
        {
            try
            {
                // ValidaDados(curriculo, Operacao);

                CompraVendaDAO dao = new CompraVendaDAO();

                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                compra.compraVenda.Tipo = "Compra";

                PreparaListaFornecedorParaCombo();

                dao.Insert(compra.compraVenda);

                return View("../Home/Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult CadastrarVenda(MainViewModel venda)
        {
            try
            {
                CompraVendaDAO dao = new CompraVendaDAO();
                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                venda.compraVenda.Tipo = "Venda";

                PreparaListaFornecedorParaCombo();

                dao.Insert(venda.compraVenda);
                return View("../Home/Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private void PreparaListaFornecedorParaCombo()
        {
            FornecedorDAO FornecedorDao = new FornecedorDAO();
            var Fornecedores = FornecedorDao.ListaFornecedor();
            List<SelectListItem> listaFornecedor = new List<SelectListItem>();

            listaFornecedor.Add(new SelectListItem("Selecione um Fornecedor...", "0"));
            foreach (var Fornecedor in Fornecedores)
            {
                SelectListItem item = new SelectListItem(Fornecedor.Nome, Fornecedor.Codigo.ToString());
                listaFornecedor.Add(item);
            }
            ViewBag.Fornecedor = listaFornecedor;
        }
    }
}