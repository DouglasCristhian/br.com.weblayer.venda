using System;
using System.Collections.Generic;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class Produto_Manager 
    {

        public List<Produto> GetProduto(string filtro)
        {
            List<Produto> lista = new List<Produto>();

            lista.Add(new Produto { id_Codigo = "AAA", ds_Nome = "Manteiga com Sal", ds_UniMedida = "CX", id_TabPreco = "0,05" });
            lista.Add(new Produto { id_Codigo = "BBB", ds_Nome = "Manteiga sem Sal", ds_UniMedida = "CX", id_TabPreco = "0,50" });
            lista.Add(new Produto { id_Codigo = "CCC", ds_Nome = "Margarina com Sal", ds_UniMedida = "CX", id_TabPreco = "1.00" });

            return lista;
        }

        public void Save(Produto obj)
        {
            //salvar na base
            //var y = 0;
            //var x = 1 / y;
            string erros="";


            //regras....
            if (obj.id_Codigo.Length < 3) //codigo do produto deve ter mais de 10 caracteres
                erros= erros + "\n Código do produto é inválido!Ele deve ter no mínimo 10 carac...";

            if (obj.ds_Nome.Length < 20) //bla bla...
                erros = erros + "\n Descrição do produto não pode ser blala...";


            if (erros.Length>0)
                throw new Exception(erros);

            //persistir os dados,,,

        }

        public void Delete(Produto obj)
        {
            //excluir na base
            var y = 0;
            var x = 1 / y;
        }


    }
}