using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Sklad
{
    public class Sklad_prihod_prods : INotifyPropertyChanged
    {
        [Key]
        private int id;
        public int ID { get { return id; } set { id = value; OnPropertyChanged(nameof(id)); } }

        private int prihodID;
        public int PrihodID { get { return prihodID; } set { prihodID = value; OnPropertyChanged(nameof(PrihodID)); } }

        private int productID;
        public int ProductID { get { return productID; } set { productID = value; OnPropertyChanged(nameof(ProductID)); } }

        private int count;
        public int Count { get { return count; } 
            set { count = value; OnPropertyChanged(nameof(Count)); OnPropertyChanged(nameof(Cost_with_dost)); OnPropertyChanged(nameof(Cost)); if (Sklad_prihod != null) Sklad_prihod.UpdateZenaDost(); } }

        private double price;
        public double Price { get { return price; } set { price = value; OnPropertyChanged(nameof(Price)); OnPropertyChanged(nameof(Cost)); if (Sklad_prihod != null) Sklad_prihod.UpdateZenaDost(); } }

        private double price_with_deliv;
        public double Price_with_deliv { get { return price_with_deliv; } set { price_with_deliv = value; OnPropertyChanged(nameof(Price_with_deliv)); OnPropertyChanged(nameof(Cost_with_dost)); } }
        public double Cost_with_dost { get { return Price_with_deliv * count; } set { if (count != 0) Price_with_deliv = value / count; else Price_with_deliv = 0; } }
        public double Cost { get { return Price * count; } set { if (count != 0) Price = value / count; else Price = 0; } }

        [ForeignKey("productID")]
        public Sklad_prihod Sklad_prihod { get; set; }
        [ForeignKey("prihodID")]
        public Product Tovar { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
