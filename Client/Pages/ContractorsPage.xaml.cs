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
    /// Логика взаимодействия для ContractorsPage.xaml
    /// </summary>
    public partial class ContractorsPage : ContentControl
    {
        public ContractorViewModel ContractorsVM { get; set; }
        public ContractorsPage()
        {
            InitializeComponent();
            ContractorsVM = new ContractorViewModel();
            this.DataContext = ContractorsVM;
        }

        private void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            ContractorsVM.Add();
        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            ContractorsVM.Save();
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            ContractorsVM.Update();
        }

        private void datagridTovary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
