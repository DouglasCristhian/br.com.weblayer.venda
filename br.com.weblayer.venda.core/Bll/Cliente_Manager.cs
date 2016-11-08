using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.core.Model;
using System;

namespace br.com.weblayer.venda.core.Bll
{
    public class Cliente_Manager 
    {
        public string Mensagem;

        public IList<Cliente> GetClientes(string filtro)
        {
            return new ClienteRepository().List();
        }

        public void Save(Cliente obj)
        {
            var erros = "";

            //regras....
            if (obj.id_Codigo.Length < 2)
                erros = erros + "\n O código do cliente é inválido! Ele deve ter no mínimo 2 caracteres!";

            if (obj.ds_NomeFantasia.Length < 10)
                erros = erros + "\n A descrição do produto deve ter no mínimo 10 caracteres!";

            //TODO: Devidas exceções

            if (erros.Length > 0)
                throw new Exception(erros);

            var Repository = new ClienteRepository();
            Repository.Save(obj);

            Mensagem = $"Cliente {obj.ds_RazaoSocial} atualizado com sucesso";
        }

        public void Delete(Cliente obj)
        {
            var Repository = new ClienteRepository();
            Repository.Delete(obj);

            Mensagem = $"Cliente {obj.ds_RazaoSocial} excluído com sucesso";
        }


    }
}