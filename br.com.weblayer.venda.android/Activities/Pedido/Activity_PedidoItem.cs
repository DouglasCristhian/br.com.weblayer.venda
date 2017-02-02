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
    [Activity(Label = "Adicionar Itens ao Pedido")]
    public class Activity_PedidoItem : Activity_Base
    {
        private EditText txtIdProduto;
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
        private Produto produto;
        private Cliente cliente;
        private double go;
        private string Operacao;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_PedidoItem;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();

                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Trazendo o obj do pedido da tela anterior
            string jsonPedido = Intent.GetStringExtra("JsonPedido");
            if (jsonPedido == null)
                return;        

            pedido = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(jsonPedido);

            string jsonProdutosPedidoList = Intent.GetStringExtra("JsonPedidoItem");
            if (jsonProdutosPedidoList != null) //Edição
            {
                ped_item = Newtonsoft.Json.JsonConvert.DeserializeObject<PedidoItem>(jsonProdutosPedidoList);
                Operacao = "editar";
            }
            else
            { 
                Operacao = "incluir";
            }

            string jsonCliente = Intent.GetStringExtra("JsonCliente");
            if (jsonCliente != null)
                cliente = Newtonsoft.Json.JsonConvert.DeserializeObject<Cliente>(jsonCliente);

            FindViews();
            SetStyle();
            BindData();
            BindViews();
        }

        private void FindViews()
        {
            txtIdProduto = FindViewById<EditText>(Resource.Id.txtIdProduto);
            txtValorItem = FindViewById<EditText>(Resource.Id.txtValorItem);
            txtQuantidadeItem = FindViewById<EditText>(Resource.Id.txtQuantidade);
            txtValorTotal = FindViewById<TextView>(Resource.Id.txtValorTotal);

            btnSalvarPedidoItem = FindViewById<Button>(Resource.Id.btnSalvar);
            btnSalvarOutroPedItem = FindViewById<Button>(Resource.Id.btnSalvarOutro);
            btnCancelarPedidoItem = FindViewById<Button>(Resource.Id.btnCancelar);
            btnLimparPedidoItem = FindViewById<Button>(Resource.Id.btnLimparPedidoItem);

            btnSalvarAtualizar = FindViewById<Button>(Resource.Id.btnSalvarAtualizar);
            btnExcluirPedidoItem = FindViewById<Button>(Resource.Id.btnExcluirPedidoItem);

            if (Operacao == "incluir")
            {
                btnSalvarAtualizar.Visibility = ViewStates.Gone;
                btnExcluirPedidoItem.Visibility = ViewStates.Gone;
            }
            else if (Operacao == "editar")
            {
                btnSalvarPedidoItem.Visibility = ViewStates.Gone;
                btnSalvarOutroPedItem.Visibility = ViewStates.Gone;
                btnLimparPedidoItem.Visibility = ViewStates.Gone;
            }
        }

        private void BindViews()
        {
            if (ped_item == null)
                return;

            txtIdProduto.Text = ped_item.ds_produto.ToString();
            txtQuantidadeItem.Text = ped_item.nr_quantidade.ToString();
            txtValorItem.Text = ped_item.vl_item.ToString();
            double go = double.Parse(ped_item.nr_quantidade.ToString()) * double.Parse(ped_item.vl_item.ToString());
            txtValorTotal.Text = go.ToString();
        }

        private void SetStyle()
        {
            txtIdProduto.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtValorItem.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtQuantidadeItem.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtValorTotal.SetBackgroundResource(Resource.Drawable.EditTextStyle);
        }

        private void BindModel()
        {
            if (ped_item == null)
                ped_item = new PedidoItem();

            ped_item.id_pedido = pedido.id;
            ped_item.ds_produto = txtIdProduto.Text.ToString();

            if (Operacao == "editar")
            {
                ped_item.id_produto = ped_item.id_produto;
            }
            else
            {
                ped_item.id_produto = int.Parse(produto.id_codigo);
            }
           
            ped_item.nr_quantidade = int.Parse(txtQuantidadeItem.Text.ToString());
            ped_item.vl_item = double.Parse(txtValorItem.Text.ToString());
        }

        private void BindData()
        {
            btnSalvarPedidoItem.Click += BtnSalvarPedidoItem_Click;
            btnSalvarOutroPedItem.Click += BtnSalvarOutroPedItem_Click;
            btnSalvarAtualizar.Click += BtnSalvarAtualizar_Click;
            btnLimparPedidoItem.Click += BtnLimparPedidoItem_Click;
            btnCancelarPedidoItem.Click += BtnCancelarPedidoItem_Click;
            btnExcluirPedidoItem.Click += BtnExcluirPedidoItem_Click;

            txtValorItem.TextChanged += TxtValorItem_TextChanged;
            txtQuantidadeItem.TextChanged += TxtQuantidadeItem_TextChanged;

            if (Operacao == "incluir")
            {
                if (pedido.fl_status == 0 || pedido.fl_status == 3)
                {
                    txtIdProduto.Click += TxtIdProduto_Click;
                    txtValorItem.Enabled = true;
                    txtQuantidadeItem.Enabled = true;
                }
            }

            if (pedido.fl_status != 0 && pedido.fl_status != 3)
            {
                txtValorItem.Enabled = false;
                txtQuantidadeItem.Enabled = false;
                btnSalvarAtualizar.Visibility = ViewStates.Gone;
                btnCancelarPedidoItem.Visibility = ViewStates.Gone;
                btnExcluirPedidoItem.Visibility = ViewStates.Gone;
            }
        }

        private void Clean()
        {
            txtIdProduto.Text = "";
            txtValorItem.Text = "";
            txtQuantidadeItem.Text = "";
            txtValorTotal.Text = "0";
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

        private void BtnSalvarAtualizar_Click(object sender, EventArgs e)
        {
            if (!ValidateViews())
                return;

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
                ped_item = null;

                Toast.MakeText(this, $"Item {txtIdProduto.Text} adicionado ao pedido com sucesso!", ToastLength.Long).Show();
                Intent intent = new Intent(/*this, typeof(Activity_EditarPedidos)*/);
                SetResult(Result.Ok, intent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
            Clean();
        }

        private void BtnSalvarPedidoItem_Click(object sender, EventArgs e)
        {
            if (!ValidateViews())
                return;
            try
            {
                Save();
                if (ValidateViews())
                {
                    Finish();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void BtnExcluirPedidoItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                var jsonidproduto = data.GetStringExtra("JsonIdProduto");
                produto = Newtonsoft.Json.JsonConvert.DeserializeObject<Produto>(jsonidproduto);
                txtIdProduto.Text = produto.ds_nome;
                txtValorItem.Text = produto.vl_Valor.ToString();

                var tabprecoprod = new ProdutoTabelaPreco_Manager().Get(cliente.id_tabelapreco, produto.id);

                if (tabprecoprod != null)
                    txtValorItem.Text = tabprecoprod.vl_Valor.ToString();
            }
        }

        private void Save()
        {
            if (!ValidateViews())
                return;

            try
            {
                BindModel();

                var ped = new PedidoItem_Manager();
                ped.Save(ped_item);

                Intent myIntent = new Intent();
                myIntent.PutExtra("mensagem", ped.Mensagem);
                SetResult(Result.Ok, myIntent);
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
