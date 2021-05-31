using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public class TipoProdutoDAO 
    {
        private TiposProdutosViewModel MontaTiposProdutosSelect(DataRow registro)
        {
            TiposProdutosViewModel f = new TiposProdutosViewModel()
            {
                Codigo = registro["CodTipo"].ToString(),

                Descricao = registro["Descricao"].ToString()
            };
            return f;
        }

        public List<TiposProdutosViewModel> ListaTipoProduto()
        {

            List<TiposProdutosViewModel> lista = new List<TiposProdutosViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemTipo", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaTiposProdutosSelect(registro));
            return lista;
        }
    }
}
