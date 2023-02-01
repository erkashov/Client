using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class User : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string papaname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string FIO
        {
            get { return (surname + " " + name + " " + papaname).Trim(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
