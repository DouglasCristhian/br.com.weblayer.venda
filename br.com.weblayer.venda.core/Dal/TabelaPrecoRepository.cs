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

        public TabelaPreco Get(int id)
        {
            return Database.GetConnection().Table<TabelaPreco>().Where(x => x.id == id).FirstOrDefault();
        }

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
            var clientes = new ClienteRepository().GetBytabPreco(entidade.id);
            if (clientes.Count > 0)
            {
                throw new Exception("Tabela de preço não pode ser excluída pois existem clientes vinculados a ela!");
            }

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
        }

    }
}