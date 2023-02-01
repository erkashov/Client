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
    /// Логика взаимодействия для TovaryPage.xaml
    /// </summary>
    public partial class TovaryPage : ContentControl
    {
        private TovaryViewModel VM { get; set; }
        public Action CloseAction { get; set; }
        public TovaryPage()
        {
            InitializeComponent();
            VM = new TovaryViewModel();
            this.DataContext = VM;
        }
        public TovaryPage(bool _IsReturn = false, Action closeAction = null)
        {
            InitializeComponent();
            VM = new TovaryViewModel(_IsReturn);
            this.DataContext = VM;
            CloseAction = closeAction;
        }
        private async void ToolBarControl_AddClick(object sender, RoutedEventArgs e)
        {
            VM.TovaryList.Add(new Product() {});
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if(datagridTovary.SelectedItem != null)
            {
                if(datagridTovary.SelectedItem is Product)
                {
                    VM.Delete((datagridTovary.SelectedItem as Product).ID);
                }
            }
        }

        private void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            VM.Save();
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            VM.Update();
        }

        private void datagridTovary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (VM.IsReturn)
            {
                if (datagridTovary.SelectedItem != null)
                {
                    Global.Kod_Tov = (datagridTovary.SelectedItem as Product).ID;
                    Global.SelectedTovar = (datagridTovary.SelectedItem as Product);
                    if (Global.DialogWindow != null) Global.DialogWindow.Close();
                    if (CloseAction != null) CloseAction();
                }
            }
        }

        private void CopyBN_Click(object sender, RoutedEventArgs e)
        {
            if (datagridTovary.SelectedItem != null)
            {
                if (datagridTovary.SelectedItem is Product)
                {
                    VM.Copy((datagridTovary.SelectedItem as Product).ID);
                }
            }
        }
    }
}
