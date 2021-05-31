using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public class CorProdutoDAO
    {
        private CorProdutoViewModel MontaCorProdutoSelect(DataRow registro)
        {
            CorProdutoViewModel f = new CorProdutoViewModel()
            {
                Codigo = registro["CodCores"].ToString(),

                Descricao = registro["Cor"].ToString()
            };
            return f;
        }

        public List<CorProdutoViewModel> ListaCorProduto()
        {

            List<CorProdutoViewModel> lista = new List<CorProdutoViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemCor", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaCorProdutoSelect(registro));
            return lista;
        }
    }
}
