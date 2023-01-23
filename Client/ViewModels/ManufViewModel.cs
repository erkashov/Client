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
    public class ManufViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Manufacture> _manufList;
        public ObservableCollection<Manufacture>  ManufList { get { return _manufList; } set { _manufList = value; OnPropertyChanged(nameof(ManufList)); } }

        public ManufViewModel()
        {
            Filter();
        }

        public async Task Filter()
        {
            ManufList = await HttpRequests<ObservableCollection<Manufacture>>.GetRequestAsync("api/Manufactures", ManufList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public async Task Save()
        {
            foreach (Manufacture man in ManufList)
            {
                try
                {
                    await HttpRequests<Manufacture>.PutRequest("api/Manufactures/" + man.ID, man);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Filter();
        }

        public void Add()
        {
            ManufList.Add(new Manufacture());
        }

        public async Task Delete(int id)
        {
            try
            {
                await HttpRequests<Manufacture>.DeleteRequest("api/Manufactures/" + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Filter();

        }
    }
}
