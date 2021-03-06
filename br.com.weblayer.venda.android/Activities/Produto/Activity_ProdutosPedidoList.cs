using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.android.Adapters;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.core.Model;
using System.Collections.Generic;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Lista de Itens do Pedido")]
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            menu.RemoveItem(Resource.Id.action_refresh);
            menu.RemoveItem(Resource.Id.action_sobre);
            menu.RemoveItem(Resource.Id.action_salvar);
            menu.RemoveItem(Resource.Id.action_deletar);
            menu.RemoveItem(Resource.Id.action_help);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_adicionar:

                    var obj_cliente = new Cliente_Manager().Get(ped.id_cliente);

                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(Activity_PedidoItem));
                    intent.PutExtra("JsonPedido", Newtonsoft.Json.JsonConvert.SerializeObject(ped));
                    intent.PutExtra("JsonCliente", Newtonsoft.Json.JsonConvert.SerializeObject(obj_cliente));
                    StartActivityForResult(intent, 0);
                    break;

            }
            return base.OnOptionsItemSelected(item);
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
            SetResult(Result.Ok, intent);
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                string mensagem = data.GetStringExtra("mensagem");
                Toast.MakeText(this, mensagem, ToastLength.Short).Show();

                FillList();

                Intent intent = new Intent();
                SetResult(Result.Ok, intent);

            }
        }
    }
}