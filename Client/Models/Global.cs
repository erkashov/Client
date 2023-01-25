using Client.Models;
using Client.Windows;
using HandyControl.Controls;
using Newtonsoft.Json;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    public static class Global
    {
        public static string Api { get; set; } = "http://26.187.80.25:7082/";
        public static HttpClient client = new HttpClient();
        public static Frame MainFrame { get; set; }
        public static MainWindow MainWin { get; set; }
        public static Dialog DialogWindow { get; set; }
        public static int Kod_Tov = 0;
        public static Product SelectedTovar;

        public static NotificationManager notificationManager = new NotificationManager();

        public static void ShowNotif(string title, string Message, NotificationType type)
        {
            notificationManager.Show(title, Message, type/*, onClick: () => SomeAction()*/);
        }

        public static void ShowWarning(string message, string title = "Внимание")
        {
            Global.ShowNotif(title, message, NotificationType.Warning);
        }

        public static void ErrorLog(string message, string title = "Ошибка")
        {
            if (message == "Невозможно соединиться с удаленным сервером") ShowNotif(message, "Повторите операцию", NotificationType.Error);
            else if (message.Contains("Unauthorized"))
            {
                Global.ShowNotif("Внимание", "Требуется авторизация" + message.ToString(), NotificationType.Warning);
                MainWin.Hide();
                (new AuthWindow()).Show();
            }
            else ShowNotif(title, message, NotificationType.Error);
        }

        public static string FormattedPhoneNumber(string phone)
        {
            if (phone == null)
                return string.Empty;

            switch (phone.Length)
            {
                case 7:
                    return Regex.Replace(phone, @"(\d{3})(\d{4})", "$1-$2");
                case 10:
                    return Regex.Replace(phone, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                case 11:
                    return Regex.Replace(phone, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1-$2-$3-$4");
                default:
                    return phone;
            }
        }
    }

    public class HttpRequests<T>
    {
        public static async Task<T> GetRequestAsync(string url, T responce) 
        {
            try
            {
                HttpResponseMessage response = await Global.client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    responce = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    Global.ErrorLog(response.StatusCode.ToString() + " " + await response.Content.ReadAsStringAsync());
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
            return responce;
        }
        public static T GetRequest(string url, T responce)
        {
            try
            {
                HttpResponseMessage response = Global.client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    responce = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Global.ErrorLog(response.StatusCode.ToString());
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
            return responce;
        }
        public static async Task DeleteRequest(string url)
        {
            HttpResponseMessage response = await Global.client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                string excep = response.Content.ReadAsStringAsync().Result;
                Global.ErrorLog(response.StatusCode.ToString() + " " + excep);
            }
        }

        public static async Task PutRequest(string url, T responce)
        {
            HttpResponseMessage response = await Global.client.PutAsJsonAsync(url, responce);
            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
                string excep = response.Content.ReadAsStringAsync().Result;
                Global.ErrorLog(response.StatusCode.ToString() + " " + excep);
            }
        }

        public static async Task<T> PostRequest(string url, T responce)
        {
            HttpResponseMessage response = await Global.client.PostAsJsonAsync(url, responce);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                string excep = response.Content.ReadAsStringAsync().Result;
                Global.ErrorLog(response.StatusCode.ToString() + " " + excep);
                return responce; 
            }
        }
    }

    public class HttpPostRequests<T, K>
    {
        public static async Task<T> PostRequest(string url, T responce, K query)
        {
            try
            {
                HttpResponseMessage response = await Global.client.PostAsJsonAsync(url, query);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    string excep = response.Content.ReadAsStringAsync().Result;
                    Global.ErrorLog(response.StatusCode.ToString() + " " + excep);
                    return responce;
                }
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.InnerException.Message);
                return responce;
            }
        }
    }

    public class GetEnum<T>
    {
        public static T Get(string url, T e)
        {
            if (e == null)
            {

                e = HttpRequests<T>.GetRequest(url, e);
            }
            return e;
        }

    }
}
