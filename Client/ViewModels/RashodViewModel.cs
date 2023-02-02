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

namespace Client.ViewModels
{
    public class RashodViewModel : BaseViewModel
    {
        public ObservableCollection<Type_oplaty> Spr_Oplat_Sklad { get { return Enums.Spr_Oplat_Sklad; } set { OnPropertyChanged(nameof(Spr_Oplat_Sklad)); } }
        public ObservableCollection<User> Users { get { return Enums.Employes; } set { OnPropertyChanged(nameof(Users)); } }

        private Sklad_rashod _rashod;
        public Sklad_rashod Rashod { get { return _rashod; } set { _rashod = value; OnPropertyChanged(nameof(Rashod)); } }
        private int ID;
        public RashodViewModel(int iD)
        {
            Route = "Sklad_rashod/";
            ID = iD;
            Update();
        }

        public void AddTovar()
        {
            if (Rashod.Sklad_rashod_tov == null) Rashod.Sklad_rashod_tov = new ObservableCollection<Sklad_rashod_prods>();
            Rashod.Sklad_rashod_tov.Add(new Sklad_rashod_prods() { RashodID = Rashod.ID });
        }

        public override async Task Update()
        {
            Rashod = await HttpRequests<Sklad_rashod>.GetRequestAsync(Route + ID, Rashod);
            if (Rashod.Sklad_rashod_tov != null)
            {
                foreach (Sklad_rashod_prods prod in Rashod.Sklad_rashod_tov)
                {
                    prod.Sklad_rashod = Rashod;
                }
            }
        }

        public override async Task Save()
        {
            IsDataLoaded = false;
            if (Rashod.Sklad_rashod_tov != null)
            {
                foreach (Sklad_rashod_prods prod in Rashod.Sklad_rashod_tov)
                {
                    prod.Sklad_rashod = null;
                }
            }
            try
            {
                await HttpRequests<Sklad_rashod>.PutRequest(Route + Rashod.ID, Rashod);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
            IsDataLoaded = true;
        }

        public override async Task Delete(int id)
        {
            try
            {
                await HttpRequests<Sklad_rashod_prods>.DeleteRequest(Route + "Tov?id=" + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }
    }
}
