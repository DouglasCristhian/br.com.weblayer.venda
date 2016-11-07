using System.Collections.Generic;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class Cliente_Manager 
    {
        

        public List<Cliente> GetClientes(string filtro)
        {

            List<Cliente> lista = new List<Cliente>();

            lista.Add(new Cliente { id_Codigo = "1111", ds_RazaoSocial = "AAAAAAAA", ds_NomeFantasia = "AAAAAAAA", ds_Cnpj = "9999999999"});
            lista.Add(new Cliente { id_Codigo = "2222", ds_RazaoSocial = "BBBBBBBB", ds_NomeFantasia = "BBBBBBBB", ds_Cnpj = "9999999999" });
            lista.Add(new Cliente { id_Codigo = "3333", ds_RazaoSocial = "CCCCCCCC", ds_NomeFantasia = "CCCCCCCC", ds_Cnpj = "9999999999" });
            lista.Add(new Cliente { id_Codigo = "4444", ds_RazaoSocial = "DDDDDDDD", ds_NomeFantasia = "DDDDDDDD", ds_Cnpj = "9999999999" });


            return lista;
        }

        public void Save(Cliente obj)
        {
            
        }

        public void Delete(Cliente obj)
        {

        }


    }
}