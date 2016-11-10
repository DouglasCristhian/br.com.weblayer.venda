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

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_PedidoItem")]
    public class Activity_PedidoItem : Activity_Base
    {
        private EditText txtIdPedido;
        private EditText txtIdProduto;
        private EditText txtValorItem;
        private EditText txtQuantidadeItem;
        private EditText txtValorTotal;
        private Button btnCancelar;
        private Button btnAdicionarOutro;
        private Button btnFinalizarPedidoItem;
        private string jsonnotaId;
        private string jsonnotaValor;
        private double go;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_PedidoItem);

            jsonnotaId = Intent.GetStringExtra("JsonNotaIdProduto");
            if (jsonnotaId == null)
            {
                jsonnotaId = "";
            }

            jsonnotaValor = Intent.GetStringExtra("JsonNotaValorProduto");
            if (jsonnotaValor == null)
            {
                jsonnotaValor = "";
            }

            FindViews();
            BindViews(); 
        }

        private void FindViews()
        {
            txtIdPedido = FindViewById<EditText>(Resource.Id.txtIdPedido);
            txtIdProduto = FindViewById<EditText>(Resource.Id.txtIdProduto);
            txtValorItem = FindViewById<EditText>(Resource.Id.txtValorItem);
            txtQuantidadeItem = FindViewById<EditText>(Resource.Id.txtQuantidade);
            txtValorTotal = FindViewById<EditText>(Resource.Id.txtValorTotal);
            btnCancelar = FindViewById<Button>(Resource.Id.btnCancelar);
            btnAdicionarOutro = FindViewById<Button>(Resource.Id.btnAdicionarOutro);
            btnFinalizarPedidoItem = FindViewById<Button>(Resource.Id.btnFinalizarPedidoItem);
        }

        private void BindViews()
        {
            txtIdProduto.Text = jsonnotaId;
            txtValorItem.Text = jsonnotaValor;
            btnCancelar.Click += BtnCancelar_Click;
            btnAdicionarOutro.Click += BtnAdicionarOutro_Click;
            btnFinalizarPedidoItem.Click += BtnFinalizarPedidoItem_Click;
            txtIdProduto.Click += TxtIdProduto_Click;
        }

        private void TxtQuantidadeItem_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            
        }

        private void TxtIdProduto_Click(object sender, EventArgs e)
        {
            Intent myIntent = new Intent();
            myIntent.SetClass(this, typeof(Activity_PedidoProduto));
            StartActivityForResult(myIntent, 0);
            Finish();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                Toast.MakeText(this, "Chegou", ToastLength.Short).Show();             
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            txtIdPedido.Text = "";
            txtIdProduto.Text = "";
            txtValorItem.Text = "";
            txtQuantidadeItem.Text = "";
            txtValorTotal.Text = "";
        }

        private void BtnAdicionarOutro_Click(object sender, EventArgs e)
        {

        }

        private void BtnFinalizarPedidoItem_Click(object sender, EventArgs e)
        {
            Finish();
        }

    }
}