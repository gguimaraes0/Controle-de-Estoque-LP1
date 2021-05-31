using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.DAO
{
    public abstract class PadraoDAO<T> where T : PadraoViewModel
    {
        public PadraoDAO()
        {
            SetTabela();
        }
        protected string Tabela { get; set; }
        protected string NomeSpListagem { get; set; } = "spListagem";
        protected abstract SqlParameter[] CriaParametros(T model);
        protected abstract T MontaModel(DataRow registro);
        protected abstract void SetTabela();


        public virtual void Insert(T model)
        {
            HelperDAO.ExecutaProc("spInsert_" + Tabela, CriaParametros(model));
        }
        public virtual void Update(T model)
        {
            HelperDAO.ExecutaProc("sp_Update_" + Tabela, CriaParametros(model));
        }
        public virtual void Delete(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            HelperDAO.ExecutaProc("spDelete", p);
        }
        public virtual T Consulta(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("sp_ConsultaDado_FiltroID", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }
        //public virtual int ProximoId()
        //{
        //    var p = new SqlParameter[]
        //    {
        //        new SqlParameter("tabela", Tabela)
        //    };
        //    var tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
        //    return Convert.ToInt32(tabela.Rows[0][0]);
        //}

        // 1 é o primeiro campo da tabela,
        // ou seja, a chave primária
        public virtual List<T> Listagem(string ordem = "1")
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela),
                new SqlParameter("Ordem", ordem)

            };
            var tabela = HelperDAO.ExecutaProcSelect(NomeSpListagem, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }

    }
}
