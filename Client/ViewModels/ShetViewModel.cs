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
    public class ShetViewModel : BaseViewModel
    {
        private int kod_zap;
        private Shet _shet; 
        public Shet Shet { get { return _shet; } set { _shet = value; OnPropertyChanged(nameof(Shet)); } }
        public ObservableCollection<User> Users { get { return Enums.Employes; } set { OnPropertyChanged(nameof(Users)); } }
        public ObservableCollection<Contractor> Contractors { get { return Enums.Contractors; } set { OnPropertyChanged(nameof(Contractors)); } }

        public ShetViewModel(int kod_zap)
        {
            this.kod_zap = kod_zap;
            Route = "Shets/";
            Update();
        }
        public override async Task Update()
        {
            IsDataLoaded = false;
            Shet = await HttpRequests<Shet>.GetRequestAsync(Route + kod_zap, Shet);
            IsDataLoaded = true;
        }

        public async Task UpdateZena(int productID)
        {
            if (IsDataLoaded)
            {
                Shet_prods prod = Shet.Sheta_tov.Where(s => s.productID == productID).FirstOrDefault();
                if(prod != null)
                {
                    prod.Price = await HttpRequests<double>.GetRequestAsync($"Zen_roznichnie/Zena?id={prod.productID}&tipOplaty=2&count={prod.Count}", prod.Price);
                }
            }
        }
        public void AddTovar()
        {
            if (Shet.Sheta_tov == null) Shet.Sheta_tov = new ObservableCollection<Shet_prods>();
            Shet.Sheta_tov.Add(new Shet_prods() { shetID = Shet.ID });
        }
        public override async Task Save()
        {
            IsDataLoaded = false;
            foreach (Shet_prods tov in Shet.Sheta_tov) tov.Sheta = null;
            try
            {
                await HttpRequests<Shet>.PutRequest(Route + Shet.ID, Shet);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
            IsDataLoaded = true;
        }

        public override async Task Delete(int kod_zap)
        {
            try
            {
                await HttpRequests<Shet_prods>.DeleteRequest(Route + "Tov?id=" + kod_zap);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }


    }
}
