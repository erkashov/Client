using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Sklad
{
    public class Karta
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
    }
}
