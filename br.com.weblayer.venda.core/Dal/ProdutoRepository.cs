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

namespace br.com.weblayer.venda.core.Dal
{
    public class ProdutoRepository
    {
        public string Mensage { get; set; }

        public Produto Get(int id)
        {
            return Database.GetConnection().Table<Produto>().Where(x => x.id == id).FirstOrDefault();
        }


        public void Save(Produto entidade)
        {

            try
            {
                if (entidade.id>0)
                    Database.GetConnection().Update(entidade);
                else
                    Database.GetConnection().Insert(entidade);
            }
            catch (Exception e)
            {
                Mensage = $"Falha ao Inserir a entidade {entidade.GetType()}. Erro: {e.Message}";
            }
        }

        public void Delete(Produto entidade)
        {
            Database.GetConnection().Delete(entidade);
        }

        public IList<Produto> List()
        {
            
            return Database.GetConnection().Table<Produto>().ToList();
             
        }

        public void MakeDataMock()
        {
            if (List().Count > 0)
                return;
            
            Save(new Produto() { id_codigo = "11111", ds_nome = "LAPIS DE COR AMARELO", ds_unimedida = "CX", id_tabpreco = 1, vl_Valor = 5.00 });
            Save(new Produto() { id_codigo = "22222", ds_nome = "LAPIS DE COR VERMELHO", ds_unimedida = "CX", id_tabpreco = 2, vl_Valor = 10.00 });
            Save(new Produto() { id_codigo = "33333", ds_nome = "LAPIS DE COR AZUL", ds_unimedida = "CX", id_tabpreco = 3, vl_Valor = 15.00 });
            Save(new Produto() { id_codigo = "44444", ds_nome = "LAPIS DE COR PRETO", ds_unimedida = "CX", id_tabpreco = 4, vl_Valor = 20.00 });
        }

    }
}