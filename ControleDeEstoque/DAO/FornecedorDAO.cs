using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public class FornecedorDAO : PadraoDAO<FornecedorViewModel>
    {
        public List<FornecedorViewModel> Listagem()
        {
            List<FornecedorViewModel> lista = new List<FornecedorViewModel>();
            //string sql = "select * from Usuarios";
            //DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            var tabela = HelperDAO.ExecutaProcSelect("spListagemFornecedor", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaFornecedor(registro));
            return lista;
        }

        private FornecedorViewModel MontaFornecedor(DataRow registro)
        {
            FornecedorViewModel u = new FornecedorViewModel();
            u.Codigo = registro["CodFornecedor"].ToString();
            u.CEP = registro["CEPFornecedor"].ToString();
            u.CNPJ = registro["CNPJFornecedor"].ToString();
            u.Complemento = registro["ComplementoFornecedor"].ToString();
            u.Email = registro["EmailFornecedor"].ToString();
            u.Nome = registro["NomeFornecedor"].ToString();
            u.Numero = registro["NumeroFornecedor"].ToString();
            u.Telefone = registro["TelefoneFornecedor"].ToString();

            return u;
        }
        protected override SqlParameter[] CriaParametros(FornecedorViewModel fornecedor)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("CNPJFornecedor", fornecedor.CNPJ);
            parametros[1] = new SqlParameter("NomeFornecedor", fornecedor.Nome);
            parametros[2] = new SqlParameter("EmailFornecedor", fornecedor.Email);
            parametros[3] = new SqlParameter("TelefoneFornecedor", fornecedor.Telefone);
            parametros[4] = new SqlParameter("NumeroFornecedor", fornecedor.Numero);
            parametros[5] = new SqlParameter("ComplementoFornecedor", fornecedor.Complemento);
            parametros[6] = new SqlParameter("CEPFornecedor", fornecedor.CEP);
            return parametros;
        }
        protected override FornecedorViewModel MontaModel(DataRow registro)
        {
            FornecedorViewModel f = new FornecedorViewModel();

            if (f.CNPJ.ToString() != null)
                f.CNPJ = registro["CNPJFornecedor"].ToString();
            if (f.Nome != null)
                f.Nome = registro["NomeFornecedor"].ToString();
            if (f.Email != null)
                f.Email = registro["EmailFornecedor"].ToString();
            if (f.Telefone != null)
                f.Telefone = registro["TelefoneFornecedor"].ToString();
            if (f.Numero != null)
                f.Numero = registro["NumeroFornecedor"].ToString();
            if (f.Complemento != null)
                f.Complemento = registro["ComplementoFornecedor"].ToString();
            if (f.CEP != null)
                f.CEP = registro["CEPFornecedor"].ToString();

            return f;
        }

        private FornecedorViewModel MontaFornecedorSelect(DataRow registro)
        {
            FornecedorViewModel f = new FornecedorViewModel()
            {
                Codigo = registro["CodFornecedor"].ToString(),

                CNPJ = registro["CNPJFornecedor"].ToString(),
            
                Nome = registro["NomeFornecedor"].ToString(),
          
                Email = registro["EmailFornecedor"].ToString(),
          
                Telefone = registro["TelefoneFornecedor"].ToString(),
          
                Numero = registro["NumeroFornecedor"].ToString(),
           
                Complemento = registro["ComplementoFornecedor"].ToString(),

                CEP = registro["CEPFornecedor"].ToString()
            };
            return f;
        }
        protected override void SetTabela()
        {
            Tabela = "Fornecedores";
            NomeSpListagem = "spListagemFornecedores";
            CodName = "CodFornecedor";
        }
        public List<FornecedorViewModel> ListaFornecedor()
        {
          
            List<FornecedorViewModel> lista = new List<FornecedorViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemFornecedor", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaFornecedorSelect(registro));
            return lista;
        }
    }
}
