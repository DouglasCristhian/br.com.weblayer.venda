using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.android.Activities;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.core.Model;
using Android.Content;
using static Android.Widget.AdapterView;

namespace br.com.weblayer.venda.android.Fragments
{
    [Activity(Label = "Editar Produto", MainLauncher = false)]
    public class Activity_EditarProduto : Activity_Base
    {
        private EditText txtCodigoProd;
        private EditText txtNomeProd;
        private Spinner spinUniMedidaProd;
        private string spinValor;
        private EditText txtTabelaPrecoProd;
        private Produto prod;
     //   private List<string> mItems;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_EditarProduto);

            var jsonnota = Intent.GetStringExtra("JsonNotaProd");
            if (jsonnota == null)
            {
                prod = null;
            }
            else
            {
                prod = Newtonsoft.Json.JsonConvert.DeserializeObject<Produto>(jsonnota);
            }

           /* mItems = new List<string>(3);
            mItems.Add("CX");
            mItems.Add("PCT");
            mItems.Add("UN");*/

            FindViews();
            Spinner();
            BindView();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.Botoes_Editar, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_salvar:
                    Save();
                    return true;

                case Resource.Id.action_deletar:
                    Delete();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void Spinner()
        {
            //SpinnerListAdapter adapter = new SpinnerListAdapter(this, mItems);

            spinUniMedidaProd.ItemSelected += new EventHandler<ItemSelectedEventArgs>(spinUnidadeMedidadProd_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.unidades_medida, Android.Resource.Layout.SimpleListItem1);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
           spinUniMedidaProd.Adapter = adapter;

            //adapter3 = adapter3.GetItem(spinValor);

            //spinValor = adapter[position].ToString();

           // spinValor = spinUniMedidaProd.SelectedItem.ToString();

            // SpinUniMedidadeProd.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinUnidadeMedidadProd_ItemSelected);
            //var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.unidades_medida, Android.Resource.Layout.SimpleSpinnerItem);

            //adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleExpandableListItem1);        
            // spinUniMedidadeProd.Adapter = adapter;
        }

        private void spinUnidadeMedidadProd_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;

           // int position = spinner.SelectedItemPosition;
            //string result = spinner[position].ToString();

            spinValor = e.ToString();
            spinValor = spinUniMedidaProd.SelectedItem.ToString();
        }

        private void FindViews() //mapear as variaveis para as views
        {
            txtCodigoProd = FindViewById<EditText>(Resource.Id.txtCodigo);
            txtNomeProd = FindViewById<EditText>(Resource.Id.txtNome);
            spinUniMedidaProd = FindViewById<Spinner>(Resource.Id.spinnerUnidadeMedida);          
            txtTabelaPrecoProd = FindViewById<EditText>(Resource.Id.txtTabelaPrecos);
        }

        private void BindView() //pegar dados do modelo e atribuir as views
        {
            if (prod == null)
                return;       

            txtCodigoProd.Text = prod.id_Codigo;
            txtNomeProd.Text = prod.ds_Nome;
            txtTabelaPrecoProd.Text = prod.id_TabPreco;
            spinValor = prod.ds_UniMedida.ToString();
       
        }
                
        private void BindModel()
        {
            if (prod == null)
                prod = new Produto();

            prod.id_Codigo = txtCodigoProd.Text;
            prod.ds_Nome = txtNomeProd.Text;
            prod.id_TabPreco = txtTabelaPrecoProd.Text;
            prod.ds_UniMedida = spinValor;

        }

        private bool ValidateViews()
        {
            var validacao = true;

            if (txtCodigoProd.Length() == 0)
            {
                validacao = false;
                txtCodigoProd.Error = "C�digo do produto inv�lido!";
            }

            if (txtNomeProd.Length() == 0)
            {
                validacao = false;
                txtNomeProd.Error = "Nome do produto inv�lido!";
            }

            if (txtTabelaPrecoProd.Length() == 0)
            {
                validacao = false;
                txtTabelaPrecoProd.Error = "Tabela de pre�o inv�lida!";
            }

            return validacao;
        }

        private void Save()
        {
            if (!ValidateViews())
                return;
            try
            {
                BindModel();

                var produto = new Produto_Manager();
                produto.Save(prod);

                Intent myIntent = new Intent(this, typeof(Activity_Produtos));
                myIntent.PutExtra("mensagem", produto.Mensagem);
                SetResult(Result.Ok, myIntent);
                Finish();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void Delete()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

            alert.SetTitle("Tem certeza que deseja excluir este produto?");

            alert.SetNegativeButton("N�o!", (senderAlert, args) =>
            {

            });

            alert.SetPositiveButton("Sim!", (senderAlert, args) =>
            {
                try
                {
                    var produto = new Produto_Manager();
                    produto.Delete(prod);

                    Intent myIntent = new Intent(this, typeof(Activity_Produtos));
                    myIntent.PutExtra("mensagem", produto.Mensagem);
                    SetResult(Result.Ok, myIntent);
                    Finish();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }

            });

            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
    }
}