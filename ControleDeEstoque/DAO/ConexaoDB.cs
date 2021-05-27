using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Helpers
{
    public class ConexaoDB
    {

        /// <summary>
        /// Retorna a conexão sql
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConexao()
        {
            string strCon = "";//@"Data Source=LOCALHOST\SQL2017;Initial Catalog=N2;integrated security=true";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
