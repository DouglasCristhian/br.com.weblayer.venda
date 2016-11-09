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
    [Table("Pedidos")]
    public class Pedido
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(20), NotNull]
        public string id_Codigo { get; set; }

        [MaxLength(15), NotNull]
        public DateTime dt_emissao { get; set; }

        [MaxLength(20), NotNull]
        public string id_cliente { get; set; }

        [MaxLength(20), NotNull]
        public string id_vendedor { get; set; }

        [MaxLength(20)]
        public double vl_total { get; set; }

        [MaxLength(200), NotNull]
        public string ds_observacao { get; set; }
    }
}