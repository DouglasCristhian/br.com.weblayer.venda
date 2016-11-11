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
    public class PedidoItemRepository
    {
        public string Mensage { get; set; }

        public void Save(PedidoItem entidade)
        {
            try
            {
                if (entidade.id > 0)
                    Database.GetConnection().Update(entidade);
                else
                    Database.GetConnection().Insert(entidade);
            }
            catch (Exception e)
            {
                Mensage = $"Falha ao Inserir a entidade {entidade.GetType()}. Erro: {e.Message}";
            }
        }

        public void Delete(PedidoItem entidade)
        {
            Database.GetConnection().Delete(entidade);
        }

        public IList<PedidoItem> List()
        {
            return Database.GetConnection().Table<PedidoItem>().ToList();
        }

        public void MakeDataMock()
        {
            if (List().Count > 0)
                return;

            Save(new PedidoItem() { id_pedido = 1, id_produto = 01, nr_quantidade = 5, vl_item = 15.00 });
            Save(new PedidoItem() { id_pedido = 2, id_produto = 02, nr_quantidade = 6, vl_item = 10.00 });
            Save(new PedidoItem() { id_pedido = 3, id_produto = 03, nr_quantidade = 7, vl_item = 5.00 });
        }
    }
}