using Client.Models;
using Client.Models.Sklad;
using Client.Pages.Sklad;
using Client.ViewModels;
using HandyControl.Controls;
using Notifications.Wpf.ViewModels.Base;
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
    /// Логика взаимодействия для ShetPage.xaml
    /// </summary>
    public partial class ShetPage : ContentControl
    {
        public ShetViewModel ShetVM { get; set; }
        public ShetPage(int kod_zap)
        {
            InitializeComponent();
            ShetVM = new ShetViewModel(kod_zap);
            this.DataContext = ShetVM;
        }

        private void datagridRashods_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            
        }

        private void TovBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagridTovars.CurrentItem != null)
            {
                OstatkiPage win = new OstatkiPage(true, UpdateTovar);
                win.Width = 1200;
                win.Height = 800;
                Global.DialogWindow = Dialog.Show(win);
            }
        }
        public void UpdateTovar()
        {
            if (Global.Kod_Tov != 0 && datagridTovars.SelectedItem != null)
            {
                (datagridTovars.SelectedItem as Shet_prods).productID = Global.Kod_Tov;
                (datagridTovars.SelectedItem as Shet_prods).Tovar = Global.SelectedTovar;
            }
        }

        private void PdfBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExcelBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBN_Click(object sender, RoutedEventArgs e)
        {
            ShetVM.AddTovar();
        }

        private void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            ShetVM.Save();
        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridTovars.CurrentItem != null)
            {
                if (datagridTovars.CurrentItem is Shet_prods)
                {
                    ShetVM.Delete((datagridTovars.CurrentItem as Shet_prods).ID);
                }
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            ShetVM.Update();
        }
    }
}
