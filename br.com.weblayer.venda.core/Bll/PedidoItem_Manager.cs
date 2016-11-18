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
using br.com.weblayer.venda.core.Dal;
using System.Collections;

namespace br.com.weblayer.venda.core.Bll
{
    public class PedidoItem_Manager 
    {
        public string Mensagem;

        public IList<PedidoItem> GetPedidoItem(string obj, int id)
        {
            // List<PedidoItem> filtrado = new PedidoItemRepository().List().Where
            // return filtrado;
            return new PedidoItemRepository().List();
        }

        public void Save(PedidoItem obj)
        {
            var Repository = new PedidoItemRepository();
            Repository.Save(obj);
        }

        public void Delete(PedidoItem obj)
        {
            var Repository = new PedidoItemRepository();
            Repository.Delete(obj);

            Mensagem = $"Pedido {obj.id_pedido} excluído com sucesso";
        }
    }
}