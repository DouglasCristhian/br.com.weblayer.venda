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
using Android.Graphics;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Configurações")]
    public class Activity_Configuracoes : Activity_Base
    {
        CheckBox checkbox1;
        CheckBox checkbox2;
        CheckBox checkbox3;
        LinearLayout lnrLayout;
        TextView txt1;
        TextView txt2;
        TextView txt3;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_Configuracoes);

            FindViews();
            BindData();
        }  

        private void FindViews()
        {
            checkbox1 = FindViewById<CheckBox>(Resource.Id.checkBox_Tema1);
            checkbox2 = FindViewById<CheckBox>(Resource.Id.checkBox_Tema2);
            checkbox3 = FindViewById<CheckBox>(Resource.Id.checkBox_Tema3);
            lnrLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            txt1 = FindViewById<TextView>(Resource.Id.txtTema1Teste);
            txt2 = FindViewById<TextView>(Resource.Id.txtTema2Teste);
            txt3 = FindViewById<TextView>(Resource.Id.txtTema3Teste);
        }

        private void BindData()
        {
            checkbox1.CheckedChange += Checkbox1_CheckedChange;
            checkbox2.CheckedChange += Checkbox2_CheckedChange;
            checkbox3.CheckedChange += Checkbox3_CheckedChange;
        }

        private void Checkbox1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (checkbox1.Checked)
            {
                checkbox2.Enabled = false;
                checkbox3.Enabled = false;
                txt1.Text = "Ativado";
            }
            else
            {
                checkbox2.Enabled = true;
                checkbox3.Enabled = true;
                txt1.Text = "Desativado";
            }         
        }

        private void Checkbox2_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (checkbox2.Checked)
            {
                checkbox1.Enabled = false;
                checkbox3.Enabled = false;
                txt2.Text = "Ativado";
            }
            else
            {
                checkbox1.Enabled = true;
                checkbox3.Enabled = true;
                txt2.Text = "Desativado";
            }
        }

        private void Checkbox3_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (checkbox3.Checked)
            {
                checkbox1.Enabled = false;
                checkbox2.Enabled = false;
                txt3.Text = "Ativado";
            }
            else
            {
                checkbox1.Enabled = true;
                checkbox2.Enabled = true;
                txt3.Text = "Desativado";
            }
        }
    }
}