using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using br.com.weblayer.venda.android.Activities;
using Android.Views;

namespace br.com.weblayer.venda.android
{
    [Activity(Label = "Home", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity_Base
    {
        private List<string> ItensLista;
        private ListView ListViewHome;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Activity_Home);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            core.Dal.Database.Initialize();

            FindViews();
            BindData();                  
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //base.OnCreateOptionsMenu(menu);
            MenuInflater.Inflate(Resource.Layout.Botoes_Home, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.action_configuracoes:
                    StartActivity(typeof(Activity_Configuracoes));
                    return true;

                case Resource.Id.action_ajuda:
                    StartActivity(typeof(Activity_Ajuda));
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FindViews()
        {
            ListViewHome = FindViewById<ListView>(Resource.Id.listviewHome);
        }

        private void BindData()
        {
            ListViewHome.ItemClick += ListViewHome_ItemClick;

            ItensLista = new List<string>();
            ItensLista.Add("Produtos");
            ItensLista.Add("Clientes");
            ItensLista.Add("Tabela de Preços");
            ItensLista.Add("Pedidos");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ItensLista);
            ListViewHome.Adapter = adapter;
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
                    StartActivity(typeof(Activity_Pedido));
                    break;

                default:
                    break;
            }
        }
    }
}

