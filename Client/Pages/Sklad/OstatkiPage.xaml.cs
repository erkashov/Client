using Client.Models.Sklad;
using Client.ViewModels;
using HandyControl.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages.Sklad
{
    /// <summary>
    /// Логика взаимодействия для OstatkiPage.xaml
    /// </summary>
    public partial class OstatkiPage : ContentControl, INotifyPropertyChanged
    {
        public OstatkiViewModel VM { get; set; }
        public Action CloseAction { get; set; }
        public OstatkiPage(bool _IsReturn = false, Action closeAction = null)
        {
            InitializeComponent();
            VM = new OstatkiViewModel(_IsReturn);
            this.DataContext = VM;
            CloseAction = closeAction;
        }
        public OstatkiPage()
        {
            InitializeComponent();
            VM = new OstatkiViewModel(false);
            this.DataContext = VM;
        }

        private async void navControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            VM.Ostatki = await HttpRequests<ObservableCollection<Sklad_tov_OSTATKI>>.GetRequestAsync($"api/Sklad_tov_OSTATKI/categ/{tabControl.Selected}", VM.Ostatki);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (VM.IsReturn)
            {
                if(ostatkiDataGrid.SelectedItem != null)
                {
                    VM.SelectedTovar = Global.Kod_Tov = (decimal)(ostatkiDataGrid.SelectedItem as Sklad_tov_OSTATKI).kod_tovara;
                    Global.SelectedTovar = (ostatkiDataGrid.SelectedItem as Sklad_tov_OSTATKI).Tovar;
                    if (Global.DialogWindow != null) Global.DialogWindow.Close();
                    if(CloseAction != null) CloseAction();
                }
            }
        }
    }
}
