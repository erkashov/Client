using System;
using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public partial class Sheta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sheta()
        {
            /*this.Sklad_dostavki = new HashSet<Sklad_dostavki>();
            this.Sheta_tov = new HashSet<Sheta_tov>();*/
            //this.Sklad_rashod = new HashSet<Sklad_rashod>();
        }
        [Key]
        public decimal kod_zap { get; set; }
        public Nullable<decimal> nom_1C { get; set; }
        public Nullable<System.DateTime> data { get; set; }
        public Nullable<decimal> org { get; set; }
        public Nullable<decimal> pok { get; set; }
        public Nullable<decimal> summa { get; set; }
        public Nullable<decimal> nds { get; set; }
        public Nullable<decimal> kod_polz { get; set; }
        public Nullable<System.DateTime> date_izm { get; set; }
        public string prim { get; set; }
        public Nullable<decimal> pribyl { get; set; }
        public Nullable<decimal> ves_itog { get; set; }
        public Nullable<decimal> dost { get; set; }
        public string dost_spopl { get; set; }
        public Nullable<bool> dost_vkl { get; set; }
        public string machinename { get; set; }
        public Nullable<decimal> summa_post { get; set; }
        public Nullable<decimal> status { get; set; }
        public Nullable<bool> korr { get; set; }
        public Nullable<decimal> kod_rasch_sheta { get; set; }
        public string valuta { get; set; }
        public Nullable<bool> oplachen { get; set; }
        public Nullable<bool> small_zena { get; set; }
        public string rukopisn_text { get; set; }
        public bool rukopisn_bool { get; set; }
        public Nullable<decimal> nazenka { get; set; }
        public Nullable<decimal> skidka_na_exp { get; set; }
        public Nullable<bool> ne_per_zen_s_dost { get; set; }
        public Nullable<decimal> doc_po_oplate { get; set; }
        public Nullable<bool> spec { get; set; }
        public Nullable<bool> sklad { get; set; }
        public Nullable<bool> rezerv { get; set; }
        public Nullable<bool> prim_is_plat { get; set; }
        public Nullable<bool> zakup_na_sklad { get; set; }
        public Nullable<decimal> kurs_valuty { get; set; }
        public Nullable<decimal> volume_itog { get; set; }
        public Nullable<bool> transit { get; set; }
        public Nullable<bool> close { get; set; }
        public Nullable<bool> za_nal { get; set; }
        public Nullable<System.DateTime> data_oplaty { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Sklad_dostavki> Sklad_dostavki { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Sheta_tov> Sheta_tov { get; set; }
        /*[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sklad_rashod> Sklad_rashod { get; set; }*/
    }
}
