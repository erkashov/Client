using Microsoft.Xaml.Behaviors.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Sheta_tov : INotifyPropertyChanged
    {
        [Key]
        public int kod_zap { get; set; }
        public int kod_sheta { get; set; }
        public int kod_tovara { get; set; }
        public double kol { get; set; }

        private double? zena;
        public double Zena { get { return zena.HasValue ? zena.Value : 0; } set { zena = value; OnPropertyChanged(nameof(Zena)); Summa = Zena * kol; } }

        private double? summa;
        public double Summa { get { return summa.HasValue ? summa.Value : 0; } set { summa = value; OnPropertyChanged(nameof(Summa)); } }

        public double? summa_nds { get; set; }
        public double? vsego { get; set; }
        public virtual Shet Sheta { get; set; }
        private Tovary tovar;
        public Tovary Tovar { get { return tovar; } set { tovar = value; UpdateZena(); OnPropertyChanged(nameof(Tovar)); } }

        public Sheta_tov()
        {
            if (!zena.HasValue) zena = 0;
            if (!summa.HasValue) summa = 0;
            if (!summa_nds.HasValue) summa_nds = 0;
            if (!vsego.HasValue) vsego = 0;
        }

        public async Task UpdateZena()
        {
            zena = await HttpRequests<double>.GetRequestAsync($"api/Zen_roznichnie/Zena?id={kod_tovara}&tipOplaty=2&count={kol}", new double());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}