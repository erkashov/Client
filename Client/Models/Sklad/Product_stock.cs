using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.Sklad
{
    public class Product_stock
    {
        public int productID { get; set; }
        public int coming_stock { get; set; }
        public int total_stock { get; set; }
        public int rezerved_stock { get; set; }
        public virtual Product Tovar { get; set; }
    }
}
