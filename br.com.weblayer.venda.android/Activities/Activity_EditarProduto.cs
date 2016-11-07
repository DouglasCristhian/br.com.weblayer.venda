using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.android.Activities;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.android.Fragments
{
    [Activity(Label = "Editar Produto")]
    public class Activity_EditarProduto : Activity_Base
    {
        private EditText txtCodigoProd;
        private EditText txtNomeProd;
        private EditText txtUniMedidadeProd;
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

        private void FindViews()
        {
            txtCodigoProd = FindViewById<EditText>(Resource.Id.txtCodigo);
            txtNomeProd = FindViewById<EditText>(Resource.Id.txtNome);
            txtUniMedidadeProd = FindViewById<EditText>(Resource.Id.txtUnidadeMedida);
            txtTabelaPrecoProd = FindViewById<EditText>(Resource.Id.txtTabelaPrecos);
        }

        private void BindView()
        {
            if (prod == null)
                return;

            txtCodigoProd.Text = prod.id_Codigo;
            txtNomeProd.Text = prod.ds_Nome;
            txtUniMedidadeProd.Text = prod.ds_UniMedida;
            txtTabelaPrecoProd.Text = prod.id_TabPreco;
        }

        private void BindModel()
        {
            if (prod == null)
                prod = new Produto();

            prod.id_Codigo = txtCodigoProd.Text;
            prod.ds_Nome = txtNomeProd.Text;
            prod.ds_UniMedida = txtUniMedidadeProd.Text;
            prod.id_TabPreco = txtTabelaPrecoProd.Text;
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

            if (txtUniMedidadeProd.Length() == 0)
            {
                validacao = false;
                txtUniMedidadeProd.Error = "Unidade de medida inválida!";
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

                Finish();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }

        }

        private void Delete()
        {
            if (!ValidateViews())
                return;

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