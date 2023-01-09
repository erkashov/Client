using Client.Models;
using Client.Models.Sklad;
using Client.ViewModels;
using Client.Windows;
using ControlzEx.Theming;
using HandyControl.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Логика взаимодействия для RashodsPage.xaml
    /// </summary>
    public partial class RashodsPage : ContentControl
    {
        public RashodyViewModel RashodyViewModel { get; set; }

        public RashodsPage()
        {
            InitializeComponent();
            RashodyViewModel = new RashodyViewModel();
            this.DataContext = RashodyViewModel;
        }

        private void datagridRashods_MoseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagridRashods.SelectedItem != null)
            {
                RashodPage rashod = new RashodPage((datagridRashods.SelectedItem as Sklad_rashod).Kod_zap);
                Global.MainWin.ShowPage(new PagesContainer("Расход №" + (datagridRashods.SelectedItem as Sklad_rashod).Nom_rash, rashod));
            }
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridRashods.CurrentItem != null)
            {
                if (datagridRashods.CurrentItem is Sklad_rashod)
                {
                    try
                    {
                        await HttpRequests<Sklad_rashod>.DeleteRequest("api/Sklad_rashod/" + (datagridRashods.CurrentItem as Sklad_rashod).Kod_zap);
                    }
                    catch (Exception ex)
                    {
                        Global.ErrorLog(ex.Message);
                    }
                    RashodyViewModel.Filter();
                }
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            RashodyViewModel.Filter();
        }

        private async void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            RashodyViewModel.Save();
        }

        private async void AddBN_Click(object sender, RoutedEventArgs e)
        {
            Sklad_rashod rashod = new Sklad_rashod();
            await HttpRequests<Sklad_rashod>.PostRequest("api/Sklad_rashod/", rashod);
            RashodyViewModel.Filter();
        }
    }
}
