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
    public class TovaryViewModel : BaseViewModel
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
            Route = "Tovary/";
            Update();
            IsReturn = _IsReturn;
            CloseBTNVisible = IsReturn ? Visibility.Visible : Visibility.Collapsed;
        }

        public override async Task Update()
        {
            TovaryList = await HttpRequests<ObservableCollection<Product>>.GetRequestAsync(Route, TovaryList);
        }

        public override async Task Save()
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
                    await HttpRequests<Product>.PutRequest(Route + Convert.ToInt32(tov.ID), tov);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Update();
        }

        public override async Task Delete(int id)
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
    }
}
