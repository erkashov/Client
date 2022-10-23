using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Category
    {
        [Key]
        public int kod_zap { get; set; }
        public string name { get; set; }
        public Nullable<int> parent_kod_zap { get; set; }

        [ForeignKey("parent_kod_zap")]
        public Category Parent { get; set; }
    }
}
