using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class ProdutoViewModel : PadraoViewModel
    {
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public string Descricao { get; set; }
        public string Quantidade { get; set; }
        public string CodigoFornecedor { get; set; }
    }
}
