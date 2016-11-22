using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Text;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.venda.core.Model;
using Android.Views;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_PedidoItem")]
    public class Activity_PedidoItem : Activity_Base
    {
        private TextView txtIdProduto;
        private EditText txtValorItem;
        private EditText txtQuantidadeItem;
        private TextView txtValorTotal;
        private Button btnSalvarPedidoItem;
        private Button btnSalvarOutroPedItem;
        private Button btnCancelarPedidoItem;
        private Button btnLimparPedidoItem;
        private Button btnSalvarAtualizar;
        private Button btnExcluirPedidoItem;
        private PedidoItem ped_item;
        private Pedido pedido;
        private string jsonnotaId;
        private string jsonnotaValor;
        private string jsonProdutosPedidoList;
        private string IdPedido;
        private string IdItem;
        private string Operacao;
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

            jsonProdutosPedidoList = Intent.GetStringExtra("JsonProdutosPedidoList");
            if (jsonProdutosPedidoList != null)
            {
                ped_item = Newtonsoft.Json.JsonConvert.DeserializeObject<PedidoItem>(jsonProdutosPedidoList);
                IdPedido = ped_item.id_pedido.ToString();
                IdItem = ped_item.id.ToString();
            }

            IdPedido = Intent.GetStringExtra("Id_Pedido");
            if (IdPedido == null)
            {
                IdPedido = "";
            }

            Operacao = Intent.GetStringExtra("Operacao");

            FindViews();
            BindData();
            BindViews();
        }

        private void FindViews()
        {
            txtIdProduto = FindViewById<TextView>(Resource.Id.txtIdProduto);
            txtValorItem = FindViewById<EditText>(Resource.Id.txtValorItem);
            txtQuantidadeItem = FindViewById<EditText>(Resource.Id.txtQuantidade);
            txtValorTotal = FindViewById<TextView>(Resource.Id.txtValorTotal);

            btnSalvarPedidoItem = FindViewById<Button>(Resource.Id.btnSalvar);
            btnSalvarOutroPedItem = FindViewById<Button>(Resource.Id.btnSalvarOutro);
            btnCancelarPedidoItem = FindViewById<Button>(Resource.Id.btnCancelar);
            btnLimparPedidoItem = FindViewById<Button>(Resource.Id.btnLimparPedidoItem);

            btnSalvarAtualizar = FindViewById<Button>(Resource.Id.btnSalvarAtualizar);        
            btnExcluirPedidoItem = FindViewById<Button>(Resource.Id.btnExcluirPedidoItem);

            if (Operacao == "Adicionar")
            {
                btnSalvarAtualizar.Visibility = ViewStates.Gone;
                btnExcluirPedidoItem.Visibility = ViewStates.Gone;
                IdItem = "0";
            }
            else if (Operacao == "Atualizar")
            {
                btnSalvarPedidoItem.Visibility = ViewStates.Gone;
                btnSalvarOutroPedItem.Visibility = ViewStates.Gone;
                btnLimparPedidoItem.Visibility = ViewStates.Gone;
            }
        }

        private void BindViews()
        {
            txtIdProduto.Text = jsonnotaId;
            txtValorItem.Text = jsonnotaValor;

            if (ped_item == null)
                return;

            txtIdProduto.Text = ped_item.id_produto.ToString();
            txtQuantidadeItem.Text = ped_item.nr_quantidade.ToString();
            txtValorItem.Text = ped_item.vl_item.ToString();
            double go = double.Parse(ped_item.nr_quantidade.ToString()) * double.Parse(ped_item.vl_item.ToString());
            txtValorTotal.Text = go.ToString();
        }

        private void BindData()
        {
            btnSalvarPedidoItem.Click += BtnSalvarPedidoItem_Click;
            btnSalvarOutroPedItem.Click += BtnSalvarOutroPedItem_Click;
            btnSalvarAtualizar.Click += BtnSalvarAtualizar_Click;
            btnLimparPedidoItem.Click += BtnLimparPedidoItem_Click;
            btnCancelarPedidoItem.Click += BtnCancelarPedidoItem_Click;                     
            btnExcluirPedidoItem.Click += BtnExcluirPedidoItem_Click;

            txtIdProduto.Click += TxtIdProduto_Click;
            txtValorItem.TextChanged += TxtValorItem_TextChanged;
            txtQuantidadeItem.TextChanged += TxtQuantidadeItem_TextChanged;
        }

        private bool ValidateViews()
        {
            var validacao = true;

            if (txtIdProduto.Length() == 0)
            {
                validacao = false;
                txtIdProduto.Error = "Código do produto inválido!";
            }

            if (txtQuantidadeItem.Length() == 0)
            {
                validacao = false;
                txtQuantidadeItem.Error = "Nome do produto inválido!";
            }

            if (txtValorItem.Length() == 0)
            {
                validacao = false;
                txtValorItem.Error = "Tabela de preço inválida!";
            }

            return validacao;
        }

        private void Clean()
        {
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

        private void TxtValorItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtValorItem.Text.Length == 0)
            {
                go = 0;
            }
            else if ((txtValorItem.Text.Length != 0) && (txtQuantidadeItem.Text.Length == 0))
            {
                go = 0;
            }
            else
            {
                go = double.Parse(txtValorItem.Text.ToString()) * double.Parse(txtQuantidadeItem.Text.ToString());
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
                jsonnotaId = data.GetStringExtra("JsonNotaIdProduto");
                jsonnotaValor = data.GetStringExtra("JsonNotaValorProduto");

                BindViews();
            }
        }

        private void BtnSalvarAtualizar_Click(object sender, EventArgs e)
        {
            if (!ValidateViews())
                return;

            Intent intent = new Intent(this, typeof(Activity_EditarPedidos));
            SetResult(Result.Ok, intent);
            Save();
            Finish();        
        }


        private void BtnLimparPedidoItem_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void BtnCancelarPedidoItem_Click(object sender, EventArgs e)
        {
            Finish();           
        }

        private void BtnSalvarOutroPedItem_Click(object sender, EventArgs e)
        {
            if (!ValidateViews())
                return;
            try
            {
                Save();
                Toast.MakeText(this, $"Item {txtIdProduto.Text} adicionado ao pedido com sucesso!", ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(Activity_EditarPedidos));
                SetResult(Result.Ok, intent);                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
            Clean();
        }

        //SALVAR AQUI-------------------------------------------------------------------SALVAR AQUI//
        private void BtnSalvarPedidoItem_Click(object sender, EventArgs e)
        {
            if (!ValidateViews())
                return;
            try
            {               
                Save();
                Toast.MakeText(this, $"Item {txtIdProduto.Text} adicionado ao pedido com sucesso!", ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(Activity_EditarPedidos));
                SetResult(Result.Ok, intent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
            Finish();
        }

        private void BtnExcluirPedidoItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void BindModel()
        {
            ped_item = new PedidoItem();

            ped_item.id = int.Parse(IdItem.ToString());
            ped_item.id_pedido = int.Parse(IdPedido.ToString());
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
               // Finish();

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


                    Intent intent = new Intent();
                    intent.PutExtra("mensagem", ped.Mensagem);
                    SetResult(Result.Ok, intent);

                    ped.Delete(ped_item);

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
