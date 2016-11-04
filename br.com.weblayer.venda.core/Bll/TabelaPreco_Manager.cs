using System.Collections.Generic;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class TabelaPreco_Manager
    {
        

        public List<TabelaPreco> GetTabelaPreco(string filtro)
        {

            List<TabelaPreco> lista = new List<TabelaPreco>();

            lista.Add(new TabelaPreco { id_Codigo = "TAB1", ds_Descricao = "Tabela Promoção 1",Valor = decimal.Parse("9.99"),DescontoMaximo = 0});
            lista.Add(new TabelaPreco { id_Codigo = "TAB2", ds_Descricao = "Tabela Promoção 2", Valor = decimal.Parse("20.99"), DescontoMaximo = 0 });
            lista.Add(new TabelaPreco { id_Codigo = "TAB3", ds_Descricao = "Tabela Promoção 3", Valor = decimal.Parse("45.99"), DescontoMaximo = 0 });

            return lista;
        }

        public void Save(TabelaPreco obj)
        {
            //salvar na base
            //var y = 0;
            //var x = 1 / y;
            //string erros="";
            
            //regras....
            //if (obj.ds_CodigoProduto.Length < 3) //codigo do produto deve ter mais de 10 caracteres
            //    erros= erros + "\n Código do produto é inválido!Ele deve ter no mínimo 10 carac...";

            //if (obj.ds_NomeProduto.Length < 20) //bla bla...
            //    erros = erros + "\n Descrição do produto não pode ser blala...";


            //if (erros.Length>0)
            //    throw new Exception(erros);

            //persistir os dados,,,

        }

        public void Delete(TabelaPreco obj)
        {
            //excluir na base
            //var y = 0;
            //var x = 1 / y;
        }


    }
}