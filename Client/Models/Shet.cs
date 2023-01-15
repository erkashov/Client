using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Client.Models
{
    public partial class Shet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shet()
        {
            this.Sheta_tov = new ObservableCollection<Sheta_tov>();
        }
        [Key]
        public int kod_zap { get; set; }
        public int nom_shet { get; set; }
        public System.DateTime date_sheta { get; set; }
        public int pok { get; set; }
        public Nullable<double> summa { get; set; }
        public int id_polz { get; set; }
        public string prim { get; set; }
        public bool korr { get; set; }
        public bool oplachen { get; set; }
        public bool sklad { get; set; }
        public bool prim_is_plat { get; set; }
        public Nullable<System.DateTime> date_oplaty { get; set; }
        public virtual ObservableCollection<Sheta_tov> Sheta_tov { get; set; }
        public double SummaAll
        {
            get
            {
                if(Sheta_tov!= null) return Sheta_tov.Sum(p=>p.kol*p.Zena);
                return 0;
            }
        }
    }
}
