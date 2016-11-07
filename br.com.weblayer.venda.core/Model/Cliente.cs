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
    [Table("Cliente")]
    public class Cliente 
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(60), NotNull]
        public string id_Codigo { get; set; }

        [MaxLength(200), NotNull]
        public string ds_RazaoSocial { get; set; }

        [MaxLength(200), NotNull]
        public string ds_NomeFantasia { get; set; }

        [MaxLength(20)]
        public string ds_Cnpj { get; set; }
    }
}