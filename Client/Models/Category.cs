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
        public int ID { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public Nullable<int> parentID { get; set; }

        [ForeignKey("parentID")]
        public Category Parent { get; set; }

        public List<Category> Children
        {
            get
            {
                return Enums.Spr_category.Where(p=>p.parentID ==  ID).ToList();
            }
        }
    }
}
