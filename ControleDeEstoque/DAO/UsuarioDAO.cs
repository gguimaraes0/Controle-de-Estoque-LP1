using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public class UsuarioDAO : PadraoDAO<UsuarioViewModel>
    {
        protected override SqlParameter[] CriaParametros(UsuarioViewModel usuario)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("Usu", usuario.Nome);
            parametros[1] = new SqlParameter("TipoProduto", usuario.Email);
            parametros[2] = new SqlParameter("TamanhoProduto", usuario.CEP);
            parametros[3] = new SqlParameter("DescricaoProduto", usuario.Numero);
            parametros[4] = new SqlParameter("QuantidadeDisponivelProduto", usuario.Telefone);
            parametros[5] = new SqlParameter("CodFornecedor", usuario.Senha);
            parametros[5] = new SqlParameter("CodFornecedor", usuario.Complemento);
      
            //  parametros[6] = new SqlParameter("FotoProduto", produto.Imagem);

            return parametros;
        }
        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            ProdutoViewModel p = new ProdutoViewModel();
            p.Tipo = Convert.ToInt32(registro["TipoProduto"]);
            p.Cor = registro["CorProduto"].ToString();
            p.Tamanho = registro["TamanhoProduto"].ToString();
            p.Descricao = registro["DescricaoProduto"].ToString();
            p.Quantidade = registro["QuantidadeProduto"].ToString();
            p.CodigoFornecedor = Convert.ToInt32(registro["CodigoFornecedorProduto"]);
            //     p.Imagem = registro["FotoProduto"].ToString();

            return null;
        }

        protected override void SetTabela()
        {
            Tabela = "Produtos";
            NomeSpListagem = "spListagemProdutos";
        }
    }
}
