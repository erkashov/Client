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
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : ContentControl
    {
        public CategoriesViewModel CategoriesVM { get; set; }
        public CategoriesPage()
        {
            InitializeComponent();
            CategoriesVM = new CategoriesViewModel();
            this.DataContext = CategoriesVM;
        }

        private void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            CategoriesVM.Add();
        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridCat.CurrentItem != null)
            {
                CategoriesVM.Delete((datagridCat.CurrentItem as Category).ID);

            }

        }

        private void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            CategoriesVM.Save();
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            CategoriesVM.Update();
        }
    }
}
