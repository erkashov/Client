using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public partial class Price : BaseModel
    {
        public int productID { get; set; }
        public double price_nal { get; set; }
        public double price_beznal { get; set; }
        public virtual Product Tovar { get; set; }
    }
}
