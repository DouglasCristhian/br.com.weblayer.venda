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

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_Pedido")]
    public class Activity_Pedido : Activity_Base
    {
        private ListView lstViewPedido;
        private IList<Pedido> lstPedido;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_Pedido);

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
                    //Intent para Editarpedidos vazio, aguarda o save para poder repopular a lista.
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(Activity_EditarPedidos));
                    StartActivityForResult(intent, 0);
                    break;
            }
            return base.OnOptionsItemSelected(item);
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
            intent.PutExtra("JsonNotaPedido", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            StartActivityForResult(intent, 0);          
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                //Seja novo ou atualizado, se o retorno das intents foi igual a Ok, a lista é repopulada
                FillList();
            }
        }
    }
}