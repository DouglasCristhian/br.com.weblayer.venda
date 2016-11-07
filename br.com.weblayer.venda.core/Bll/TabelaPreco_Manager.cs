using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class TabelaPreco_Manager
    {
        public string Mensagem;

        public IList<TabelaPreco> GetTabelaPreco(string filtro)
        {

            return new TabelaPrecoRepository().List();

        }

        public void Save(TabelaPreco obj)
        {
            var Repository = new TabelaPrecoRepository();
            Repository.Save(obj);

            Mensagem = $"Tabela de preços {obj.ds_Descricao} atualizada com sucesso";
        }

        public void Delete(TabelaPreco obj)
        {
            var Repository = new TabelaPrecoRepository();
            Repository.Delete(obj);

            Mensagem = $"Tabela de preços {obj.ds_Descricao} excluída com sucesso";
        }


    }
}