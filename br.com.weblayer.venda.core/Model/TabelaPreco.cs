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
using SQLite;

namespace br.com.weblayer.venda.core.Model
{
    [Table("TabelaPreco")]
    public class TabelaPreco
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(20)]
        public string id_Codigo { get; set; }

        [MaxLength(200)]
        public string ds_Descricao { get; set; }

        public double Valor { get; set;}

        public double DescontoMaximo { get; set; }
    }
}