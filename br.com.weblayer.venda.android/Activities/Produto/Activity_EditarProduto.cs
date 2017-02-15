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
using br.com.weblayer.venda.android.Adapters;

namespace br.com.weblayer.venda.android.Fragments
{
    [Activity(Label = "Produto", MainLauncher = false)]
    public class Activity_EditarProduto : Activity_Base
    {
        private EditText txtCodigoProd;
        private EditText txtNomeProd;
        private EditText txtValorProd;
        private Spinner spinUniMedidaProd;
        //private Spinner spinnerTblPrecoProd;
        private Produto prod;
        private string valortbpreco;
        List<mSpinner> tblprecoList;
        private string[] unidades_medida;
        private string spinValor;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_EditarProduto;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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
                "Selecione", "CX", "PCT", "UN"
            };
            
            FindViews();
            SetStyle();
            BindView();
            BindData();

            spinUniMedidaProd.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, unidades_medida);

            //tblprecoList = PopulateTabPrecoSpinnerList();
            //spinnerTblPrecoProd.Adapter = new ArrayAdapter<mSpinner>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, tblprecoList);

            if (prod != null)
            {
                spinUniMedidaProd.SetSelection(getIndex(spinUniMedidaProd, prod.ds_unimedida));
                //spinnerTblPrecoProd.SetSelection(getIndexByValue(spinnerTblPrecoProd, prod.id_tabpreco));
            }
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbarvazia, menu);
            menu.RemoveItem(Resource.Id.action_sobre);
            menu.RemoveItem(Resource.Id.action_refresh);
            menu.RemoveItem(Resource.Id.action_sair);


            //menu.RemoveItem(Resource.Id.action_sobre);
            //menu.RemoveItem(Resource.Id.action_adicionar);
            //menu.RemoveItem(Resource.Id.action_refresh);

            //if (prod == null)
            //{
            menu.RemoveItem(Resource.Id.action_deletar);
        //    }

            return base.OnCreateOptionsMenu(menu);
        }

        private void FindViews() //mapear as variaveis para as views
        {
            txtCodigoProd = FindViewById<EditText>(Resource.Id.txtCodigo);
            txtNomeProd = FindViewById<EditText>(Resource.Id.txtNome);
            txtValorProd = FindViewById<EditText>(Resource.Id.txtValorProd);
            //spinnerTblPrecoProd = FindViewById<Spinner>(Resource.Id.spinTabelaPrecosProd);
            spinUniMedidaProd = FindViewById<Spinner>(Resource.Id.spinnerUnidadeMedida);
            spinUniMedidaProd.ItemSelected += new EventHandler<ItemSelectedEventArgs>(spinUnidadeMedidadProd_ItemSelected);
            
            //spinnerTblPrecoProd.ItemSelected += new EventHandler<ItemSelectedEventArgs>(spinTblPrecosProd_ItemSelected);

        }

        private void SetStyle()
        {
            txtCodigoProd.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtNomeProd.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtValorProd.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            //spinnerTblPrecoProd.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            spinUniMedidaProd.SetBackgroundResource(Resource.Drawable.EditTextStyle);
        }

        private void BindView() //pegar dados do modelo e atribuir as views
        {
            if (prod == null)
                return;

            txtCodigoProd.Text = prod.id_codigo;
            txtNomeProd.Text = prod.ds_nome;
            txtValorProd.Text = prod.vl_Lista.ToString();
            //valortbpreco = prod.id_tabpreco.ToString();
            spinValor = prod.ds_unimedida.ToString();
        }

        private void BindModel()
        {
            if (prod == null)
                prod = new Produto();

            prod.id_codigo = txtCodigoProd.Text;
            prod.ds_nome = txtNomeProd.Text;
            //var mytabelapreco = tblprecoList[spinnerTblPrecoProd.SelectedItemPosition];
            //prod.id_tabpreco = mytabelapreco.Id();
            prod.ds_unimedida= spinValor.ToString();
            prod.vl_Lista = double.Parse(txtValorProd.Text.ToString());
        }

        private void BindData()
        {
            //spinnerTblPrecoProd.Enabled = false;
            spinUniMedidaProd.Enabled = false;
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

            //if (spinnerTblPrecoProd.SelectedItemPosition == 0)
            //{
            //    validacao = false;
            //    Toast.MakeText(this, "Por favor, insira a tabela de preços!", ToastLength.Short).Show();
            //}

            if (spinUniMedidaProd.SelectedItemPosition == 0)
            {
                validacao = false;
                Toast.MakeText(this, "Por favor, insira a unidade de medida!", ToastLength.Short).Show();
            }

            return validacao;
        }

        //private void spinTblPrecosProd_ItemSelected(object sender, ItemSelectedEventArgs e)
        //{
        //    valortbpreco = spinnerTblPrecoProd.SelectedItem.ToString();
        //}

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

        private int getIndexByValue(Spinner spinner, long myId)
        {
            int index = 0;

            var adapter = (ArrayAdapter<mSpinner>)spinner.Adapter;
            for (int i = 0; i < spinner.Count; i++)
            {
                if (adapter.GetItemId(i) == myId)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private List<mSpinner> PopulateTabPrecoSpinnerList()
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