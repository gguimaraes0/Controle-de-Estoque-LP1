using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    public class ProdutoViewModel : PadraoViewModel
    {
        public int Codigo { get; set; }
        public int Tipo { get; set; }
        public string Cor { get; set; }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        public string Tamanho { get; set; }
        public string Descricao { get; set; }
        public string Quantidade { get; set; }
        public string Imagem { get; set; }
        public int CodigoFornecedor { get; set; }
    }
}
