using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository
{
    public class SQLProductRepository : IRepository<Product>
    {
        public string Route { get; set; }

        public SQLProductRepository(string route)
        {
            Route = route;
        }

        public async Task<ObservableCollection<Product>> GetList()
        {
            return await HttpRequests<ObservableCollection<Product>>.GetRequestAsync(Route, new ObservableCollection<Product>());
        }

        public async Task<Product> Get(int id)
        {
            return await HttpRequests<Product>.GetRequestAsync(Route + id, new Product());
        }

        public async Task Create(Product Product)
        {
            await HttpRequests<Product>.PostRequest(Route, Product);
        }

        public async Task Update(Product Product)
        {
            if (!Product.IsValid)
            {
                Global.ErrorLog($"У товара {Product.Naim2} не заполнены все необходимые поля");
            }
            else await HttpRequests<Product>.PutRequest(Route + Convert.ToInt32(Product.ID), Product);
        }

        public async Task Delete(int id)
        {
            await HttpRequests<Sklad_rashod>.DeleteRequest(Route + id);
        }
    }

}
