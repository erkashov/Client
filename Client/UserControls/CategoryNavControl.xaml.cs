using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CategoryNavControl.xaml
    /// </summary>
    public partial class CategoryNavControl : UserControl
    {
        public ObservableCollection<Category> tabs { get; set; }
        public event RoutedEventHandler UpdateClick;
        private int _selected;
        public int Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        public CategoryNavControl()
        {
            InitializeComponent();
            tabs = new ObservableCollection<Category>(Enums.Spr_category.Where(p=>p.parent_kod_zap is null)/*.Where(p=>p.kod_zap > 52 && p.kod_zap < 63)*/);
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender as Button != null)
            {
                if((sender as Button).DataContext != null)
                {
                    Selected = ((sender as Button).DataContext as Category).kod_zap;
                    UpdateClick(sender, e);
                }
            }
        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender as TextBlock != null)
            {
                if ((sender as TextBlock).DataContext != null)
                {
                    Selected = ((sender as TextBlock).DataContext as Category).kod_zap;
                    UpdateClick(sender, e);
                }
            }
        }
    }
}
