using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class FornecedorViewModel : PadraoViewModel
    {
        public string CNPJ { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}
