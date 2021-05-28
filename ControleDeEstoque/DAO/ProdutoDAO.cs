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
        //public void Inserir(ProdutoViewModel produto)
        //{
        //    HelperDAO.ExecutaProc("sp_Insert_Produtos", CriaParametros(produto));
        //}
        //public void Alterar(ProdutoViewModel produto)
        //{
        //    HelperDAO.ExecutaProc("sp_Update_Produtos", CriaParametros(produto));
        //}
        //private SqlParameter[] CriaParametros(ProdutoViewModel produto)
        //{
        //    SqlParameter[] parametros = new SqlParameter[8];
        //    parametros[0] = new SqlParameter("CodProduto", produto.Codigo);
        //    parametros[1] = new SqlParameter("CorProduto", produto.Cor);
        //    parametros[2] = new SqlParameter("TipoProduto", produto.Tipo);
        //    parametros[3] = new SqlParameter("TamanhoProduto", produto.Tamanho);
        //    parametros[4] = new SqlParameter("DescricaoProduto", produto.Descricao);
        //    parametros[5] = new SqlParameter("QuantidadeDisponivelProduto", produto.Quantidade);
        //    parametros[6] = new SqlParameter("CodFornecedor", produto.CodigoFornecedor);
        //    parametros[7] = new SqlParameter("FotoProduto", produto.Imagem);

        //    return parametros;
        //}
        //public void Excluir(int id)
        //{
        //    //SqlParameter[] p = { new SqlParameter("id", id,) };
        //    //HelperDAO.ExecutaProc("sp_DeleteDado", p);
        //}
        //private ProdutoViewModel MontaModel(DataRow registro)
        //{
           
        //}

        //public ProdutoViewModel Consulta(int id)
        //{
        //    SqlParameter[] p = { new SqlParameter("id", id) };
        //    DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaAluno", p);
        //    if (tabela.Rows.Count == 0)
        //        return null;
        //    else
        //        return MontaProduto(tabela.Rows[0]);
        //}
        //public List<ProdutoViewModel> Listagem()
        //{
        //    List<ProdutoViewModel> lista = new List<ProdutoViewModel>();

        //    var p = new SqlParameter[]
        //   {
        //        new SqlParameter("", "Produto"),
        //   };

        //    DataTable tabela = HelperDAO.ExecutaProcSelect("sp_Listar", p);
        //    foreach (DataRow registro in tabela.Rows)
        //        lista.Add(MontaProduto(registro));
        //    return lista;
        //}

        protected override SqlParameter[] CriaParametros(ProdutoViewModel produto)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("CorProduto", produto.Cor);
            parametros[1] = new SqlParameter("TipoProduto", produto.Tipo);
            parametros[2] = new SqlParameter("TamanhoProduto", produto.Tamanho);
            parametros[3] = new SqlParameter("DescricaoProduto", produto.Descricao);
            parametros[4] = new SqlParameter("QuantidadeDisponivelProduto", produto.Quantidade);
            parametros[5] = new SqlParameter("CodFornecedor", produto.CodigoFornecedor);
          //  parametros[6] = new SqlParameter("FotoProduto", produto.Imagem);

            return parametros;
        }

        protected override ProdutoViewModel MontaModel(DataRow registro)
        {
            ProdutoViewModel p = new ProdutoViewModel();
            p.Tipo = Convert.ToInt32(registro["TipoProduto"]);
            p.Cor = registro["CorProduto"].ToString();
            p.Tamanho = registro["TamanhoProduto"].ToString();
            p.Descricao = registro["DescricaoProduto"].ToString();
            p.Quantidade = registro["QuantidadeProduto"].ToString();
            p.CodigoFornecedor = Convert.ToInt32(registro["CodigoFornecedorProduto"]);
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
