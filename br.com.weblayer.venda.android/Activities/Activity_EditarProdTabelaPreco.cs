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
using static Android.Widget.AdapterView;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.core.Bll;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_EditarProdTabelaPreco", MainLauncher = false)]
    public class Activity_EditarProdTabelaPreco : Activity
    {
        private Spinner spinIdProduto;
        private Spinner spinIdTabPreco;
        private EditText txt_ProdTabPreco;
        private string valoridproduto;
        private string valoridtabpreco;
        private ProdutoTabelaPreco prodtabpreco;
        List<mSpinner> tblprecospinner;
        List<mSpinner> tblprodutospinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_EditarProdTabelaPreco);

            var jsonnota = Intent.GetStringExtra("JsonProdTblPreco");

            if (jsonnota == null)
            {
                prodtabpreco = null;
            }
            else
            {
                prodtabpreco = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoTabelaPreco>(jsonnota);
            }

            FindView();
            BindView();

            tblprecospinner = PopulateSpinnerList();
            spinIdTabPreco.Adapter = new ArrayAdapter<mSpinner>(this, Android.Resource.Layout.SimpleSpinnerItem, tblprecospinner);

            tblprodutospinner = PopulateProdutoSpinnerList();
            spinIdProduto.Adapter = new ArrayAdapter<mSpinner>(this, Android.Resource.Layout.SimpleSpinnerItem, tblprodutospinner);

            if (prodtabpreco != null)
            {
                spinIdTabPreco.SetSelection(getIndex(spinIdTabPreco, prodtabpreco.id_tabpreco.ToString()));
                spinIdProduto.SetSelection(getIndex(spinIdProduto, prodtabpreco.id_produto.ToString()));
            }
            else
                prodtabpreco = null;

        }

        public override bool OnCreateOptionsMenu(IMenu item)
        {
            MenuInflater.Inflate(Resource.Layout.Botoes_Editar, item);
            return base.OnCreateOptionsMenu(item);
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

        private void FindView()
        {
            spinIdProduto = FindViewById<Spinner>(Resource.Id.spinnerIdProdutoTbl);
            spinIdTabPreco = FindViewById<Spinner>(Resource.Id.spinnerIdTabPreco);
            txt_ProdTabPreco = FindViewById<EditText>(Resource.Id.txtValorTabProd);

            spinIdTabPreco.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinIdTabPreco_ItemSelected);
            spinIdProduto.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinIdProduto_ItemSelected);
        }

        private void BindView()
        {
            if (prodtabpreco == null)
                return;

            valoridtabpreco = prodtabpreco.id_tabpreco.ToString();
            valoridproduto = prodtabpreco.id_produto.ToString();
            txt_ProdTabPreco.Text = prodtabpreco.vl_Valor.ToString();
        }

        private void BindModel()
        {
            if (prodtabpreco == null)
                prodtabpreco = new ProdutoTabelaPreco();

            var myproduto = tblprodutospinner[spinIdProduto.SelectedItemPosition];
            prodtabpreco.id_produto = myproduto.Id();
            var mytabpreco = tblprecospinner[spinIdTabPreco.SelectedItemPosition];
            prodtabpreco.id_tabpreco = mytabpreco.Id();
            prodtabpreco.vl_Valor = double.Parse(txt_ProdTabPreco.Text.ToString());
        }

        private bool ValidateViews()
        {
            var validacao = true;

            //A CONSIDERAR
            if (txt_ProdTabPreco.Length() == 0)
            {
                txt_ProdTabPreco.Error = "Valor inválido!";
            }

            return validacao;
        }

        private void spinIdProduto_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            valoridproduto = spinIdProduto.SelectedItem.ToString();
        }

        private void spinIdTabPreco_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            valoridtabpreco = spinIdTabPreco.SelectedItem.ToString();
        }

        private int getIndex(Spinner spinner, string myString)
        {
            int index = 0;

            for (int i = 1; i < spinner.Count; i++)
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
            var listatabelapreco = new TabelaPrecoRepository().List();

            minhalista.Add(new mSpinner(0, "Selecione..."));

            foreach (var item in listatabelapreco)
            {
                minhalista.Add(new mSpinner(item.id, item.ds_descricao));
            }
            
            return minhalista;
        }

        private List<mSpinner> PopulateProdutoSpinnerList()
        {
            List<mSpinner> minhalista = new List<mSpinner>();
            var listaprodutos = new ProdutoRepository().List();

            minhalista.Add(new mSpinner(0, "Selecione..."));

            foreach (var item in listaprodutos)
            {
                minhalista.Add(new mSpinner(item.id, item.id_codigo));
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

                var precos = new ProdutoTabelaPreco_Manager();
                precos.Save(prodtabpreco);

                Intent myIntent = new Intent(this, typeof(Activity_ProdTabelaPreco));
                myIntent.PutExtra("mensagem", precos.Mensagem);
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

            alert.SetTitle("Tem certeza que deseja excluir este cliente?");

            alert.SetNegativeButton("Não!", (senderAlert, args) =>
            {

            });

            alert.SetPositiveButton("Sim!", (senderAlert, args) =>
            {
                try
                {
                    var precos = new ProdutoTabelaPreco_Manager();
                    precos.Delete(prodtabpreco);

                    Intent myIntent = new Intent(this, typeof(Activity_Clientes));
                    myIntent.PutExtra("mensagem", precos.Mensagem);
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