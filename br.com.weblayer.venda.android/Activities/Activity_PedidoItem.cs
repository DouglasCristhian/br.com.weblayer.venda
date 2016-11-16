using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Text;
using br.com.weblayer.venda.core.Bll;
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
        private TextView txtValorTotal;
        private Button btnCancelar;
        private Button btnAdicionarOutro;
        private Button btnFinalizarPedidoItem;
        private PedidoItem ped_item;
        private string jsonnotaId;
        private string jsonnotaValor;
        private double go;
        public double montante;

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

        private void Clean()
        {
            txtIdPedido.Text = "";
            txtIdProduto.Text = "";
            txtValorItem.Text = "";
            txtQuantidadeItem.Text = "";
            txtValorTotal.Text = "0";
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
            //Intent para pegar o produto escolhido e trazer para a activity PedidoItem
            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_PedidoProduto));
            StartActivityForResult(intent, 0);

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                Toast.MakeText(this, "Chegou", ToastLength.Short).Show();

                jsonnotaId = data.GetStringExtra("JsonNotaIdProduto");
                jsonnotaValor = data.GetStringExtra("JsonNotaValorProduto");

                BindViews();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Clean();
            //CANCELA
        }

        private void BtnAdicionarOutro_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                Toast.MakeText(this, $"Item {txtIdPedido.Text} adicionado ao pedido com sucesso!", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
            Clean();
            //TODO: SALVAR PEDIDOITEM
        }

        private void BtnFinalizarPedidoItem_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Activity_EditarPedidos));
            intent.PutExtra("valoritem", montante.ToString());
            SetResult(Result.Ok, intent);
            Finish();

            //Save();
        }

        private void BindModel()
        {
            ped_item = new PedidoItem();

            ped_item.id_pedido = int.Parse(txtIdPedido.Text.ToString());
            ped_item.id_produto = int.Parse(txtIdProduto.Text);
            ped_item.nr_quantidade = int.Parse(txtQuantidadeItem.Text.ToString());
            ped_item.vl_item = double.Parse(txtValorItem.Text.ToString());   
        }

        private void Save()
        {
            try
            {
                BindModel();

                var ped = new PedidoItem_Manager();
                ped.Save(ped_item);

                montante = montante + double.Parse(txtValorTotal.Text.ToString());
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
                    var ped = new PedidoItem_Manager();
                    ped.Delete(ped_item);

                    Intent intent = new Intent();
                    intent.PutExtra("mensagem", ped.Mensagem);
                    SetResult(Result.Ok, intent);
                   // Finish();
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
