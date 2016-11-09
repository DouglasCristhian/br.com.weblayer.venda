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
    public class PedidoRepository 
    {
        public string Mensage { get; set; }

        public void Save(Pedido entidade)
        {
            try
            {
                if (entidade.id > 0)
                    Database.GetConnection().Update(entidade);
                else
                    Database.GetConnection().Insert(entidade);
            }
            catch (Exception e)
            {
                Mensage = $"Falha ao Inserir a entidade {entidade.GetType()}. Erro: {e.Message}";
            }
        }

        public void Delete(Pedido entidade)
        {
            Database.GetConnection().Delete(entidade);
        }

        public IList<Pedido> List()
        {

            return Database.GetConnection().Table<Pedido>().ToList();

        }

        public void MakeDataMock()
        {
            if (List().Count > 0)
                return;

            Save(new Pedido() { id_Codigo = "1", dt_emissao = DateTime.Parse("2016/04/01"), id_cliente = "C_A", id_vendedor = "V_A", vl_total = 50.0, ds_observacao = "Testando 123" });

        }
    }
}
