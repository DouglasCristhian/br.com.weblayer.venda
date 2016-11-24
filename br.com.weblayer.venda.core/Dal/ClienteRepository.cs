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
    public class ClienteRepository
    {
        public string Mensage { get; set; }

        public IList<Cliente> GetBytabPreco(int id_tabpreco)
        {
            return Database.GetConnection().Table<Cliente>().Where(x => x.id_TabelaPreco == id_tabpreco).ToList();
            //return Database.GetConnection().Table<Cliente>().ToList();
        }

        public void Save(Cliente entidade)
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

        public void Delete(Cliente entidade)
        {
            Database.GetConnection().Delete(entidade);
        }

        public IList<Cliente> List()
        {
            
            return Database.GetConnection().Table<Cliente>().ToList();
             
        }

        public void MakeDataMock()
        {
            if (List().Count > 0)
                return;

            Save(new Cliente() { id_Codigo = "1", ds_NomeFantasia = "UNITY SISTEMAS", ds_RazaoSocial = "XPTO SOFTWARE", ds_Cnpj = "456824535", id_TabelaPreco = 11});
            Save(new Cliente() { id_Codigo = "2", ds_NomeFantasia = "INVISIBLE TUCS", ds_RazaoSocial = "TPA ONIX", ds_Cnpj = "564545787", id_TabelaPreco = 22 });       
        }
    }
}