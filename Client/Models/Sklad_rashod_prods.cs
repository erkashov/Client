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
            set { count = value; OnPropertyChanged(nameof(Count)); OnPropertyChanged(nameof(Summa));}
        }
        /*public Nullable<decimal> Count_pachek
        {
            get { return count_pachek is null ? 0 : (decimal)count_pachek; }
            set { count_pachek = value; OnPropertyChanged(nameof(Count_pachek)); }
        }*/
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
            get { return Tovar != null ? Tovar.dlina * Tovar.shir * Tovar.tol * Count : 0; }
        }
        public async Task UpdateZena()
        {
            Price = await HttpRequests<double>.GetRequestAsync($"api/Zen_roznichnie/Zena?id={ProductID}&tipOplaty=2&count={(int)Count}", Price);
        }
        public double Summa
        {
            get
            {
                return Count * Price;
            }
        }

        public virtual Sklad_rashod Sklad_rashod { get; set; }

        private Product _tovar;
        public Product Tovar { get { return _tovar; } set { _tovar = value; OnPropertyChanged(nameof(Tovar)); } }
    }
}
