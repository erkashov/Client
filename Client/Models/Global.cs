using Client.Models;
using Newtonsoft.Json;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    public static class Global
    {
        public static string Api = "https://localhost:7082/";
        public static HttpClient client = new HttpClient();
        private static ObservableCollection<Spr_oplat_sklad> _spr_Oplat_Sklad;
        private static ObservableCollection<Spr_period_filtr> _spr_Periods_Filter;
        public async static Task<ObservableCollection<Spr_oplat_sklad>> GetSprOplatSklad()
        {
            if (_spr_Oplat_Sklad == null)
            {
                if (File.Exists(Environment.CurrentDirectory + "\\Spr_Oplat_Sklad.json"))
                {
                    using (StreamReader file = File.OpenText(Environment.CurrentDirectory + "\\Spr_Oplat_Sklad.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        _spr_Oplat_Sklad = (ObservableCollection<Spr_oplat_sklad>)serializer.Deserialize(file, typeof(List<Spr_oplat_sklad>));
                    }
                }
                else
                {
                    HttpRequests<ObservableCollection<Spr_oplat_sklad>> httpRequests = new HttpRequests<ObservableCollection<Spr_oplat_sklad>>();
                    _spr_Oplat_Sklad = await httpRequests.GetRequest("api/Spr_Oplat_Sklad", _spr_Oplat_Sklad);

                    /*// serialize JSON directly to a file
                    using (StreamWriter file = File.CreateText(Environment.CurrentDirectory + "\\spr_oplat_sklad.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, _skladList);
                    }*/
                }
            }
            return _spr_Oplat_Sklad;
        }

        public async static Task<ObservableCollection<Spr_period_filtr>> GetSprPeriodsFilter()
        {
            if (_spr_Periods_Filter == null)
            {
                HttpRequests<ObservableCollection<Spr_period_filtr>> periodsHttp = new HttpRequests<ObservableCollection<Spr_period_filtr>>();
                _spr_Periods_Filter = await periodsHttp.GetRequest($"api/Spr_period_filtr", _spr_Periods_Filter);
            }
            return _spr_Periods_Filter;
        }

        public static NotificationManager notificationManager = new NotificationManager();

        public static void ShowNotif(string title, string Message, NotificationType type)
        {
            notificationManager.Show(title, Message, type/*, onClick: () => SomeAction()*/);
        }

        public static void ErrorLog( string message, string title = "Ошибка")
        {
            ShowNotif(title, message, NotificationType.Error);
        }

    }

    public class HttpRequests<T>
    {
        public async Task<T> GetRequest(string url, T responce) 
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
                    Global.ErrorLog(response.StatusCode.ToString());
                    //if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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

        public async Task PutRequest(string url, T responce)
        {
            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(responce), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Global.client.PutAsJsonAsync(url, responce);
            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
                string excep = response.Content.ReadAsStringAsync().Result;
                Global.ErrorLog(response.StatusCode.ToString() + " " + excep);
                //if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            }
        }

        public async Task<T> PostRequest(string url, T responce)
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
                //if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            }
            
        }
    }
}
