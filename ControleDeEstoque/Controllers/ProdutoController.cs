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
    public class ProdutoController : Controller
    {
        private readonly ILogger<ProdutoController> _logger;


        public IActionResult Index()
        {
            ProdutoDAO dao = new ProdutoDAO();
            List<ProdutoViewModel> lista = dao.Listagem();
            return View(lista);
        }

        public ProdutoController(ILogger<ProdutoController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroProduto()
        {
            ProdutoViewModel produto = new ProdutoViewModel();
            return View("CadastroProduto", produto);
        }

        public IActionResult ListagemProduto()
        {
            return View("ListagemProduto");
        }


        //public IActionResult NovoProduto()
        //{
        //    try
        //    {
        //        ProdutoViewModel produto = new ProdutoViewModel();
        //        //incluir

        //        return View("CadastroProduto", produto);
        //    }
        //    catch (Exception erro)
        //    {
        //        return View("Error", new ErrorViewModel(erro.Message));
        //    }

        //}

        public IActionResult CadastrarProduto(ProdutoViewModel produto)
        {
            try
            {
                // ValidaDados(curriculo, Operacao);

                ProdutoDAO dao = new ProdutoDAO();

                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                produto.Codigo = produto.Codigo;
                produto.Imagem = "0x31353136383531363834313638";
                dao.Insert(produto);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
     
    }
}