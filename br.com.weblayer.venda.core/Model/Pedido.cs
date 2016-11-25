using System;
using SQLite;

namespace br.com.weblayer.venda.core.Model
{
    [Table("Pedidos")]
    public class Pedido
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(20)]
        public string id_codigo { get; set; }

        [MaxLength(15)]
        public DateTime dt_emissao { get; set; }

        [MaxLength(20)]
        public int id_cliente { get; set; }

        [MaxLength(20)]
        public int id_vendedor { get; set; }

        [MaxLength(20)]
        public double vl_total { get; set; }

        [MaxLength(200)]
        public string ds_observacao { get; set; }
    }
}