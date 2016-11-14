using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Model;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.logistica.android.Helpers;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Pedidos", MainLauncher = false)]
    public class Activity_EditarPedidos : Activity_Base
    {
        private EditText txtid_Codigo;
        private EditText txtid_Cliente;
        private EditText txtid_Vendedor;
        private TextView txtDataEmissao;
        private EditText txtValor_Total;
        private EditText txtObservacao;
        private Button btnAdicionar;
        private Button btnFinalizar;
        private PedidoItem pedidoitem;
        private Pedido pedido;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_EditarPedidos);

            var jsonnota = Intent.GetStringExtra("JsonNotaPedidoItem");
            if (jsonnota == null)
            {
                pedidoitem = null;
            }
            else
            {
                pedidoitem = Newtonsoft.Json.JsonConvert.DeserializeObject<PedidoItem>(jsonnota);
            }
            
            //TODO: Desabilitar botão finalizar pedido se faltarem dados
            FindViews();
            BindData();
            BindViews();
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

        private void BindData()
        {
            btnAdicionar.Click += BtnAdicionar_Click;
            btnFinalizar.Click += BtnFinalizar_Click1;
            txtDataEmissao.Click += EventtxtDataEmissao_Click;
        }

        private void EventtxtDataEmissao_Click(object sender, EventArgs e)
        {
            DatePickerHelper frag = DatePickerHelper.NewInstance(delegate (DateTime time)
            {
                txtDataEmissao.Text = time.ToShortDateString();
            });

            frag.Show(FragmentManager, DatePickerHelper.TAG);
        }

        private void BtnFinalizar_Click1(object sender, EventArgs e)
        {
            Save();
            Finish();
        }

        private void FindViews()
        {
            txtid_Codigo = FindViewById<EditText>(Resource.Id.txtCodigoPedido);
            txtid_Cliente = FindViewById<EditText>(Resource.Id.txtIdCliente);
            txtid_Vendedor = FindViewById<EditText>(Resource.Id.txtIdvendedor);
            txtDataEmissao = FindViewById<TextView>(Resource.Id.txtDataEmissao);
            txtValor_Total = FindViewById<EditText>(Resource.Id.txtValorTotal);
            txtObservacao = FindViewById<EditText>(Resource.Id.txtObservacao);
            btnAdicionar = FindViewById<Button>(Resource.Id.btnAdicionar);
            btnFinalizar = FindViewById<Button>(Resource.Id.btnFinalizar);
        }

        private void BindViews()
        {
            if (pedido == null)
                return;

            txtid_Codigo.Text = pedidoitem.id_pedido.ToString();
            txtid_Vendedor.Text = pedido.id_vendedor.ToString();
            txtid_Cliente.Text = pedido.id_cliente.ToString();
            txtValor_Total.Text = pedidoitem.vl_item.ToString();
            txtDataEmissao.Text = pedido.dt_emissao.ToString();
            txtObservacao.Text = pedido.ds_observacao.ToString();         
        }

        private void BindModel()
        {
            if (pedidoitem == null)
                pedido = new Pedido();

            pedido.id_Codigo = txtid_Codigo.Text;
            pedido.id_vendedor = txtid_Vendedor.Text;
            pedido.id_cliente = txtid_Cliente.Text;
            pedido.vl_total = double.Parse(txtValor_Total.Text);
            pedido.dt_emissao = DateTime.Parse(txtDataEmissao.Text);
            pedido.ds_observacao = txtObservacao.Text;
        }


        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Activity_PedidoItem));
        }

        private void BtnFinalizar_Click(object sender, EventArgs e)
        { 
            
            //Finish();
        }

        private void Save()
        {
          //  try
           // {
                BindModel();

                var ped = new Pedido_Manager();
                ped.Save(pedido);

                Intent myIntent = new Intent(this, typeof(Activity_Produtos));
                myIntent.PutExtra("mensagem", ped.Mensagem);
                SetResult(Result.Ok, myIntent);
                Finish();
           // }
           // catch (Exception ex)
          //  {
           //    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
          //  }
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
                    var ped = new Pedido_Manager();
                    ped.Delete(pedido);

                    Intent myIntent = new Intent(this, typeof(Activity_Produtos));
                    myIntent.PutExtra("mensagem", ped.Mensagem);
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
