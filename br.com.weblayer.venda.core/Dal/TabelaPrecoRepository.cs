using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Dal
{
    public class TabelaPrecoRepository
    {
        public string Mensage { get; set; }

        public void Save(TabelaPreco entidade)
        {

            try
            {
                if (entidade.id>0)
                    Database.GetConnection().Update(entidade);
                else
                    Database.GetConnection().Insert(entidade);
            }
            catch (Exception e)
            {
                Mensage = $"Falha ao Inserir a entidade {entidade.GetType()}. Erro: {e.Message}";
            }
        }

        public void Delete(TabelaPreco entidade)
        {
            Database.GetConnection().Delete(entidade);
        }

        public IList<TabelaPreco> List()
        {
            
            return Database.GetConnection().Table<TabelaPreco>().ToList();
             
        }

        public void MakeDataMock()
        {
            if (List().Count > 0)
                return;

            Save(new TabelaPreco() { id_Codigo = "11", ds_Descricao = "TABELA 1", Valor = 5.00, DescontoMaximo = 5 });
            Save(new TabelaPreco() { id_Codigo = "22", ds_Descricao = "TABELA 2", Valor = 12.00, DescontoMaximo = 3 });
            Save(new TabelaPreco() { id_Codigo = "33", ds_Descricao = "TABELA 3", Valor = 7.00, DescontoMaximo = 4 });
            Save(new TabelaPreco() { id_Codigo = "44", ds_Descricao = "TABELA 4", Valor = 9.00, DescontoMaximo = 3 });
        }

    }
}