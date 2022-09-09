using Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public partial class MainWindow : Window
    {
        bool IsDataLoaded = false;
        public ObservableCollection<Sklad_rashod> rashods = new ObservableCollection<Sklad_rashod>();
        private DateTime? _startTimeFilter = new DateTime(2022, 3, 9);
        private DateTime? _endTimeFilter = new DateTime(2022, 3, 9);
        public DateTime? StartFilterDate
        {
            get
            {
                return _startTimeFilter;
            }
            set
            {
                _startTimeFilter = value;
                Filter();
            }
        }
        public DateTime? EndFilterDate
        {
            get
            {
                return _endTimeFilter;
            }
            set
            {
                _endTimeFilter = value;
                Filter();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Global.client.BaseAddress = new Uri(Global.Api);
            Global.client.DefaultRequestHeaders.Accept.Clear();
            Global.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            GetProduct();
        }

        public async void GetProduct()
        {
            HttpRequests<ObservableCollection<Sklad_rashod>> httpRequests = new HttpRequests<ObservableCollection<Sklad_rashod>>();
            rashods = await httpRequests.GetRequest($"api/Sklad_rashod/Sklad_rashod_date?" +
                (StartFilterDate.HasValue ? ("start=" + StartFilterDate.Value.ToString("s", System.Globalization.CultureInfo.InvariantCulture)) + "&" : "") +
                (EndFilterDate.HasValue ? ("end=" + EndFilterDate.Value.ToString("s", System.Globalization.CultureInfo.InvariantCulture)) : ""), rashods);
                /*$"&end={new DateTime(2022, 03, 10, 0, 0, 0).ToString("s", System.Globalization.CultureInfo.InvariantCulture)}" ;*/
            //datagridRashods.Items.Clear();
            datagridRashods.ItemsSource = rashods;
        }

        public void Filter()
        {
            if(IsDataLoaded) GetProduct();
        }

        private void datagridRashods_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void datagridRashods_MoseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datagridRashods.SelectedItem != null)
            {
                Global.ShowNotif("Внимание", (datagridRashods.SelectedItem as Sklad_rashod).nom_rash.ToString(), Notification.Wpf.NotificationType.Information);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            rashods.Add(new Sklad_rashod());
            rashods[0].Otpustil = "dcdcd";
        }

        private async void dateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            HttpRequests<ObservableCollection<Sklad_rashod>> httpRequests = new HttpRequests<ObservableCollection<Sklad_rashod>>();
            rashods = await httpRequests.GetRequest($"api/Sklad_rashod/Sklad_rashod_date?" +
                (StartFilterDate.HasValue ? $"start={dateStart.SelectedDate.Value.ToString("s", System.Globalization.CultureInfo.InvariantCulture)}&" : "") +
                (EndFilterDate.HasValue ? $"end={dateEnd.SelectedDate.Value.ToString("s", System.Globalization.CultureInfo.InvariantCulture)}":""), rashods);
        }

        private void MinusDayBN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlusDayBN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AllRB_Checked(object sender, RoutedEventArgs e)
        {
            IsDataLoaded = false;
            StartFilterDate = null;
            IsDataLoaded = true;
            EndFilterDate = null;
        }

        private void PeriodsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DayDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            IsDataLoaded = false;
            StartFilterDate = DayDP.SelectedDate;
            IsDataLoaded = true;
            EndFilterDate = DayDP.SelectedDate;
        }

        private void DayStartDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StartFilterDate = DayStartDP.SelectedDate;
        }

        private void DayEndDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EndFilterDate = DayEndDP.SelectedDate;
        }

        private void MonthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class PropertyGridDemoModel
    {
        [Category("Периоды")]
        public string String { get; set; }
        [Category("Category2")]
        public int Integer { get; set; }
        [Category("Category2")]
        public bool Boolean { get; set; }
        [Category("Category1")]
        public Gender Enum { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public ImageSource ImageSource { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
