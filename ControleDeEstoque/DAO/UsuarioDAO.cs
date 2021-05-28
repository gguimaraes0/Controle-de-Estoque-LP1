using ControleDeEstoque.Helpers;
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
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("EmailUsuario", usuario.Email);
            parametros[1] = new SqlParameter("SenhaUsuario", usuario.Senha);
            parametros[2] = new SqlParameter("NomeUsuario", usuario.Nome);
            parametros[3] = new SqlParameter("NumeroUsuario", usuario.Numero);
            parametros[4] = new SqlParameter("ComplementoUsuario", usuario.Complemento);
            parametros[5] = new SqlParameter("TelefoneUsuario", usuario.Telefone);
            parametros[6] = new SqlParameter("CEPUsuario", usuario.CEP);
            return parametros;
        }
         protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel U = new UsuarioViewModel();
            U.Email = registro["EmailUsuario"].ToString();
            U.Senha = registro["SenhaUsuario"].ToString();
            U.Nome = registro["NomeUsuario"].ToString();
            U.Numero = registro["NumeroUsuario"].ToString();
            U.Complemento = registro["ComplementoUsuario"].ToString();
            U.Telefone = registro["TelefoneUsuario"].ToString();
            U.CEP = registro["CEPUsuario"].ToString();

            return U;
        }
        protected override void SetTabela()
        {
            Tabela = "Usuarios";
            NomeSpListagem = "spListagemUsuarios";
        }

        public bool LoginJaExiste(string Login)
        {
            string sql = $"select * from tb_Usuarios where EmailUsuario = '{Login}'";
            DataTable tabela = ConexaoDB.ExecutaSelect(sql);
            return tabela.Rows.Count > 0;
        }
        public  UsuarioViewModel RetornaUsuarioLogado(UsuarioViewModel usuario)
        {
            string sql = $"select * from Usuarios WHERE EmailUsuario ='{usuario.Email}' AND SenhaUsuario= '{usuario.Senha}'";
            DataTable tabela = ConexaoDB.ExecutaSelect(sql);
            if (tabela.Rows.Count == 0)
                return null;
            else
            {
                return MontaModel(tabela.Rows[0]);
            }
        }
    }
}
