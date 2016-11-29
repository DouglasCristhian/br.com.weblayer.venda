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

namespace br.com.weblayer.venda.android.Adapters
{
    class mSpinner 
    {
        public int id;
        public string ds;

        public mSpinner(int idprod, string dsprod)
        {
            id = idprod;
            ds = dsprod;
        }

        public int Id()
        {
            return id;
        }

        public override string ToString()
        {
            return ds;
        }
    }
}