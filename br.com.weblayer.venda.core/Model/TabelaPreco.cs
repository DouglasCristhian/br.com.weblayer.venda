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
    public class TabelaPreco
    {

        public TabelaPreco( ) {}

        public TabelaPreco(string id_codigo, string ds_descricao)
        {
            this.id_Codigo = id_Codigo;
            this.ds_Descricao = ds_descricao;
        }

        public string id_Codigo { get; set; }
        public string ds_Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal DescontoMaximo { get; set; }
    }
}