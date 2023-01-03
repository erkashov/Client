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
    /// Логика взаимодействия для PrihodPage.xaml
    /// </summary>
    public partial class PrihodPage : ContentControl, INotifyPropertyChanged
    {
        private bool IsLoaded = false;
        public ObservableCollection<Tovary> TovaryList { get; set; }
        private Sklad_prihod _sklad_prihod;
        public Sklad_prihod Prihod { get { return _sklad_prihod; } set { _sklad_prihod = value; OnPropertyChanged(nameof(Prihod)); } }
        private int Kod_zap = 0;
        public PrihodPage(int kod_zap)
        {
            InitializeComponent();
            Kod_zap = kod_zap;
            GetPrihod();
            this.DataContext = this;
            IsLoaded = true;
        }
        public async Task GetPrihod()
        {
            TovaryList = HttpRequests<ObservableCollection<Tovary>>.GetRequest("api/Tovary", TovaryList);
            Prihod = await HttpRequests<Sklad_prihod>.GetRequestAsync("api/Sklad_prihod/" + Kod_zap, Prihod);
            foreach(Sklad_prihod_tov tov in Prihod.Sklad_prihod_tov) tov.Sklad_prihod = Prihod;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            Prihod.Sklad_prihod_tov.Add(new Sklad_prihod_tov() { Kod_prihoda = Kod_zap });
        }

        private async void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            IsLoaded = false;
            foreach (Sklad_prihod_tov tov in Prihod.Sklad_prihod_tov) tov.Sklad_prihod = null;
            HttpRequests<Sklad_prihod> httpRequests = new HttpRequests<Sklad_prihod>();
            try
            {
                await httpRequests.PutRequest("api/Sklad_prihod/" + Prihod.Kod_zap, Prihod);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            GetPrihod();
            IsLoaded = true;
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridTovars.CurrentItem != null)
            {
                if (datagridTovars.CurrentItem is Sklad_prihod_tov)
                {
                    try
                    {
                        await HttpRequests<Sklad_prihod_tov>.DeleteRequest("api/Sklad_prihod/Tov?id=" + (datagridTovars.CurrentItem as Sklad_prihod_tov).Kod_zap);
                    }
                    catch (Exception ex)
                    {
                        Global.ErrorLog(ex.Message);
                    }
                    GetPrihod();
                }
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {

        }

        private void datagridTovars_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //if (e.Column == zenaCol || e.Column == zenadostCol || e.Column == countCol) Prihod.UpdateZenaDost();
        }
    }
}
