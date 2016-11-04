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
    [Activity(Label = "Tabela de Preços")]
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

            intent.PutExtra("JsonNotaProd", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            StartActivityForResult(intent, 0);
        }

    }
}