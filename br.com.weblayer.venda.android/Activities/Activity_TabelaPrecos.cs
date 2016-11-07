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
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.android.Adapters;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Tabela de Pre�os")]
    public class Activity_TabelaPrecos : Activity_Base
    {
        private ListView lstViewTabelaPrecos;
        private List<TabelaPreco> lstTabelaPrecos;

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
                    StartActivity(typeof(Activity_EditarTabelaPreco));
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

    }
}