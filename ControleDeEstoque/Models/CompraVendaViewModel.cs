using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class CompraVendaViewModel : PadraoViewModel
    {
        public string CodigoFornecedor { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoUsuario { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public string Data { get; set; }
        public string CodigoProduto { get; set; }
        public string Quantidade { get; set; }
    }
}
