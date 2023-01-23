using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Manufacture : BaseModel
    {
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }
    }
}
