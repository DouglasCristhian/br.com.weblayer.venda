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
using br.com.weblayer.venda.core.Bll;
using Java.Text;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Activity_EditarTabelaPreco")]
    public class Activity_EditarTabelaPreco : Activity_Base
    {
        private EditText txtCodTabelaPreco;
        private EditText txtDescricaoTabelaPreco;
        private EditText txtValorTabelaPreco;
        private EditText txtDescMaxTabelaPreco;
        private TabelaPreco tblPreco;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_EditarTabelaPreco);

            var jsonnota = Intent.GetStringExtra("JsonNotaTabela");
            if (jsonnota == null)
            {
                tblPreco = null;
            }
            else
            {
                tblPreco = Newtonsoft.Json.JsonConvert.DeserializeObject<TabelaPreco>(jsonnota);
            }

            FindViews();
            BindView();
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

        private bool ValidateViews()
        {
            var validacao = true;

            if (txtCodTabelaPreco.Length() == 0)
            {
                validacao = false;
                txtCodTabelaPreco.Error = "C�digo da tabela inv�lido!";
            }

            if (txtDescricaoTabelaPreco.Length() == 0)
            {
                validacao = false;
                txtDescricaoTabelaPreco.Error = "Descri��o da tabela inv�lida!";
            }

            if (txtValorTabelaPreco.Length() == 0)
            {
                validacao = false;
                txtValorTabelaPreco.Error = "Valor da tabela inv�lido!";
            }

            if (txtDescMaxTabelaPreco.Length() == 0)
            {
                validacao = false;
                txtDescMaxTabelaPreco.Error = "Desconto m�ximo inv�lido!";
            }

            return validacao;
        }

        private void FindViews()
        {
            txtCodTabelaPreco = FindViewById<EditText>(Resource.Id.txtCodigoTabelaPreco);
            txtDescricaoTabelaPreco = FindViewById<EditText>(Resource.Id.txtDescricaoTabelaPreco);
            txtValorTabelaPreco = FindViewById<EditText>(Resource.Id.txtValorTabelaPreco);
            txtDescMaxTabelaPreco = FindViewById<EditText>(Resource.Id.txtDescontoMaxTabelaPreco);
        }

        private void BindView()
        {
            if (tblPreco == null)
                return;

            txtCodTabelaPreco.Text = tblPreco.id_Codigo;
            txtDescricaoTabelaPreco.Text = tblPreco.ds_Descricao;
            txtValorTabelaPreco.Text = tblPreco.Valor.ToString();
            txtDescMaxTabelaPreco.Text = tblPreco.DescontoMaximo.ToString();
        }

        private void BindModel()
        {
            if (tblPreco == null)
                tblPreco = new TabelaPreco();

            tblPreco.id_Codigo = txtCodTabelaPreco.Text;
            tblPreco.ds_Descricao = txtDescricaoTabelaPreco.Text;
            tblPreco.Valor = decimal.Parse(txtValorTabelaPreco.Text);
            tblPreco.DescontoMaximo = Convert.ToDecimal(txtDescMaxTabelaPreco.Text);

        }

        private void Save()
        {
            if (!ValidateViews())
                return;

            try
            {
                BindModel();
                var tabelapreco = new TabelaPreco_Manager();
                tabelapreco.Save(tblPreco);

                Finish();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void Delete()
        {
            if (!ValidateViews())
                return;

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Tem certeza que deseja excluir esta tabela?");

            alert.SetNegativeButton("N�o!", (senderAlert, args) =>
            {

            });

            alert.SetPositiveButton("Sim!", (senderAlert, args) =>
            {
                try
                {
                    var tabela = new TabelaPreco_Manager();

                    tabela.Delete(tblPreco);

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