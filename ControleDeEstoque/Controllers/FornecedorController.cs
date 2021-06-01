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
    public class FornecedorController : DefaultController
    {
        private readonly ILogger<FornecedorController> _logger;

        public FornecedorController(ILogger<FornecedorController> logger)
        {
            _logger = logger;
        }

        public IActionResult CadastroFornecedor()
        {
            MainViewModel mainViewModel = new MainViewModel();

            return View("CadastroFornecedor", mainViewModel);
        }

        public IActionResult ListagemFornecedor()
        {
            try
            {
                FornecedorDAO dao = new FornecedorDAO();
                MainViewModel fornecedor = new MainViewModel();
                fornecedor.fornecedores = dao.Listagem();

                return View(fornecedor);
            }
            catch (Exception erro)
            {
                return View("../Home/Index");
            }
        }

        public IActionResult CadastrarFornecedor(FornecedorViewModel fornecedor)
        {
            try
            {
                MainViewModel mainViewModel = new MainViewModel();
                mainViewModel.fornecedor = fornecedor;
                if (fornecedor == null)
                    fornecedor.IsEmpty = true;

                string Operacao = ViewBag.Operacao = "I";
                ValidaDados(fornecedor);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    return View("../Fornecedor/CadastroFornecedor", mainViewModel);
                }
                else
                {
                    //Preencher todos os CPFs para mantê-los iguais na hora de salvar no banco 
                    FornecedorDAO dao = new FornecedorDAO();
                    dao.Insert(fornecedor);
                    return View("../Home/Index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private void ValidaDados(FornecedorViewModel fornecedor)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net
            FornecedorDAO dao = new FornecedorDAO();
            if (string.IsNullOrEmpty(fornecedor.CEP))
                ModelState.AddModelError("fornecedor.CEP", "Obrigatório informar um CEP.");

            if (string.IsNullOrEmpty(fornecedor.CNPJ))
                ModelState.AddModelError("fornecedor.CNPJ", "Obrigatório informar CPF/CNPJ.");

            if (string.IsNullOrEmpty(fornecedor.Complemento))
                ModelState.AddModelError("fornecedor.Complemento", "Obrigatório informar o Complemento.");

            if (string.IsNullOrEmpty(fornecedor.Email))
                ModelState.AddModelError("fornecedor.Email", "Obrigatório informar o Email.");

            if (string.IsNullOrEmpty(fornecedor.Nome))
                ModelState.AddModelError("fornecedor.Nome", "Obrigatório informar o Nome.");

            if (string.IsNullOrEmpty(fornecedor.Numero) || int.Parse(fornecedor.Numero) < 0)
                ModelState.AddModelError("fornecedor.Numero", "Obrigatório informar um número válido.");

            if (string.IsNullOrEmpty(fornecedor.Telefone))
                ModelState.AddModelError("fornecedor.Telefone", "Obrigatório informar o Telefone.");
        }

        public IActionResult Delete(string pk)
        {
            try
            {
                FornecedorDAO dao = new FornecedorDAO();
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