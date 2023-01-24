using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class PriceViewModel : BaseViewModel
    {
        private ObservableCollection<Price> _priceList;
        public ObservableCollection<Price> PriceList { get { return _priceList; } set { _priceList = value; OnPropertyChanged(nameof(PriceList)); } }
        public PriceViewModel()
        {
            Route = "Zen_roznichnie/"; 
            Update();
        }

        public override async Task Update()
        {
            PriceList = await HttpRequests<ObservableCollection<Price>>.GetRequestAsync(Route, PriceList);
        }

        public override async Task Save()
        {
            foreach (Price price in PriceList)
            {
                try
                {
                    await HttpRequests<Price>.PutRequest(Route + price.ID, price);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Update();
        }

        public void Add()
        {
            PriceList.Add(new Price());
        }

        public override async Task Delete(int id)
        {
            try
            {
                await HttpRequests<Price>.DeleteRequest(Route + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }
    }
}
