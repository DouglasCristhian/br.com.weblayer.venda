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
    public class Adapter_Cliente_ListView : BaseAdapter<Cliente>
    {
        public List<Cliente> mItems;
        private Context mContext;

        public Adapter_Cliente_ListView(Context context, List<Cliente> items)
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

        public override Cliente this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_Clientes_ListView, null, false);
            }

            row.FindViewById<TextView>(Resource.Id.txtCodigoCliente).Text = mItems[position].id_Codigo.ToString();
            row.FindViewById<TextView>(Resource.Id.txtRazaoSocial).Text = mItems[position].ds_RazaoSocial.ToString();
            row.FindViewById<TextView>(Resource.Id.txtNomeFantasia).Text = mItems[position].ds_NomeFantasia.ToString();
            row.FindViewById<TextView>(Resource.Id.txtCNPJ).Text = mItems[position].ds_Cnpj.ToString();

            return row;
        }
    }
}