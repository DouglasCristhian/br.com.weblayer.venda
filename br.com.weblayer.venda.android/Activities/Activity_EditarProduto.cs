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
using br.com.weblayer.venda.android.ClasseTeste;
using br.com.weblayer.venda.android.Activities;
using br.com.weblayer.venda.android.Adapters;
using System.Threading.Tasks;

namespace br.com.weblayer.venda.android.Fragments
{
    [Activity(Label = "Editar Produto")]
    public class Activity_EditarProduto : Activity
    {
        private EditText txtCodigoProd;
        private EditText txtNomeProd;
        private EditText txtUniMedidadeProd;
        private EditText txtTabelaPrecoProd;
        private Produto prod;
        private Button btnSalvar;
        private Button btnExcluir;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_EditarProduto);

            var jsonnota = Intent.GetStringExtra("JsonNotaProd");
            prod = Newtonsoft.Json.JsonConvert.DeserializeObject<Produto>(jsonnota);

            FindViews();
            BindData();
        }

        private void FindViews()
        {
            txtCodigoProd = FindViewById<EditText>(Resource.Id.txtCodigo);
            txtNomeProd = FindViewById<EditText>(Resource.Id.txtNome);
            txtUniMedidadeProd = FindViewById<EditText>(Resource.Id.txtUnidadeMedida);
            txtTabelaPrecoProd = FindViewById<EditText>(Resource.Id.txtTabelaPrecos);
            btnSalvar = FindViewById<Button>(Resource.Id.btnSalvarProduto);
            btnExcluir = FindViewById<Button>(Resource.Id.btnExcluirProduto);
        }

        private void BindData()
        {
            txtCodigoProd.Text = prod.ds_CodigoProduto;
            txtNomeProd.Text = prod.ds_NomeProduto;
            txtUniMedidadeProd.Text = prod.ds_UniMedidaProduto;
            txtTabelaPrecoProd.Text = prod.ds_TblPrecoProduto;

            btnSalvar.Click += BtnSalvar_Click;
            btnExcluir.Click += BtnExcluir_Click;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
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
                Finish();
            });

            RunOnUiThread(() => 
            {
                alert.Show();
            });
        }

    }
}