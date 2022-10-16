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
        public Sklad_rashod()
        {
            IsTovOpl = false;
            IsNotInProdazhiHistory = false;
            IsInvent = false;
            otgruzheno = false;
            na_Pechat_Dost = false;
            sdano = false;
            на_производство = false;
            IsDostOpl = false;

            summa = 0;
            summa_karta = 0;
            summa_beznal = 0; ;
            weight = 0;
            volume = 0;
            dolg = 0;
        }

        [Key]
        public int? kod_zap { get; set; }

        private bool? isTovOpl;
        private bool? isInvent;
        private bool? isNotInProdazhiHistory;
        private bool? opl_pok_karta;
        private bool? otgruzheno;
        private bool? na_Pechat_Dost;
        private bool? sdano;
        private bool? на_производство;

        public bool? IsTovOpl
        {
            get { return isTovOpl; }
            set
            {
                isTovOpl = value;
                OnPropertyChanged(nameof(IsTovOpl));
            }
        }
        public bool? IsInvent
        {
            get { return isInvent; }
            set
            {
                isInvent = value;
                OnPropertyChanged(nameof(IsInvent));
            }
        }
        public bool? IsNotInProdazhiHistory
        {
            get { return isNotInProdazhiHistory; }
            set
            {
                isNotInProdazhiHistory = value;
                OnPropertyChanged(nameof(IsNotInProdazhiHistory));
            }
        }
        public bool? Otgruzheno
        {
            get { return otgruzheno; }
            set
            {
                otgruzheno = value;
                OnPropertyChanged(nameof(Otgruzheno));
            }
        }
        public Nullable<bool> Na_Pechat_Dost
        {
            get { return na_Pechat_Dost; }
            set
            {
                na_Pechat_Dost = value;
                OnPropertyChanged(nameof(Na_Pechat_Dost));
            }
        }
        public Nullable<bool> Sdano
        {
            get { return sdano; }
            set
            {
                sdano = value;
                OnPropertyChanged(nameof(Sdano));
            }
        }
        public Nullable<bool> На_производство
        {
            get { return на_производство; }
            set
            {
                на_производство = value;
                OnPropertyChanged(nameof(На_производство));
            }
        }

        private Nullable<DateTime> data_sozdania;
        private Nullable<DateTime> data_rash;
        private Nullable<DateTime> data_opl;
        private Nullable<DateTime> data_otgruzki;
        private Nullable<TimeSpan> time_rash;
        private Nullable<TimeSpan> time_otgruzki;
        public Nullable<DateTime> Data_rash { get { return data_rash; } set { data_rash = value; OnPropertyChanged(nameof(Data_rash)); } }
        public Nullable<DateTime> Data_otgruzki { get { return data_otgruzki; } set { data_otgruzki = value; OnPropertyChanged(nameof(Data_otgruzki)); } }
        public Nullable<DateTime> Data_sozdania { get { return data_sozdania; } set { data_sozdania = value; OnPropertyChanged(nameof(Data_sozdania)); } }
        public Nullable<DateTime> Data_opl { get { return data_opl; } set { data_opl = value; OnPropertyChanged(nameof(Data_opl)); } }

        private Nullable<int> nom_rash;
        private Nullable<decimal> summa;
        private Nullable<decimal> summa_beznal;
        private Nullable<decimal> summa_karta;
        private Nullable<decimal> shet;
        private Nullable<decimal> org;
        private Nullable<decimal> oplata;
        private Nullable<decimal> weight;
        private Nullable<decimal> volume;
        private Nullable<decimal> karta;
        private Nullable<decimal> dolg;
        private Nullable<decimal> region;
        private Nullable<decimal> skidka;
        private Nullable<decimal> phone_pokup;

        public Nullable<int> Nom_rash { get { return Convert.ToInt32(nom_rash); } set { nom_rash = value; OnPropertyChanged(nameof(Nom_rash)); } }
        public Nullable<decimal> Summa { get { return summa; } set { summa = value; OnPropertyChanged( nameof(Summa)); } }
        public Nullable<decimal> Summa_beznal { get { return summa_beznal; } set { summa_beznal = value; OnPropertyChanged(nameof(Summa_beznal)); } }
        public Nullable<decimal> Summa_karta { get { return summa_karta; } set { summa_karta = value; OnPropertyChanged(nameof(Summa_karta)); } }
        public Nullable<decimal> Shet { get { return shet; } set { shet = value; OnPropertyChanged(nameof(Shet));} }
        public Nullable<decimal> Org { get { return org; } set { org = value; OnPropertyChanged(nameof(Org)); } }
        public Nullable<decimal> Oplata { get { return oplata; } set { oplata = value; OnPropertyChanged(nameof(Oplata)); } }
        public Nullable<decimal> Weight { get { return weight; } set { weight = value; OnPropertyChanged(nameof(Weight)); } }
        public Nullable<decimal> Volume { get { return volume; } set { volume = value; OnPropertyChanged(nameof(Volume)); } }
        public Nullable<decimal> Karta { get { return karta; } set { karta = value; OnPropertyChanged(nameof(Karta)); } }
        public Nullable<decimal> Dolg { get { return dolg; } set { dolg = value; OnPropertyChanged(nameof(Dolg)); } }
        public Nullable<decimal> Skidka { get { return skidka; } set { skidka = value; OnPropertyChanged(nameof(Skidka)); } }
        public Nullable<decimal> Phone_pokup { get { return phone_pokup; } set { phone_pokup = value; OnPropertyChanged(nameof(Phone_pokup)); } }


        private string prim_buh;
        private string primZavSklad;
        private string prim;
        private string first_phone;
        private string second_phone;
        private string name_kontact_person;
        private string name_pokup;
        private string last_polz;
        private string last_machiname;
        private string last_changes_date;
        private string otpustil;

        public string Prim_buh { get { return prim_buh; } set { prim_buh = value; OnPropertyChanged(nameof(Prim_buh)); } }
        public string PrimZavSklad { get { return primZavSklad; } set { primZavSklad = value; OnPropertyChanged(nameof(PrimZavSklad)); } }
        public string First_phone { get { return first_phone; } set { first_phone = value; OnPropertyChanged(nameof(First_phone)); } }
        public string Second_phone { get { return second_phone; } set { second_phone = value; OnPropertyChanged(nameof(Second_phone)); } }
        public string Name_kontact_person { get { return name_kontact_person; } set { name_kontact_person = value; OnPropertyChanged(nameof(Name_kontact_person)); } }
        public string Name_pokup { get { return name_pokup; } set { name_pokup = value; OnPropertyChanged(nameof(Name_pokup)); } }
        public string Last_polz { get { return last_polz; } set { last_polz = value; OnPropertyChanged(nameof(Last_polz)); } }
        public string Last_machiname { get { return last_machiname; } set { last_machiname = value; OnPropertyChanged(nameof(Last_machiname)); } }
        public string Last_changes_date { get { return last_changes_date; } set { last_changes_date = value; OnPropertyChanged(nameof(Last_changes_date)); } }
        public string Otpustil { get { return this.otpustil; } set { this.otpustil = value; OnPropertyChanged(nameof(Otpustil));} }
        public string Prim { get { return this.prim; } set { this.prim = value; OnPropertyChanged(nameof(Prim)); } }
        
        public bool? IsDostOpl
        {
            get
            {
                if (Sklad_dostavki == null) return false;
                return Sklad_dostavki.Where(p => p.opl_klientom == true).Count() > 0 ? true : false;
            }
            set
            {
                if(Sklad_dostavki != null)
                {
                    foreach (var dost in Sklad_dostavki) dost.opl_klientom = value.Value;
                } 
            }
        }

        public virtual ICollection<Sklad_rashod_tov> Sklad_rashod_tov { get; set; }
        public virtual ICollection<Sklad_dostavki> Sklad_dostavki { get; set; }


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
