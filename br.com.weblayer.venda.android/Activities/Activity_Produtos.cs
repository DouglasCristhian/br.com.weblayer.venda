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
using br.com.weblayer.venda.android.Fragments;

using br.com.weblayer.venda.android.Adapters;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Produtos")]
    public class Activity_Produtos : Activity_Base
    {
        private ListView lstViewProdutos;
        private List<Produto> lstProdutos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_Produtos);

            FindViews();
            BindViews();
            FillList();         
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.Botoes_Produtos, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_AddProduct:
                    StartActivity(typeof(Activity_EditarProduto));
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FindViews()
        {
            lstViewProdutos = FindViewById<ListView>(Resource.Id.listViewProdutos);
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
            intent.SetClass(this, typeof(Activity_EditarProduto));

            intent.PutExtra("JsonNotaProd", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            StartActivityForResult(intent, 0);
        }
    }
}