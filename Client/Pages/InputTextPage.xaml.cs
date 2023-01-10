﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Логика взаимодействия для InputTextPage.xaml
    /// </summary>
    public partial class InputTextPage : ContentControl
    {
        public InputTextPage()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Uri.IsWellFormedUriString(textTB.Text, UriKind.Absolute))
            {
                Properties.Settings.Default.URL = textTB.Text;
                Properties.Settings.Default.Save();
                if (Global.DialogWindow != null) Global.DialogWindow.Close();

                Global.Api = Properties.Settings.Default.URL;
                Global.client = new System.Net.Http.HttpClient();
                Global.client.BaseAddress = new Uri(Global.Api);
                Global.client.DefaultRequestHeaders.Accept.Clear();
                Global.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Global.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
            }
            else Global.ErrorLog("Неправильная ссылка");
        }
    }
}
