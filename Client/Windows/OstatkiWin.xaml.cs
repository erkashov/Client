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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Windows
{
    /// <summary>
    /// Логика взаимодействия для OstatkiWin.xaml
    /// </summary>
    public partial class OstatkiWin : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Sklad_tov_OSTATKI> _ostatki;
        public ObservableCollection<Sklad_tov_OSTATKI> Ostatki
        {
            get { return _ostatki; }
            set { _ostatki = value; OnPropertyChanged(nameof(Ostatki)); }
        }
        public OstatkiWin()
        {
            InitializeComponent();
            Ostatki = new ObservableCollection<Sklad_tov_OSTATKI>();
            this.DataContext = this;
        }

        private async void navControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            Ostatki = await HttpRequests<ObservableCollection<Sklad_tov_OSTATKI>>.GetRequestAsync($"api/Sklad_tov_OSTATKI/categ/{tabControl.Selected}", Ostatki);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
