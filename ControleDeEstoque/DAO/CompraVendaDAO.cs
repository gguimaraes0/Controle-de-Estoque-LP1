using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public class CompraVendaDAO : PadraoDAO<CompraVendaViewModel>
    {
        protected override SqlParameter[] CriaParametros(CompraVendaViewModel compraVenda)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("Tipo", compraVenda.Tipo);
            parametros[1] = new SqlParameter("CodCliente", compraVenda.CodigoCliente);
            parametros[2] = new SqlParameter("CodUsuario", compraVenda.CodigoUsuario);
            parametros[3] = new SqlParameter("CodFornecedor", compraVenda.CodigoFornecedor);
            parametros[4] = new SqlParameter("Quantidade", compraVenda.Quantidade);
            parametros[5] = new SqlParameter("CodProdutos", compraVenda.CodigoProduto);
            parametros[6] = new SqlParameter("Data", compraVenda.Data);
            return parametros;
        }
        protected override CompraVendaViewModel MontaModel(DataRow registro)
        {
            CompraVendaViewModel U = new CompraVendaViewModel();

            U.Tipo = registro["Tipo"].ToString();
            U.CodigoCliente =registro["CodCliente"].ToString();
            U.CodigoUsuario = registro["CodUsuario"].ToString();
            U.CodigoFornecedor = registro["CodFornecedor"].ToString();
            U.Quantidade = registro["Quantidade"].ToString();
            U.CodigoProduto = registro["CodProdutos"].ToString();
            U.Data = registro["Data"].ToString();

            return U;
        }
        protected override void SetTabela()
        {
            Tabela = "Compras_Vendas";
            NomeSpListagem = "spListagemCompras_Vendas";
        }

    }
}
