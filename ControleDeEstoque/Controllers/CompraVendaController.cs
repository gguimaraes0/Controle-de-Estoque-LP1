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
            PreparaListaClienteParaCombo();
            PreparaListaProdutoParaCombo();
            PreparaListaUsuarioParaCombo();
            return View("CadastroCompra", mainViewModel);
        }

        public IActionResult CadastroVenda()
        {
            MainViewModel mainViewModel = new MainViewModel();
            PreparaListaFornecedorParaCombo();
            PreparaListaClienteParaCombo();
            PreparaListaProdutoParaCombo();
            PreparaListaUsuarioParaCombo();
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

                    //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                    compra.compraVenda.Tipo = "Compra";

                    PreparaListaFornecedorParaCombo();
                    PreparaListaClienteParaCombo();
                    PreparaListaUsuarioParaCombo();
                    PreparaListaProdutoParaCombo();

                    // dao.Insert(compra.compraVenda);
                    dao.InserirCompra(compra.compraVenda);
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
            if (string.IsNullOrEmpty(compraVenda.CodigoCliente) || compraVenda.CodigoCliente == "0")
                ModelState.AddModelError("compraVenda.CodigoCliente", "Obrigatório informar o Código do Cliente.");

            if (string.IsNullOrEmpty(compraVenda.CodigoFornecedor) || compraVenda.CodigoFornecedor == "0")
                ModelState.AddModelError("compraVenda.CodigoFornecedor", "Obrigatório informar  o Código do Fornecedor.");

            if (string.IsNullOrEmpty(compraVenda.CodigoProduto) || compraVenda.CodigoProduto == "0")
                ModelState.AddModelError("compraVenda.CodigoProduto", "Obrigatório informar o o Código do Produto.");

            if (string.IsNullOrEmpty(compraVenda.CodigoUsuario) || compraVenda.CodigoUsuario == "0")
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

        private void PreparaListaClienteParaCombo()
        {
            ClienteDAO ClienteDao = new ClienteDAO();
            var Clientes = ClienteDao.ListaCliente();
            List<SelectListItem> listaCliente = new List<SelectListItem>();

            listaCliente.Add(new SelectListItem("Selecione um Cliente...", "0"));
            foreach (var Cliente in Clientes)
            {
                SelectListItem item = new SelectListItem(Cliente.Nome, Cliente.Codigo.ToString());
                listaCliente.Add(item);
            }
            ViewBag.Cliente = listaCliente;
        }
        private void PreparaListaUsuarioParaCombo()
        {
            UsuarioDAO UsuarioDao = new UsuarioDAO();
            var Usuarios = UsuarioDao.ListaUsuario();
            List<SelectListItem> listaUsuario = new List<SelectListItem>();

            listaUsuario.Add(new SelectListItem("Selecione o Usuario...", "0"));
            foreach (var Usuario in Usuarios)
            {
                SelectListItem item = new SelectListItem(Usuario.Nome, Usuario.Codigo.ToString());
                listaUsuario.Add(item);
            }
            ViewBag.Usuario = listaUsuario;
        }

        private void PreparaListaProdutoParaCombo()
        {
            ProdutoDAO ProdutoDao = new ProdutoDAO();
            var Produtos = ProdutoDao.ListaProduto();
            List<SelectListItem> listaProduto = new List<SelectListItem>();

            listaProduto.Add(new SelectListItem("Selecione um Produto...", "0"));
            foreach (var Produto in Produtos)
            {
                SelectListItem item = new SelectListItem(Produto.Descricao, Produto.Codigo.ToString());
                listaProduto.Add(item);
            }
            ViewBag.Produto = listaProduto;
        }
    }
}