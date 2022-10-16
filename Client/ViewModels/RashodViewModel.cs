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
    public class RashodViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Spr_oplat_sklad> Spr_Oplat_Sklad { get { return Global.Spr_Oplat_Sklad; } set { Global.Spr_Oplat_Sklad = value; OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }


        private ObservableCollection<Spr_period_filtr> _spr_Periods_Filter;
        public ObservableCollection<Spr_period_filtr> Spr_Periods_Filter { get { return _spr_Periods_Filter; } set { _spr_Periods_Filter = value; OnPropertyChanged(nameof(Spr_Periods_Filter)); } }
        

        private ObservableCollection<string> _spr_Managers_Filter;
        public ObservableCollection<string> Spr_Managers_Filter { get { return _spr_Managers_Filter; } set { _spr_Managers_Filter = value; OnPropertyChanged(nameof(Spr_Managers_Filter)); } }


        private Sklad_rashod _rashod;
        public Sklad_rashod Rashod { get { return _rashod; } set { _rashod = value; OnPropertyChanged(nameof(Rashod));} }

        public RashodViewModel()
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
