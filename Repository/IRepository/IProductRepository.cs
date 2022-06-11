using Capstone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Repository.IRepository
{
    public interface IProductRepository
    {
        List<ProductViewModel> GetProducts();
        void AddCartItem(UserViewModel user, string id);
        void SubtractCartItem(UserViewModel user, string id);
    }
}
