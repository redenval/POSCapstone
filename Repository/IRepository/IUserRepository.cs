using Capstone.Data;
using Capstone.ViewModels;
using System.Collections.Generic;

namespace Capstone.Repository.IRepository
{
    public interface IUserRepository
    {
        void Create(UserViewModel user);
        void CreateOrUpdate(UserViewModel user);
        void Delete(int id);
        void Update(int id, UserViewModel user);
        UserViewModel IsExist(UserViewModel user);
        bool IsPhoneNumberExist(string phone);
        bool IsEmailExist(string email);
        bool ValidateUserLogin(string email, string password);
        UserViewModel GetUser(string email);
        CartViewModel GetCartViewModel(string email);
        List<UserViewModel> GetAllUsers();
        bool RemoveUser(string id);
    }
}
