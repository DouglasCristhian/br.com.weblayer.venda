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

namespace br.com.weblayer.venda.core.Bll
{
    public class PedidoItem_Manager 
    {
        public string Mensagem;

        public IList<PedidoItem> GetPedidoItem()
        {
            return new PedidoItemRepository().List();
        }

        public void Save(Pedido obj)
        {


            var Repository = new PedidoRepository();
            Repository.Save(obj);


        }

    }
}