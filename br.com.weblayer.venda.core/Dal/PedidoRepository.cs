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
                if ((entidade.id > 0) && Get(entidade.id) != null)
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
            var repoitem = new PedidoItemRepository();

            try
            {
                //Listando os itens e excluindo....
                var itens = repoitem.List(entidade.id);
                foreach (var item in itens)
                    repoitem.Delete(item);

                Database.GetConnection().Delete(entidade);
            }
            catch (Exception e)
            {
                Mensage = $"Falha ao deletar a entidade {entidade.GetType()}. Erro: {e.Message}";
            }


        }

        public IList<Pedido> List()
        {
            return Database.GetConnection().Table<Pedido>().ToList();
        }

        public void MakeDataMock()
        {
            if (List().Count > 0)
                return;

            Save(new Pedido() { id_codigo = "1", dt_emissao = DateTime.Parse("2016/04/01"), id_cliente = 1, ds_cliente = "UNITY SISTEMAS", id_vendedor = 1, ds_vendedor = "Maria Lina", vl_total = 0.0, ds_observacao = "Testando 123", fl_status = 0});
            Save(new Pedido() { id_codigo = "2", dt_emissao = DateTime.Parse("2016/06/07"), id_cliente = 2, ds_cliente = "INVISIBLE TUCS", id_vendedor = 1, ds_vendedor = "Saory Emanoelle", vl_total = 0.0, ds_observacao = "Testando 456", fl_status = 1 });
            Save(new Pedido() { id_codigo = "3", dt_emissao = DateTime.Parse("2017/03/01"), id_cliente = 1, ds_cliente = "UNITY SISTEMAS", id_vendedor = 1, ds_vendedor = "Douglas Christian", vl_total = 0.0, ds_observacao = "Testando 789", fl_status = 2 });
            Save(new Pedido() { id_codigo = "4", dt_emissao = DateTime.Parse("2017/01/01"), id_cliente = 2, ds_cliente = "INVISIBLE TUCS", id_vendedor = 1, ds_vendedor = "Saory Emanoelle", vl_total = 0.0, ds_observacao = "Testando 000", fl_status = 3 });
            Save(new Pedido() { id_codigo = "4", dt_emissao = DateTime.Parse("2017/01/01"), id_cliente = 2, ds_cliente = "SISTEMA TUCS", id_vendedor = 1, ds_vendedor = "Saory Emanoelle", vl_total = 0.0, ds_observacao = "Testando 011", fl_status = 4 });

        }
    }
}
