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
using br.com.weblayer.venda.android.Adapters;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Escolha o Produto")]
    public class Activity_PedidoProduto : Activity
    {
        private ListView lstViewProdutos;
        private IList<Produto> lstProdutos;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_PedidoProduto);

            FindViews();
            BindViews();
            FillList();
        }

        private void FindViews()
        {
            lstViewProdutos = FindViewById<ListView>(Resource.Id.listViewProdutos2);
        }

        private void BindViews()
        {
            lstViewProdutos.ItemClick += LstViewProdutos_ItemClick;
        }

        private void FillList()
        {
            lstProdutos = new Produto_Manager().GetProduto("");
            lstViewProdutos.Adapter = new Adapter_Produtos_ListView(this, lstProdutos);
        }

        private void LstViewProdutos_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewProdutoClick = sender as ListView;
            var t = lstProdutos[e.Position];

            Intent intent = new Intent();
            intent.PutExtra("JsonIdProduto", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}
