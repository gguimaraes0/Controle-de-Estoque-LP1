﻿using ControleDeEstoque.DAO;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            PreparaListaFornecedorParaCombo();
            PreparaListaTipoParaCombo();
            PreparaListaCorParaCombo();
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
                PreparaListaFornecedorParaCombo();
                PreparaListaTipoParaCombo();
                PreparaListaCorParaCombo();
                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                dao.Insert(produto);
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

        private void PreparaListaCorParaCombo()
        {
            CorProdutoDAO CorProdutoDao = new CorProdutoDAO();
            var CoresProduto = CorProdutoDao.ListaCorProduto();
            List<SelectListItem> listaCorProduto = new List<SelectListItem>();

            listaCorProduto.Add(new SelectListItem("Selecione uma Cor...", "0"));
            foreach (var CorProduto in CoresProduto)
            {
                SelectListItem item = new SelectListItem(CorProduto.Descricao, CorProduto.Codigo.ToString());
                listaCorProduto.Add(item);
            }
            ViewBag.CorProduto = listaCorProduto;
        }

        private void PreparaListaTipoParaCombo()
        {
            TipoProdutoDAO TipoProdutoDao = new TipoProdutoDAO();
            var TiposProdutos = TipoProdutoDao.ListaTipoProduto();
            List<SelectListItem> listaTipoProduto = new List<SelectListItem>();

            listaTipoProduto.Add(new SelectListItem("Selecione o Tipo...", "0"));
            foreach (var TipoProduto in TiposProdutos)
            {
                SelectListItem item = new SelectListItem(TipoProduto.Descricao, TipoProduto.Codigo.ToString());
                listaTipoProduto.Add(item);
            }
            ViewBag.TipoProduto = listaTipoProduto;
        }
    }
}