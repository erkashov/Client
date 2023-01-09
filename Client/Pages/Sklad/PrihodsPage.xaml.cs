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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages.Sklad
{
    /// <summary>
    /// Логика взаимодействия для PrihodsPage.xaml
    /// </summary>
    public partial class PrihodsPage : ContentControl, INotifyPropertyChanged
    {
        private bool IsDataLoaded = false;
        private DateTime dateStart;
        public DateTime DateStart { get { return dateStart; } set { dateStart = value; Filter(); } }
        private DateTime dateEnd;
        public DateTime DateEnd { get { return dateEnd; } set { dateEnd = value; Filter(); } }

        private ObservableCollection<Sklad_prihod> _sklad_prihods;
        public ObservableCollection<Sklad_prihod> Sklad_prihods { get { return _sklad_prihods; } set { _sklad_prihods = value; OnPropertyChanged(nameof(Sklad_prihods)); } }
        public PrihodsPage()
        {
            InitializeComponent();
            DateEnd = DateStart = DateTime.Now;
            IsDataLoaded = true;
            Filter();
            this.DataContext = this;
        }
        public async Task Filter()
        {
            if (!IsDataLoaded) return;
            IsDataLoaded = false;
            RashodyQueryParams queryParams = new RashodyQueryParams(DateStart, DateEnd, "");

            Sklad_prihods = new ObservableCollection<Sklad_prihod>(await HttpPostRequests<List<Sklad_prihod>, RashodyQueryParams>.PostRequest("api/Sklad_prihod/Filter", new List<Sklad_prihod>(), queryParams));
            IsDataLoaded = true;
        }

        private void calendarFilter_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
           if(IsDataLoaded) Filter();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private async void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            Sklad_prihod prihod = new Sklad_prihod();
            prihod.Id_polz = 1;
            prihod = await HttpRequests<Sklad_prihod>.PostRequest("api/Sklad_prihod/", prihod);
            Filter();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagridPrihods.SelectedItem != null)
            {
                PrihodPage rashod = new PrihodPage((datagridPrihods.SelectedItem as Sklad_prihod).Kod_zap);
                Global.MainWin.ShowPage(new PagesContainer("Приход №" + (datagridPrihods.SelectedItem as Sklad_prihod).Nom_prih, rashod));
            }
        }

        private async void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            foreach (Sklad_prihod prihod in Sklad_prihods)
            {
                try
                {
                    await HttpRequests<Sklad_prihod>.PutRequest("api/Sklad_prihod/" + prihod.Kod_zap, prihod);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Filter();
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridPrihods.CurrentItem != null)
            {
                if (datagridPrihods.CurrentItem is Sklad_prihod)
                {
                    try
                    {
                        await HttpRequests<Sklad_prihod_tov>.DeleteRequest("api/Sklad_prihod/" + (datagridPrihods.CurrentItem as Sklad_prihod).Kod_zap);
                    }
                    catch (Exception ex)
                    {
                        Global.ErrorLog(ex.Message);
                    }
                    Filter();
                }
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
