using Client.Models;
using Client.ViewModels;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Client.Pages.Sklad
{
    /// <summary>
    /// Логика взаимодействия для RashodPage.xaml
    /// </summary>
    public partial class RashodPage : ContentControl
    {
        public decimal Id { get; set; }
        public RashodViewModel ViewModel { get; set; }
        public string Header { get; set; }
        public bool IsLoaded = false;
        private object SelectedItem = null;
        public RashodPage(int id)
        {
            Id = id;
            this.DataContext = ViewModel;
            Header = "Расхлод №";
            InitializeComponent();
            GetVM(id);
            IsLoaded = true;
        }

        public async void GetVM(int id)
        {
            IsLoaded = false;
            ViewModel = new RashodViewModel(id);
            this.DataContext = ViewModel;
            IsLoaded = true;
        }

        private async void AddBN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddTovar();
        }

        private async void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if(datagridRashods.CurrentItem != null)
            {
                if(datagridRashods.CurrentItem is Sklad_rashod_prods)
                {
                    ViewModel.Delete((datagridRashods.CurrentItem as Sklad_rashod_prods).ID);
                }
            }
        }

        private void TovBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagridRashods.CurrentItem != null)
            {
                SelectedItem = datagridRashods.CurrentItem;
                OstatkiPage win = new OstatkiPage(true, UpdateTovar);
                win.Width = 1200;
                win.Height = 800;
                Global.DialogWindow = Dialog.Show(win);
            }
        }

        public void UpdateTovar()
        {
            if (Global.Kod_Tov != 0 && SelectedItem != null)
            {
                ViewModel.Rashod.Sklad_rashod_tov.Where(p=>p.ID ==  (SelectedItem as Sklad_rashod_prods).ID).FirstOrDefault().ProductID = Convert.ToInt32(Global.Kod_Tov);
                ViewModel.Rashod.Sklad_rashod_tov.Where(p=>p.ID ==  (SelectedItem as Sklad_rashod_prods).ID).FirstOrDefault().Tovar = Global.SelectedTovar;
                SelectedItem = null;
            } 
        }

        private async void datagridRashods_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (IsLoaded)
            {
                if(e.Column == count)
                {
                    if(datagridRashods.CurrentItem != null)
                    {
                        await (datagridRashods.CurrentItem as Sklad_rashod_prods).UpdateZena();
                    }
                }
            }
        }

        private void ContentControl_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void PdfBTN_Click(object sender, RoutedEventArgs e)
        {
            print_file("pdf");
        }

        private void ExcelBTN_Click(object sender, RoutedEventArgs e)
        {
            print_file("xlsx");
        }

        private async void print_file(string format)
        {
            string path = "";
            path = await HttpRequests<string>.GetRequestAsync($"Sklad_rashod/File?id={ViewModel.Rashod.ID}&format={format}&printZeny=true&printBeznal=true", path);
            if(path != "")
            {
                Process.Start(path);
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
