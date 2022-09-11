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
        private ObservableCollection<Spr_oplat_sklad> _spr_Oplat_Sklad;
        public ObservableCollection<Spr_oplat_sklad> Spr_Oplat_Sklad { get { return _spr_Oplat_Sklad; } set { _spr_Oplat_Sklad = value; OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }

        private ObservableCollection<Spr_period_filtr> _spr_Periods_Filter;
        public ObservableCollection<Spr_period_filtr> Spr_Periods_Filter { get { return _spr_Periods_Filter; } set { _spr_Periods_Filter = value; OnPropertyChanged(nameof(Spr_Periods_Filter)); } }

        private ObservableCollection<Othruzheno> _spr_Otgruzheno;
        public ObservableCollection<Othruzheno> Spr_Otgruzheno { get { return _spr_Otgruzheno; } set { _spr_Otgruzheno = value; OnPropertyChanged(nameof(Spr_Otgruzheno)); } }
        
        private ObservableCollection<TovarOpl> _spr_TovarOplachen;
        public ObservableCollection<TovarOpl> Spr_TovarOplachen { get { return _spr_TovarOplachen; } set { _spr_TovarOplachen = value; OnPropertyChanged(nameof(Spr_TovarOplachen)); } }
        private Othruzheno _selectedOtgruzheno;
        public Othruzheno SelectedOtgruzheno { get { return _selectedOtgruzheno;} set {  _selectedOtgruzheno = value; OnPropertyChanged(nameof(SelectedOtgruzheno));} }
        private TovarOpl _selectedTovarOpl;
        public TovarOpl SelectedTovarOpl { get { return _selectedTovarOpl;} set { _selectedTovarOpl = value; OnPropertyChanged(nameof(SelectedTovarOpl));} }
        public RashodyViewModel()
        {
            Fill();
        }

        private async Task Fill()
        {
            Spr_Oplat_Sklad = await Global.GetSprOplatSklad();
            Spr_Periods_Filter = await Global.GetSprPeriodsFilter();
            Spr_Otgruzheno = new ObservableCollection<Othruzheno>() { Othruzheno.Все, Othruzheno.Да, Othruzheno.Нет };
            SelectedOtgruzheno = Othruzheno.Все;
            Spr_TovarOplachen = new ObservableCollection<TovarOpl>() { TovarOpl.Все, TovarOpl.Да, TovarOpl.Нет };
            SelectedTovarOpl = TovarOpl.Все;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public enum Othruzheno
        {
            Все,
            Да,
            Нет
        }

        public enum TovarOpl
        {
            Все,
            Да,
            Нет
        }

        public enum Sdano
        {
            Все,
            Да,
            Нет
        }
    }
}
