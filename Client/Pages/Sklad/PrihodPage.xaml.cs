using Client.Models;
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
    /// Логика взаимодействия для PrihodPage.xaml
    /// </summary>
    public partial class PrihodPage : ContentControl
    {
        public PrihodViewModel PrihodVM { get; set; }
        public PrihodPage(int id)
        {
            InitializeComponent();
            PrihodVM = new PrihodViewModel(id);
            this.DataContext = PrihodVM;
        }

        private void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            PrihodVM.Add();
        }

        private async void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            PrihodVM.Save();
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridTovars.CurrentItem != null)
            {
                if (datagridTovars.CurrentItem is Sklad_prihod_prods)
                {
                    PrihodVM.Delete((datagridTovars.CurrentItem as Sklad_prihod_prods).ID);
                }
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            PrihodVM.Update();
        }

        private void TovBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagridTovars.CurrentItem != null)
            {
                TovaryPage win = new TovaryPage(true, UpdateTovar);
                win.Width = 1200;
                win.Height = 800;
                Global.DialogWindow = Dialog.Show(win);
            }
        }
        public void UpdateTovar()
        {
            if (Global.Kod_Tov != 0 && datagridTovars.SelectedItem != null)
            {
                (datagridTovars.SelectedItem as Sklad_prihod_prods).ProductID = Global.Kod_Tov;
                (datagridTovars.SelectedItem as Sklad_prihod_prods).Tovar = Global.SelectedTovar;
                (datagridTovars.SelectedItem as Sklad_prihod_prods).Sklad_prihod = PrihodVM.Prihod;
            }
        }
    }
}
