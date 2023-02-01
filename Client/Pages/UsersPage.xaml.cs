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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : ContentControl
    {
        UsersViewModel VM { get; set; }
        public UsersPage()
        {
            InitializeComponent();
            VM = new UsersViewModel();

            this.DataContext = VM;
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            VM.Update();
        }

        private void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            VM.Save();
        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridUsers.SelectedItem != null)
            {
                VM.Delete((datagridUsers.SelectedItem as User).ID);
            }
        }

        private void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            VM.Add();
        }

        private void datagridUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
