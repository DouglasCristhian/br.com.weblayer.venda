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

namespace br.com.weblayer.venda.android.Activities
{
    class TabelaPrecoSpinner
    {
        public int id_Tabela;
        public string ds_Tabela;

        public TabelaPrecoSpinner(int Id_tabela, string Ds_Tabela)
        {
            id_Tabela = Id_tabela;
            ds_Tabela = Ds_Tabela;

        }

        public int Id()
        {
            return id_Tabela;
        }

        public override string ToString()
        {
            return ds_Tabela;
        }
    }
}
