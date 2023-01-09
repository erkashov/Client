﻿using Client.Models;
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
    /// Логика взаимодействия для ShetaPage.xaml
    /// </summary>
    public partial class ShetaPage : ContentControl
    {
        public ShetaViewModel ShetaVM { get; set; }
        public ShetaPage()
        {
            InitializeComponent();
            ShetaVM = new ShetaViewModel();
            this.DataContext = ShetaVM;
        }

        private void datagridRashods_MoseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagridSheta.SelectedItem != null)
            {
                /*RashodPage rashod = new RashodPage((datagridRashods.SelectedItem as Sklad_rashod).Kod_zap);
                Global.MainWin.ShowPage(new PagesContainer("Расход №" + (datagridRashods.SelectedItem as Sklad_rashod).Nom_rash, rashod));*/
            }
        }

        private async void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (datagridSheta.CurrentItem != null)
            {
                if (datagridSheta.CurrentItem is Shet)
                {
                    try
                    {
                        await HttpRequests<Sklad_rashod>.DeleteRequest("api/Sheta/" + (datagridSheta.CurrentItem as Shet).kod_zap);
                    }
                    catch (Exception ex)
                    {
                        Global.ErrorLog(ex.Message);
                    }
                    ShetaVM.Filter();
                }
            }
        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {
            ShetaVM.Filter();
        }

        private async void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            ShetaVM.Save();
        }

        private async void AddBN_Click(object sender, RoutedEventArgs e)
        {
            Shet shet = new Shet();
            await HttpRequests<Shet>.PostRequest("api/Sheta/", shet);
            ShetaVM.Filter();
        }
    }
}