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

namespace br.com.weblayer.venda.android.ClasseTeste
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

        public void Salvar(Produto obj)
        {
            //salvar na base
        }

        public void Excluir(Produto obj)
        {
            //excluir na base
        }


    }
}