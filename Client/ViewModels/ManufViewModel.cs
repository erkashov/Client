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
    public class ManufViewModel : BaseViewModel
    {
        private ObservableCollection<Manufacture> _manufList;
        public ObservableCollection<Manufacture>  ManufList { get { return _manufList; } set { _manufList = value; OnPropertyChanged(nameof(ManufList)); } }

        public ManufViewModel()
        {
            Route = "Manufactures/";
            Update();
        }

        public async override Task Update()
        {
            ManufList = await HttpRequests<ObservableCollection<Manufacture>>.GetRequestAsync(Route, ManufList);
        }

        public async override Task Save()
        {
            foreach (Manufacture man in ManufList)
            {
                try
                {
                    await HttpRequests<Manufacture>.PutRequest(Route + man.ID, man);
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
            ManufList.Add(new Manufacture());
        }

        public async override Task Delete(int id)
        {
            try
            {
                await HttpRequests<Manufacture>.DeleteRequest(Route + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();

        }
    }
}
