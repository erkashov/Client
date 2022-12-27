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
        public string short_name { get; set; }
        public string color { get; set; }
        public Nullable<int> parent_kod_zap { get; set; }

        [ForeignKey("parent_kod_zap")]
        public Category Parent { get; set; }

        public List<Category> Children
        {
            get
            {
                return Enums.Spr_category.Where(p=>p.parent_kod_zap ==  kod_zap).ToList();
            }
        }
    }
}
