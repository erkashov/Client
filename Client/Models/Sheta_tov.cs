using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

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
        public double kol { get; set; }
        public double Kol
        {
            get { return kol; }
            set { kol = value; OnPropertyChanged(nameof(Kol)); }
        }
        public double? zena { get; set; }
        public double Zena
        {
            get { return zena != null ? zena.Value : 0; } set { zena = value; } 
        }
        public double? summa { get; set; }
        public double Summa
        {
            get { return kol * Zena; } set { if(kol != 0) Zena = (Summa / kol); OnPropertyChanged(nameof(Summa)); OnPropertyChanged(nameof(Zena)); }
        }
        public double? summa_nds { get; set; }
        public double? vsego { get; set; }
        public virtual Shet Sheta { get; set; }
        public virtual Tovary Tovar { get; set; }
    }
}