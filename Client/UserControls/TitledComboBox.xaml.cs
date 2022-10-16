using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для TitledComboBox.xaml
    /// </summary>
    public partial class TitledComboBox : UserControl
    {
        public SelectionChangedEventHandler selectionChanged;
        public int WidthCB { get; set; }
        public string Title { get; set; }
        public string DisplayMemberPath { get; set; }
        public object ItemsSource { get; set; }
        public string SelectedValuePath { get; set; }
        public object SelectedValue { get; set; }


        public TitledComboBox()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(selectionChanged != null) selectionChanged(sender, e);
        }
    }
}
