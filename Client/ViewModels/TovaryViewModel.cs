using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class TovaryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tovary> _tovaryList;
        public ObservableCollection<Tovary> TovaryList { get { return _tovaryList; } set { _tovaryList = value; OnPropertyChanged(nameof(TovaryList)); } }
        private Tovary _selectedTovar;
        public Tovary SelectedTovar { get { return _selectedTovar; }
            set { _selectedTovar = value; OnPropertyChanged(nameof(SelectedTovar)); } }

        public bool IsReturn { get; set; }
        public Visibility CloseBTNVisible { get; set; }

        public TovaryViewModel(bool _IsReturn = false)
        {
            Filter();
            IsReturn = true;
            CloseBTNVisible = IsReturn ? Visibility.Visible : Visibility.Collapsed;
        }

        public async Task Filter()
        {
            TovaryList = await HttpRequests<ObservableCollection<Tovary>>.GetRequestAsync("api/Tovary", TovaryList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public async Task Save()
        {
            foreach (Tovary tov in TovaryList)
            {
                if(!tov.IsValid)
                {
                    Global.ErrorLog($"У товара {tov.Naim2} не заполнены все необходимые поля");
                    continue;
                }
                try
                {
                    await HttpRequests<Tovary>.PutRequest("api/Tovary/" + Convert.ToInt32(tov.kod_tovara), tov);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Filter();
        }
    }
}
