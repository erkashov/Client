using Client.Models;
using Client.Models.Sklad;
using Notifications.Wpf.ViewModels.Base;
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
    public class ShetViewModel : INotifyPropertyChanged
    {
        private int kod_zap;
        private Shet _shet; 
        bool IsDataLoaded = false;
        public Shet Shet { get { return _shet; } set { _shet = value; OnPropertyChanged(nameof(Shet)); } }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ShetViewModel(int kod_zap)
        {
            this.kod_zap = kod_zap;
            
            Update();
        }
        public async Task Update()
        {
            IsDataLoaded = false;
            Shet = await HttpRequests<Shet>.GetRequestAsync("api/Sheta/" + kod_zap, Shet);
            IsDataLoaded = true;
        }
        public void AddTovar()
        {
            if (Shet.Sheta_tov == null) Shet.Sheta_tov = new ObservableCollection<Sheta_tov>();
            Shet.Sheta_tov.Add(new Sheta_tov() { kod_sheta = Shet.kod_zap });
        }
        public async Task Save()
        {
            IsDataLoaded = false;
            foreach (Sheta_tov tov in Shet.Sheta_tov) tov.Sheta = null;
            try
            {
                await HttpRequests<Shet>.PutRequest("api/Sheta/" + Shet.kod_zap, Shet);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
            IsDataLoaded = true;
        }

        public async Task DeleteTovar(int kod_zap)
        {
            try
            {
                await HttpRequests<Sheta_tov>.DeleteRequest("api/Sheta/Tov?id=" + kod_zap);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }
    }
}
