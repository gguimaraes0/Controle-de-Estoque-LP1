using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class CompraVendaViewModel : PadraoViewModel
    {
        public int CodigoFornecedor { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoUsuario { get; set; }
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public string Data { get; set; }
        public int CodigoProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
