using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.android.Adapters
{
    public class Adapter_Produtos_ListView : BaseAdapter<Produto>//, IFilterable
    {
        public IList<Produto> mItems;
        private Context mContext;

        public Adapter_Produtos_ListView(Context context, IList<Produto> items)
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

            row.FindViewById<TextView>(Resource.Id.txtCodigoProduto).Text = mItems[position].id_codigo.ToString();
            row.FindViewById<TextView>(Resource.Id.txtNomeProduto).Text = mItems[position].ds_nome.ToString();
            row.FindViewById<TextView>(Resource.Id.txtUniMedidaProduto).Text = mItems[position].ds_unimedida.ToString();
            row.FindViewById<TextView>(Resource.Id.txtTabPreco).Text = mItems[position].id_tabpreco.ToString();
            row.FindViewById<TextView>(Resource.Id.txtValorProduto).Text = mItems[position].vl_Valor.ToString();

            return row;
        }

        private void Filtrar(SearchView letra)
        {
            letra.QueryTextChange += Letra_QueryTextChange;
        }

        private void Letra_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            
        }
    }
}