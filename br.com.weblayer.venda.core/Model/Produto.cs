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
using SQLite;

namespace br.com.weblayer.venda.core.Model
{
    [Table("Produto")]
    public class Produto 
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(12), NotNull]
        public string id_Codigo { get; set;}

        [MaxLength(200), NotNull]
        public string ds_Nome { get; set; }

        [MaxLength(20), NotNull]
        public string ds_UniMedida { get; set; }

        [MaxLength(20), NotNull]
        public string id_TabPreco { get; set; }
    }
}