using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using br.com.weblayer.venda.core.Model;
using br.com.weblayer.venda.core.Bll;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_EditarCliente")]
    public class Activity_EditarCliente : Activity_Base
    {
        private EditText txtCodCli;
        private EditText txtRazaoSocialCli;
        private EditText txtNomeFantasiaCli;
        private EditText txtCNPJCli;
        private Cliente cli;

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

        private void FindViews()
        {
            txtCodCli = FindViewById<EditText>(Resource.Id.txtCodigoCliente);
            txtNomeFantasiaCli = FindViewById<EditText>(Resource.Id.txtNomeFantasia);
            txtRazaoSocialCli = FindViewById<EditText>(Resource.Id.txtRazaoSocial);
            txtCNPJCli = FindViewById<EditText>(Resource.Id.txtCNPJ);
        }

        private void BindView()
        {
            if (cli == null)
                return;

            txtCodCli.Text = cli.id_Codigo;
            txtNomeFantasiaCli.Text = cli.ds_NomeFantasia;
            txtRazaoSocialCli.Text = cli.ds_RazaoSocial;
            txtCNPJCli.Text = cli.ds_Cnpj;
        }

        private void BindModel()
        {
            if (cli == null)
                cli = new Cliente();

            cli.id_Codigo = txtCodCli.Text;
            cli.ds_NomeFantasia = txtNomeFantasiaCli.Text;
            cli.ds_RazaoSocial = txtRazaoSocialCli.Text;
            cli.ds_Cnpj = txtCNPJCli.Text;
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