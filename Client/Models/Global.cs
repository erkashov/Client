﻿using Client.Models;
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

        public static NotificationManager notificationManager = new NotificationManager();

        public static void ShowNotif(string title, string Message, NotificationType type)
        {
            notificationManager.Show(title, Message, type/*, onClick: () => SomeAction()*/);
        }

        public static void ErrorLog( string message, string title = "Ошибка")
        {
            if(message == "Невозможно соединиться с удаленным сервером") ShowNotif(message, "Повторите операцию", NotificationType.Error);
            else ShowNotif(title, message, NotificationType.Error);
        }

    }

    public class HttpRequests<T>
    {
        public async Task<T> GetRequestAsync(string url, T responce) 
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

        public T GetRequest(string url, T responce)
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

    public class HttpPostRequests<T, K>
    {
        public async Task<T> PostRequest(string url, T responce, K query)
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
                    //if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                }
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.InnerException.Message);
                return responce;
            }

        }
    }
}
