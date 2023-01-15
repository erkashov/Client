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
            VM.TovaryList.Add(new Tovary());
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if(datagridTovary.CurrentItem != null)
            {
                if(datagridTovary.CurrentItem is Tovary)
                {
                    try
                    {
                        await HttpRequests<Sklad_rashod>.DeleteRequest("api/Tovary/" + (datagridTovary.CurrentItem as Tovary).kod_tovara);
                    }
                    catch (Exception ex)
                    {
                        Global.ErrorLog(ex.Message);
                    }
                    VM.Filter();
                }
            }
        }

        private void ToolBarControl_SaveClick(object sender, RoutedEventArgs e)
        {
            VM.Save();
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            VM.Filter();
        }

        private void datagridTovary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (VM.IsReturn)
            {
                if (datagridTovary.SelectedItem != null)
                {
                    Global.Kod_Tov = (datagridTovary.SelectedItem as Tovary).kod_tovara;
                    Global.SelectedTovar = (datagridTovary.SelectedItem as Tovary);
                    if (Global.DialogWindow != null) Global.DialogWindow.Close();
                    if (CloseAction != null) CloseAction();
                }
            }
        }
    }
}
