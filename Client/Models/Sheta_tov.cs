using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class Sheta_tov
    {
        [Key]
        public int kod_zap { get; set; }
        public int kod_sheta { get; set; }
        public int kod_tovara { get; set; }
        public double kol { get; set; }
        public double? zena { get; set; }
        public double? summa { get; set; }
        public double? summa_nds { get; set; }
        public double? vsego { get; set; }
        public virtual Shet Sheta { get; set; }
        public virtual Tovary Tovar { get; set; }
    }
}