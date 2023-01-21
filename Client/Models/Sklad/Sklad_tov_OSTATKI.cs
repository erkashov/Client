using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.Sklad
{
    public class Sklad_tov_OSTATKI
    {
        [Key]
        public int kod_tovara { get; set; }
        public Nullable<decimal> prihod_total { get; set; }
        public Nullable<decimal> rashod_total { get; set; }
        public Nullable<decimal> rek_kol_pachek { get; set; }
        public Nullable<decimal> not_in_sklad { get; set; }
        public Nullable<System.DateTime> data_prih { get; set; }
        public Nullable<decimal> sr_zena { get; set; }
        public Nullable<decimal> sr_zena_new { get; set; }
        public Nullable<System.DateTime> data_prih_last { get; set; }
        public Nullable<int> OSTATOK { get; set; }
        public Nullable<decimal> OSTATOK_new { get; set; }
        public Nullable<decimal> rezerved { get; set; }
        public Nullable<decimal> prihod_beznal { get; set; }
        public Nullable<decimal> OSTATOK_total { get; set; }
        public Nullable<decimal> kod_tov { get; set; }

        [ForeignKey("kod_tovara")]
        public virtual Product Tovar { get; set; }
    }
}
