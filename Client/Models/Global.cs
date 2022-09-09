using Client.Models;
using Newtonsoft.Json;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    public static class Global
    {
        public static string Api = "https://localhost:7082/";
        public static HttpClient client = new HttpClient();
        private static List<Spr_oplat_sklad> _skladList;
        public static List<Spr_oplat_sklad> spr_Oplat_Sklads
        {
            get
            {
                if (_skladList == null)
                {
                    if (File.Exists(Environment.CurrentDirectory + "\\spr_oplat_sklad.json"))
                    {
                        using (StreamReader file = File.OpenText(Environment.CurrentDirectory + "\\spr_oplat_sklad.json"))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                             _skladList = (List<Spr_oplat_sklad>)serializer.Deserialize(file, typeof(List<Spr_oplat_sklad>));
                        }
                    }
                    else
                    {
                        /*HttpRequests<List<Spr_oplat_sklad>> httpRequests = new HttpRequests<List<Spr_oplat_sklad>>();
                        _skladList = httpRequests.GetRequest("api/Spr_oplat_sklad", _skladList).Result;

                        // serialize JSON directly to a file
                        using (StreamWriter file = File.CreateText(Environment.CurrentDirectory + "\\spr_oplat_sklad.json"))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(file, _skladList);
                        }*/
                    }
                }
                return _skladList;
            }
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
            return responce;
        }
    }
}
