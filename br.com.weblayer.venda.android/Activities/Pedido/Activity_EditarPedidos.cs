using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Model;
using br.com.weblayer.venda.core.Bll;
using br.com.weblayer.logistica.android.Helpers;
using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.android.Adapters;
using System.Globalization;
using Android.Graphics;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Editar Pedidos", MainLauncher = false)]
    public class Activity_EditarPedidos : Activity_Base
    {
        private EditText txtid_Codigo;
        private EditText txtid_Vendedor;
        private EditText txtDataEmissao;
        private TextView txtValor_Total;
        private TextView txtStatusPedido;
        private EditText txtObservacao;
        private TextView lblStatusPedido;
        private Button btnAdicionar;
        private Button btnItensPedido;
        private Button btnFinalizar;
        private Pedido pedido;
        private string idcliente;
        private Spinner spinnerClientes;
        List<mSpinner> tblclientespinner;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_EditarPedidos;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_salvar:
                    Save();
                    if (ValidateViews())
                    {
                        Finish();
                    }
      
                    return true;

                case Resource.Id.action_deletar:
                    Delete();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Receber o JsonPedido para fazer edição e salvar
            var jsonnota = Intent.GetStringExtra("JsonPedido");
            if (jsonnota == null)
            {
                pedido = null;
            }
            else
            {
                pedido = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(jsonnota);
            }

            FindViews();
            SetStyle();
            BindData();
            BindViews();

            tblclientespinner = PopulateSpinner();
            spinnerClientes.Adapter = new ArrayAdapter<mSpinner>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, tblclientespinner);

            if (pedido != null)
            {
                spinnerClientes.SetSelection(getIndexByValue(spinnerClientes, pedido.id_cliente));
            }
            else
                pedido = null;
     
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            menu.RemoveItem(Resource.Id.action_sobre);
            menu.RemoveItem(Resource.Id.action_adicionar);
            menu.RemoveItem(Resource.Id.action_refresh);
            menu.RemoveItem(Resource.Id.action_help);

            if (pedido == null)
            {
                menu.RemoveItem(Resource.Id.action_deletar);
            }

            if (pedido != null)
            {
                if (pedido.fl_status != 0 || pedido.fl_status == 3)
                {
                    menu.RemoveItem(Resource.Id.action_salvar);
                    menu.RemoveItem(Resource.Id.action_deletar);
                }
            }

            return base.OnCreateOptionsMenu(menu);
        }

        private int getIndex(Spinner spinner, string myString)
        {
            int index = 0;

            for (int i = 0; i < spinner.Count; i++)
            {
                if (spinner.GetItemAtPosition(i).ToString().Equals(myString, StringComparison.InvariantCultureIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private int getIndexByValue(Spinner spinner, long myId)
        {
            int index = 0;

            var adapter = (ArrayAdapter<mSpinner>)spinner.Adapter;
            for (int i = 0; i < spinner.Count; i++)
            {
                if (adapter.GetItemId(i) == myId)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void FindViews()
        {
            txtid_Codigo = FindViewById<EditText>(Resource.Id.txtCodigoPedido);
            spinnerClientes = FindViewById<Spinner>(Resource.Id.spinnerIdCliente);
            txtid_Vendedor = FindViewById<EditText>(Resource.Id.txtIdvendedor);
            txtDataEmissao = FindViewById<EditText>(Resource.Id.txtDataEmissao);
            txtValor_Total = FindViewById<TextView>(Resource.Id.txtValorTotal);
            txtObservacao = FindViewById<EditText>(Resource.Id.txtObservacao);
            lblStatusPedido = FindViewById<TextView>(Resource.Id.lblStatusPedido);
            txtStatusPedido = FindViewById<TextView>(Resource.Id.txtStatusPedido);
            btnAdicionar = FindViewById<Button>(Resource.Id.btnAdicionar);
            btnItensPedido = FindViewById<Button>(Resource.Id.btnItensPedido);
            btnFinalizar = FindViewById<Button>(Resource.Id.btnFinalizar);

            txtDataEmissao.SetBackgroundColor(Android.Graphics.Color.LightGray);

            spinnerClientes.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(TblClientes_ItemSelected);
        }

        private void SetStyle()
        {
            txtid_Codigo.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            spinnerClientes.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtid_Vendedor.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtDataEmissao.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtValor_Total.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtObservacao.SetBackgroundResource(Resource.Drawable.EditTextStyle);

            if (pedido != null)
            {
                if ((pedido.fl_status == 0) || (pedido.fl_status == 1))
                    txtStatusPedido.SetBackgroundResource(Resource.Drawable.StatusAbertoFinalizado);

                if (pedido.fl_status == 2)
                    txtStatusPedido.SetBackgroundResource(Resource.Drawable.StatusSincronizado);

                if (pedido.fl_status == 3)
                    txtStatusPedido.SetBackgroundResource(Resource.Drawable.StatusRecusado);

                if (pedido.fl_status == 4)
                    txtStatusPedido.SetBackgroundResource(Resource.Drawable.StatusRealizado);

                if (pedido.fl_status == 5)
                    txtStatusPedido.SetBackgroundResource(Resource.Drawable.StatusFaturado);

                if (pedido.fl_status == 6)
                    txtStatusPedido.SetBackgroundResource(Resource.Drawable.StatusEntregue);
            }          
        }

        private void TblClientes_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            idcliente = spinnerClientes.SelectedItem.ToString();
        }

        private void BindViews()
        {
            if (pedido == null)
                return;

            txtid_Codigo.Text = pedido.id_codigo.ToString();
            txtid_Vendedor.Text = pedido.ds_vendedor.ToString();
            idcliente = pedido.ds_cliente.ToString();
            txtValor_Total.Text = pedido.vl_total.ToString();
            txtDataEmissao.Text = pedido.dt_emissao.Value.ToString("dd/MM/yyyy");
            txtObservacao.Text = pedido.ds_observacao.ToString();
            txtStatusPedido.Text = Status();
        }

        private string Status()
        {
            string status = "";

            if (pedido.fl_status == 0)
            {
                status ="Aberto";
            }

            if (pedido.fl_status == 1)
            {
                status = "Finalizado";
            }

            if(pedido.fl_status == 2)
            {
                status = "Sincronizado";
            }

            if (pedido.fl_status == 3)
            {
                status = "Recusado";
            }

            if (pedido.fl_status == 4)
            {
                status = "Realizado";
            }


            if (pedido.fl_status == 5)
            {
                status = "Faturado";
            }


            if (pedido.fl_status ==6)
            {
                status = "Entregue";
            }

            return status;
        }

        private void BindModel()
        {
            if (pedido == null)
                pedido = new Pedido();

            string data = (txtDataEmissao.Text);
            var datahora = DateTime.Parse(data, CultureInfo.CreateSpecificCulture("pt-BR"));

            pedido.id_codigo = txtid_Codigo.Text;
            pedido.id_vendedor = 1;
            pedido.ds_cliente = idcliente;
            pedido.ds_vendedor = txtid_Vendedor.Text.ToString();
            var idcli = tblclientespinner[spinnerClientes.SelectedItemPosition];
            pedido.id_cliente = idcli.Id();
            pedido.dt_emissao = datahora;
            pedido.ds_observacao = txtObservacao.Text;
        }

        private void BindData()
        {
            txtStatusPedido.Enabled = false;

            if (pedido == null)
            {
                txtDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataEmissao.Click += EventtxtDataEmissao_Click;
                txtStatusPedido.Visibility = ViewStates.Gone;
                lblStatusPedido.Visibility = ViewStates.Gone;

                if (txtValor_Total.Text == "0")
                {
                    btnFinalizar.Visibility = ViewStates.Gone;
                }
            }

            if (pedido != null)
            {
                if (pedido.fl_status != 0 && pedido.fl_status != 3)
                {
                    txtid_Codigo.Enabled = false;
                    txtid_Vendedor.Enabled = false;
                    txtObservacao.Enabled = false;
                    spinnerClientes.Enabled = false;
                    btnFinalizar.Visibility = ViewStates.Gone;
                    btnAdicionar.Visibility = ViewStates.Gone;
                }

                if (pedido.vl_total == 0)
                {
                    btnFinalizar.Visibility = ViewStates.Gone;
                }
            }

            btnAdicionar.Click += BtnAdicionar_Click;
            btnFinalizar.Click += BtnFinalizar_Click;
            txtValor_Total.Click += TxtValor_Total_Click;
            btnItensPedido.Click += TxtValor_Total_Click;
        }

        private bool ValidateViews()
        {
            var validacao = true;
            if (txtid_Codigo.Length() == 0)
            {
                validacao = false;
                txtid_Codigo.Error = "Código do Pedido inválido!";
            }

            if (txtid_Vendedor.Length() == 0)
            {
                validacao = false;
                txtid_Vendedor.Error = "Código do Vendedor inválido!";
            }

            if (spinnerClientes.SelectedItemPosition == 0)
            {
                validacao = false;
                Toast.MakeText(this, "Por favor, selecione o código do cliente", ToastLength.Short).Show();
            }

            return validacao;
        }

        private List<mSpinner> PopulateSpinner()
        {
            List<mSpinner> minhalista = new List<mSpinner>();
            var listaclientes = new ClienteRepository().List();

            minhalista.Add(new mSpinner(0, "Selecione o cliente..."));

            foreach (var item in listaclientes)
            {
                minhalista.Add(new mSpinner(item.id, item.ds_NomeFantasia));
            }

            return minhalista;
        }

        private void TxtValor_Total_Click(object sender, EventArgs e)
        {
            if (txtValor_Total.Text.ToString() == "0")
                return;

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_ProdutosPedidoList));
            intent.PutExtra("JsonPedido", Newtonsoft.Json.JsonConvert.SerializeObject(pedido));
            StartActivityForResult(intent, 0);
        }

        private void EventtxtDataEmissao_Click(object sender, EventArgs e)
        {
            DatePickerHelper frag = DatePickerHelper.NewInstance(delegate (DateTime time)
            {
                txtDataEmissao.Text = time.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
            });

            frag.Show(FragmentManager, DatePickerHelper.TAG);
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            if (!ValidateViews())
                return;

            //Começa intent para adicionar um novo pedidoitem. Aguarda resultado para trazer o valor do item de volta
            Save();

            var obj_cliente = new Cliente_Manager().Get(pedido.id_cliente);

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_PedidoItem));
            intent.PutExtra("JsonPedido", Newtonsoft.Json.JsonConvert.SerializeObject(pedido));
            intent.PutExtra("JsonCliente", Newtonsoft.Json.JsonConvert.SerializeObject(obj_cliente));
            StartActivityForResult(intent, 0);
        }

        private void BtnFinalizar_Click(object sender, EventArgs e)
        {
            if (!ValidateViews())
                return;

            if (pedido != null)
            {
                if (pedido.vl_total == 0)
                {
                    Toast.MakeText(this, "Um pedido não pode ser finalizado sem conter itens", ToastLength.Long).Show();
                    return;
                }     
                else
                {
                        pedido.fl_status = 1;
                        Save();

                        if (ValidateViews())
                            Finish();
                 }         
            }
            else
            {
                if (txtValor_Total.Text == "0")
                {
                    Toast.MakeText(this, "Um pedido não pode ser finalizado sem conter itens", ToastLength.Long).Show();
                    return;
                }
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                string mensagem = data.GetStringExtra("mensagem");

                if (mensagem != null)
                {
                    Toast.MakeText(this, mensagem, ToastLength.Short).Show();
                }
                //Atualizar o obj de pedido
                pedido = new Pedido_Manager().Get(pedido.id);
                BindViews();

                Intent intent = new Intent();
                SetResult(Result.Ok, intent);
            }
        }

        private void Save()
        {
            if (!ValidateViews())
                return;
            try
            {
                BindModel();

                var ped = new Pedido_Manager();
                ped.Save(pedido);

                Intent intent = new Intent();
                intent.PutExtra("mensagem", ped.Mensagem);
                SetResult(Result.Ok, intent);
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
                    var ped = new Pedido_Manager();
                    ped.Delete(pedido);

                    //Começa intent para enviar mensagem à Activity anterior (Activity_Pedido)
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
