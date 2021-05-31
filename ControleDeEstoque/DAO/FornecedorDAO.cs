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
        protected override SqlParameter[] CriaParametros(FornecedorViewModel fornecedor)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("CNPJFornecedor", fornecedor.CNPJ);
            parametros[1] = new SqlParameter("NomeFornecedor", fornecedor.Nome);
            parametros[2] = new SqlParameter("EmailFornecedor", fornecedor.Email);
            parametros[3] = new SqlParameter("TelefoneFornecedor", fornecedor.Telefone);
            parametros[4] = new SqlParameter("NumeroFornecedor", fornecedor.Numero);
            parametros[5] = new SqlParameter("ComplementoFornecedor", fornecedor.Telefone);
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
        protected override void SetTabela()
        {
            Tabela = "Fornecedores";
            NomeSpListagem = "spListagemFornecedores";
        }
    }
}
