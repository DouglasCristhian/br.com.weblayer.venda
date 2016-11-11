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

namespace br.com.weblayer.venda.android.Adapters
{
    public class Adapter_PedidoItem_ListView : BaseAdapter<PedidoItem>
    {
        public IList<PedidoItem> mItems;
        private Context mContext;

        public Adapter_PedidoItem_ListView(Context context, IList<PedidoItem> items)
        {
            mItems = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override PedidoItem this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_PedidoItem_ListView, null, false);
            }

            row.FindViewById<TextView>(Resource.Id.txtIdPedido).Text = "Código Pedido: " + mItems[position].id_pedido.ToString();
            row.FindViewById<TextView>(Resource.Id.txtIdProduto).Text = "Código Produto: " + mItems[position].id_produto.ToString();
            row.FindViewById<TextView>(Resource.Id.txtVlItem).Text = "Valor Item: " + mItems[position].vl_item.ToString();
            row.FindViewById<TextView>(Resource.Id.txtQuantidade).Text = "Quantidade Item: " + mItems[position].nr_quantidade.ToString();

            return row;
        }
    }
}