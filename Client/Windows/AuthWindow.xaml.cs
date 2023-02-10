using Client.Models;
using Client.Pages;
using HandyControl.Controls;
using HandyControl.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Shapes;
using Window = System.Windows.Window;

namespace Client.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            try
            {
                if (Global.client.BaseAddress == null)
                {
                    Global. InitializeClient();
                }

                ConfigHelper.Instance.SetLang("ru");
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            InitializeComponent();
        }

        private void authBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = Global.client.GetAsync($"Users/Token?username={loginTB.Text}&password={passwordTB.Text}").Result;
                if (response.IsSuccessStatusCode)
                {
                    Properties.Settings.Default.Token = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                    Properties.Settings.Default.Save();

                    Global.InitializeClient();

                    response = Global.client.GetAsync($"Users/CheckToken").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Global.CurrentUser = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                        if (Global.CurrentUser.role == "client")
                        {
                            HandyControl.Controls.MessageBox.Show("Перейдите на сайт masterwood.ru для оформления заказа");
                            return;
                        }

                        if (Global.MainWin == null) (new MainWindow()).Show();
                        else Global.MainWin.Show();
                        this.Close();
                    }
                }
                else
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Global.ErrorLog("Неправильный логин");
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        Global.ErrorLog("Неправильный пароль");
                    }
                    else Global.ErrorLog(response.StatusCode.ToString());
                }
            }
            catch (HttpRequestException ex)
            {
                Global.ErrorLog(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.F10)
            {
                InputTextPage win = new InputTextPage();
                Global.DialogWindow = Dialog.Show(win);
            }
        }
    }
}
