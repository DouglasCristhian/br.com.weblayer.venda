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

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Pedidos", MainLauncher = true)]
    public class Activity_Pedidos : Activity_Base
    {
        private EditText id_Codigo;
        private EditText id_Cliente;
        private EditText id_Vendedor;
        private EditText DataEmissao;
        private EditText Valor_Total;
        private EditText Observacao;
        private Button btnAdicionar;
        private Button btnFinalizar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_Pedidos);

            //TODO: Desabilitar botão finalizar pedido se faltarem dados

            FindViews();
            BindViews();
        }

        private void FindViews()
        {
            id_Codigo = FindViewById<EditText>(Resource.Id.txtCodigoCliente);
            id_Cliente = FindViewById<EditText>(Resource.Id.txtIdCliente);
            id_Vendedor = FindViewById<EditText>(Resource.Id.txtIdvendedor);
            DataEmissao = FindViewById<EditText>(Resource.Id.txtDataEmissao);
            Valor_Total = FindViewById<EditText>(Resource.Id.txtValorTotal);
            Observacao = FindViewById<EditText>(Resource.Id.txtObservacao);
            btnAdicionar = FindViewById<Button>(Resource.Id.btnAdicionar);
            btnFinalizar = FindViewById<Button>(Resource.Id.btnFinalizar);
        }

        private void BindViews()
        {
            btnAdicionar.Click += BtnAdicionar_Click;
            btnFinalizar.Click += BtnFinalizar_Click;
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Activity_PedidoItem));
        }

        private void BtnFinalizar_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}