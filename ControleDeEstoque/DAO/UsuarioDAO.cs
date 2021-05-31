using ControleDeEstoque.Helpers;
using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public class UsuarioDAO : PadraoDAO<UsuarioViewModel>
    {
        public List<UsuarioViewModel> Listagem()
        {
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();
            //string sql = "select * from Usuarios";
            //DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            var tabela = HelperDAO.ExecutaProcSelect("spListagemUsuario",null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuario(registro));
            return lista;
        }

        private UsuarioViewModel MontaUsuario(DataRow registro)
        {
            UsuarioViewModel u = new UsuarioViewModel();
            u.Nome = registro["NomeUsuario"].ToString();
            u.CEP = registro["CEPUsuario"].ToString();
            u.Complemento = registro["ComplementoUsuario"].ToString();
            u.Email = registro["EmailUsuario"].ToString();
            u.Telefone = registro["TelefoneUsuario"].ToString();
            u.Numero = registro["NumeroUsuario"].ToString();
            u.Codigo = registro["CodUsuario"].ToString();
            u.Senha = registro["SenhaUsuario"].ToString();

            return u;
        }

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

            if(U.Email != null)
              U.Email = registro["EmailUsuario"].ToString();
            if (U.Senha != null)
                U.Senha = registro["SenhaUsuario"].ToString();
            if (U.Nome != null)
                U.Nome = registro["NomeUsuario"].ToString();
            if (U.Numero != null)
                U.Numero = registro["NumeroUsuario"].ToString();
            if (U.Email != null)
                U.Email = registro["ComplementoUsuario"].ToString();
            if (U.Telefone != null)
                U.Telefone = registro["TelefoneUsuario"].ToString();
            if (U.CEP != null)
                U.CEP = registro["CEPUsuario"].ToString();

            return U;
        }
        protected override void SetTabela()
        {
            Tabela = "Usuarios";
            NomeSpListagem = "spListagemUsuarios";
        }

        public UsuarioViewModel ValidaLogin(UsuarioViewModel usuario)
        {
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaLogin", CriaParametrosLogin(usuario));
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        protected SqlParameter[] CriaParametrosLogin(UsuarioViewModel usuario)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("EmailUsuario", usuario.Email);
            parametros[1] = new SqlParameter("SenhaUsuario", usuario.Senha);

            return parametros;
        }
    }
}
