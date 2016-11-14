using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Text;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_PedidoItem")]
    public class Activity_PedidoItem : Activity_Base
    {
        private EditText txtIdPedido;
        private EditText txtIdProduto;
        private EditText txtValorItem;
        private EditText txtQuantidadeItem;
        private TextView txtValorTotal;
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
            BindData();
            BindViews();
        }

        private void FindViews()
        {
            txtIdPedido = FindViewById<EditText>(Resource.Id.txtIdPedido);
            txtIdProduto = FindViewById<EditText>(Resource.Id.txtIdProduto);
            txtValorItem = FindViewById<EditText>(Resource.Id.txtValorItem);
            txtQuantidadeItem = FindViewById<EditText>(Resource.Id.txtQuantidade);
            txtValorTotal = FindViewById<TextView>(Resource.Id.txtValorTotal);
            btnCancelar = FindViewById<Button>(Resource.Id.btnCancelar);
            btnAdicionarOutro = FindViewById<Button>(Resource.Id.btnAdicionarOutro);
            btnFinalizarPedidoItem = FindViewById<Button>(Resource.Id.btnFinalizarPedidoItem);
        }

        private void BindViews()
        {
            txtIdProduto.Text = jsonnotaId;
            txtValorItem.Text = jsonnotaValor;
        }

        private void BindData()
        {
            btnCancelar.Click += BtnCancelar_Click;
            btnAdicionarOutro.Click += BtnAdicionarOutro_Click;
            btnFinalizarPedidoItem.Click += BtnFinalizarPedidoItem_Click;
            txtIdProduto.Click += TxtIdProduto_Click;
            txtQuantidadeItem.TextChanged += TxtQuantidadeItem_TextChanged;
        }

        private void TxtQuantidadeItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtQuantidadeItem.Text.Length == 0)
            {
                go = 0;
            }
            else if ((txtQuantidadeItem.Text.Length != 0) && (txtValorItem.Text.Length == 0))
            {
                go = 0;
            }
            else
            { 
               go = double.Parse(txtQuantidadeItem.Text.ToString()) * double.Parse(txtValorItem.Text.ToString());
            }
            txtValorTotal.Text = go.ToString();
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
            Clean();
            //CANCELA
        }

        private void BtnAdicionarOutro_Click(object sender, EventArgs e)
        {
            Clean();
            //TODO: SALVAR PEDIDOITEM
        }

        private void BtnFinalizarPedidoItem_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Clean()
        {
            txtIdPedido.Text = "";
            txtIdProduto.Text = "";
            txtValorItem.Text = "";
            txtQuantidadeItem.Text = "";
            txtValorTotal.Text = "";
        }
    }
}