using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;
using br.com.weblayer.venda.android.Activities;
using Android.Views;
using Android.Content;

namespace br.com.weblayer.venda.android
{
    [Activity(Label = "Home", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<string> ItensLista;
        private ListView ListViewHome;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Activity_Home);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            FindViews();
            BindData();                  
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);
            MenuInflater.Inflate(Resource.Layout.Botao_Configuracao, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public bool onOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.action_configuracoes:

                    Ir_Configuracoes();

                    return true;

                case Resource.Id.action_ajuda:
                    Toast.MakeText(this, "uia2", ToastLength.Short).Show();

                    return true;

                default:
                    return true;
            }
        }

        private void Ir_Configuracoes()
        {
            Intent i = new Intent(this, typeof(Activity_Configuracoes));
            StartActivity(i);

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
                    StartActivity(typeof(Activity_Pedidos));
                    break;

                default:
                    break;
            }
        }
    }
}

