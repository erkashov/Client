﻿using Client.Models;
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
        private ObservableCollection<Spr_oplat_sklad> _spr_Oplat_Sklad;
        public ObservableCollection<Spr_oplat_sklad> Spr_Oplat_Sklad { get { return _spr_Oplat_Sklad; } set { _spr_Oplat_Sklad = value; OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }


        private ObservableCollection<Spr_period_filtr> _spr_Periods_Filter;
        public ObservableCollection<Spr_period_filtr> Spr_Periods_Filter { get { return _spr_Periods_Filter; } set { _spr_Periods_Filter = value; OnPropertyChanged(nameof(Spr_Periods_Filter)); } }
        

        private ObservableCollection<string> _spr_Managers_Filter;
        public ObservableCollection<string> Spr_Managers_Filter { get { return _spr_Managers_Filter; } set { _spr_Managers_Filter = value; OnPropertyChanged(nameof(Spr_Managers_Filter)); } }


        private ObservableCollection<Sklad_rashod> _rashods;
        public ObservableCollection<Sklad_rashod> Rashods { get { return _rashods; } set { _rashods = value; OnPropertyChanged(nameof(Rashods)); /*Years = new ObservableCollection<int>(Rashods.Select(p => p.Data_rash.Value.Year).Distinct());*/ } }


        private ObservableCollection<int> _months;
        public ObservableCollection<int> Months { get { return _months; } set { _months = value; OnPropertyChanged(nameof(Months)); } }


        private ObservableCollection<int> _years;
        public ObservableCollection<int> Years { get { return _years; } set { _years = value; OnPropertyChanged(nameof(Years)); } }

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