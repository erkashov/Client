using Client.Models;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для RashodWin.xaml
    /// </summary>
    public partial class RashodWin : Window
    {
        public decimal Id { get; set; }
        public RashodViewModel ViewModel { get; set; }
        public RashodWin(decimal id)
        {
            InitializeComponent();
            Id = id;
            this.DataContext = ViewModel;
            GetVM();
        }

        public async void GetVM()
        {
            ViewModel = new RashodViewModel();
            ViewModel.Rashod = await HttpRequests<Sklad_rashod>.GetRequestAsync("api/Sklad_rashod/" + Convert.ToInt32(Id), ViewModel.Rashod);
            this.DataContext = ViewModel;
        }

        private void AddBN_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            HttpRequests<Sklad_rashod> httpRequests = new HttpRequests<Sklad_rashod>();
            try
            {
                await httpRequests.PutRequest("api/Sklad_rashod/" + ViewModel.Rashod.Kod_zap, ViewModel.Rashod);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            GetVM();
        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {

        }

        private void TovBtn_Click(object sender, RoutedEventArgs e)
        {
            if(datagridRashods.SelectedCells.Count == 1)
            {
                OstatkiWin win = new OstatkiWin();
                win.ShowDialog();
            }
        }
    }
}
