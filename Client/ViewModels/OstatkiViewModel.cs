using Client.Models.Sklad;
using Client.UserControls;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
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
    public class OstatkiViewModel : INotifyPropertyChanged, IDialogResultable<decimal>
    {
        private decimal _selectedTovar;
        public decimal SelectedTovar { get { return _selectedTovar; } set { _selectedTovar = value; OnPropertyChanged(nameof(SelectedTovar)); } }
        private ObservableCollection<Sklad_tov_OSTATKI> _ostatki;
        public ObservableCollection<Sklad_tov_OSTATKI> Ostatki
        {
            get { return _ostatki; }
            set { _ostatki = value; OnPropertyChanged(nameof(Ostatki)); }
        }
        public bool IsReturn { get; set; }
        public Visibility CloseBTNVisible { get; set; }
        public string Header { get; set; }
        decimal IDialogResultable<decimal>.Result { get => SelectedTovar; set => SelectedTovar = value; }
        Action IDialogResultable<decimal>.CloseAction { get => () => Global.DialogWindow.Close(); set => throw new NotImplementedException(); }

        public OstatkiViewModel(bool _IsReturn = false)
        {
            Header = "Остатки";
            Ostatki = new ObservableCollection<Sklad_tov_OSTATKI>();
            IsReturn = true;
            CloseBTNVisible = IsReturn ? Visibility.Visible : Visibility.Collapsed;
        }
        public OstatkiViewModel()
        {
            Header = "Остатки";
            Ostatki = new ObservableCollection<Sklad_tov_OSTATKI>();
            IsReturn = true;
            CloseBTNVisible = IsReturn ? Visibility.Visible : Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
