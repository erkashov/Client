using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public partial class Tovary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public Tovary()
            {
            }
            [Key]
            public int kod_tovara { get; set; }
            public string naim { get; set; }
            public Nullable<double> dlina { get; set; }
            public Nullable<double> shir { get; set; }
            public Nullable<double> tol { get; set; }
            public string nazn { get; set; }
            public string sort { get; set; }
            public string dekor { get; set; }
            public string zvet { get; set; }
            public string material { get; set; }
            public int? proizv { get; set; }
            public string gor_otgr { get; set; }
            public Nullable<double> ves_lista { get; set; }
            public Nullable<int> kol_list_v_pachke { get; set; }
            public Nullable<int> kol_pachek_v20fut { get; set; }
            public string klass_emis { get; set; }
            public Nullable<int> kod_normativa { get; set; }
            public Nullable<bool> udal { get; set; }
            public Nullable<int> kod_1C { get; set; }
            public Nullable<int> kod_categ { get; set; }

            public bool IsEquals(Tovary t)
            {
                if (t == null) return false;
                if (this == t) return true;
                if (object.ReferenceEquals(this, t)) return true;
                if (t.naim == this.naim && t.sort == this.sort && t.dlina == this.dlina && t.shir == this.shir && t.tol == this.tol/*КАТЕГОРИЯ*/) return true;
                return false;
            }

            public Category Category { get; set; }

        


        /*[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tovary()
        {
            *//*this.crm_Zayavki_transp = new HashSet<crm_Zayavki_transp>();
            this.It_Zayavka_Postavchik_Tovary = new HashSet<It_Zayavka_Postavchik_Tovary>();
            this.Ostatok_tov_v_1C = new HashSet<Ostatok_tov_v_1C>();
            this.Prays_nastr = new HashSet<Prays_nastr>();
            this.Мониторинг_конкурент_цены = new HashSet<Мониторинг_конкурент_цены>();
            this.Zen = new HashSet<Zen>();*//*
        }
        [Key]
        public int kod_tovara { get; set; }
        public string naim { get; set; }
        public Nullable<decimal> dlina { get; set; }
        public Nullable<decimal> shir { get; set; }
        public Nullable<decimal> tol { get; set; }
        public string nazn { get; set; }
        public string sort { get; set; }
        public string dekor { get; set; }
        public string zvet_naim { get; set; }
        public string material { get; set; }
        public string proizv { get; set; }
        public string gor_otgr { get; set; }
        public Nullable<int> list_v_fure { get; set; }
        public Nullable<decimal> kv_v_fure { get; set; }
        public Nullable<decimal> kub_v_fure { get; set; }
        public string naim2 { get; set; }
        public Nullable<decimal> rek_naz_kv { get; set; }
        public Nullable<decimal> rek_naz_kub { get; set; }
        public Nullable<decimal> rek_naz_list { get; set; }
        public Nullable<int> list_v_20fut { get; set; }
        public Nullable<int> list_v_40fut { get; set; }
        public Nullable<int> list_v_vagone { get; set; }
        public Nullable<int> plotnost { get; set; }
        public Nullable<decimal> ves_lista { get; set; }
        public Nullable<decimal> ves_upak_na_pachku { get; set; }
        public Nullable<decimal> ves_v_fure { get; set; }
        public Nullable<int> kol_list_v_pachke { get; set; }
        public Nullable<int> kol_pachek_v_fure { get; set; }
        public Nullable<decimal> kol_pachek_v20fut { get; set; }
        public Nullable<decimal> kol_pachek_v40fut { get; set; }
        public Nullable<decimal> kol_pachek_vagone { get; set; }
        public string klass_emis { get; set; }
        public Nullable<decimal> kod_normativa { get; set; }
        public Nullable<bool> samovyvoz { get; set; }
        public string naim_kp { get; set; }
        public string naim_bez_proizv_sort { get; set; }
        public Nullable<System.DateTime> date_izm { get; set; }
        public string machinename { get; set; }
        public Nullable<bool> udal { get; set; }
        public Nullable<decimal> kod_1C { get; set; }
        public Nullable<decimal> ves_pachki { get; set; }
        public Nullable<int> list_v_poluvagone { get; set; }
        public Nullable<decimal> Ostatok_1C { get; set; }
        public string naim_bez_proizv_dekor { get; set; }
        public Nullable<System.DateTime> spisanie_1C_date { get; set; }
        public string naim_bez_proizv_dekor_sort { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sklad_rashod_tov> Sklad_rashod_tov { get; set; }

        public bool IsEquals(Tovary t)
        {
            if (t == null) return false;
            if (this == t) return true;
            if (object.ReferenceEquals(this, t)) return true;
            if (t.naim == this.naim && t.sort == this.sort && t.dlina == this.dlina && t.shir == this.shir && t.tol == this.tol*//*КАТЕГОРИЯ*//*) return true;
            return false;
        }

        public Nullable<int> kod_categ { get; set; }

        public Category Category { get; set; }*/
        /*public int kod_tovara { get; set; }
        public string naim { get; set; }
        public decimal dlina { get; set; }
        public decimal shir { get; set; }
        public decimal tol { get; set; }
        public string nazn { get; set; }
        public string sort { get; set; }
        public string dekor { get; set; }
        public string zvet_naim { get; set; }
        public string material { get; set; }
        public string proizv { get; set; }
        public string gor_otgr { get; set; }
        public string naim2 { get; set; }
        public Nullable<int> plotnost { get; set; }
        public Nullable<decimal> ves_lista { get; set; }
        *//*public string naim_kp { get; set; }
        public string naim_bez_proizv_sort { get; set; }
        public string naim_bez_proizv_dekor { get; set; }
        public string naim_bez_proizv_dekor_sort { get; set; }*//*
        public Nullable<System.DateTime> date_izm { get; set; }
        public string machinename { get; set; }
        //public Nullable<bool> udal { get; set; }
        public Nullable<decimal> kod_1C { get; set; }


        public Nullable<int> list_v_fure { get; set; }
        public Nullable<decimal> kv_v_fure { get; set; }
        public Nullable<decimal> kub_v_fure { get; set; }
        public Nullable<decimal> rek_naz_kv { get; set; }
        public Nullable<decimal> rek_naz_kub { get; set; }
        public Nullable<decimal> rek_naz_list { get; set; }
        public Nullable<int> list_v_20fut { get; set; }
        public Nullable<int> list_v_40fut { get; set; }
        public Nullable<int> list_v_vagone { get; set; }
        public Nullable<decimal> ves_upak_na_pachku { get; set; }
        public Nullable<decimal> ves_v_fure { get; set; }
        public Nullable<int> kol_list_v_pachke { get; set; }
        public Nullable<int> kol_pachek_v_fure { get; set; }
        public Nullable<decimal> kol_pachek_v20fut { get; set; }
        public Nullable<decimal> kol_pachek_v40fut { get; set; }
        public Nullable<decimal> kol_pachek_vagone { get; set; }
        public Nullable<decimal> ves_pachki { get; set; }
        public Nullable<int> list_v_poluvagone { get; set; }
        public string klass_emis { get; set; }*/

        //public Nullable<decimal> kod_normativa { get; set; }
        //public Nullable<bool> samovyvoz { get; set; }



        //public Nullable<decimal> Ostatok_1C { get; set; }
        //public Nullable<System.DateTime> spisanie_1C_date { get; set; }
        /*
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<crm_Zayavki_transp> crm_Zayavki_transp { get; set; }
        public virtual Dekor Dekor1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<It_Zayavka_Postavchik_Tovary> It_Zayavka_Postavchik_Tovary { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ostatok_tov_v_1C> Ostatok_tov_v_1C { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prays_nastr> Prays_nastr { get; set; }
        public virtual Proizv Proizv1 { get; set; }
        public virtual Spisok_zvet Spisok_zvet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Мониторинг_конкурент_цены> Мониторинг_конкурент_цены { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zen> Zen { get; set; }*/
    }
}
