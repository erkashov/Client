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
    public class ShetaViewModel : BaseViewModel
    {
        public ObservableCollection<User> Spr_Managers_Filter { get { return Enums.Users; } set { OnPropertyChanged(nameof(Spr_Managers_Filter)); } }
        private ObservableCollection<Shet> _sheta;
        public ObservableCollection<Shet> Sheta
        {
            get { return _sheta; }
            set { _sheta = value; OnPropertyChanged(nameof(Sheta)); }
        }

        private DateTime dateStart;
        public DateTime DateStart { get { return dateStart; } set { dateStart = value; Update(); } }
        private DateTime dateEnd;
        public DateTime DateEnd { get { return dateEnd; } set { dateEnd = value; Update(); } }
        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged(nameof(Search)); Update(); } }

        private User _selectedManager;
        public User SelectedManager { get { return _selectedManager; } set { _selectedManager = value; OnPropertyChanged(nameof(SelectedManager)); Update(); } }
        public List<string> EnumVseDaNet { get { return new List<string>() { "Все", "Да", "Нет" }; } }

        private string _selectedOplachen;
        public string SelectedOplachen { get { return _selectedOplachen; } set { _selectedOplachen = value; OnPropertyChanged(nameof(SelectedOplachen)); Update(); } }

        public ShetaViewModel()
        {
            Route = "Shets/";
            DateEnd = DateStart = DateTime.Now;
            SelectedOplachen = "Все";
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
                SelectedManager = SelectedManager
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
            Sheta = new ObservableCollection<Shet>(await HttpPostRequests<List<Shet>, RashodyQueryParams>.PostRequest(Route + "Filter", new List<Shet>(), queryParams));
            IsDataLoaded = true;
        }

        public async override Task Save()
        {
            foreach (Shet shet in Sheta)
            {
                try
                {
                    await HttpRequests<Shet>.PutRequest(Route + shet.ID, shet);
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
