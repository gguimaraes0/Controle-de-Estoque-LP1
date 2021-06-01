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
                string Operacao = ViewBag.Operacao = "I";
                ValidaDados(compra.compraVenda);
                if (ModelState.IsValid == false)
                {
                    PreparaListaFornecedorParaCombo();
                    return View("../CompraVenda/CadastroCompra");
                }
                else
                {
                    CompraVendaDAO dao = new CompraVendaDAO();

                    //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                    compra.compraVenda.Tipo = "Compra";
                    dao.Insert(compra.compraVenda);
                    return View("../Home/Index");
                }
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

                string Operacao = ViewBag.Operacao = "I";
                ValidaDados(venda.compraVenda);
                if (ModelState.IsValid == false)
                {
                    PreparaListaFornecedorParaCombo();
                    return View("../CompraVenda/CadastroVenda");
                }
                else
                {
                    CompraVendaDAO dao = new CompraVendaDAO();
                    //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                    venda.compraVenda.Tipo = "Venda";
                    PreparaListaFornecedorParaCombo();
                    dao.Insert(venda.compraVenda);
                    return View("../Home/Index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        private void ValidaDados(CompraVendaViewModel compraVenda)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net
            FornecedorDAO dao = new FornecedorDAO();
            if (string.IsNullOrEmpty(compraVenda.CodigoCliente))
                ModelState.AddModelError("compraVenda.CodigoCliente", "Obrigatório informar o Código do Cliente.");

            if (string.IsNullOrEmpty(compraVenda.CodigoFornecedor) || compraVenda.CodigoFornecedor == "0")
                ModelState.AddModelError("compraVenda.CodigoFornecedor", "Obrigatório informar  o Código do Fornecedor.");

            if (string.IsNullOrEmpty(compraVenda.CodigoProduto))
                ModelState.AddModelError("compraVenda.CodigoProduto", "Obrigatório informar o o Código do Produto.");

            if (string.IsNullOrEmpty(compraVenda.CodigoUsuario))
                ModelState.AddModelError("compraVenda.CodigoUsuario", "Obrigatório informar o Código do Usuário.");

            if (string.IsNullOrEmpty(compraVenda.Data))
                ModelState.AddModelError("compraVenda.Data", "Obrigatório informar a Data.");

            if (string.IsNullOrEmpty(compraVenda.Quantidade) || int.Parse(compraVenda.Quantidade) < 0)
                ModelState.AddModelError("compraVenda.Quantidade", "Obrigatório informar uma Quantidade válido.");
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