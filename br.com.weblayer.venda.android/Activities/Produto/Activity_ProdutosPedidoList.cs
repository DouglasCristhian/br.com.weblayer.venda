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
    [Activity(Label = "Lista de Produtos do Pedido")]
    public class Activity_ProdutosPedidoList : Activity_Base
    {
        private ListView lstViewProdutosPedido;
        private IList<PedidoItem> lstPedidoItem;
        private string IdPedido;
        private Pedido ped;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_ProdutosPedidoList;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            IdPedido = Intent.GetStringExtra("JsonPedido");
            if (IdPedido == null)
                return;

            ped = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(IdPedido);

            FindViews();
            BindViews();
            FillList();       
        }

        private void FindViews()
        {
            lstViewProdutosPedido = FindViewById<ListView>(Resource.Id.listViewProdutosPedido);
        }

        private void BindViews()
        {
            lstViewProdutosPedido.ItemClick += LstViewProdutosPedido_ItemClick;
        }

        private void FillList()
        {
            lstPedidoItem = new PedidoItem_Manager().GetPedidoItem(int.Parse(ped.id.ToString()));
            if (lstPedidoItem.Count == 0)
            {
                Intent intent = new Intent(this, typeof(Activity_EditarPedidos));
                SetResult(Result.Ok, intent);
                Finish();
            }
            else
                lstViewProdutosPedido.Adapter = new Adapter_PedidoItem_ListView(this, lstPedidoItem);
        }

        private void LstViewProdutosPedido_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewProdutoClick = sender as ListView;
            var t = lstPedidoItem[e.Position];

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_PedidoItem));
            intent.PutExtra("JsonPedidoItem", Newtonsoft.Json.JsonConvert.SerializeObject(t));
            intent.PutExtra("JsonPedido", Newtonsoft.Json.JsonConvert.SerializeObject(ped));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {                
                FillList();
                Intent intent = new Intent(this, typeof(Activity_EditarPedidos));
                SetResult(Result.Ok, intent);

            }
        }
    }
}