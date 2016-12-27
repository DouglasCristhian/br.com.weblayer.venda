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
    [Activity(Label = "Pre�os")]
    public class Activity_ProdTabelaPreco : Activity
    {
        private ListView lstViewProdTabPreco;
        private IList<ProdutoTabelaPreco> lstprecos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_ProdTabelaPreco);

            FindViews();
            BindViews(); 
            FillList();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.Botoes_InserirNovo, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_AddProduct:
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(Activity_EditarProdTabelaPreco));
                    StartActivityForResult(intent, 0);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FindViews()
        {
            lstViewProdTabPreco = FindViewById<ListView>(Resource.Id.lstViewProdTabPreco);
        }

        private void BindViews()
        {
            lstViewProdTabPreco.ItemClick += LstViewProdTabPreco_ItemClick;
        }

        private void FillList()
        {
            lstprecos = new ProdutoTabelaPreco_Manager().GetProdTabPreco("");
            lstViewProdTabPreco.Adapter = new Adapter_ProdTabelaPreco_ListView(this, lstprecos);
        }

        private void LstViewProdTabPreco_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewProd = sender as ListView;
            var t = lstprecos[e.Position];

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_EditarProdTabelaPreco));

            intent.PutExtra("JsonProdTblPreco", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                var mensagem = data.GetStringExtra("mensagem");
                Toast.MakeText(this, mensagem, ToastLength.Short).Show();

                FillList();
            }
        }
    }
}