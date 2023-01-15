using Client.Models;
using Client.Models.Sklad;
using Notifications.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Client.ViewModels
{
    public class ShetViewModel : INotifyPropertyChanged
    {
        private int kod_zap;
        private Shet _shet;
        public Shet Shet { get { return _shet; } set { _shet = value; OnPropertyChanged(nameof(Shet)); } }
        public ObservableCollection<User> Users { get { return Enums.Users; } set { OnPropertyChanged(nameof(Users)); } }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ShetViewModel(int kod_zap)
        {
            this.kod_zap = kod_zap;
            Update();
        }
        public async Task Update()
        {
            Shet = await HttpRequests<Shet>.GetRequestAsync("api/Sheta/" + kod_zap, Shet);
        }
    }
}
