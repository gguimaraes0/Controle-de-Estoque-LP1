﻿using ControleDeEstoque.DAO;
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
            return View("CadastroCompra", mainViewModel);
        }

        public IActionResult CadastroVenda()
        {
            MainViewModel mainViewModel = new MainViewModel();
            return View("CadastroVenda", mainViewModel);
        }

        public IActionResult ListagemCompraVenda()
        {
            return View("ListagemCompraVenda");
        }

        public IActionResult CadastrarCompra(CompraVendaViewModel compra)
        {
            try
            {
                // ValidaDados(curriculo, Operacao);

                CompraVendaDAO dao = new CompraVendaDAO();

                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                compra.Tipo = "Compra";
                compra.Data = DateTime.Now.ToString();
                dao.Insert(compra);
                return View("../Home/Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult CadastrarVenda(UsuarioViewModel usuario)
        {
            try
            {
                // ValidaDados(curriculo, Operacao);

                UsuarioDAO dao = new UsuarioDAO();

                //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                usuario.Codigo = usuario.Codigo;
                dao.Insert(usuario);
                return View("../Home/Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}