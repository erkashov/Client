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
        private ObservableCollection<Product> _tovaryList;
        public ObservableCollection<Product> TovaryList { get { return _tovaryList; } set { _tovaryList = value; OnPropertyChanged(nameof(TovaryList)); } }
        public ObservableCollection<Category> Categories { get { return Enums.Spr_category; } set { OnPropertyChanged(nameof(Categories)); } }
        public ObservableCollection<Manufacture> Manufactures { get { return Enums.Manufactures; } set { OnPropertyChanged(nameof(Manufactures)); } }
        private Product _selectedTovar;
        public Product SelectedTovar { get { return _selectedTovar; }
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
            TovaryList = await HttpRequests<ObservableCollection<Product>>.GetRequestAsync("api/Tovary", TovaryList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public async Task Save()
        {
            foreach (Product tov in TovaryList)
            {
                if(!tov.IsValid)
                {
                    Global.ErrorLog($"У товара {tov.Naim2} не заполнены все необходимые поля");
                    continue;
                }
                try
                {
                    await HttpRequests<Product>.PutRequest("api/Tovary/" + Convert.ToInt32(tov.ID), tov);
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
