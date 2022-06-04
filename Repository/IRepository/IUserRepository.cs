using Capstone.Models;

namespace Capstone.Repository.IRepository
{
    public interface IUserRepository
    {
        void Create(UserViewModel user);
        void Delete(int id);
        void Update(int id, UserViewModel user);
        bool IsExist(UserViewModel user);
    }
}
