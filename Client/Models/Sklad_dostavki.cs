using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Sklad_dostavki
    {
        [Key]
        public decimal id { get; set; }
        public decimal sklad_rashod_id { get; set; }
        public string address { get; set; }
        public decimal summa { get; set; }
        public decimal tip_opl_id { get; set; }
        public bool opl_klientom { get; set; }
        public Nullable<System.DateTime> data_opl_klientom { get; set; }
        public bool opl_voditelu { get; set; }
        public string prim { get; set; }
        public bool provereno { get; set; }
        public Nullable<decimal> karta_id { get; set; }
        public Nullable<decimal> perevoz_id { get; set; }
        public Nullable<decimal> voditel_id { get; set; }
        public Nullable<bool> opl_na_vigruz { get; set; }
        public Nullable<decimal> shet { get; set; }
        public Nullable<System.DateTime> data_rash { get; set; }
        public Nullable<decimal> platel { get; set; }
        public string otpustil { get; set; }

        /*public virtual Karta Karta { get; set; }
        public virtual Perevozy Perevozy { get; set; }*/

        [ForeignKey("shet")]
        public virtual Sheta Sheta { get; set; }
        [ForeignKey("sklad_rashod_id")]
        public virtual Sklad_rashod Sklad_rashod { get; set; }
        /*public virtual Sklad_voditely Sklad_voditely { get; set; }*/
        [ForeignKey("tip_opl_id")]
        public virtual Spr_oplat_sklad Spr_oplat_sklad { get; set; }
    }
}
