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
    public class Sklad_prihod_tov : INotifyPropertyChanged
    {
        [Key]
        private int kod_zap;
        public int Kod_zap { get { return kod_zap; } set { kod_zap = value; OnPropertyChanged(nameof(kod_zap)); } }

        private int kod_prihoda;
        public int Kod_prihoda { get { return kod_prihoda; } set { kod_prihoda = value; OnPropertyChanged(nameof(Kod_prihoda)); } }

        private int kod_tovara;
        public int Kod_tovara { get { return kod_tovara; } set { kod_tovara = value; OnPropertyChanged(nameof(Kod_tovara)); } }

        private int count;
        public int Count { get { return count; } 
            set { count = value; OnPropertyChanged(nameof(Count)); OnPropertyChanged(nameof(Cost_with_dost)); OnPropertyChanged(nameof(Cost)); if (Sklad_prihod != null) Sklad_prihod.UpdateZenaDost(); } }

        private double zena;
        public double Zena { get { return zena; } set { zena = value; OnPropertyChanged(nameof(Zena)); OnPropertyChanged(nameof(Cost)); if (Sklad_prihod != null) Sklad_prihod.UpdateZenaDost(); } }

        private double zena_with_dost;
        public double Zena_with_dost { get { return zena_with_dost; } set { zena_with_dost = value; OnPropertyChanged(nameof(Zena_with_dost)); OnPropertyChanged(nameof(Cost_with_dost)); } }
        public double Cost_with_dost { get { return Zena_with_dost * count; } set { if (count != 0) Zena_with_dost = value / count; else Zena_with_dost = 0; } }
        public double Cost { get { return Zena * count; } set { if (count != 0) Zena = value / count; else Zena = 0; } }

        [ForeignKey("kod_prihoda")]
        public Sklad_prihod Sklad_prihod { get; set; }
        [ForeignKey("kod_tovara")]
        public Tovary Tovar { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
