using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Shet_prods : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        [Key]
        public int ID { get; set; }
        public int shetID { get; set; }
        public int productID { get; set; }
        private double count { get; set; }
        public double Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(nameof(Count)); OnPropertyChanged(nameof(Summa)); OnPropertyChanged(nameof(Sheta.SummaAll)); }
        }
        private double? price { get; set; }
        public double Price
        {
            get { return price != null ? price.Value : 0; } set { price = value; } 
        }
        public double Summa
        {
            get { return Count * Price; } set { if(Count != 0) Price = (Summa / Count); OnPropertyChanged(nameof(Summa)); OnPropertyChanged(nameof(Price)); OnPropertyChanged(nameof(Shet.SummaAll)); }
        }
        public virtual Shet Sheta { get; set; }
        private Product tovar;
        public Product Tovar { get { return tovar; } set { tovar = value; /*UpdateZena();*/ OnPropertyChanged(nameof(Tovar)); } }

        public Shet_prods()
        {
            if (!price.HasValue) price = 0;
        }

        public async Task UpdateZena()
        {
            price = await HttpRequests<double>.GetRequestAsync($"api/Zen_roznichnie/Zena?id={productID}&tipOplaty=2&count={count}", new double());
        }
    }
}