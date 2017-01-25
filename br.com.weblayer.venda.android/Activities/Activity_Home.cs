using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using br.com.weblayer.venda.android.Activities;
using Android.Views;
using System;
using Android.Support.V7.App;

namespace br.com.weblayer.venda.android
{
    [Activity(MainLauncher = true)]
    public class MainActivity : Activity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private List<string> ItensLista;
        private ListView ListViewHome;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Activity_Home);

            core.Dal.Database.Initialize();

            FindViews();
            BindData();
        }

        private void Toolbar_MenuItemClick(object sender, Android.Support.V7.Widget.Toolbar.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.action_sobre:
                    StartActivity(typeof(Activity_Sobre));
                    break;
            }
        }

        private void FindViews()
        {
            ListViewHome = FindViewById<ListView>(Resource.Id.listviewHome);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = " W/Vendas";
            toolbar.SetLogo(Resource.Mipmap.ic_launcher);
            toolbar.InflateMenu(Resource.Menu.menu_toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_adicionar);
            toolbar.Menu.RemoveItem(Resource.Id.action_deletar);
            toolbar.Menu.RemoveItem(Resource.Id.action_salvar);
            toolbar.Menu.RemoveItem(Resource.Id.action_adicionar);
        }

        private void BindData()
        {
            ListViewHome.ItemClick += ListViewHome_ItemClick;

            ItensLista = new List<string>();
            ItensLista.Add("Produtos");
            ItensLista.Add("Clientes");
            ItensLista.Add("Tabela de Preços");
            ItensLista.Add("Preços");
            ItensLista.Add("Pedidos");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ItensLista);
            ListViewHome.Adapter = adapter;

            toolbar.MenuItemClick += Toolbar_MenuItemClick;
        }


        private void ListViewHome_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            switch(e.Position)
            {
                case 0:
                    StartActivity(typeof(Activity_Produtos));
                    break;

                case 1:
                    StartActivity(typeof(Activity_Clientes));
                    break;

                case 2:
                    StartActivity(typeof(Activity_TabelaPrecos));
                    break;

                case 3:
                    StartActivity(typeof(Activity_ProdTabelaPreco));
                    break;

                case 4:
                    StartActivity(typeof(Activity_Pedido));
                    break;

                default:
                    break;
            }
        }
    }
}

