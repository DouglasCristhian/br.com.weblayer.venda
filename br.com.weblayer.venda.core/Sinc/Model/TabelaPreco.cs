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
    public class TabelaPreco
    {
        public int id { get; set; }
        public string id_codigo { get; set; }
        public string ds_descricao { get; set; }
        public double vl_valor { get; set; }
        public double vl_descontomaximo { get; set; }
    }
}