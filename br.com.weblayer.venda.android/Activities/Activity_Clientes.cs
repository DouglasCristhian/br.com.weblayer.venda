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
    [Activity(Label = "Clientes")]
    public class Activity_Clientes : Activity_Base
    {
        private ListView lstViewClientes;
        private IList<Cliente> lstClientes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_Clientes);

            FindViews();
            BindData();
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
                    StartActivity(typeof(Activity_EditarCliente));
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FindViews()
        {
            lstViewClientes = FindViewById<ListView>(Resource.Id.lstViewCliente);
        }

        private void BindData()
        {
            lstViewClientes.ItemClick += LstViewClientes_ItemClick;
        }

        private void FillList()
        {
            lstClientes = new Cliente_Manager().GetClientes("");
            lstViewClientes.Adapter = new Adapter_Cliente_ListView(this, lstClientes);
        }

        private void LstViewClientes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewClienteClick = sender as ListView;
            var t = lstClientes[e.Position];

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_EditarCliente));

            intent.PutExtra("JsonNotaCli", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            StartActivityForResult(intent, 0);
        }
    }
}