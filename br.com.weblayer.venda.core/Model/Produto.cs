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

namespace br.com.weblayer.venda.core.Model
{
    public class Produto 
    {
        public string id_Codigo { get; set; }
        public string ds_Nome { get; set; }
        public string ds_UniMedida { get; set; }
        public string id_TabPreco { get; set; }
    }
}