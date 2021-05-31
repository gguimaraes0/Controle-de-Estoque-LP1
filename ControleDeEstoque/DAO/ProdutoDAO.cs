using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public class ProdutoDAO : PadraoDAO<ProdutoViewModel>
    {
    
        protected override SqlParameter[] CriaParametros(ProdutoViewModel produto)
        {

           object imgByte = produto.ImagemEmByte;
            if (imgByte == null)
                imgByte = 0;

            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("CorProduto", produto.Cor);
            parametros[1] = new SqlParameter("TipoProduto", produto.Tipo);
            parametros[2] = new SqlParameter("TamanhoProduto", produto.Tamanho);
            parametros[3] = new SqlParameter("DescricaoProduto", produto.Descricao);
            parametros[4] = new SqlParameter("QuantidadeDisponivelProduto", produto.Quantidade);
            parametros[5] = new SqlParameter("FotoProduto", imgByte);
            parametros[6] = new SqlParameter("CodFornecedor", produto.CodigoFornecedor);
            //  parametros[6] = new SqlParameter("FotoProduto", produto.Imagem);

            return parametros;
        }

        protected override ProdutoViewModel MontaModel(DataRow registro)
        {
            ProdutoViewModel p = new ProdutoViewModel();
            p.Tipo = registro["TipoProduto"].ToString();
            p.Cor = registro["CorProduto"].ToString();
            p.Tamanho = registro["TamanhoProduto"].ToString();
            p.Descricao = registro["DescricaoProduto"].ToString();
            p.Quantidade = registro["QuantidadeProduto"].ToString();
            p.CodigoFornecedor = Convert.ToInt32(registro["CodigoFornecedorProduto"]);
            if (registro["FotoProduto"] != DBNull.Value)
                p.ImagemEmByte = registro["FotoProduto"] as byte[];
            //     p.Imagem = registro["FotoProduto"].ToString();

            return p;
        }

        protected override void SetTabela()
        {
            Tabela = "Produtos";
            NomeSpListagem = "spListagemProdutos";
        }
    }
}
