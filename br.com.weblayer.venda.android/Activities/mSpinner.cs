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

namespace br.com.weblayer.venda.android.Activities
{
    class mSpinner 
    {
        public int id_Produto;
        public string ds_Produto;

        public mSpinner(int idprod, string dsprod)
        {
            id_Produto = idprod;
            ds_Produto = dsprod;
        }

        public int Id()
        {
            return id_Produto;
        }

        public override string ToString()
        {
            return ds_Produto;
        }
    }
}