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
using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;

namespace br.com.weblayer.venda.android.Fragments
{
    [Activity(Label = "Editar Produto", MainLauncher = false)]
    public class Activity_EditarProduto : Activity_Base
    {
        private EditText txtCodigoProd;
        private EditText txtNomeProd;
        private Spinner spinUniMedidaProd;
        private Spinner spinnerTblPrecoProd;
        private Produto prod;
        private string valortbpreco;
        List<mSpinner> tblprecoList;
        private string[] unidades_medida;
        private string spinValor;

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

            unidades_medida = new string[]
            {
                "CX", "PCT", "UN"
            };
            
            FindViews();
            BindView();

            tblprecoList = PopulateSpinnerList();
            spinnerTblPrecoProd.Adapter = new ArrayAdapter<mSpinner>(this, Android.Resource.Layout.SimpleSpinnerItem, tblprecoList);

            spinUniMedidaProd.SetSelection(getIndex(spinUniMedidaProd, prod.ds_unimedida));

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

        private void FindViews() //mapear as variaveis para as views
        {
            txtCodigoProd = FindViewById<EditText>(Resource.Id.txtCodigo);
            txtNomeProd = FindViewById<EditText>(Resource.Id.txtNome);
            spinnerTblPrecoProd = FindViewById<Spinner>(Resource.Id.spinTabelaPrecosProd);
            spinUniMedidaProd = FindViewById<Spinner>(Resource.Id.spinnerUnidadeMedida);
            spinUniMedidaProd.ItemSelected += new EventHandler<ItemSelectedEventArgs>(spinUnidadeMedidadProd_ItemSelected);

            spinUniMedidaProd.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, unidades_medida);
            spinnerTblPrecoProd.ItemSelected += new EventHandler<ItemSelectedEventArgs>(spinTblPrecosProd_ItemSelected);
        }

        private void BindView() //pegar dados do modelo e atribuir as views
        {
            if (prod == null)
                return;

            txtCodigoProd.Text = prod.id_codigo;
            txtNomeProd.Text = prod.ds_nome;
            valortbpreco = prod.id_tabpreco.ToString();
            spinValor = prod.ds_unimedida.ToString();
        }

        private void BindModel()
        {
            if (prod == null)
                prod = new Produto();

            prod.id_codigo = txtCodigoProd.Text;
            prod.ds_nome = txtNomeProd.Text;
            var mytabelapreco = tblprecoList[spinnerTblPrecoProd.SelectedItemPosition];
            prod.id_tabpreco = mytabelapreco.Id();
            prod.ds_unimedida= spinValor.ToString();
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

            return validacao;
        }

        private void spinTblPrecosProd_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            valortbpreco = spinnerTblPrecoProd.SelectedItem.ToString();
        }

        private void spinUnidadeMedidadProd_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            spinValor = spinUniMedidaProd.SelectedItem.ToString();
        }

        private int getIndex(Spinner spinner, string myString)
        {
            int index = 0;

            for (int i = 0; i < spinner.Count; i++)
            {
                if (spinner.GetItemAtPosition(i).ToString().Equals(myString, StringComparison.InvariantCultureIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private List<mSpinner> PopulateSpinnerList()
        {
            List<mSpinner> minhalista = new List<mSpinner>();
            TabelaPrecoRepository repo = new TabelaPrecoRepository();

            for (int i = 1; i <= 4; i++)
            {
                var go = repo.Get(i);
                if (go != null)
                {
                    minhalista.Add(new mSpinner(go.id, go.ds_descricao));
                    var teste = go.id;
                }
            }

            return minhalista;
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