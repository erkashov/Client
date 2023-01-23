using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Client.ViewModels
{
    public class ContractorViewModel : BaseViewModel
    {
        private ObservableCollection<Contractor> _contractorList;
        public ObservableCollection<Contractor> ContractorList { get { return _contractorList; } set { _contractorList = value; OnPropertyChanged(nameof(ContractorList)); } }
        private Contractor _selectedContractor;
        public Contractor SelectedContractor
        {
            get { return _selectedContractor; }
            set { _selectedContractor = value; OnPropertyChanged(nameof(SelectedContractor)); }
        }

        public bool IsReturn { get; set; }
        public Visibility CloseBTNVisible { get; set; }

        public ContractorViewModel(bool _isReturn = false)
        {
            Route = "Contractors/";
            Update();
            IsReturn = _isReturn;
            CloseBTNVisible = IsReturn ? Visibility.Visible : Visibility.Collapsed;
        }

        public override async Task Update()
        {
            ContractorList = await HttpRequests<ObservableCollection<Contractor>>.GetRequestAsync(Route, ContractorList);
        }

        public override async Task Save()
        {
            foreach (Contractor contr in ContractorList)
            {
                /*if (!tov.IsValid)
                {
                    Global.ErrorLog($"У товара {tov.Naim2} не заполнены все необходимые поля");
                    continue;
                }*/
                try
                {
                    await HttpRequests<Contractor>.PutRequest(Route + contr.ID, contr);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Update();
        }

        public void Add()
        {
            ContractorList.Add(new Contractor());
        }

        public override async Task Delete(int id)
        {
            try
            {
                await HttpRequests<Contractor>.DeleteRequest(Route + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }
    }
}
