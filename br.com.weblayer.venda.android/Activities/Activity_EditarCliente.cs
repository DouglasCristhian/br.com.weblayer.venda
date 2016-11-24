using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Model;
using br.com.weblayer.venda.core.Bll;
using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using static Android.Widget.AdapterView;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_EditarCliente")]
    public class Activity_EditarCliente : Activity_Base
    {
        private EditText txtCodCli;
        private EditText txtRazaoSocialCli;
        private EditText txtNomeFantasiaCli;
        private EditText txtCNPJCli;
        public Spinner spinnerTabelaPreco;
        private Cliente cli;
        List<TabelaPrecoSpinner> tblprecospinner;
        private string spinvalortbl;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_EditarClientes);

            var jsonnota = Intent.GetStringExtra("JsonNotaCli");

            if (jsonnota == null)
            {
                cli = null;
            }
            else
            {
                cli = Newtonsoft.Json.JsonConvert.DeserializeObject<Cliente>(jsonnota);
            }

            FindViews();
            BindView();

            tblprecospinner = PopulateSpinnerList();          
            spinnerTabelaPreco.Adapter = new ArrayAdapter<TabelaPrecoSpinner>(this, Android.Resource.Layout.SimpleSpinnerItem, tblprecospinner);

            if (cli != null)
            {
                spinnerTabelaPreco.SetSelection(getIndex(spinnerTabelaPreco, cli.id_TabelaPreco.ToString()));
            }
            else
                cli = null;
        }

        public override bool OnCreateOptionsMenu(IMenu item)
        {
            MenuInflater.Inflate(Resource.Layout.Botoes_Editar, item);
            return base.OnCreateOptionsMenu(item);
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

        private void FindViews()
        {
            txtCodCli = FindViewById<EditText>(Resource.Id.txtCodigoCliente);
            txtNomeFantasiaCli = FindViewById<EditText>(Resource.Id.txtNomeFantasia);
            txtRazaoSocialCli = FindViewById<EditText>(Resource.Id.txtRazaoSocial);
            txtCNPJCli = FindViewById<EditText>(Resource.Id.txtCNPJ);
            spinnerTabelaPreco = FindViewById<Spinner>(Resource.Id.spinnerTblPrecos);

            spinnerTabelaPreco.ItemSelected += new EventHandler<ItemSelectedEventArgs>(spinTblPreco_ItemSelected);

        }

        private void BindView()
        {
            if (cli == null)
                return;

            txtCodCli.Text = cli.id_Codigo;
            txtNomeFantasiaCli.Text = cli.ds_NomeFantasia;
            txtRazaoSocialCli.Text = cli.ds_RazaoSocial;
            txtCNPJCli.Text = cli.ds_Cnpj;
            spinvalortbl = cli.id_TabelaPreco.ToString();
        }

        private void BindModel()
        {
            if (cli == null)
                cli = new Cliente();

            cli.id_Codigo = txtCodCli.Text;
            cli.ds_NomeFantasia = txtNomeFantasiaCli.Text;
            cli.ds_RazaoSocial = txtRazaoSocialCli.Text;
            cli.ds_Cnpj = txtCNPJCli.Text;
            var mytabelapreco = tblprecospinner[spinnerTabelaPreco.SelectedItemPosition];
            cli.id_TabelaPreco = mytabelapreco.Id();
        }

        private bool ValidateViews()
        {
            var validacao = true;

            if (txtCodCli.Length() == 0)
            {
                validacao = false;
                txtCodCli.Error = "Código do cliente inválido!";
            }

            if (txtNomeFantasiaCli.Length() == 0)
            {
                validacao = false;
                txtNomeFantasiaCli.Error = "Nome fantasia inválido!";
            }

            if (txtRazaoSocialCli.Length() == 0)
            {
                validacao = false;
                txtRazaoSocialCli.Error = "Razão social inválida!";
            }

            if (txtCNPJCli.Length() == 0)
            {
                validacao = false;
                txtCNPJCli.Error = "CNPJ inválido!";
            }

            return validacao;
        }

        private void spinTblPreco_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            spinvalortbl = spinnerTabelaPreco.SelectedItem.ToString();
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

        private List<TabelaPrecoSpinner> PopulateSpinnerList()
        {
            List<TabelaPrecoSpinner> minhalista = new List<TabelaPrecoSpinner>();
            TabelaPrecoRepository repo = new TabelaPrecoRepository();

            for (int i = 1; i <= 4; i++)
            {
                var go = repo.Get(i);
                if (go != null)
                {
                    minhalista.Add(new TabelaPrecoSpinner(go.id,go.ds_Descricao));
                    var teste = go.id;
                }
            }
        
            return minhalista;
        }

        private void Save()
        {
            if (!ValidateViews())
                return;
            try
            {
                BindModel();

                var cliente = new Cliente_Manager();
                cliente.Save(cli);

                Intent myIntent = new Intent(this, typeof(Activity_Clientes));
                myIntent.PutExtra("mensagem", cliente.Mensagem);
                SetResult(Result.Ok, myIntent);
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

            alert.SetTitle("Tem certeza que deseja excluir este cliente?");

            alert.SetNegativeButton("Não!", (senderAlert, args) =>
            {

            });

            alert.SetPositiveButton("Sim!", (senderAlert, args) =>
            {
                try
                {
                    var cliente = new Cliente_Manager();
                    cliente.Delete(cli);

                    Intent myIntent = new Intent(this, typeof(Activity_Clientes));
                    myIntent.PutExtra("mensagem", cliente.Mensagem);
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