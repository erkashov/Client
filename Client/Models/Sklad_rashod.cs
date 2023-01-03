using Client.Models.Sklad;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Models
{
    public class Sklad_rashod : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int kod_zap;
        public int Kod_zap { get { return kod_zap; } set { kod_zap = value; OnPropertyChanged(nameof(Kod_zap)); } }

        private int nom_rash;
        public int Nom_rash { get { return nom_rash; } set { nom_rash = value; OnPropertyChanged(nameof(Nom_rash)); } }

        private DateTime date_rash;
        public DateTime Date_rash { get { return date_rash; } set { date_rash = value; OnPropertyChanged(nameof(Date_rash)); } }

        private Nullable<System.DateTime> date_otgruzki;
        public Nullable<DateTime> Date_otgruzki { get { return date_otgruzki; } set { date_otgruzki = value; OnPropertyChanged(nameof(Date_otgruzki)); } }

        private Nullable<System.DateTime> date_sozdania;
        public Nullable<DateTime> Date_sozdania { get { return date_sozdania; } set { date_sozdania = value; OnPropertyChanged(nameof(Date_sozdania)); } }

        private Nullable<System.DateTime> date_raspila;
        public Nullable<DateTime> Date_raspila { get { return date_raspila; } set { date_raspila = value; OnPropertyChanged(nameof(Date_raspila)); } }

        private Nullable<System.DateTime> date_opl;
        public Nullable<DateTime> Date_opl { get { return date_opl; } set { date_opl = value; OnPropertyChanged(nameof(Date_opl)); } }

        private int id_polz;
        public int Id_polz { get { return id_polz; } set { id_polz = value; OnPropertyChanged(nameof(Id_polz)); } }

        private string prim;
        public string Prim { get { return prim; } set { prim = value; OnPropertyChanged(nameof(Prim)); } }

        private string prim_zav_sklad;
        public string Prim_zav_sklad { get { return prim_zav_sklad; } set { prim_zav_sklad = value; OnPropertyChanged(nameof(Prim_zav_sklad)); } }

        private string prim_buh;
        public string Prim_buh { get { return prim_buh; } set { prim_buh = value; OnPropertyChanged(nameof(Prim_buh)); } }

        private Nullable<int> shet;
        public Nullable<int> Shet { get { return shet; } set { shet = value; OnPropertyChanged(nameof(Shet)); } }

        [ForeignKey("shet")]
        public virtual Sheta Sheta { get; set; }

        private bool otgruzheno;
        public bool Otgruzheno { get { return otgruzheno; } set { otgruzheno = value; OnPropertyChanged(nameof(Otgruzheno)); } }

        private Nullable<decimal> summa_nal;
        public Nullable<decimal> Summa_nal { get { return summa_nal; } set { summa_nal = value; OnPropertyChanged(nameof(Summa_nal)); } }
        private Nullable<decimal> summa_beznal;
        public Nullable<decimal> Summa_beznal { get { return summa_beznal; } set { summa_beznal = value; OnPropertyChanged(nameof(Summa_beznal)); } }

        private Nullable<decimal> summa_karta;
        public Nullable<decimal> Summa_karta { get { return summa_karta; } set { summa_karta = value; OnPropertyChanged(nameof(Summa_karta)); } }

        private Nullable<decimal> dolg;
        public Nullable<decimal> Dolg { get { return dolg; } set { dolg = value; OnPropertyChanged(nameof(Dolg)); } }

        private Nullable<bool> sdano;
        public Nullable<bool> Sdano { get { return sdano; } set { sdano = value; OnPropertyChanged(nameof(Sdano)); } }
        private Nullable<decimal> weight;
        public Nullable<decimal> Weight { get { return weight; } set { weight = value; OnPropertyChanged(nameof(Weight)); } }

        private Nullable<decimal> volume;
        public Nullable<decimal> Volume { get { return volume; } set { volume = value; OnPropertyChanged(nameof(Volume)); } }

        private Nullable<bool> is_invent;
        public Nullable<bool> Is_invent { get { return is_invent; } set { is_invent = value; OnPropertyChanged(nameof(Is_invent)); } }

        private Nullable<bool> is_tov_opl;
        public Nullable<bool> Is_tov_opl { get { return is_tov_opl; } set { is_tov_opl = value; OnPropertyChanged(nameof(Is_tov_opl)); } }

        private Nullable<bool> isnt_in_prodazhi_history;
        public Nullable<bool> Isnt_in_prodazhi_history { get { return isnt_in_prodazhi_history; } set { isnt_in_prodazhi_history = value; OnPropertyChanged(nameof(Isnt_in_prodazhi_history)); } }

        private string first_phone;
        public string First_phone { get { return first_phone; } set { first_phone = value; OnPropertyChanged(nameof(First_phone)); } }

        private string name_pokup;
        public string Name_pokup { get { return name_pokup; } set { name_pokup = value; OnPropertyChanged(nameof(Name_pokup)); } }

        private string name_kontact_person;
        public string Name_kontact_person { get { return name_kontact_person; } set { name_kontact_person = value; OnPropertyChanged(nameof(Name_kontact_person)); } }

        private string second_phone;
        public string Second_phone { get { return second_phone; } set { second_phone = value; OnPropertyChanged(nameof(Second_phone)); } }

        private Nullable<bool> na_raspile;
        public Nullable<bool> Na_raspile { get { return na_raspile; } set { na_raspile = value; OnPropertyChanged(nameof(Na_raspile)); } }

        private Nullable<bool> na_pechat_dost;
        public Nullable<bool> Na_pechat_dost { get { return na_pechat_dost; } set { na_pechat_dost = value; OnPropertyChanged(nameof(Na_pechat_dost)); } }

        private Nullable<bool> raspileno;
        public Nullable<bool> Raspileno { get { return raspileno; } set { raspileno = value; OnPropertyChanged(nameof(Raspileno)); } }

        private int oplata;
        public int Oplata { get { return oplata; } set { oplata = value; OnPropertyChanged(nameof(Oplata)); OnPropertyChanged(nameof(KartaCBVisibility)); OnPropertyChanged(nameof(ShetVisibility)); } }

        public decimal SummaAll
        {
            get { return Sklad_rashod_tov != null ? Sklad_rashod_tov.Sum(p => Math.Ceiling(p.Summa)) : 0; }
        }
        public int CountDost { get { return Sklad_dostavki != null ? Sklad_dostavki.Count() : 0; } }
        public decimal SummaDost { get { return Sklad_dostavki != null ? Sklad_dostavki.Select(p => p.summa).Sum() : 0; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        [ForeignKey("oplata")]
        public virtual Spr_oplat_sklad Spr_oplat_sklad { get; set; }
        [ForeignKey("id_polz")]
        public virtual User Polz { get; set; }

        public Sklad_rashod()
        {
            is_tov_opl = false;
            isnt_in_prodazhi_history = false;
            Is_invent = false;
            otgruzheno = false;
            sdano = false;
            IsDostOpl = false;

            summa_nal = 0;
            summa_karta = 0;
            summa_beznal = 0; ;
            weight = 0;
            volume = 0;
            dolg = 0;
        }

        public bool? IsDostOpl
        {
            get
            {
                if (Sklad_dostavki == null) return false;
                return Sklad_dostavki.Where(p => p.opl_klientom == true).Count() > 0 ? true : false;
            }
            set
            {
                if (Sklad_dostavki != null)
                {
                    foreach (var dost in Sklad_dostavki) dost.opl_klientom = value.Value;
                }
            }
        }

        public Visibility KartaCBVisibility { get { return Oplata == 10002 || Oplata == 3 ? Visibility.Visible : Visibility.Collapsed; } set { OnPropertyChanged(nameof(KartaCBVisibility)); } }
        public Visibility ShetVisibility { get { return Oplata == 2 ? Visibility.Visible : Visibility.Collapsed; } set { OnPropertyChanged(nameof(ShetVisibility)); } }

        private ObservableCollection<Sklad_rashod_tov> _Sklad_rashod_tov;
        public ObservableCollection<Sklad_rashod_tov> Sklad_rashod_tov { get { return _Sklad_rashod_tov; } set { _Sklad_rashod_tov = value; OnPropertyChanged(nameof(Sklad_rashod_tov)); } }
        public ObservableCollection<Sklad_dostavki> Sklad_dostavki { get; set; }
    }
}
