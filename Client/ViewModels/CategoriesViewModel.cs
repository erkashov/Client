using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        private ObservableCollection<Category> _catList;
        public ObservableCollection<Category> CategoryList { get { return _catList; } set { _catList = value; OnPropertyChanged(nameof(CategoryList)); } }

        public CategoriesViewModel()
        {
            Route = "Categories/";
            Update();
        }

        public async override Task Update()
        {
            CategoryList = await HttpRequests<ObservableCollection<Category>>.GetRequestAsync(Route, CategoryList);
        }

        public async override Task Save()
        {
            foreach (Category cat in CategoryList)
            {
                try
                {
                    await HttpRequests<Category>.PutRequest(Route + cat.ID, cat);
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
            CategoryList.Add(new Category());
        }

        public async override Task Delete(int id)
        {
            try
            {
                await HttpRequests<Category>.DeleteRequest(Route + id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();

        }
    }
}
