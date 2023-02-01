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
    public class UsersViewModel : BaseViewModel
    {
        UserRepository UserRepository { get; set; }

        private ObservableCollection<User> _userList;
        public ObservableCollection<User> UserList { get { return _userList; } set { _userList = value; OnPropertyChanged(nameof(UserList)); } }
        private ObservableCollection<string> _roles;
        public ObservableCollection<string> Roles { get { return _roles; } set { _roles = value; OnPropertyChanged(nameof(Roles)); } }
        public ObservableCollection<Manufacture> Manufactures { get { return Enums.Manufactures; } set { OnPropertyChanged(nameof(Manufactures)); } }
        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        }

        public UsersViewModel()
        {
            Route = "Users/";
            UserRepository = new UserRepository(Route);
            Roles = new ObservableCollection<string>() { "admin", "manager", "client" };
            Update();
        }

        public override async Task Update()
        {
            UserList = await UserRepository.GetList();
        }

        public override async Task Save()
        {
            foreach (User user in UserList)
            {
                try
                {
                    await UserRepository.Update(user);
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
                await UserRepository.Delete(id);
            }
            catch (Exception ex)
            {
                Global.ErrorLog(ex.Message);
            }
            Update();
        }

        public async Task Add()
        {
            UserList.Add(new User());
        }
    }
}
