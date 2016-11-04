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
    public class Adapter_TabelaPreco_ListView : BaseAdapter<TabelaPreco>
    {
        public List<TabelaPreco> mItems;
        private Context mContext;

        public Adapter_TabelaPreco_ListView(Context context, List<TabelaPreco> items)
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

        public override TabelaPreco this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_TabelaPreco_ListView, null, false);
            }

            row.FindViewById<TextView>(Resource.Id.txtCodigoTabelaPreco).Text = mItems[position].id_Codigo.ToString();
            row.FindViewById<TextView>(Resource.Id.txtDescricaoTabelaPreco).Text = mItems[position].ds_Descricao.ToString();
            row.FindViewById<TextView>(Resource.Id.txtValorTabelaPreco).Text = mItems[position].Valor.ToString();
            row.FindViewById<TextView>(Resource.Id.txtDescontoMaxTabelaPreco).Text = mItems[position].DescontoMaximo.ToString();

            return row;
        }
    }
}