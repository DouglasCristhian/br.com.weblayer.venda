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
    [Activity(Label = "Activity_Pedidos")]
    public class Activity_Pedidos : Activity_Base
    {
        private ListView lstViewPedidoItem;
        private IList<PedidoItem> lstPedidoItem;

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
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(Activity_EditarPedidos));
                    StartActivityForResult(intent, 0);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void FindViews()
        {
            lstViewPedidoItem = FindViewById<ListView>(Resource.Id.listviewPedidoItem);
        }

        private void BindViews()
        {
            lstViewPedidoItem.ItemClick += LstViewPedidoItem_ItemClick;
        }

        private void FillList()
        {
            lstPedidoItem = new PedidoItem_Manager().GetPedidoItem();
            lstViewPedidoItem.Adapter = new Adapter_PedidoItem_ListView(this, lstPedidoItem);
        }

        private void LstViewPedidoItem_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewPedidoItemClick = sender as ListView;
            var t = lstPedidoItem[e.Position];

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_EditarPedidos));

            intent.PutExtra("JsonNotaPedidoItem", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            StartActivityForResult(intent, 0);          
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                FillList();
            }
        }


    }
}