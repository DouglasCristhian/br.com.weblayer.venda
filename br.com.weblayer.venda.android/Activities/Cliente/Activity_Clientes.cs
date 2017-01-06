using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Model;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.android.Adapters;
using System;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Clientes")]
    public class Activity_Clientes : Activity_Base
    {
        private ListView lstViewClientes;
        private IList<Cliente> lstClientes;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_Clientes;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_adicionar:
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(Activity_EditarCliente));
                    StartActivityForResult(intent, 0);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FindViews();
            BindViews();
            FillList();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            menu.RemoveItem(Resource.Id.action_ajuda);
            menu.RemoveItem(Resource.Id.action_deletar);
            menu.RemoveItem(Resource.Id.action_salvar);
            menu.RemoveItem(Resource.Id.action_configuracoes);
            return base.OnCreateOptionsMenu(menu);
        }

        private void FindViews()
        {
            lstViewClientes = FindViewById<ListView>(Resource.Id.lstViewCliente);
        }

        private void BindViews()
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