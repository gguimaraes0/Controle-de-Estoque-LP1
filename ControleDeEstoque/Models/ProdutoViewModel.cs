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
        public int CodigoFornecedor { get; set; }

        /// <summary>
        /// Imagem recebida do form pelo controller
        /// </summary>
        public IFormFile Imagem { get; set; }
        /// <summary>
        /// Imagem em bytes pronta para ser salva
        /// </summary>
        public byte[] ImagemEmByte { get; set; }
        /// <summary>
        /// Imagem usada para ser enviada ao form no formato para ser exibida
        /// </summary>
        public string ImagemEmBase64
        {
            get
            {
                if (ImagemEmByte != null)
                    return Convert.ToBase64String(ImagemEmByte);
                else
                    return string.Empty;
            }
        }
    }
}
