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

        }

        private void PdfBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExcelBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
