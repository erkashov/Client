using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Contractor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public int ID { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string OGRN { get; set; }
        public string ur_adres { get; set; }
        public string rukovod { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string rashet_shet { get; set; }
        public string korr_shet { get; set; }
        public string BIK { get; set; }

    }
}
