using System;
using System.Collections.Generic;
using br.com.weblayer.venda.core.Dal;
using br.com.weblayer.venda.core.Model;

namespace br.com.weblayer.venda.core.Bll
{
    public class Produto_Manager 
    {

        public IList<Produto> GetProduto(string filtro)
        {

            var Repository = new ProdutoRepository();
            return Repository.List();
            
        }

        public void Save(Produto obj)
        {
            
            var erros="";
            
            //regras....
            if (obj.id_Codigo.Length < 2) 
                erros= erros + "\n C�digo do produto � inv�lido!Ele deve ter no m�nimo 2 carac...";

            if (obj.ds_Nome.Length < 10) 
                erros = erros + "\n Descri��o do produto n�o pode ter no m�nimo 10 caracteres";


            if (erros.Length>0)
                throw new Exception(erros);


            var Repository = new ProdutoRepository();
            Repository.Save(obj);


        }

        public void Delete(Produto obj)
        {
            var Repository = new ProdutoRepository();
            Repository.Delete(obj);
        }


    }
}