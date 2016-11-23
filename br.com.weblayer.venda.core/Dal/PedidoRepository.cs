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
        private double vl_totalitens;

        public string Mensage { get; set; }

        public Pedido Get(int id)
        {
            return Database.GetConnection().Table<Pedido>().Where(x => x.id == id).FirstOrDefault();
        }

        public void Save(Pedido entidade)
        {
            try
            {
                if (entidade.id > 0)
                {
                    var repoitem = new PedidoItemRepository();
                    var itens = repoitem.List(entidade.id);
                    foreach (var item in itens)
                        vl_totalitens += item.nr_quantidade * item.vl_item;

                    entidade.vl_total = vl_totalitens;
                    Database.GetConnection().Update(entidade);
                }
                else
                {
                    Database.GetConnection().Insert(entidade);
                }
            }
            catch (Exception e)
            {
                Mensage = $"Falha ao Inserir a entidade {entidade.GetType()}. Erro: {e.Message}";
            }
        }

        public void Delete(Pedido entidade)
        {
           // Database.GetConnection().Delete(entidade);

            var repoitem = new PedidoItemRepository();

            //Listando os itens e excluindo....
            var itens = repoitem.List(entidade.id);
            foreach (var item in itens)
                repoitem.Delete(item);

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

            Save(new Pedido() { id_Codigo = "1", dt_emissao = DateTime.Parse("2016/04/01"), id_cliente = "C_A", id_vendedor = "V_A", vl_total = 0.0, ds_observacao = "Testando 123" });

        }
    }
}
