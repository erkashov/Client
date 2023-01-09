using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class Spr_oplat_sklad
    {
        [Key]
        public int kod_zap { get; set; }
        public string naim { get; set; }
    }
}
