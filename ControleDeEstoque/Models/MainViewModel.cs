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
        }

        public UsuarioViewModel usuario { get; set; }
        public ClienteViewModel cliente { get; set; }
        public CompraVendaViewModel compraVenda { get; set; }
        public ProdutoViewModel produto { get; set; }
        public FornecedorViewModel fornecedor { get; set; }
    }
}
