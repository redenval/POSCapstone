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
        string GetCartItem(UserViewModel user, string id);
        void AddCartItem(UserViewModel user, string id);
        void SubtractCartItem(UserViewModel user, string id);
        void RemoveCartItem(UserViewModel user, string id);
        void CheckoutOrder(UserViewModel user);
        int GetTotalCartItem(UserViewModel user);
        ViewOrderViewModel GetUserOrders(UserViewModel user);
        ManageOrderViewModel GetAllUserOrders();
        void ApproveOrder(string orderId);
        void ConfirmOrder(string orderId);
    }
}
