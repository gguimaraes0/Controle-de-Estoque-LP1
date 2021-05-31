using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class MainViewModel
    {
        //public ClienteViewModel clienteViewModel = new ClienteViewModel();
        //public UsuarioViewModel usuarioViewModel = new UsuarioViewModel();
        public MainViewModel()
        {
            usuario = new UsuarioViewModel();
            cliente = new ClienteViewModel();
            compraVenda = new CompraVendaViewModel();
            produto = new ProdutoViewModel();
            fornecedor = new FornecedorViewModel();

            usuarios = new List<UsuarioViewModel>();
            clientes = new List<ClienteViewModel>();
            compraVendas = new List<CompraVendaViewModel>();
            produtos = new List<ProdutoViewModel>();
            fornecedores = new List<FornecedorViewModel>();
        }

        public List<ClienteViewModel> clientes;

        public List<CompraVendaViewModel> compraVendas;

        public List<ProdutoViewModel> produtos;

        public List<FornecedorViewModel> fornecedores;

        public List<UsuarioViewModel> usuarios;
        public UsuarioViewModel usuario { get; set; }
        public ClienteViewModel cliente { get; set; }
        public CompraVendaViewModel compraVenda { get; set; }
        public ProdutoViewModel produto { get; set; }
        public FornecedorViewModel fornecedor { get; set; }
    }
}
