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

namespace br.com.weblayer.venda.core.Sinc.Model
{
    public class ProdutoTabelaPreco
    {
        public int id { get; set; }
        public int id_produto { get; set; }
        public int id_tabpreco { get; set; }
        public double vl_Valor { get; set; }
    }
}