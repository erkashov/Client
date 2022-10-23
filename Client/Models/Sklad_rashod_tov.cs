using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Sklad_rashod_tov
    {
        [Key]
        public decimal kod_zap { get; set; }
        public Nullable<int> kod_rashoda { get; set; }
        public Nullable<int> tov { get; set; }
        public Nullable<decimal> count { get; set; }
        public Nullable<decimal> count_pachek { get; set; }
        public Nullable<decimal> nom_prih { get; set; }
        public Nullable<decimal> zena { get; set; }
        public Nullable<decimal> pribyl { get; set; }
        public Nullable<decimal> weight { get; set; }
        public Nullable<decimal> volume { get; set; }
        public decimal summa
        {
            get
            {
                return (decimal)(count * zena);
            }
        }
        //public Nullable<decimal> ploshad { get; set; }
        [ForeignKey("kod_rashoda")]
        public virtual Sklad_rashod Sklad_rashod { get; set; }
        [ForeignKey("tov")]
        public virtual Tovary Tovar { get; set; }

    }
}
