using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class ClienteViewModel : PadraoDadosViewModel
    {
        public string Senha { get; set; }
        public string DataNascimento { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
    }
}
