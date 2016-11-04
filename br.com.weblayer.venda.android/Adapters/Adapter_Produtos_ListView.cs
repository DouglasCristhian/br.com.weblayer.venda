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
    public class Adapter_Produtos_ListView : BaseAdapter<Produto>
    {
        public List<Produto> mItems;
        private Context mContext;

        public Adapter_Produtos_ListView(Context context, List<Produto> items)
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

        public override Produto this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_Produtos_ListView, null, false);
            }

            row.FindViewById<TextView>(Resource.Id.txtCodigoProduto).Text = mItems[position].ds_CodigoProduto.ToString();
            row.FindViewById<TextView>(Resource.Id.txtNomeProduto).Text = mItems[position].ds_NomeProduto.ToString();
            row.FindViewById<TextView>(Resource.Id.txtUniMedidaProduto).Text = mItems[position].ds_UniMedidaProduto.ToString();
            row.FindViewById<TextView>(Resource.Id.txtTblPrecoProduto).Text = mItems[position].ds_TblPrecoProduto.ToString();

            return row;
        }
    }
}