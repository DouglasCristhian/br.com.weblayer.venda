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
    public class Adapter_ProdTabelaPreco_ListView : BaseAdapter<ProdutoTabelaPreco>
    {
        public IList<ProdutoTabelaPreco> mitems;
        public Context mContext;

        public Adapter_ProdTabelaPreco_ListView(Context context, IList<ProdutoTabelaPreco> items)
        {
            mitems = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                return mitems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ProdutoTabelaPreco this[int position]
        {
            get
            {
                return mitems[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_ProdTabelaPreco_ListView, null, false);
            }

            row.FindViewById<TextView>(Resource.Id.txtIdProdutoTblPreco).Text = "Código do Produto: " + mitems[position].id_produto.ToString();
            row.FindViewById<TextView>(Resource.Id.txtIdTabelaPrecoTblPrecos).Text = "Código da Tabela: " + mitems[position].id_tabpreco.ToString();
            row.FindViewById<TextView>(Resource.Id.txtValorPrecos).Text = "Preços: " + mitems[position].vl_Valor.ToString();


            return row;
        }
    }
}