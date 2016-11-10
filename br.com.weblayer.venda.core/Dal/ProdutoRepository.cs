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
            
            Save(new Produto() { id_Codigo = "A11111", ds_Nome = "LAPIS DE COR AMARELO", ds_UniMedida = "CX", id_TabPreco = "A1", ds_ValorProduto = 5.00 });
            Save(new Produto() { id_Codigo = "A22222", ds_Nome = "LAPIS DE COR VERMELHO", ds_UniMedida = "CX", id_TabPreco = "A2", ds_ValorProduto = 10.00 });
            Save(new Produto() { id_Codigo = "A33333", ds_Nome = "LAPIS DE COR AZUL", ds_UniMedida = "CX", id_TabPreco = "A3", ds_ValorProduto = 15.00 });
            Save(new Produto() { id_Codigo = "A44444", ds_Nome = "LAPIS DE COR PRETO", ds_UniMedida = "CX", id_TabPreco = "A4", ds_ValorProduto = 20.00 });
        }

    }
}