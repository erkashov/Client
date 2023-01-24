using Client.Models;
using Client.Models.Sklad;
using Client.ViewModels;
using HandyControl.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для PricePage.xaml
    /// </summary>
    public partial class PricePage : ContentControl
    {
        PriceViewModel PriceVM { get; set; }
        public PricePage()
        {
            InitializeComponent();
            PriceVM = new PriceViewModel();
            this.DataContext = PriceVM;
        }

        private void datagridPrices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddBN_Click(object sender, RoutedEventArgs e)
        {
            PriceVM.Add();
        }

        private void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            PriceVM.Save();
        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            PriceVM.Update();
        }

        private void TovBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagridPrices.CurrentItem != null)
            {
                TovaryPage win = new TovaryPage(true, UpdateTovar);
                win.Width = 1200;
                win.Height = 800;
                Global.DialogWindow = Dialog.Show(win);
            }
        }
        public void UpdateTovar()
        {
            if (Global.Kod_Tov != 0 && datagridPrices.SelectedItem != null)
            {
                (datagridPrices.SelectedItem as Price).productID = Global.Kod_Tov;
                (datagridPrices.SelectedItem as Price).Tovar = Global.SelectedTovar;
            }
        }
    }
}
