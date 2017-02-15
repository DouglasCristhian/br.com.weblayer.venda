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
using br.com.weblayer.venda.core.Bll;

namespace br.com.weblayer.venda.android.Adapters
{
    public class Adapter_ProdTabelaPreco_ListView : BaseAdapter<ProdutoTabelaPreco>
    {
        public Produto produto;
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

            TabelaPreco tblPreco;
            TabelaPreco_Manager tbl = new TabelaPreco_Manager();
            tblPreco = tbl.Get(mitems[position].id_tabpreco);

            Produto prod;
            Produto_Manager prod_manager = new Produto_Manager();
            prod = prod_manager.Get(mitems[position].id_produto);

            //row.FindViewById<TextView>(Resource.Id.txtIdProdutoTblPreco).Text = "C�digo do Produto: " + mitems[position].id_produto.ToString();            
            //row.FindViewById<TextView>(Resource.Id.txtIdTabelaPrecoTblPrecos).Text = "C�digo da Tabela: " + mitems[position].id_tabpreco.ToString();
            row.FindViewById<TextView>(Resource.Id.txtIdProdutoTblPreco).Text = "Descri��o do Produto: " + prod.ds_nome.ToString();
            row.FindViewById<TextView>(Resource.Id.txtIdTabelaPrecoTblPrecos).Text = "Descri��o da Tabela: " + tblPreco.ds_descricao.ToString();
            row.FindViewById<TextView>(Resource.Id.txtValorPrecos).Text = "Pre�os: " + mitems[position].vl_Valor.ToString();

            return row;
        }
    }
}