using System.Collections.Generic;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class Cliente_Manager 
    {
        

        public List<Cliente> GetProduto(string filtro)
        {

            List<Cliente> lista = new List<Cliente>();

            lista.Add(new Cliente { id_Codigo = "1111", ds_RazaoSocial = "AAAAAAAA", ds_NomeFantasia = "AAAAAAAA", ds_Cnpj = "9999999999"});
            lista.Add(new Cliente { id_Codigo = "2222", ds_RazaoSocial = "BBBBBBBB", ds_NomeFantasia = "BBBBBBBB", ds_Cnpj = "9999999999" });
            lista.Add(new Cliente { id_Codigo = "3333", ds_RazaoSocial = "CCCCCCCC", ds_NomeFantasia = "CCCCCCCC", ds_Cnpj = "9999999999" });
            lista.Add(new Cliente { id_Codigo = "4444", ds_RazaoSocial = "DDDDDDDD", ds_NomeFantasia = "DDDDDDDD", ds_Cnpj = "9999999999" });


            return lista;
        }

        public void Save(Produto obj)
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

        public void Delete(Produto obj)
        {
            //excluir na base
            //var y = 0;
            //var x = 1 / y;
        }


    }
}