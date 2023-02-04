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
        private Product _tovar;
        public virtual Product Tovar { get { return _tovar; } set { _tovar = value; OnPropertyChanged(nameof(Tovar)); } }
    }
}
