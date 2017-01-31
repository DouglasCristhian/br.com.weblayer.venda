using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Model;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.android.Adapters;
using br.com.weblayer.venda.android.Fragments;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Pedidos")]
    public class Activity_Pedido : Activity_Base
    {
        private ListView lstViewPedido;
        private IList<Pedido> lstPedido;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_Pedido;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                //case Resource.Id.action_sair:
                //    Finish();

                //    return true;

                case Resource.Id.action_adicionar:
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(Activity_EditarPedidos));
                    StartActivityForResult(intent, 0);
                    break;

                case Resource.Id.action_help:
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    Fragment_Legendas dialog = new Fragment_Legendas();
                    dialog.Show(transaction, "dialog");
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
            menu.RemoveItem(Resource.Id.action_deletar);
            menu.RemoveItem(Resource.Id.action_salvar);
            menu.RemoveItem(Resource.Id.action_refresh);
            menu.RemoveItem(Resource.Id.action_sobre);
            return base.OnCreateOptionsMenu(menu);
        }

        private void FindViews()
        {
            lstViewPedido = FindViewById<ListView>(Resource.Id.listviewPedido);
        }

        private void BindViews()
        {
            lstViewPedido.ItemClick += LstViewPedidoItem_ItemClick;
        }

        private void FillList()
        {
            lstPedido = new Pedido_Manager().GetPedidos("");
            lstViewPedido.Adapter = new Adapter_Pedido_ListView(this, lstPedido);
        }

        private void LstViewPedidoItem_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewPedidoItemClick = sender as ListView;
            var t = lstPedido[e.Position];

            //Intent para o EditarPedidos já com dados, para editar o pedido. Aguarda retorno do save para atualizar a lista
            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_EditarPedidos));
            intent.PutExtra("JsonPedido", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            StartActivityForResult(intent, 0);          
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                var mensagem = data.GetStringExtra("mensagem");

                if (mensagem != null)
                {
                    Toast.MakeText(this, mensagem, ToastLength.Short).Show();
                }
                //Seja novo ou atualizado, se o retorno das intents foi igual a Ok, a lista é repopulada
                FillList();
            }
        }
    }
}