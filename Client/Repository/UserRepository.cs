using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository
{
    public class UserRepository : IRepository<User>
    {
        public string Route { get; set; }

        public UserRepository(string route)
        {
            Route = route;
        }

        public async Task<ObservableCollection<User>> GetList()
        {
            return await HttpRequests<ObservableCollection<User>>.GetRequestAsync(Route, new ObservableCollection<User>());
        }

        public async Task<User> Get(int id)
        {
            return await HttpRequests<User>.GetRequestAsync(Route + id, new User());
        }

        public async Task Create(User user)
        {
            await HttpRequests<User>.PostRequest(Route, user);
        }

        public async Task Update(User user)
        {
            await HttpRequests<User>.PutRequest(Route + Convert.ToInt32(user.ID), user);
        }

        public async Task Delete(int id)
        {
            await HttpRequests<User>.DeleteRequest(Route + id);
        }
    }
}
