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
            this.Sklad_prihod_tov = new ObservableCollection<Sklad_prihod_prods>();
        }
        [Key]
        private int id;
        public int ID { get { return this.id; } set { this.id = value; OnPropertyChanged(nameof(ID)); } }

        private DateTime date_prih ;
        public DateTime Date_prih { get { return this.date_prih ; } set { this.date_prih = value; OnPropertyChanged(nameof(Date_prih)); } }

        private int userID ;
        public int UserID { get { return this.userID; } set { this.userID = value; OnPropertyChanged(nameof(UserID)); } }

        private string prim ;
        public string Prim { get { return this.prim ; } set { this.prim = value; OnPropertyChanged(nameof(Prim)); } }

        private int nom_prih ;
        public int Nom_prih { get { return this.nom_prih; } set { this.nom_prih = value; OnPropertyChanged(nameof(Nom_prih)); } }

        private Nullable<int> contractorID ;
        public Nullable<int> ContractorID {  get { return this.contractorID; } set { this.contractorID = value; OnPropertyChanged(nameof(ContractorID)); } }
        
        private Nullable<bool> transport_ot_post ;
        public Nullable<bool> Transport_ot_post { get { return this.transport_ot_post; } set { this.transport_ot_post = value; OnPropertyChanged(nameof(Transport_ot_post)); OnPropertyChanged(nameof(Cost_Dost_Enable)); UpdateZenaDost(); } }
        public bool Cost_Dost_Enable { get { return !(Transport_ot_post != null && Transport_ot_post.Value); } }
        private Nullable<bool> is_korr ;
        public Nullable<bool> Is_korr { get { return this.is_korr; } set { this.is_korr = value; OnPropertyChanged(nameof(Is_korr)); } }

        private bool is_in_sklad ;
        public bool Is_in_sklad { get { return this.is_in_sklad; } set { this.is_in_sklad = value; OnPropertyChanged(nameof(Is_in_sklad)); } }

        private Nullable<double> dop_rash ;
        public Nullable<double> Dop_rash { get { return this.dop_rash; } set { this.dop_rash = value; OnPropertyChanged(nameof(Dop_rash)); UpdateZenaDost(); } }

        private Nullable<double> deliv_cost ;
        public Nullable<double> Deliv_cost { get { return this.deliv_cost;} set { this.deliv_cost = value; OnPropertyChanged(nameof(Deliv_cost)); UpdateZenaDost(); } }

        private ObservableCollection<Sklad_prihod_prods> _Sklad_prihod_tov ;
        public ObservableCollection<Sklad_prihod_prods> Sklad_prihod_tov { get { return this._Sklad_prihod_tov; } set { this._Sklad_prihod_tov = value; OnPropertyChanged(nameof(Sklad_prihod_tov)); } }
        
        private User polz ;
        public User Polz { get { return this.polz; } set { this.polz = value; OnPropertyChanged(nameof(Polz));} }
        public Contractor Contractor { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void UpdateZenaDost()
        {
            double dost = (Deliv_cost != null && Cost_Dost_Enable ? Deliv_cost.Value : 0) + (Dop_rash != null ? Dop_rash.Value : 0);
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
            foreach(Sklad_prihod_prods tov in Sklad_prihod_tov)
            {
                if (Cost_Dost_Enable) tov.Price_with_deliv = tov.Price + dost_1;
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
