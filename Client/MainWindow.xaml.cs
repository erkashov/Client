using Client.Models;
using Client.Models.Sklad;
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
        private List<Sklad_rashod> rashods = new List<Sklad_rashod>();
        public RashodyViewModel RashodyViewModel { get; set; }

        private DateTime? StartFilterDate = new DateTime(2022, 3, 9);
        private DateTime? EndFilterDate = new DateTime(2022, 3, 9);

        public MainWindow()
        {
            ConfigHelper.Instance.SetLang("ru");
            InitializeComponent();
            Global.client.BaseAddress = new Uri(Global.Api);
            Global.client.DefaultRequestHeaders.Accept.Clear();
            Global.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DayDP.SelectedDate = DateTime.Now;
            //получаем ViewModel
            GetVM();
            ThemeManager.Current.ChangeTheme(this, "Light.Violet");
        }

        public async void GetVM()
        {
            IsDataLoaded = false;
            HttpRequests<RashodyViewModel> httpRequests4 = new HttpRequests<RashodyViewModel>();
            RashodyViewModel = new RashodyViewModel();
            RashodyViewModel = await httpRequests4.GetRequestAsync("/api/Sklad_rashod/VM", RashodyViewModel);
            //Global.Spr_Oplat_Sklad = RashodyViewModel.Spr_Oplat_Sklad;
            #region code
            /*  HttpRequests<ObservableCollection<Sklad_rashod>> httpRequests = new HttpRequests<ObservableCollection<Sklad_rashod>>();
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
  */
            #endregion
            //получаем список расходов
            GetProduct();
            //фильтруем его
            Filter();
            this.DataContext = RashodyViewModel;
        }

        public async void GetProduct()
        {
            IsDataLoaded = false;
            RashodyQueryParams queryParams = new RashodyQueryParams(StartFilterDate, EndFilterDate, /*SearchTB.Text*/"");
            HttpPostRequests<List<Sklad_rashod>, RashodyQueryParams> postRequests = new HttpPostRequests<List<Sklad_rashod>, RashodyQueryParams>();
            rashods = await postRequests.PostRequest("api/Sklad_rashod/Filter", rashods, queryParams);
            RashodyViewModel.Rashods = new ObservableCollection<Sklad_rashod>(rashods);
            RashodyViewModel.Spr_Managers_Filter = new ObservableCollection<string>(RashodyViewModel.Rashods.Select(p => p.Otpustil).Distinct().OrderBy(p => p));
            IsDataLoaded = true;
            Filter();
        }

        public void Filter()
        {
            if (IsDataLoaded)
            {
                IsDataLoaded = false;
                List<Sklad_rashod> filt = new List<Sklad_rashod>(rashods);
                if (ManagerCB.SelectedItem != null && (ManagerCB.SelectedItem as string) != "") 
                    filt = filt.Where(p => p.Otpustil == (ManagerCB.SelectedItem as string)).ToList();
                if (OplachenCB.SelectedItem != null)
                {
                    switch ((OplachenCB.SelectedItem as ComboBoxItem).Content)
                    {
                        case "Да":
                            {
                                //если значение null, то не берем
                                filt = filt.Where(p => p.IsTovOpl.HasValue ? p.IsTovOpl.Value : false).ToList();
                                break;
                            }
                        case "Нет":
                            {
                                //если значениие nullб то берем
                                filt = filt.Where(p => p.IsTovOpl.HasValue ? !p.IsTovOpl.Value : true).ToList();
                                break;
                            }
                    }
                }
                if (OtgruzhenoCB.SelectedItem != null)
                {
                    switch ((OtgruzhenoCB.SelectedItem as ComboBoxItem).Content)
                    {
                        case "Да":
                            {
                                //если значение null, то не берем
                                filt = filt.Where(p => p.Otgruzheno.HasValue ? p.Otgruzheno.Value : false).ToList();
                                break;
                            }
                        case "Нет":
                            {
                                //если значение null, то берем
                                filt = filt.Where(p => p.Otgruzheno.HasValue ? !p.Otgruzheno.Value : true).ToList();
                                break;
                            }
                    }
                }
                if (TipOplatyCB.SelectedItem != null && (TipOplatyCB.SelectedItem as Spr_oplat_sklad).naim != "")
                {
                    filt = filt.Where(p => p.Oplata == ((Spr_oplat_sklad)TipOplatyCB.SelectedItem).kod_zap).ToList();
                }
                RashodyViewModel.Rashods = new ObservableCollection<Sklad_rashod>(filt);
                IsDataLoaded = true;
            }
        }

        private void datagridRashods_MoseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagridRashods.SelectedItem != null)
            {
                RashodWin rashod = new RashodWin((datagridRashods.SelectedItem as Sklad_rashod).kod_zap.Value);
                rashod.Show();
            }
        }

        private void RashodWin(decimal v, object id)
        {
            throw new NotImplementedException();
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
            GetProduct();
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
                    GetProduct();
                }
            }
        }

        private void DayDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StartFilterDate = DayDP.SelectedDate;
            EndFilterDate = DayDP.SelectedDate;
            GetProduct();
        }

        private void DayStartDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StartFilterDate = DayStartDP.SelectedDate;
            GetProduct();
        }

        private void DayEndDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EndFilterDate = DayEndDP.SelectedDate;
            GetProduct();
        }

        private void MonthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthCB.SelectedIndex != -1 && YearCB.SelectedIndex != -1)
            {
                StartFilterDate = new DateTime((int)YearCB.SelectedItem, (int)MonthCB.SelectedItem, 1);
                EndFilterDate = StartFilterDate.Value.AddMonths(1).AddDays(-1);
            }
            GetProduct();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetProduct();
        }

        private void ManagerCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ToolBarControl_DeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void ToolBarControl_UpdateClick(object sender, RoutedEventArgs e)
        {

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
            GetProduct();
        }

        private async void AddBN_Click(object sender, RoutedEventArgs e)
        {
            HttpRequests<Sklad_rashod> httpRequests = new HttpRequests<Sklad_rashod>();
            Sklad_rashod rashod = new Sklad_rashod();
            rashod.Otpustil = "Test";
            rashod.Nom_rash = 999;
            rashod = await httpRequests.PostRequest("api/Sklad_rashod/", rashod);
        }

        private void PeriodsRB_Checked(object sender, RoutedEventArgs e)
        {
            if(IsDataLoaded) PeriodsCB_SelectionChanged(sender, null);
        }
        private void DayRB_Checked(object sender, RoutedEventArgs e)
        {
            if (IsDataLoaded) DayDP_SelectedDateChanged(sender, null);
        }
        private void DaysRB_Checked(object sender, RoutedEventArgs e)
        {
            if (IsDataLoaded) DayStartDP_SelectedDateChanged(sender, null);
        }
        private void MonthRB_Checked(object sender, RoutedEventArgs e)
        {
            if (IsDataLoaded) MonthCB_SelectionChanged(sender, null);
        }
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
