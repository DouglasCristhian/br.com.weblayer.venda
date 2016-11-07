using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class Cliente_Manager 
    {
        

        public IList<Cliente> GetClientes(string filtro)
        {
            return new ClienteRepository().List();
        }

        public void Save(Cliente obj)
        {
            var Repository = new ClienteRepository();
            Repository.Save(obj);
        }

        public void Delete(Cliente obj)
        {
            var Repository = new ClienteRepository();
            Repository.Delete(obj);
        }


    }
}