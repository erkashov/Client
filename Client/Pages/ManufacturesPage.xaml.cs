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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManufacturesPage.xaml
    /// </summary>
    public partial class ManufacturesPage : ContentControl
    {
        public ManufViewModel ManufVM { get; set; }
        public ManufacturesPage()
        {
            InitializeComponent();
            ManufVM = new ManufViewModel();
            this.DataContext = ManufVM;
        }

        private void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            ManufVM.Add();
        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridManufs.CurrentItem != null)
            {
                if (datagridManufs.CurrentItem is Manufacture)
                {
                    ManufVM.Delete((datagridManufs.CurrentItem as Manufacture).ID);
                }
            }
            
        }

        private void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            ManufVM.Save();
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            ManufVM.Update();
        }
    }
}
