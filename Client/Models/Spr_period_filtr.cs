using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Spr_period_filtr
    {
        [Key]
        public decimal kod_zap { get; set; }
        public string naim { get; set; }
        public int day { get; set; }
    }
}
