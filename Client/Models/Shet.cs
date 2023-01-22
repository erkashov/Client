using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Client.Models
{
    public partial class Shet
    {
        public Shet()
        {
            this.Sheta_tov = new ObservableCollection<Shet_prods>();
        }
        [Key]
        public int ID { get; set; }
        public int nom_shet { get; set; }
        public System.DateTime date_sheta { get; set; }
        public int? contractorID { get; set; }
        public int userID { get; set; }
        public string prim { get; set; }
        public bool is_korr { get; set; }
        public bool oplachen { get; set; }
        public Nullable<System.DateTime> date_oplaty { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual User Polz { get; set; }

        public virtual ObservableCollection<Shet_prods> Sheta_tov { get; set; }
        public double SummaAll
        {
            get
            {
                if(Sheta_tov!= null) return Sheta_tov.Sum(p=>p.Count*p.Price);
                return 0;
            }
        }
    }
}
