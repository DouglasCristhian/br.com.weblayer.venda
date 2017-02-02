using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using br.com.weblayer.venda.android.Activities;
using Android.Views;
using System;
using Android.Support.V7.App;
using br.com.weblayer.venda.core.Sinc;
using System.Threading;
using System.Threading.Tasks;

namespace br.com.weblayer.venda.android
{
    [Activity(MainLauncher = false)]
    public class Activity_Home : Activity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private List<string> ItensLista;
        private ListView ListViewHome;
        //ProgressDialog pd;

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

                case Resource.Id.action_refresh:

                    Sinc_Manager manager = new Sinc_Manager();

                    var progressDialog = ProgressDialog.Show(this, "Por favor aguarde...", "Verificando os dados...", true);
                    new Thread(new ThreadStart(delegate
                    {
                        System.Threading.Thread.Sleep(3000);

                        //LOAD METHOD TO GET ACCOUNT INFO
                        RunOnUiThread(() => manager.Sincronizar());
                        RunOnUiThread(() => Toast.MakeText(this, "Sincronização Finalizada", ToastLength.Short).Show());

                        //HIDE PROGRESS DIALOG
                        RunOnUiThread(() => progressDialog.Hide());


                    })).Start();
                    
                    break;


                case Resource.Id.action_sair:

                    Finish();
                    break;
            }
        }

            //private void PlotView()
            //{
            //    var manager = new Sinc_Manager();
            //manager.Sincronizar();
            //    pd.Dismiss();
            //}

    private void FindViews()
        {
            ListViewHome = FindViewById<ListView>(Resource.Id.listviewHome);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = " W/Vendas";
            toolbar.SetLogo(Resource.Mipmap.ic_launcher);
            toolbar.InflateMenu(Resource.Menu.menu_toolbarvazia);

            //toolbar.Menu.RemoveItem(Resource.Id.action_adicionar);
            //toolbar.Menu.RemoveItem(Resource.Id.action_deletar);
            //toolbar.Menu.RemoveItem(Resource.Id.action_salvar);
            //toolbar.Menu.RemoveItem(Resource.Id.action_adicionar);
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

