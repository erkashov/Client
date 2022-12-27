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
    public class Sklad_rashod_tov : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        [Key]
        private decimal kod_zap ;
        private Nullable<int> kod_rashoda;
        private Nullable<int> tov;
        private Nullable<decimal> count;
        private Nullable<decimal> count_pachek;
        private Nullable<decimal> nom_prih;
        private Nullable<decimal> zena;
        private Nullable<decimal> pribyl;
        private Nullable<decimal> weight;
        private Nullable<decimal> volume;
        public decimal Kod_zap
        {
            get { return kod_zap; }
            set { kod_zap = value; OnPropertyChanged(nameof(Kod_zap)); }
        }
        public int Kod_rashoda
        {
            get { return (int)kod_rashoda; }
            set { kod_rashoda = value; OnPropertyChanged(nameof(Kod_rashoda)); }
        }
        public Nullable<int> Tov
        {
            get { return tov; }
            set { tov = value; OnPropertyChanged(nameof(Tov)); }
        }
        public Nullable<decimal> Count
        {
            get { return count is null ? 0 : (decimal)count; }
            set { count = value; OnPropertyChanged(nameof(Count)); OnPropertyChanged(nameof(Summa));}
        }
        public Nullable<decimal> Count_pachek
        {
            get { return count_pachek is null ? 0 : (decimal)count_pachek; }
            set { count_pachek = value; OnPropertyChanged(nameof(Count_pachek)); }
        }
        public Nullable<decimal> Nom_prih
        {
            get { return nom_prih; }
            set { nom_prih = value; OnPropertyChanged(nameof(Nom_prih)); }
        }
        public Nullable<decimal> Zena
        {
            get { return zena is null ? 0 : (decimal)zena; }
            set { zena = value; OnPropertyChanged(nameof(Zena)); OnPropertyChanged(nameof(Summa)); }
        }
        public Nullable<decimal> Pribyl
        {
            get { return pribyl is null ? 0 : (decimal)pribyl; }
            set { pribyl = value; OnPropertyChanged(nameof(Pribyl)); }
        }
        public Nullable<decimal> Weight
        {
            get { return weight is null ? 0 : (decimal)weight; }
            set { weight = value; OnPropertyChanged(nameof(Weight)); }
        }
        public Nullable<decimal> Volume
        {
            get { return volume is null ? 0 : (decimal)volume; }
            set { volume = value; OnPropertyChanged(nameof(Volume)); }
        }
        public async Task UpdateZena()
        {
            Zena = await HttpRequests<decimal>.GetRequestAsync($"api/Zen_roznichnie/Zena?id={tov}&tipOplaty=2&count={(int)Count}", (decimal)Zena);
        }
        public decimal Summa
        {
            get
            {
                return (decimal)(Count * Zena);
            }
        }

        [ForeignKey("kod_rashoda")]
        public virtual Sklad_rashod Sklad_rashod { get; set; }
        private Tovary _tovar;
        [ForeignKey("tov")]
        public Tovary Tovar { get { return _tovar; } set { _tovar = value; OnPropertyChanged(nameof(Tovar)); } }
    }
}
