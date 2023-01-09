﻿using Client.Models;
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
        public RashodPage(decimal id)
        {
            Id = id;
            this.DataContext = ViewModel;
            Header = "Расхлод №";
            InitializeComponent();
            GetVM();
            IsLoaded = true;
        }

        public async void GetVM()
        {
            IsLoaded = false;
            ViewModel = new RashodViewModel();
            ViewModel.Rashod = await HttpRequests<Sklad_rashod>.GetRequestAsync("api/Sklad_rashod/" + Convert.ToInt32(Id), ViewModel.Rashod);
            this.DataContext = ViewModel;
            IsLoaded = true;
        }

        private async void AddBN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddTovar();
            /*Sklad_rashod_tov tov = new Sklad_rashod_tov();
            tov.Kod_rashoda = (int)ViewModel.Rashod.Kod_zap;
            await HttpRequests<Sklad_rashod_tov>.PostRequest("api/Sklad_rashod/Tov", tov);
            ViewModel.Rashod = await HttpRequests<Sklad_rashod>.GetRequestAsync("api/Sklad_rashod/" + Convert.ToInt32(Id), ViewModel.Rashod);*/
        }

        private async void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            IsLoaded = false;
            try
            {
                await HttpRequests<Sklad_rashod>.PutRequest("api/Sklad_rashod/" + ViewModel.Rashod.Kod_zap, ViewModel.Rashod);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            GetVM();
            IsLoaded = true;
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if(datagridRashods.CurrentItem != null)
            {
                if(datagridRashods.CurrentItem is Sklad_rashod_tov)
                {
                    try
                    {
                        await HttpRequests<Sklad_rashod_tov>.DeleteRequest("api/Sklad_rashod/Tov?id=" + (int)((datagridRashods.CurrentItem as Sklad_rashod_tov).Kod_zap));
                    }
                    catch (Exception ex)
                    {
                        Global.ErrorLog(ex.Message);
                    }
                    GetVM();
                }
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {

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
                ViewModel.Rashod.Sklad_rashod_tov.Where(p=>p.Kod_zap ==  (SelectedItem as Sklad_rashod_tov).Kod_zap).FirstOrDefault().Kod_tovara = Convert.ToInt32(Global.Kod_Tov);
                ViewModel.Rashod.Sklad_rashod_tov.Where(p=>p.Kod_zap ==  (SelectedItem as Sklad_rashod_tov).Kod_zap).FirstOrDefault().Tovar = Global.SelectedTovar;
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
                        await (datagridRashods.CurrentItem as Sklad_rashod_tov).UpdateZena();
                    }
                }
            }
        }

        private void ContentControl_GotFocus(object sender, RoutedEventArgs e)
        {
            //GetVM();
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
            path = await HttpRequests<string>.GetRequestAsync($"api/Sklad_rashod/File?id={ViewModel.Rashod.Kod_zap}&format={format}&printZeny=true&printBeznal=true", path);
            if(path != "")
            {
                Process.Start(path);
            }
        }
    }
}