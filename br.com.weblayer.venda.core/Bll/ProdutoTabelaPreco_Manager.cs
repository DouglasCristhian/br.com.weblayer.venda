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
using br.com.weblayer.venda.core.Dal;

namespace br.com.weblayer.venda.core.Bll
{
    public class ProdutoTabelaPreco_Manager
    {
        public string Mensagem;

        public ProdutoTabelaPreco Get(int id_tabelapreco, int id_produto)
        {
            return new ProdutoTabelaPrecoRepository().Get(id_tabelapreco, id_produto);
        }

        public IList<ProdutoTabelaPreco> GetProdTabPreco(string filtro)
        {
            return new ProdutoTabelaPrecoRepository().List();
        }       

        public void Save(ProdutoTabelaPreco obj)
        {
            var Repository = new ProdutoTabelaPrecoRepository();
            Repository.Save(obj);

            Mensagem = $"Tabela de preços {obj.id} foi atualizada com sucesso!";
        }

        public void Delete(ProdutoTabelaPreco obj)
        {
            var Repository = new ProdutoTabelaPrecoRepository();
            Repository.Delete(obj);

            Mensagem = $"Tabela de preços {obj.id} foi excluída com sucesso!";
        }
    }
}