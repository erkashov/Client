using Client.Models;
using Client.Models.Sklad;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class RashodViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Spr_oplat_sklad> Spr_Oplat_Sklad { get { return Enums.Spr_Oplat_Sklad; } set { OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }
        public ObservableCollection<Karta> Spr_karty { get { return Enums.Spr_karty; } set { OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }


        private Sklad_rashod _rashod;
        public Sklad_rashod Rashod { get { return _rashod; } 
            set { _rashod = value; OnPropertyChanged(nameof(Rashod)); } }
        
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
