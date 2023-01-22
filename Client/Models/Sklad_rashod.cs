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
        private int id;
        public int ID { get { return id; } set { id = value; OnPropertyChanged(nameof(ID)); } }

        private int nom_rash;
        public int Nom_rash { get { return nom_rash; } set { nom_rash = value; OnPropertyChanged(nameof(Nom_rash)); } }

        private DateTime date_rash;
        public DateTime Date_rash { get { return date_rash; } set { date_rash = value; OnPropertyChanged(nameof(Date_rash)); } }

        private Nullable<System.DateTime> date_otgruzki;
        public Nullable<DateTime> Date_otgruzki { get { return date_otgruzki; } set { date_otgruzki = value; OnPropertyChanged(nameof(Date_otgruzki)); } }

        private System.DateTime date_sozdania;
        public DateTime Date_sozdania { get { return date_sozdania; } set { date_sozdania = value; OnPropertyChanged(nameof(Date_sozdania)); } }

        private Nullable<System.DateTime> date_oplaty;
        public Nullable<DateTime> Date_oplaty { get { return date_oplaty; } set { date_oplaty = value; OnPropertyChanged(nameof(Date_oplaty)); } }

        private int userID;
        public int UserID { get { return userID; } set { userID = value; OnPropertyChanged(nameof(UserID)); } }

        private string prim;
        public string Prim { get { return prim; } set { prim = value; OnPropertyChanged(nameof(Prim)); } }

       private Nullable<int> shetID;
        public Nullable<int> ShetID { get { return shetID; } set { shetID = value; OnPropertyChanged(nameof(ShetID)); } }

        public virtual Shet Sheta { get; set; }

        private bool otgruzheno;
        public bool Otgruzheno { get { return otgruzheno; } set { otgruzheno = value; OnPropertyChanged(nameof(Otgruzheno)); } }

        private bool oplacheno;
        public bool Oplacheno { get { return oplacheno; } set { oplacheno = value; OnPropertyChanged(nameof(Oplacheno)); } }

        private int type_oplatyID;
        public int Type_oplatyID { get { return type_oplatyID; } set { type_oplatyID = value; OnPropertyChanged(nameof(Type_oplatyID)); OnPropertyChanged(nameof(ShetVisibility)); } }

        public double SummaAll
        {
            get { return Sklad_rashod_tov != null ? Sklad_rashod_tov.Sum(p => Math.Ceiling(p.Summa)) : 0; }
        }

        public double Weight
        {
            get { return Sklad_rashod_tov != null ? Sklad_rashod_tov.Sum(p => Math.Ceiling(p.Weight)) : 0; }
        }
        public double Volume
        {
            get { return Sklad_rashod_tov != null ? Sklad_rashod_tov.Sum(p => Math.Ceiling(p.Volume)) : 0; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Type_oplaty Spr_oplat_sklad { get; set; }
        public User polz;
        public User Polz { get { return polz; } set { polz = value; OnPropertyChanged(nameof(Polz)); } } 
        
        public Sklad_rashod()
        {
        }

        public Visibility ShetVisibility { get { return Type_oplatyID == 2 ? Visibility.Visible : Visibility.Collapsed; } set { OnPropertyChanged(nameof(ShetVisibility)); } }

        private ObservableCollection<Sklad_rashod_prods> _Sklad_rashod_tov;
        public ObservableCollection<Sklad_rashod_prods> Sklad_rashod_tov { get { return _Sklad_rashod_tov; } set { _Sklad_rashod_tov = value; OnPropertyChanged(nameof(Sklad_rashod_tov)); } }
    }
}
