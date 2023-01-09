using Client.Models;
using Client.Models.Sklad;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Client.ViewModels
{
    public class RashodyViewModel : INotifyPropertyChanged
    {
        bool IsDataLoaded = false;
        public ObservableCollection<Spr_oplat_sklad> Spr_Oplat_Sklad { get { return Enums.Spr_Oplat_Sklad; } set { OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }

        public ObservableCollection<Spr_period_filtr> Spr_Periods_Filter { get { return Enums.Spr_Periods_Filter; } set { OnPropertyChanged(nameof(Spr_Periods_Filter)); } }
        
        public ObservableCollection<User> Spr_Managers_Filter { get { return Enums.Users; } set { OnPropertyChanged(nameof(Spr_Managers_Filter)); } }


        private ObservableCollection<Sklad_rashod> _rashods;
        public ObservableCollection<Sklad_rashod> Rashods { get { return _rashods; } 
                                                            set { _rashods = value; OnPropertyChanged(nameof(Rashods)); } }

        public ObservableCollection<int> Months { get { return Enums.Months; } set { OnPropertyChanged(nameof(Months)); } }
        public ObservableCollection<int> Years { get { return Enums.RashodyYears; } set { OnPropertyChanged(nameof(Years)); } }

        private DateTime dateStart;
        public DateTime DateStart { get { return dateStart; } set { dateStart = value; Filter();} }
        private DateTime dateEnd;
        public DateTime DateEnd { get { return dateEnd; } set { dateEnd = value; Filter();} }
        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged(nameof(Search)); Filter(); } }

        private Spr_oplat_sklad _selectedTipOpl;
        public Spr_oplat_sklad SelectedTipOpl { get { return _selectedTipOpl; } set { _selectedTipOpl = value; OnPropertyChanged(nameof(SelectedTipOpl)); Filter(); } }

        private User _selectedManager;
        public User SelectedManager { get { return _selectedManager; } set { _selectedManager = value; OnPropertyChanged(nameof(SelectedManager)); Filter(); } }
        public List<string> EnumVseDaNet { get { return new List<string>() { "Все", "Да", "Нет" }; } }

        private string _selectedOtgruzheno;
        public string SelectedOtgruzheno { get { return _selectedOtgruzheno; } set { _selectedOtgruzheno = value; OnPropertyChanged(nameof(SelectedOtgruzheno)); Filter(); } }
        private string _selectedOplachen;
        public string SelectedOplachen { get { return _selectedOplachen; } set { _selectedOplachen = value; OnPropertyChanged(nameof(SelectedOplachen)); Filter(); } }
        public PropertyGridDemoModel DemoModel { get; set; }

        public RashodyViewModel()
        {
            DateEnd = DateStart = DateTime.Now;
            SelectedOplachen = SelectedOtgruzheno = "Все";
            Search = "";
            IsDataLoaded = true;
            Filter();
            DemoModel = new PropertyGridDemoModel
            {
            };
        }

        public async Task Filter()
        {
            if (!IsDataLoaded) return;
            IsDataLoaded = false;
            RashodyQueryParams queryParams = new RashodyQueryParams(DateStart, DateEnd, Search)
            {
                SelectedTipOpl = SelectedTipOpl, SelectedManager = SelectedManager
            };
            switch (SelectedOplachen)
            {
                case "Да":
                    queryParams.SelectedOplachen = true;
                    break;
                case "Нет":
                    queryParams.SelectedOplachen = false;
                    break;
            }
            switch (SelectedOtgruzheno)
            {
                case "Да":
                    queryParams.SelectedOtgruzheno = true;
                    break;
                case "Нет":
                    queryParams.SelectedOtgruzheno = false;
                    break;
            }
            Rashods = new ObservableCollection<Sklad_rashod>(await HttpPostRequests<List<Sklad_rashod>, RashodyQueryParams>.PostRequest("api/Sklad_rashod/Filter", new List<Sklad_rashod>(), queryParams));
            IsDataLoaded = true;
        }
        public async Task Save()
        {
            foreach (Sklad_rashod rashod in Rashods)
            {
                try
                {
                    await HttpRequests<Sklad_rashod>.PutRequest("api/Sklad_rashod/" + Convert.ToInt32(rashod.Kod_zap), rashod);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Filter();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class PropertyGridDemoModel
    {
        public Otgruzheno Otgruzheno { get; set; }
        public bool IsDataLoaded { get; set; }
    }

    public enum Otgruzheno
    {
        Все,
        Да,
        Нет
    }
}
