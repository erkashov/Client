using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Sheta_tov : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        [Key]
        public int kod_zap { get; set; }
        public int kod_sheta { get; set; }
        public int kod_tovara { get; set; }
        private double kol { get; set; }
        public double Kol
        {
            get { return kol; }
            set { kol = value; OnPropertyChanged(nameof(Kol)); OnPropertyChanged(nameof(Summa)); OnPropertyChanged(nameof(Sheta.SummaAll)); }
        }
        private double? zena { get; set; }
        public double Zena
        {
            get { return zena != null ? zena.Value : 0; } set { zena = value; } 
        }
        public double Summa
        {
            get { return Kol * Zena; } set { if(Kol != 0) Zena = (Summa / Kol); OnPropertyChanged(nameof(Summa)); OnPropertyChanged(nameof(Zena)); OnPropertyChanged(nameof(Shet.SummaAll)); }
        }
        public virtual Shet Sheta { get; set; }
        private Tovary tovar;
        public Tovary Tovar { get { return tovar; } set { tovar = value; UpdateZena(); OnPropertyChanged(nameof(Tovar)); } }

        public Sheta_tov()
        {
            if (!zena.HasValue) zena = 0;
        }

        public async Task UpdateZena()
        {
            zena = await HttpRequests<double>.GetRequestAsync($"api/Zen_roznichnie/Zena?id={kod_tovara}&tipOplaty=2&count={kol}", new double());
        }
    }
}