using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class TabelaPreco_Manager
    {
        

        public IList<TabelaPreco> GetTabelaPreco(string filtro)
        {

            return new TabelaPrecoRepository().List();

        }

        public void Save(TabelaPreco obj)
        {
            var Repository = new TabelaPrecoRepository();
            Repository.Save(obj);
        }

        public void Delete(TabelaPreco obj)
        {
            var Repository = new TabelaPrecoRepository();
            Repository.Delete(obj);
        }


    }
}