using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.android.Adapters;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Tabela de Preços")]
    public class Activity_TabelaPrecos : Activity_Base
    {
        private ListView lstViewTabelaPrecos;
        private IList<TabelaPreco> lstTabelaPrecos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_TabelaPrecos);

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
                    intent.SetClass(this, typeof(Activity_EditarTabelaPreco));
                    StartActivityForResult(intent, 0);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FindViews()
        {
            lstViewTabelaPrecos = FindViewById<ListView>(Resource.Id.listViewTabelaPrecos);
        }

        private void BindViews()
        {
            lstViewTabelaPrecos.ItemClick += LstViewTabelaPrecos_Click;
        }

        private void FillList()
        {
            lstTabelaPrecos = new TabelaPreco_Manager().GetTabelaPreco("");
            lstViewTabelaPrecos.Adapter = new Adapter_TabelaPreco_ListView(this, lstTabelaPrecos);
        }

        private void LstViewTabelaPrecos_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewTabelaPreco = sender as ListView;
            var t = lstTabelaPrecos[e.Position];

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_EditarTabelaPreco));

            intent.PutExtra("JsonNotaTabela", Newtonsoft.Json.JsonConvert.SerializeObject(t));
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