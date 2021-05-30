using ControleDeEstoque.DAO;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Controllers
{
    public class ProdutoController : DefaultController
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
            MainViewModel mainViewModel = new MainViewModel();
            return View("CadastroProduto", mainViewModel);
        }

        public IActionResult ListagemProduto()
        {
            return View("ListagemProduto");
        }

        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }

        public IActionResult CadastrarProduto(ProdutoViewModel produto)
        {
            try
            {
                // ValidaDados(curriculo, Operacao);

                ProdutoDAO dao = new ProdutoDAO();

                produto.ImagemEmByte = ConvertImageToByte(produto.Imagem);

                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                dao.Insert(produto);
                return View("../Home/Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

    }
}