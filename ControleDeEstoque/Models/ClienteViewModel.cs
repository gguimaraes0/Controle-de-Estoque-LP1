using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class ClienteViewModel : PadraoViewModel
    {
        public string Senha { get; set; }
        public string DataNascimento { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}
