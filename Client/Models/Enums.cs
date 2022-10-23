using Client.Models.Sklad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Enums
    {
        private static ObservableCollection<Spr_oplat_sklad> _spr_Oplat_Sklad;
        private static ObservableCollection<Spr_period_filtr> _spr_Periods_Filter;
        private static ObservableCollection<Karta> _spr_karty;
        private static ObservableCollection<Category> _spr_category;

        public static ObservableCollection<Spr_oplat_sklad> Spr_Oplat_Sklad
        {
            get
            {
                return GetEnum<ObservableCollection<Spr_oplat_sklad>>.Get("api/Enum/Oplaty", _spr_Oplat_Sklad);
            }
        }

        public static ObservableCollection<Spr_period_filtr> Spr_Periods_Filter
        {
            get
            {
                return GetEnum<ObservableCollection<Spr_period_filtr>>.Get("api/Enum/Periods", _spr_Periods_Filter);
            }
        }

        public static ObservableCollection<Karta> Spr_karty
        {
            get
            {
                return GetEnum<ObservableCollection<Karta>>.Get("api/Enum/Karty", _spr_karty);
            }
        }

        public static ObservableCollection<Category> Spr_category
        {
            get
            {
               return GetEnum<ObservableCollection<Category>>.Get("api/Enum/Category", _spr_category);
            }
        }


        public static ObservableCollection<Spr_oplat_sklad> GetSprOplatSklad()
        {
            if (_spr_Oplat_Sklad == null)
            {
                if (false/*File.Exists(Environment.CurrentDirectory + "\\Spr_Oplat_Sklad.json")*/)
                {
                    /*using (StreamReader file = File.OpenText(Environment.CurrentDirectory + "\\Spr_Oplat_Sklad.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        _spr_Oplat_Sklad = (ObservableCollection<Spr_oplat_sklad>)serializer.Deserialize(file, typeof(List<Spr_oplat_sklad>));
                    }*/
                }
                else
                {
                    HttpRequests<ObservableCollection<Spr_oplat_sklad>> httpRequests = new HttpRequests<ObservableCollection<Spr_oplat_sklad>>();
                    _spr_Oplat_Sklad = httpRequests.GetRequest("api/Enum/Oplaty", _spr_Oplat_Sklad);

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

        public static ObservableCollection<Spr_period_filtr> GetSprPeriodsFilter()
        {
            if (_spr_Periods_Filter == null)
            {
                HttpRequests<ObservableCollection<Spr_period_filtr>> periodsHttp = new HttpRequests<ObservableCollection<Spr_period_filtr>>();
                _spr_Periods_Filter = periodsHttp.GetRequest($"api/Enum/Periods", _spr_Periods_Filter);
            }
            return _spr_Periods_Filter;
        }
    }

    public class GetEnum<T>
    {
        public static T Get(string url, T e)
        {
            if (e == null)
            {
                HttpRequests<T> periodsHttp = new HttpRequests<T>();
                e = periodsHttp.GetRequest(url, e);
            }
            return e;
        }

    }
}
