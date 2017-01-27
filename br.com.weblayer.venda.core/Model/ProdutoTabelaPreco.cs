using SQLite;

namespace br.com.weblayer.venda.core.Model
{
    [Table("ProdutoTabelaPreco")]
    public class ProdutoTabelaPreco
    {
        [PrimaryKey]
        public int id { get; set; }
        
        public int id_produto { get; set; }

        public int id_tabpreco{ get; set; }

        public double vl_Valor{ get; set; }
    }
}