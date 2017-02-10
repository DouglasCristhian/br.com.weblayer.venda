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

            row.FindViewById<TextView>(Resource.Id.txtIdProdutoPedidoItem).Text = "Produto: " + mItems[position].ds_produto.ToString();
            row.FindViewById<TextView>(Resource.Id.txtValorPedidoItem).Text = "Valor Unitário: " + mItems[position].vl_Lista.ToString("##,##0.00");
            row.FindViewById<TextView>(Resource.Id.txtQuantidadePedidoItem).Text = "Quantidade Item: " + mItems[position].nr_quantidade.ToString();
            double go = double.Parse(mItems[position].nr_quantidade.ToString()) * double.Parse(mItems[position].vl_Venda.ToString());
            row.FindViewById<TextView>(Resource.Id.txtValorTotalPedidoItem).Text = "Total: " + go.ToString("##,##0.00");

            return row;
        }
    }
}