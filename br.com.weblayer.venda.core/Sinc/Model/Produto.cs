namespace br.com.weblayer.venda.core.Sinc.Model
{
    public class Produto
    {
        public int id { get; set; }
        public string id_codigo { get; set; }
        public string ds_nome { get; set; }
        public string ds_unimedida { get; set; }
        public int id_tabpreco { get; set; }
        //public double vl_Valor { get; set; }
        public double vl_Lista { get; set; }
        public double vl_Venda { get; set; }
    }
}