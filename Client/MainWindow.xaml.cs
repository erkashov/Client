using Client.Models;
using Client.Models.Sklad;
using Client.Pages;
using Client.Pages.Sklad;
using Client.ViewModels;
using Client.Windows;
using ControlzEx.Theming;
using HandyControl.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
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

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<PagesContainer> _p;
        public ObservableCollection<PagesContainer> PagesCont { get { return _p; } set { _p = value; OnPropertyChanged(nameof(PagesCont)); } }

        public int _selectedIndex;
        public int SelectedIndex { get { return _selectedIndex; } set { _selectedIndex = value; OnPropertyChanged(nameof(SelectedIndex)); } }

        public MainWindow()
        {
            PagesCont = new ObservableCollection<PagesContainer>();
            InitializeComponent();
            Global.MainWin = this;
            this.DataContext = this;
        }

        private void rashodsM_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new PagesContainer("Список расходов", new RashodsPage()));
        }

        private void ostatkiM_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new PagesContainer("Остатки", new OstatkiPage()));
        }

        public void ShowPage(PagesContainer p)
        {
            if (p == null) return;
            PagesContainer page = PagesCont.Where(q => q.Title == p.Title).FirstOrDefault();
            if (page == null)
            {
                PagesCont.Add(p);
                page = p;
            }
            SelectedIndex = PagesCont.IndexOf(page);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void prihodsM_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new PagesContainer("Список приходов", new PrihodsPage()));
        }

        private void toavryM_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new PagesContainer("Товары", new TovaryPage()));
        }

        private void shetaM_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new PagesContainer("Счета", new ShetaPage()));
        }
    }

    public class PagesContainer
    {
        public PagesContainer(string title, ContentControl page)
        {
            Title = title;
            Page = page;
        }
        public ContentControl Page { get; set; }
        public string Title { get; set; }
    }

    public class PropertyGridDemoModel
    {
        /*private Othruzheno otgr;
        [Category("Category1")]
        public Othruzheno Отгружено
        {
            get { return otgr; }
            set { otgr = value; }
        }

        private TovarOpl tovOpl;
        [Category("Category1")]
        public TovarOpl Товар_оплачен
        {
            get { return tovOpl; }
            set { tovOpl = value; }
        }

        private Sdano sdano;
        [Category("Category1")]
        public Sdano Сдано
        {
            get { return sdano; }
            set { sdano = value; }
        }*/
    }
    
}
