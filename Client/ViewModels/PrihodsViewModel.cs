using Client.Models.Sklad;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class PrihodsViewModel : BaseViewModel
    {
        private DateTime dateStart;
        public DateTime DateStart { get { return dateStart; } set { dateStart = value; Update(); } }
        private DateTime dateEnd;
        public DateTime DateEnd { get { return dateEnd; } set { dateEnd = value; Update(); } }

        private ObservableCollection<Sklad_prihod> _sklad_prihods;
        public ObservableCollection<Sklad_prihod> Sklad_prihods { get { return _sklad_prihods; } set { _sklad_prihods = value; OnPropertyChanged(nameof(Sklad_prihods)); } }

        private string search;
        public string Search { get { return search; } set { search = value; OnPropertyChanged(nameof(Search)); Update(); } }
        public PrihodsViewModel()
        {
            Route = "Sklad_prihods/";
            DateStart = DateTime.Now.AddDays(-7);
            DateEnd = DateTime.Now;
            Search = "";
            IsDataLoaded = true;
            Update();
        }
        public override async Task Delete(int id)
        {
            try
            {
                await HttpRequests<Sklad_prihod_prods>.DeleteRequest(Route + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }

        public override async Task Save()
        {
            foreach (Sklad_prihod prihod in Sklad_prihods)
            {
                try
                {
                    await HttpRequests<Sklad_prihod>.PutRequest(Route + prihod.ID, prihod);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Update();
        }

        public override async Task Update()
        {
            if (!IsDataLoaded) return;
            IsDataLoaded = false;
            RashodyQueryParams queryParams = new RashodyQueryParams(DateStart, DateEnd, Search);

            Sklad_prihods = new ObservableCollection<Sklad_prihod>(await HttpPostRequests<List<Sklad_prihod>, RashodyQueryParams>.PostRequest(Route + "Filter", new List<Sklad_prihod>(), queryParams));
            IsDataLoaded = true;
        }

        public async Task Add()
        {
            Sklad_prihod prihod = new Sklad_prihod();
            prihod = await HttpRequests<Sklad_prihod>.PostRequest(Route, prihod);
            Update();
        }
    }
}
