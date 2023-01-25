using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Client.Models
{
    public class Sklad_rashod_prods : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private int id ;
        private int rashodID;
        private int productID;
        private int count;
        private double price;
        public int ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(ID)); }
        }
        public int RashodID
        {
            get { return (int)rashodID; }
            set { rashodID = value; OnPropertyChanged(nameof(RashodID)); }
        }
        public int ProductID
        {
            get { return productID; }
            set { productID = value; OnPropertyChanged(nameof(ProductID)); }
        }
        public int Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(nameof(Count)); OnPropertyChanged(nameof(Summa)); OnPropertyChanged(nameof(Sklad_rashod.Weight)); OnPropertyChanged(nameof(Sklad_rashod.Volume)); OnPropertyChanged(nameof(Sklad_rashod.Pachek)); OnPropertyChanged(nameof(Sklad_rashod)); }
        }

        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(nameof(Price)); OnPropertyChanged(nameof(Summa)); }
        }
        public double Weight
        {
            get { return Tovar != null ? Tovar.ves_lista * Count : 0; }
        }
        public double Volume
        {
            get { return Tovar != null ? Tovar.dlina /1000 * Tovar.shir / 1000 * Tovar.tol / 1000 * Count : 0; }
        }
        public double Pachek
        {
            get { return Tovar != null && Tovar.kol_list_v_pachke != 0 ? (double)Count / Tovar.kol_list_v_pachke : 0; }
        }
        public async Task UpdateZena()
        {
            Price = await HttpRequests<double>.GetRequestAsync($"Zen_roznichnie/Zena?id={ProductID}&tipOplaty={Sklad_rashod.Type_oplatyID}&count={Count}", Price);
        }
        public async Task CheckStock()
        {
            int stock = await HttpRequests<int>.GetRequestAsync($"Sklad_tov_OSTATKI/{ProductID}", new int());
            if (stock < Count)
            {
                Count = stock;
                Global.ShowWarning("Такого кол-ва нет в наличии");
            }
        }
        public double Summa
        {
            get
            {
                return Count * Price;
            }
        }
        private Sklad_rashod _sklad_rashod;
        public Sklad_rashod Sklad_rashod { get { return _sklad_rashod; } set { _sklad_rashod = value; OnPropertyChanged(nameof(Sklad_rashod)); } }

        private Product _tovar;
        public Product Tovar { get { return _tovar; } set { _tovar = value; OnPropertyChanged(nameof(Tovar)); } }
    }
}
