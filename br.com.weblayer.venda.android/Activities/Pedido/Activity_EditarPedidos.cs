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

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Editar Pedidos", MainLauncher = false)]
    public class Activity_EditarPedidos : Activity_Base
    {
        private EditText txtid_Codigo;
        private EditText txtid_Vendedor;
        private TextView txtDataEmissao;
        private TextView txtValor_Total;
        private EditText txtObservacao;
        private Button btnAdicionar;
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
                spinnerClientes.SetSelection(getIndexByValue(spinnerClientes, pedido.id));
            }
            else
                pedido = null;
     
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            menu.RemoveItem(Resource.Id.action_sobre);
            menu.RemoveItem(Resource.Id.action_adicionar);
            menu.RemoveItem(Resource.Id.action_configuracoes);

            if (pedido == null)
            {
                menu.RemoveItem(Resource.Id.action_deletar);
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
            txtDataEmissao = FindViewById<TextView>(Resource.Id.txtDataEmissao);
            txtValor_Total = FindViewById<TextView>(Resource.Id.txtValorTotal);
            txtObservacao = FindViewById<EditText>(Resource.Id.txtObservacao);
            btnAdicionar = FindViewById<Button>(Resource.Id.btnAdicionar);

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
            txtid_Vendedor.Text = pedido.id_vendedor.ToString();
            idcliente = pedido.id_cliente.ToString();
            txtValor_Total.Text = pedido.vl_total.ToString();
            txtDataEmissao.Text = pedido.dt_emissao.Value.ToString("dd/MM/yyyy");
            txtObservacao.Text = pedido.ds_observacao.ToString();
        }

        private void BindModel()
        {
            if (pedido == null)
                pedido = new Pedido();

            string data = (txtDataEmissao.Text);
            var datahora = DateTime.Parse(data, CultureInfo.CreateSpecificCulture("pt-BR"));

            pedido.id_codigo = txtid_Codigo.Text;
            pedido.id_vendedor = 1;
            var idcli = tblclientespinner[spinnerClientes.SelectedItemPosition];
            pedido.id_cliente = idcli.Id();
            pedido.dt_emissao = datahora;
            pedido.ds_observacao = txtObservacao.Text;
        }

        private void BindData()
        {
            if (pedido == null)
            {
                txtDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataEmissao.Click += EventtxtDataEmissao_Click;
            }
            else
            {
                txtDataEmissao.Clickable = false;
                if (pedido.vl_total != 0)
                {
                    txtValor_Total.Click += TxtValor_Total_Click;
                }
            }

            btnAdicionar.Click += BtnAdicionar_Click;         
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

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_PedidoItem));

            var obj_cliente = new Cliente_Manager().Get(pedido.id_cliente);

            intent.PutExtra("JsonPedido", Newtonsoft.Json.JsonConvert.SerializeObject(pedido));
            intent.PutExtra("JsonCliente", Newtonsoft.Json.JsonConvert.SerializeObject(obj_cliente));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                //Atualizar o obj de pedido
                pedido = new Pedido_Manager().Get(pedido.id);
                BindViews();
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
