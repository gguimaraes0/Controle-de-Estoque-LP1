using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class CorProdutoViewModel : PadraoViewModel
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
