using Client.Models;
using Client.Models.Sklad;
using Client.ViewModels;
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
    public partial class PrihodsPage : ContentControl
    {
        PrihodsViewModel PrihodsVM { get; set; }
        public PrihodsPage()
        {
            InitializeComponent();
            PrihodsVM = new PrihodsViewModel();
            this.DataContext = PrihodsVM;
        }

        private async void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            PrihodsVM.Add();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagridPrihods.SelectedItem != null)
            {
                PrihodPage rashod = new PrihodPage((datagridPrihods.SelectedItem as Sklad_prihod).ID);
                Global.MainWin.ShowPage(new PagesContainer("Приход №" + (datagridPrihods.SelectedItem as Sklad_prihod).Nom_prih, rashod));
            }
        }

        private async void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            PrihodsVM.Save();
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridPrihods.CurrentItem != null)
            {
                if (datagridPrihods.CurrentItem is Sklad_prihod)
                {
                    PrihodsVM.Delete((datagridPrihods.CurrentItem as Sklad_prihod).ID);
                }
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            PrihodsVM.Update();
        }
    }
}
