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
    [Table("PedidoItem")]
    public class PedidoItem
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(20), NotNull]
        public int id_pedido { get; set; }

        [MaxLength(15), NotNull]
        public int id_produto { get; set; }

        [MaxLength(20), NotNull]
        public double vl_item { get; set; }

        [MaxLength(20), NotNull]
        public int nr_quantidade { get; set; }
    }
}