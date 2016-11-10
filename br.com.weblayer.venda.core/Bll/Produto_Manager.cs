using System;
using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class Produto_Manager 
    {
        public string Mensagem;

        public IList<Produto> GetProduto(string filtro)
        {
            return new ProdutoRepository().List();
        }

        public void Save(Produto obj)
        {            
            var erros="";
            
            //regras....
            if (obj.id_Codigo.Length < 2) 
                erros= erros + "\n O código do produto é inválido! Ele deve ter no mínimo 2 caracteres!";

            if (obj.ds_Nome.Length < 10) 
                erros = erros + "\n A descrição do produto deve ter no mínimo 10 caracteres!";

            if (obj.id_TabPreco.Length > 7)
                erros = erros + "\n A tabela de preços deve ter no máximo sete caracteres!";

            if (erros.Length>0)
                throw new Exception(erros);
            
            var Repository = new ProdutoRepository();
            Repository.Save(obj);

            Mensagem = $"Produto {obj.ds_Nome} atualizado com sucesso";
        }

        public void Delete(Produto obj)
        {
            var Repository = new ProdutoRepository();
            Repository.Delete(obj);

            Mensagem = $"Produto {obj.ds_Nome} excluído com sucesso";
        }
    }
}