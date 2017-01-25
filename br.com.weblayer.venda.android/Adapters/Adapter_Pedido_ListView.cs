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
    public class Adapter_Pedido_ListView : BaseAdapter<Pedido>
    {
        public IList<Pedido> mItems;
        private Context mContext;

        public Adapter_Pedido_ListView(Context context, IList<Pedido> items)
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

        public override Pedido this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_Pedido_ListView, null, false);
            }

            row.FindViewById<TextView>(Resource.Id.txtId_Pedido).Text = "Código Pedido: " + mItems[position].id_codigo.ToString();
            row.FindViewById<TextView>(Resource.Id.txtData_Emissao).Text = "Data de Emissão " + mItems[position].dt_emissao.Value.ToString("dd/MM/yyyy"); //dt_emissao.ToString();
            row.FindViewById<TextView>(Resource.Id.txtId_Cliente).Text = "Código Cliente: " + mItems[position].id_cliente.ToString();
            row.FindViewById<TextView>(Resource.Id.txtId_Vendedor).Text = "Código Vendedor: " + mItems[position].id_vendedor.ToString();
            row.FindViewById<TextView>(Resource.Id.txtValor_Total).Text = "Valor Total: " + mItems[position].vl_total.ToString();
            row.FindViewById<TextView>(Resource.Id.txt_Observacao).Text = "Observação: " + mItems[position].ds_observacao.ToString();

            return row;
        }
    }
}