using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public class ClienteDAO : PadraoDAO<ClienteViewModel>
    {
        
        public List<ClienteViewModel> Listagem()
        {
            List<ClienteViewModel> lista = new List<ClienteViewModel>();
            //string sql = "select * from Usuarios";
            //DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            var tabela = HelperDAO.ExecutaProcSelect("spListagemCliente", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaCliente(registro));
            return lista;
        }

        private ClienteViewModel MontaCliente(DataRow registro)
        {
            ClienteViewModel u = new ClienteViewModel();
            u.CEP = registro["CEPCliente"].ToString();
            u.CNPJ = registro["CNPJCliente"].ToString();
            u.Codigo= registro["CodCliente"].ToString();
            u.Complemento = registro["ComplementoCliente"].ToString();
            u.CPF= registro["CPFCliente"].ToString();
            u.DataNascimento = registro["Data_NascimentoCliente"].ToString();
            u.Email= registro["EmailCliente"].ToString();
            u.Nome = registro["NomeCliente"].ToString();
            u.Numero= registro["NumeroCliente"].ToString();
            u.Telefone= registro["TelefoneCliente"].ToString();

            return u;
        }
        protected override SqlParameter[] CriaParametros(ClienteViewModel cliente)
        {
            SqlParameter[] parametros = new SqlParameter[9];
            parametros[0] = new SqlParameter("EmailCliente", cliente.Email);
            parametros[1] = new SqlParameter("NomeCliente", cliente.Nome);
            parametros[2] = new SqlParameter("Data_NascimentoCliente", cliente.DataNascimento);
            parametros[3] = new SqlParameter("NumeroCliente", cliente.Numero);
            parametros[4] = new SqlParameter("ComplementoCliente", cliente.Complemento);
            parametros[5] = new SqlParameter("TelefoneCliente", cliente.Telefone);
            parametros[6] = new SqlParameter("CEPCliente", cliente.CEP);
            if (cliente.CPF != null)
            {
                parametros[7] = new SqlParameter("CPFCliente", cliente.CPF);
                parametros[8] = new SqlParameter("CNPJCliente", "");

            }
            else
            {
                parametros[7] = new SqlParameter("CNPJCliente", cliente.CNPJ);
                parametros[8] = new SqlParameter("CPFCliente", "");

            }

            return parametros;
        }
        protected override ClienteViewModel MontaModel(DataRow registro)
        {
            ClienteViewModel U = new ClienteViewModel();

            if (U.Email != null)
                U.Email = registro["EmailCliente"].ToString();
            if (U.Nome != null)
                U.Nome = registro["NomeCliente"].ToString();
            if (U.DataNascimento != null)
                U.DataNascimento = registro["Data_NascimentoCliente"].ToString();
            if (U.Numero != null)
                U.Numero = registro["NumeroCliente"].ToString();
            if (U.Email != null)
                U.Email = registro["ComplementoCliente"].ToString();
            if (U.Telefone != null)
                U.Telefone = registro["TelefoneCliente"].ToString();
            if (U.CEP != null)
                U.CEP = registro["CEPCliente"].ToString();
            if (U.CNPJ != null)
                U.CNPJ = registro["CNPJCliente"].ToString();
            if (U.CPF != null)
                U.CPF = registro["CPFCliente"].ToString();
            return U;
        }
        protected override void SetTabela()
        {
            Tabela = "Clientes";
            NomeSpListagem = "spListagemClientes";
            CodName = "CodCliente";
        }
    }
}
