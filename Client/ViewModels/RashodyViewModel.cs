using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class RashodyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Spr_oplat_sklad> Spr_Oplat_Sklad { get { return Enums.Spr_Oplat_Sklad; } set { OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }

        public ObservableCollection<Spr_period_filtr> Spr_Periods_Filter { get { return Enums.Spr_Periods_Filter; } set { OnPropertyChanged(nameof(Spr_Periods_Filter)); } }
        

        private ObservableCollection<string> _spr_Managers_Filter;
        public ObservableCollection<string> Spr_Managers_Filter { get { return _spr_Managers_Filter; } set { _spr_Managers_Filter = value; OnPropertyChanged(nameof(Spr_Managers_Filter)); } }


        private ObservableCollection<Sklad_rashod> _rashods;
        public ObservableCollection<Sklad_rashod> Rashods { get { return _rashods; } 
                                                            set { _rashods = value; OnPropertyChanged(nameof(Rashods));
                                                                Spr_Managers_Filter = new ObservableCollection<string>(_rashods.Select(p => p.Otpustil).Distinct().OrderBy(p => p)); } }

        public ObservableCollection<int> Months { get { return Enums.Months; } set { OnPropertyChanged(nameof(Months)); } }
        public ObservableCollection<int> Years { get { return Enums.RashodyYears; } set { OnPropertyChanged(nameof(Years)); } }

        public RashodyViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
