using Client.Models;
using Client.Models.Sklad;
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
            Global.client.BaseAddress = new Uri(Global.Api);
            Global.client.DefaultRequestHeaders.Accept.Clear();
            Global.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ConfigHelper.Instance.SetLang("ru");
            PagesCont = new ObservableCollection<PagesContainer>();
            InitializeComponent();
            Global.MainWin = this;
            this.DataContext = this;
            //ThemeManager.Current.ChangeTheme(this, "Light.Violet");
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
