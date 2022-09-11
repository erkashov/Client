using Client.Models;
using HandyControl.Tools;
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
        public List<Spr_period_filtr> periods = new List<Spr_period_filtr>();

        private DateTime? StartFilterDate = new DateTime(2022, 3, 9);
        private DateTime? EndFilterDate = new DateTime(2022, 3, 9);

        public PropertyGridDemoModel DemoModel = new PropertyGridDemoModel
        {
            Отгружено = Othruzheno.Все, Сдано = Sdano.Все, Товар_оплачен = TovarOpl.Все
        };

        public MainWindow()
        {
            ConfigHelper.Instance.SetLang("ru");
            InitializeComponent();
            Global.client.BaseAddress = new Uri(Global.Api);
            Global.client.DefaultRequestHeaders.Accept.Clear();
            Global.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            GetProduct();
            FillComboBox();
            //prop.SelectedObject = DemoModel;
        }

        public async void GetProduct()
        {
            HttpRequests<ObservableCollection<Sklad_rashod>> httpRequests = new HttpRequests<ObservableCollection<Sklad_rashod>>();
            List<string> parametrs = new List<string>();
            if (StartFilterDate.HasValue) parametrs.Add("start=" + StartFilterDate.Value.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
            if (EndFilterDate.HasValue) parametrs.Add("end=" + EndFilterDate.Value.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
            if (SearchTB.Text != "") parametrs.Add("search=" + SearchTB.Text);

            rashods = await httpRequests.GetRequest($"api/Sklad_rashod/Sklad_rashod_date?" + String.Join("&", parametrs), rashods);

            if (SearchTB.Text != "" && rashods.Count == 0)
            {
                if (PeriodsCB.SelectedIndex < PeriodsCB.Items.Count - 1)
                {
                    PeriodsRB.IsChecked = true;
                    if (PeriodsCB.SelectedIndex == -1) PeriodsCB.SelectedIndex = 0;
                    else PeriodsCB.SelectedIndex++;
                }
                else if (!AllRB.IsChecked.Value) AllRB.IsChecked = true;
            }

            datagridRashods.ItemsSource = rashods;
            IsDataLoaded = true;

        }
        public async void FillComboBox()
        {
            HttpRequests<List<Spr_period_filtr>> periodsHttp = new HttpRequests<List<Spr_period_filtr>>();
            periods = await periodsHttp.GetRequest($"api/Spr_period_filtr", periods);
            PeriodsCB.ItemsSource = periods;
        }

        public void Filter()
        {
            if (IsDataLoaded) GetProduct();
        }

        private void datagridRashods_MoseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagridRashods.SelectedItem != null)
            {
                Global.ShowNotif("Внимание", (datagridRashods.SelectedItem as Sklad_rashod).Nom_rash.ToString(), Notification.Wpf.NotificationType.Information);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            rashods.Add(new Sklad_rashod());
            rashods[0].Otpustil = "dcdcd";
        }

        private async void dateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MinusDayBN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlusDayBN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AllRB_Checked(object sender, RoutedEventArgs e)
        {
            StartFilterDate = null;
            EndFilterDate = null;
            Filter();
        }

        private void PeriodsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PeriodsCB.SelectedItem != null)
            {
                Spr_period_filtr per = (Spr_period_filtr)PeriodsCB.SelectedItem;
                if (per != null)
                {
                    if (per.day > 0)
                    {
                        StartFilterDate = DateTime.Now.Date.AddDays(-per.day);
                        EndFilterDate = DateTime.Now.Date;
                    }
                    else
                    {
                        switch (per.naim)
                        {
                            case "этот месяц":
                                {
                                    StartFilterDate = DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1);
                                    EndFilterDate = StartFilterDate.Value.AddMonths(1).AddDays(-1);
                                    break;
                                }
                            case "прошлый месяц":
                                {
                                    StartFilterDate = DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1).AddMonths(-1);
                                    EndFilterDate = StartFilterDate.Value.AddMonths(1).AddDays(-1);
                                    break;
                                }
                            case "этот год":
                                {
                                    StartFilterDate = DateTime.Now.Date.AddDays(-DateTime.Now.DayOfYear + 1);
                                    EndFilterDate = StartFilterDate.Value.AddYears(1).AddDays(-1);
                                    break;
                                }
                            case "прошлый год":
                                {
                                    StartFilterDate = DateTime.Now.Date.AddDays(-DateTime.Now.DayOfYear + 1).AddYears(-1);
                                    EndFilterDate = StartFilterDate.Value.AddYears(1).AddDays(-1);
                                    break;
                                }
                            default:
                                {
                                    Global.ShowNotif("Внимание", "Нет фильтра для " + per.naim, Notification.Wpf.NotificationType.Warning);
                                    break;
                                }
                        }
                    }
                    Filter();
                }
            }
        }

        private void DayDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StartFilterDate = DayDP.SelectedDate;
            EndFilterDate = DayDP.SelectedDate;
            Filter();
        }

        private void DayStartDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StartFilterDate = DayStartDP.SelectedDate;
            Filter();
        }

        private void DayEndDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EndFilterDate = DayEndDP.SelectedDate;
            Filter();
        }

        private void MonthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private async void SaveBN_Click(object sender, RoutedEventArgs e)
        {
            HttpRequests<Sklad_rashod> httpRequests = new HttpRequests<Sklad_rashod>();
            foreach (Sklad_rashod rashod in rashods)
            {
                try
                {
                    await httpRequests.PutRequest("api/Sklad_rashod/" + Convert.ToInt32(rashod.kod_zap), rashod);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }

        }

        private async void AddBN_Click(object sender, RoutedEventArgs e)
        {
            HttpRequests<Sklad_rashod> httpRequests = new HttpRequests<Sklad_rashod>();
            Sklad_rashod rashod = new Sklad_rashod();
            rashod.Otpustil = "Test";
            rashod.Nom_rash = 999;
            rashod = await httpRequests.PostRequest("api/Sklad_rashod/", rashod);
        }
    }

    public class PropertyGridDemoModel
    {
        private Othruzheno otgr;
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
        }
    }
    public enum Othruzheno
    {
        Все,
        Да,
        Нет
    }

    public enum TovarOpl
    {
        Все,
        Да,
        Нет
    }

    public enum Sdano
    {
        Все,
        Да,
        Нет
    }
}
