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

namespace Client.Models.Sklad
{
    public class Sklad_prihod : INotifyPropertyChanged
    {
        public Sklad_prihod()
        {
            this.Sklad_prihod_tov = new ObservableCollection<Sklad_prihod_tov>();
        }
        [Key]
        private int kod_zap;
        public int Kod_zap { get { return this.kod_zap; } set { this.kod_zap = value; OnPropertyChanged(nameof(Kod_zap)); } }

        private DateTime date_prih ;
        public DateTime Date_prih { get { return this.date_prih ; } set { this.date_prih = value; OnPropertyChanged(nameof(Date_prih)); } }

        private int id_polz ;
        public int Id_polz { get { return this.id_polz; } set { this.id_polz = value; OnPropertyChanged(nameof(Id_polz)); } }

        private string prim ;
        public string Prim { get { return this.prim ; } set { this.prim = value; OnPropertyChanged(nameof(Prim)); } }

        private int nom_prih ;
        public int Nom_prih { get { return this.nom_prih; } set { this.nom_prih = value; OnPropertyChanged(nameof(Nom_prih)); } }

        private Nullable<int> kod_poluch ;
        public Nullable<int> Kod_poluch {  get { return this.kod_poluch; } set { this.kod_poluch = value; OnPropertyChanged(nameof(Kod_poluch)); } }
        
        private Nullable<bool> transport_ot_post ;
        public Nullable<bool> Transport_ot_post { get { return this.transport_ot_post; } set { this.transport_ot_post = value; OnPropertyChanged(nameof(Transport_ot_post)); OnPropertyChanged(nameof(Cost_Dost_Enable)); UpdateZenaDost(); } }
        public bool Cost_Dost_Enable { get { return !(Transport_ot_post != null && Transport_ot_post.Value); } }
        private Nullable<bool> is_corrected ;
        public Nullable<bool> Is_corrected { get { return this.is_corrected; } set { this.is_corrected = value; OnPropertyChanged(nameof(Is_corrected)); } }

        private bool is_in_sklad ;
        public bool Is_in_sklad { get { return this.is_in_sklad; } set { this.is_in_sklad = value; OnPropertyChanged(nameof(Is_in_sklad)); } }

        private Nullable<int> kod_perevoz ;
        public Nullable<int> Kod_perevoz { get { return this.kod_perevoz; } set { this.kod_perevoz = value; OnPropertyChanged(nameof(Kod_perevoz)); } }

        private Nullable<int> kod_shet ;
        public Nullable<int> Kod_shet { get { return this.kod_shet; } set { this.kod_shet = value; OnPropertyChanged(nameof(Kod_shet)); } }

        private Nullable<double> dop_rash ;
        public Nullable<double> Dop_rash { get { return this.dop_rash; } set { this.dop_rash = value; OnPropertyChanged(nameof(Dop_rash)); UpdateZenaDost(); } }

        private Nullable<double> cost_dost ;
        public Nullable<double> Cost_dost { get { return this.cost_dost;} set { this.cost_dost = value; OnPropertyChanged(nameof(Cost_dost)); UpdateZenaDost(); } }

        private ObservableCollection<Sklad_prihod_tov> _Sklad_prihod_tov ;
        public ObservableCollection<Sklad_prihod_tov> Sklad_prihod_tov { get { return this._Sklad_prihod_tov; } set { this._Sklad_prihod_tov = value; OnPropertyChanged(nameof(Sklad_prihod_tov)); } }
        
        [ForeignKey("id_polz")]
        private User polz ;
        public User Polz { get { return this.polz; } set { this.polz = value; OnPropertyChanged(nameof(Polz));} }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void UpdateZenaDost()
        {
            double dost = (Cost_dost != null && Cost_Dost_Enable ? Cost_dost.Value : 0) + (Dop_rash != null ? Dop_rash.Value : 0);
            /*double? kub = 0;
            foreach(Sklad_prihod_tov tov in Sklad_prihod_tov)
            {
                if (tov.Tovar != null) kub += tov.Tovar.dlina * tov.Tovar.shir * tov.Tovar.tol;
            }
            if(kub.HasValue && kub.Value > 0)*/
            double countTotal = Sklad_prihod_tov.Sum(p=>p.Count);
            double dost_1 = 0;
            if(countTotal > 0)
            {
                dost_1 = dost / countTotal;
            }
            foreach(Sklad_prihod_tov tov in Sklad_prihod_tov)
            {
                if (Cost_Dost_Enable) tov.Zena_with_dost = tov.Zena + dost_1;
                //else tov.Zena = 0;
            }
        }

        public double Cost
        {
            get { return Sklad_prihod_tov.Sum(p=>p.Cost); }
        }

        public double Cost_with_dost
        {
            get { return Sklad_prihod_tov.Sum(p => p.Cost_with_dost); }
        }
    }
}
