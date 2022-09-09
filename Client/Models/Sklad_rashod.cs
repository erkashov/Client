using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Sklad_rashod : INotifyPropertyChanged
    {
        [Key]
        public decimal kod_zap { get; set; }
        public Nullable<System.DateTime> data_rash { get; set; }
        public Nullable<decimal> org { get; set; }
        public Nullable<decimal> oplata { get; set; }

        private string otpustil;
        public string Otpustil
        {
            get
            {
                return this.otpustil;
            }
            set
            {
                this.otpustil = value;
                OnPropertyChanged(nameof(Otpustil)); 
            }
        }
        public string prim { get; set; }
        public Nullable<decimal> nom_rash { get; set; }
        public Nullable<decimal> shet { get; set; }
        public Nullable<bool> otgruzheno { get; set; }
        public Nullable<decimal> summa { get; set; }
        public string name_pokup { get; set; }
        public Nullable<decimal> phone_pokup { get; set; }
        public Nullable<bool> sdano { get; set; }
        public string uznali { get; set; }
        public Nullable<decimal> summa_beznal { get; set; }
        public Nullable<decimal> summa_karta { get; set; }
        public string Адрес_Доставки { get; set; }
        public string Водитель { get; set; }
        public Nullable<bool> На_производство { get; set; }
        public Nullable<decimal> summa_dost { get; set; }
        public Nullable<bool> opl_pok_karta { get; set; }
        public Nullable<bool> opl_pok_nal { get; set; }
        public Nullable<bool> opl_voditel { get; set; }
        public Nullable<bool> isTovOpl { get; set; }
        public Nullable<decimal> weight { get; set; }
        public Nullable<decimal> volume { get; set; }
        public Nullable<bool> isChecked { get; set; }
        public Nullable<decimal> karta { get; set; }
        public string prim_buh { get; set; }
        public Nullable<System.DateTime> data_opl { get; set; }
        public Nullable<decimal> dolg { get; set; }
        public Nullable<bool> IsInvent { get; set; }
        public Nullable<bool> isNotInProdazhiHistory { get; set; }
        public Nullable<System.DateTime> data_otgruzki { get; set; }
        public Nullable<System.TimeSpan> time_rash { get; set; }
        public Nullable<System.TimeSpan> time_otgruzki { get; set; }
        public Nullable<System.DateTime> data_sozdania { get; set; }
        public Nullable<System.DateTime> opl_pok_data { get; set; }
        public string primZavSklad { get; set; }
        public Nullable<decimal> nom_inv { get; set; }
        public Nullable<decimal> region { get; set; }
        public string second_phone { get; set; }
        public string first_phone { get; set; }
        public string name_kontact_person { get; set; }
        public string last_polz { get; set; }
        public string last_machiname { get; set; }
        public string last_changes_date { get; set; }
        public Nullable<bool> Na_Pechat_Dost { get; set; }
        public Nullable<decimal> skidka { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sklad_rashod_tov> Sklad_rashod_tov { get; set; }
        [ForeignKey("shet")]
        public virtual Sheta Sheta { get; set; }

        [ForeignKey("oplata")]
        public virtual Spr_oplat_sklad Spr_oplat_sklad { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
