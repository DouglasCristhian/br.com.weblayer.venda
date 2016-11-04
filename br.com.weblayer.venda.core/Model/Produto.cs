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
        public string ds_CodigoProduto { get; set; }
        public string ds_NomeProduto { get; set; }
        public string ds_UniMedidaProduto { get; set; }
        public string ds_TblPrecoProduto { get; set; }
    }
}