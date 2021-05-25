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

        /// <summary>
        /// Executa uma instrução
        /// </summary>
        /// <param name="sql">instrução SQL</param>
        /// <param name="parametros">parametros da instrução SQL</param>
        public static void ExecutaSQL(String sql,
                          SqlParameter[] parametros)
        {
            using (var cx = ConexaoDB.GetConexao())
            {
                try
                {
                    using (var cmd = new SqlCommand(sql, cx))
                    {
                        if (parametros != null)
                            cmd.Parameters.AddRange(parametros);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    var debug = ex.Message;
                }
            }
        }
        /// <summary>
        /// Executa uma instrução
        /// </summary>
        /// <param name="sql">instrução SQL</param>
        public static void ExecutaSQL(String sql)
        {
            using (var cx = ConexaoDB.GetConexao())
            {
                try
                {
                    using (var cmd = new SqlCommand(sql, cx))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    var debug = ex.Message;
                }
            }
        }


        /// <summary>
        /// Executa uma instrução Select
        /// </summary>
        /// <param name="sql">instrução SQL</param>
        /// <returns>DataTable com os dados da instrução SQL</returns>
        public static DataTable ExecutaSelect(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    DataTable tabelaTemp = new DataTable();
                    adapter.Fill(tabelaTemp);
                    conexao.Close();
                    return tabelaTemp;
                }
            }
        }
        /// <summary>
        /// Executa uma instrução Select
        /// </summary>
        /// <param name="sql">instrução SQL</param>
        /// <returns>DataTable com os dados da instrução SQL</returns>
        public static DataTable ExecutaSelect(string sql)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    DataTable tabelaTemp = new DataTable();
                    adapter.Fill(tabelaTemp);
                    conexao.Close();
                    return tabelaTemp;
                }
            }
        }
    }
}
