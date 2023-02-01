using Client.Models;
using Client.Repository;
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
    public class TovaryViewModel : BaseViewModel
    {
        SQLProductRepository ProductRepository { get; set; }

        private ObservableCollection<Product> _tovaryList;
        public ObservableCollection<Product> TovaryList { get { return _tovaryList; } set { _tovaryList = value; OnPropertyChanged(nameof(TovaryList)); } }
        public ObservableCollection<Category> Categories { get { return Enums.Spr_category; } set { OnPropertyChanged(nameof(Categories)); } }
        public ObservableCollection<Manufacture> Manufactures { get { return Enums.Manufactures; } set { OnPropertyChanged(nameof(Manufactures)); } }
        private Product _selectedTovar;
        public Product SelectedTovar { get { return _selectedTovar; }
            set { _selectedTovar = value; OnPropertyChanged(nameof(SelectedTovar)); } }

        public bool IsReturn { get; set; }
        public Visibility CloseBTNVisible { get; set; }

        public TovaryViewModel(bool _IsReturn = false)
        {
            Route = "Tovary/";
            ProductRepository = new SQLProductRepository(Route);
            Update();
            IsReturn = _IsReturn;
            CloseBTNVisible = IsReturn ? Visibility.Visible : Visibility.Collapsed;
        }

        public override async Task Update()
        {
            TovaryList = await ProductRepository.GetList();
        }

        public override async Task Save()
        {
            foreach (Product tov in TovaryList)
            {
                try
                {
                    await ProductRepository.Update(tov);
                }
                catch (Exception ex)
                {
                    Global.ErrorLog(ex.Message);
                }
            }
            Update();
        }

        public override async Task Delete(int id)
        {
            try
            {
                await ProductRepository.Delete(id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }

        public async Task Copy(int id)
        {
            try
            {
                await ProductRepository.Create(new Product(TovaryList.Where(p => p.ID == id).FirstOrDefault()));
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();/*
            Product product = new Product(TovaryList.Where(p=>p.ID == id).FirstOrDefault());
            product.ID = 0;
            TovaryList.Add(product);*/
        }
    }
}
