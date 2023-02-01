using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository
{
    interface IRepository<T>
    {
        string Route { get; set; }
        Task<ObservableCollection<T>> GetList(); // получение всех объектов
        Task<T> Get(int id); // получение одного объекта по id
        Task Create(T item); // создание объекта
        Task Update(T item); // обновление объекта
        Task Delete(int id); // удаление объекта по id
    }
}
