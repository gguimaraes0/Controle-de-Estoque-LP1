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
        public List<ProdutoViewModel> Listagem()
        {
            List<ProdutoViewModel> lista = new List<ProdutoViewModel>();
            //string sql = "select * from Usuarios";
            //DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            var tabela = HelperDAO.ExecutaProcSelect("spListagemProduto", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaProduto(registro));
            return lista;
        }

        private ProdutoViewModel MontaProduto(DataRow registro)
        {
            ProdutoViewModel u = new ProdutoViewModel();
            u.Codigo = registro["CodProduto"].ToString();
            u.CodigoFornecedor = registro["CodFornecedor"].ToString();
            u.Cor = registro["CorProduto"].ToString();
            u.Descricao = registro["DescricaoProduto"].ToString();
            u.Quantidade = registro["QuantidadeDisponivelProduto"].ToString();
            u.Tamanho = registro["TamanhoProduto"].ToString();
            u.Tipo = registro["TipoProduto"].ToString();

            return u;
        }

        protected override SqlParameter[] CriaParametros(ProdutoViewModel produto)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("CorProduto", produto.Cor);
            parametros[1] = new SqlParameter("TipoProduto", produto.Tipo);
            parametros[2] = new SqlParameter("TamanhoProduto", produto.Tamanho);
            parametros[3] = new SqlParameter("DescricaoProduto", produto.Descricao);
            parametros[4] = new SqlParameter("QuantidadeDisponivelProduto", produto.Quantidade);
            parametros[5] = new SqlParameter("CodFornecedor", produto.CodigoFornecedor);
            //parametros[6] = new SqlParameter("CodFornecedor", produto.CodigoFornecedor);
            // parametros[6] = new SqlParameter("FotoProduto", produto.Imagem);

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
            p.CodigoFornecedor = registro["CodFornecedor"].ToString();
            //p.CodigoFornecedor = Convert.ToInt32(registro["CodigoFornecedorProduto"]);

            return p;
        }

        protected override void SetTabela()
        {
            Tabela = "Produtos";
            NomeSpListagem = "spListagemProdutos";
            CodName = "CodProduto";
        }

        private ProdutoViewModel MontaProdutoSelect(DataRow registro)
        {
            ProdutoViewModel f = new ProdutoViewModel()
            {
                Codigo = registro["CodProduto"].ToString(),

                Descricao = registro["DescricaoProduto"].ToString(),

                Tipo = registro["TipoProduto"].ToString(),

                Cor = registro["CorProduto"].ToString(),

                Tamanho = registro["TamanhoProduto"].ToString(),

                Quantidade = registro["QuantidadeDisponivelProduto"].ToString(),

                CodigoFornecedor = registro["CodFornecedor"].ToString()
            };
            return f;
        }

        public List<ProdutoViewModel> ListaProduto()
        {

            List<ProdutoViewModel> lista = new List<ProdutoViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemProduto", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaProdutoSelect(registro));
            return lista;
        }
    }
}
