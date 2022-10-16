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
    /// Логика взаимодействия для ToolBarControl.xaml
    /// </summary>
    public partial class ToolBarControl : UserControl
    {
        public event RoutedEventHandler SaveClick;
        public event RoutedEventHandler AddClick;
        public event RoutedEventHandler DeleteClick;
        public event RoutedEventHandler UpdateClick;
        public ToolBarControl()
        {
            InitializeComponent();
        }

        public void AddBN_Click(object sender, RoutedEventArgs e)
        {
            if (AddClick != null) AddClick(sender, e);
        }

        private void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            if (SaveClick != null) SaveClick(sender, e);
        }

        private void DeleteBN_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteClick != null) DeleteClick(sender, e);
        }

        private void UpdateBN_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateClick != null) UpdateClick(sender, e);

        }
    }
}
