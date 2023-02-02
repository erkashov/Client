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
using System.Web.Routing;
using System.Windows;
using System.Windows.Media;

namespace Client.ViewModels
{
    public class RashodyViewModel : BaseViewModel
    {
        public ObservableCollection<Type_oplaty> Spr_Oplat_Sklad { get { return Enums.Spr_Oplat_Sklad; } set { OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }

        public ObservableCollection<Spr_period_filtr> Spr_Periods_Filter { get { return Enums.Spr_Periods_Filter; } set { OnPropertyChanged(nameof(Spr_Periods_Filter)); } }
        
        public ObservableCollection<User> Spr_Managers_Filter { get { return Enums.Employes; } set { OnPropertyChanged(nameof(Spr_Managers_Filter)); } }


        private ObservableCollection<Sklad_rashod> _rashods;
        public ObservableCollection<Sklad_rashod> Rashods { get { return _rashods; } 
                                                            set { _rashods = value; OnPropertyChanged(nameof(Rashods)); } }

        public ObservableCollection<int> Months { get { return Enums.Months; } set { OnPropertyChanged(nameof(Months)); } }
        public ObservableCollection<int> Years { get { return Enums.RashodyYears; } set { OnPropertyChanged(nameof(Years)); } }

        private DateTime dateStart;
        public DateTime DateStart { get { return dateStart; } set { dateStart = value; Update();} }
        private DateTime dateEnd;
        public DateTime DateEnd { get { return dateEnd; } set { dateEnd = value; Update();} }
        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged(nameof(Search)); Update(); } }

        private Type_oplaty _selectedTipOpl;
        public Type_oplaty SelectedTipOpl { get { return _selectedTipOpl; } set { _selectedTipOpl = value; OnPropertyChanged(nameof(SelectedTipOpl)); Update(); } }

        private User _selectedManager;
        public User SelectedManager { get { return _selectedManager; } set { _selectedManager = value; OnPropertyChanged(nameof(SelectedManager)); Update(); } }
        public List<string> EnumVseDaNet { get { return new List<string>() { "Все", "Да", "Нет" }; } }

        private string _selectedOtgruzheno;
        public string SelectedOtgruzheno { get { return _selectedOtgruzheno; } set { _selectedOtgruzheno = value; OnPropertyChanged(nameof(SelectedOtgruzheno)); Update(); } }
        private string _selectedOplachen;
        public string SelectedOplachen { get { return _selectedOplachen; } set { _selectedOplachen = value; OnPropertyChanged(nameof(SelectedOplachen)); Update(); } }

        public RashodyViewModel()
        {
            Route = "Sklad_rashod/";
            DateStart = DateTime.Now.AddDays(-7);
            DateEnd = DateTime.Now;
            SelectedOplachen = SelectedOtgruzheno = "Все";
            Search = "";
            IsDataLoaded = true;
            Update();
        }

        public async override Task Update()
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
            Rashods = new ObservableCollection<Sklad_rashod>(await HttpPostRequests<List<Sklad_rashod>, RashodyQueryParams>.PostRequest(Route + "Filter", new List<Sklad_rashod>(), queryParams));
            IsDataLoaded = true;
        }
        public async override Task Save()
        {
            foreach (Sklad_rashod rashod in Rashods)
            {
                try
                {
                    await HttpRequests<Sklad_rashod>.PutRequest(Route + rashod.ID, rashod);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Update();
        }

        public async override Task Delete(int id)
        {
            try
            {
                await HttpRequests<Sklad_rashod>.DeleteRequest(Route + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }

        public async Task Add()
        {
            Sklad_rashod rashod = new Sklad_rashod();
            await HttpRequests<Sklad_rashod>.PostRequest(Route, rashod);
            Update();
        }
    }
}
