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
    public class ProdutoRepository
    {
        public string Mensage { get; set; }

        public void Save(Produto entidade)
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


        public void Delete(Produto entidade)
        {
            Database.GetConnection().Delete(entidade);
        }

        public IList<Produto> List()
        {
            
            return Database.GetConnection().Table<Produto>().ToList();
             
        }

    }
}