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
    public class ShetaViewModel : INotifyPropertyChanged
    {
        bool IsDataLoaded = false;
        public ObservableCollection<User> Spr_Managers_Filter { get { return Enums.Users; } set { OnPropertyChanged(nameof(Spr_Managers_Filter)); } }
        private ObservableCollection<Shet> _sheta;
        public ObservableCollection<Shet> Sheta
        {
            get { return _sheta; }
            set { _sheta = value; OnPropertyChanged(nameof(Sheta)); }
        }

        private DateTime dateStart;
        public DateTime DateStart { get { return dateStart; } set { dateStart = value; Filter(); } }
        private DateTime dateEnd;
        public DateTime DateEnd { get { return dateEnd; } set { dateEnd = value; Filter(); } }
        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged(nameof(Search)); Filter(); } }

        private User _selectedManager;
        public User SelectedManager { get { return _selectedManager; } set { _selectedManager = value; OnPropertyChanged(nameof(SelectedManager)); Filter(); } }
        public List<string> EnumVseDaNet { get { return new List<string>() { "Все", "Да", "Нет" }; } }

        private string _selectedOplachen;
        public string SelectedOplachen { get { return _selectedOplachen; } set { _selectedOplachen = value; OnPropertyChanged(nameof(SelectedOplachen)); Filter(); } }

        public ShetaViewModel()
        {
            DateEnd = DateStart = DateTime.Now;
            SelectedOplachen = "Все";
            Search = "";
            IsDataLoaded = true;
            Filter();
        }

        public async Task Filter()
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
            Sheta = new ObservableCollection<Shet>(await HttpPostRequests<List<Shet>, RashodyQueryParams>.PostRequest("api/Shets/Filter", new List<Shet>(), queryParams));
            IsDataLoaded = true;
        }
        public async Task Save()
        {
            foreach (Shet shet in Sheta)
            {
                try
                {
                    await HttpRequests<Shet>.PutRequest("api/Shets/" + Convert.ToInt32(shet.ID), shet);
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
}
