using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.android.Activities;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.core.Model;
using Android.Content;

namespace br.com.weblayer.venda.android.Fragments
{
    [Activity(Label = "Editar Produto", MainLauncher = false)]
    public class Activity_EditarProduto : Activity_Base
    {
        private EditText txtCodigoProd;
        private EditText txtNomeProd;
        private Spinner spinUniMedidadeProd;
        private string spinValor;
        private EditText txtTabelaPrecoProd;
        private Produto prod;

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

            FindViews();
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
            spinUniMedidadeProd.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinUnidadeMedidadProd_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.unidades_medida, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleExpandableListItem1);        
            spinUniMedidadeProd.Adapter = adapter;
        }

        private void spinUnidadeMedidadProd_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            spinValor = spinUniMedidadeProd.SelectedItem.ToString();
        }

        private void FindViews() //mapear as variaveis para as views
        {
            txtCodigoProd = FindViewById<EditText>(Resource.Id.txtCodigo);
            txtNomeProd = FindViewById<EditText>(Resource.Id.txtNome);
            spinUniMedidadeProd = FindViewById<Spinner>(Resource.Id.spinnerUnidadeMedida);          
            txtTabelaPrecoProd = FindViewById<EditText>(Resource.Id.txtTabelaPrecos);
        }

        private void BindView() //pegar dados do modelo e atribuir as views
        {
            if (prod == null)
                return;       

            txtCodigoProd.Text = prod.id_Codigo;
            txtNomeProd.Text = prod.ds_Nome;
            txtTabelaPrecoProd.Text = prod.id_TabPreco;
            spinValor = prod.ds_UniMedida;

            Spinner();         
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
                txtCodigoProd.Error = "Código do produto inválido!";
            }

            if (txtNomeProd.Length() == 0)
            {
                validacao = false;
                txtNomeProd.Error = "Nome do produto inválido!";
            }

            if (txtTabelaPrecoProd.Length() == 0)
            {
                validacao = false;
                txtTabelaPrecoProd.Error = "Tabela de preço inválida!";
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

            alert.SetNegativeButton("Não!", (senderAlert, args) =>
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