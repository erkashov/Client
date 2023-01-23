using Client.Models;
using Client.Models.Sklad;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class PrihodViewModel : BaseViewModel
    {
        public ObservableCollection<Product> TovaryList { get; set; }
        public ObservableCollection<Contractor> Contractors { get { return Enums.Contractors; } set { OnPropertyChanged(nameof(Contractors)); } }
        private Sklad_prihod _sklad_prihod;
        public Sklad_prihod Prihod { get { return _sklad_prihod; } set { _sklad_prihod = value; OnPropertyChanged(nameof(Prihod)); } }

        public int ID;
        public PrihodViewModel(int id)
        {
            Route = "Sklad_prihods/";
            ID = id;
            Update();
        }
        public override async Task Delete(int id)
        {
            try
            {
                await HttpRequests<Sklad_prihod_prods>.DeleteRequest(Route + "Tov?id=" + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }

        public override async Task Save()
        {
            IsDataLoaded = false;
            foreach (Sklad_prihod_prods tov in Prihod.Sklad_prihod_tov) tov.Sklad_prihod = null;
            try
            {
                await HttpRequests<Sklad_prihod>.PutRequest(Route + Prihod.ID, Prihod);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
            IsDataLoaded = true;
        }

        public override async Task Update()
        {
            TovaryList = HttpRequests<ObservableCollection<Product>>.GetRequest("Tovary", TovaryList);
            Prihod = await HttpRequests<Sklad_prihod>.GetRequestAsync(Route + ID, Prihod);
            foreach (Sklad_prihod_prods tov in Prihod.Sklad_prihod_tov) tov.Sklad_prihod = Prihod;
        }
        public async Task Add()
        {
            Prihod.Sklad_prihod_tov.Add(new Sklad_prihod_prods() { PrihodID = ID });
        }
    }
}
