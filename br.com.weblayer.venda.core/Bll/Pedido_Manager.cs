using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.core.Model;
using System;

namespace br.com.weblayer.venda.core.Bll
{
    public class Pedido_Manager
    {
        public string Mensagem;

        public IList<Pedido> GetPedidos(string filtro)
        {
            return new PedidoRepository().List();
        }

        public void Save(Pedido obj)
        {
            var erros = "";

            //regras....
            if (obj.id_Codigo.Length < 2)
                erros = erros + "\n O c�digo do pedido deve ter no m�nimo 2 caracteres!";

            if (obj.id_cliente.Length < 5)
                erros = erros + "\n A descri��o do produto deve ter no m�nimo 10 caracteres!";

            //TODO: Devidas exce��es

            if (erros.Length > 0)
                throw new Exception(erros);

            var Repository = new PedidoRepository();
            Repository.Save(obj);

            Mensagem = $"Pedido {obj.id_Codigo} atualizado com sucesso";
        }

        public void Delete(Pedido obj)
        {
            var Repository = new PedidoRepository();
            Repository.Delete(obj);

            Mensagem = $"Pedido {obj.id_Codigo} exclu�do com sucesso";
        }


    }
}