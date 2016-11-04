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
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class Produto_Manager 
    {
        List<Produto> listaprod = new List<Produto>();

        public List<Produto> GetProduto(string filtro)
        {
            //var listaprod = new List<Produto>();

            listaprod.Add(new Produto { ds_CodigoProduto = "AAA", ds_NomeProduto = "Manteiga com Sal", ds_UniMedidaProduto = "CX", ds_TblPrecoProduto = "0,05" });
            listaprod.Add(new Produto { ds_CodigoProduto = "BBB", ds_NomeProduto = "Manteiga sem Sal", ds_UniMedidaProduto = "CX", ds_TblPrecoProduto = "0,50" });
            listaprod.Add(new Produto { ds_CodigoProduto = "CCC", ds_NomeProduto = "Margarina com Sal", ds_UniMedidaProduto = "CX", ds_TblPrecoProduto = "1.00" });

            return listaprod;
        }

        public void Save(Produto obj)
        {
            //salvar na base
            //var y = 0;
            //var x = 1 / y;
            string erros="";


            //regras....
            if (obj.ds_CodigoProduto.Length < 3) //codigo do produto deve ter mais de 10 caracteres
                erros= erros + "\n Código do produto é inválido!Ele deve ter no mínimo 10 carac...";

            if (obj.ds_NomeProduto.Length < 20) //bla bla...
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