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
            BindModel();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.Botoes_EditarProduto, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_salvarproduto:
                    Save();
                    return true;

                case Resource.Id.action_deletarproduto:
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

            txtCodigoProd.Text = prod.ds_CodigoProduto;
            txtNomeProd.Text = prod.ds_NomeProduto;
            txtUniMedidadeProd.Text = prod.ds_UniMedidaProduto;
            txtTabelaPrecoProd.Text = prod.ds_TblPrecoProduto;
        }

        private void BindModel()
        {
            if (prod == null)
                prod = new Produto();

            prod.ds_CodigoProduto = txtCodigoProd.Text;
            prod.ds_NomeProduto = txtNomeProd.Text;
            prod.ds_UniMedidaProduto = txtUniMedidadeProd.Text;
            prod.ds_TblPrecoProduto = txtTabelaPrecoProd.Text;
        }

        private void Save()
        {
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
            AlertDialog alerta = new AlertDialog.Builder(this).Create();
            alerta.SetMessage("Tem certeza que deseja excluir este produto?");

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