using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.android.Fragments;

using br.com.weblayer.venda.android.Adapters;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_Produtos")]
    public class Activity_Produtos : Activity_Base
    {
        private ListView lstViewProdutos;
        private IList<Produto> lstProdutos;

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
            MenuInflater.Inflate(Resource.Layout.Botoes_InserirNovo, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_AddProduct:
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(Activity_EditarProduto));
                    StartActivityForResult(intent, 0);
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