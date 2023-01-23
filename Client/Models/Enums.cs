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
        private static ObservableCollection<Type_oplaty> _spr_Oplat_Sklad;
        private static ObservableCollection<Spr_period_filtr> _spr_Periods_Filter;
        private static ObservableCollection<Category> _spr_category;
        private static ObservableCollection<int> _rashodyYears;
        private static ObservableCollection<User> _users;
        private static ObservableCollection<Manufacture> _manufactures;
        private static ObservableCollection<Contractor> _contractors;

        public static ObservableCollection<Type_oplaty> Spr_Oplat_Sklad
        {
            get
            {
                return _spr_Oplat_Sklad = GetEnum<ObservableCollection<Type_oplaty>>.Get("api/Enum/Oplaty", _spr_Oplat_Sklad);
            }
        }

        public static ObservableCollection<Spr_period_filtr> Spr_Periods_Filter
        {
            get
            {
                return _spr_Periods_Filter = GetEnum<ObservableCollection<Spr_period_filtr>>.Get("api/Enum/Periods", _spr_Periods_Filter);
            }
        }

        public static ObservableCollection<Category> Spr_category
        {
            get
            {
               return _spr_category = GetEnum<ObservableCollection<Category>>.Get(Global.Api + "api/Enum/Category", _spr_category);
            }
        }

        public static ObservableCollection<int> Months
        {
            get
            {
                return new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            }
        }

        public static ObservableCollection<int> RashodyYears
        {
            get
            {
                return _rashodyYears = GetEnum<ObservableCollection<int>>.Get("api/Enum/YearsRashods", _rashodyYears);
            }
        }

        public static ObservableCollection<User> Users
        {
            get
            {
                return _users = GetEnum<ObservableCollection<User>>.Get("api/Enum/Users", _users);
            }
        }

        public static ObservableCollection<Manufacture> Manufactures
        {
            get
            {
                return _manufactures = GetEnum<ObservableCollection<Manufacture>>.Get("api/Manufactures", _manufactures);
            }
        }

        public static ObservableCollection<Contractor> Contractors
        {
            get
            {
                return _contractors = GetEnum<ObservableCollection<Contractor>>.Get("api/Contractors", _contractors);
            }
        }
    }

    
}
