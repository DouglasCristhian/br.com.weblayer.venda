using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace br.com.weblayer.venda.android.Activities
{
    [Activity(Label = "Configurações")]
    public class Activity_Configuracoes : Activity_Base
    {
        CheckBox checkbox1;
        CheckBox checkbox2;
        CheckBox checkbox3;
        LinearLayout lnrLayout;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_Configuracoes;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FindViews();
            BindData();
        }  

        private void FindViews()
        {
            checkbox1 = FindViewById<CheckBox>(Resource.Id.checkBox_Tema1);
            checkbox2 = FindViewById<CheckBox>(Resource.Id.checkBox_Tema2);
            checkbox3 = FindViewById<CheckBox>(Resource.Id.checkBox_Tema3);
            lnrLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
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
            }
            else
            {
                checkbox2.Enabled = true;
                checkbox3.Enabled = true;
            }         
        }

        private void Checkbox2_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (checkbox2.Checked)
            {
                checkbox1.Enabled = false;
                checkbox3.Enabled = false;
            }
            else
            {
                checkbox1.Enabled = true;
                checkbox3.Enabled = true;
            }
        }

        private void Checkbox3_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (checkbox3.Checked)
            {
                checkbox1.Enabled = false;
                checkbox2.Enabled = false;
            }
            else
            {
                checkbox1.Enabled = true;
                checkbox2.Enabled = true;
            }
        }
    }
}