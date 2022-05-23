using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.Repository.IRepository
{
    public interface IAdminRepository
    {
        bool CheckAdminCredentials(string user, string pass);
        void AddInventoryItem(ItemViewModel itemViewModel);
        void EditInventoryItem(ItemViewModel itemViewModel);
        void DeleteInventoryItem(int id);
        void AddAccount(AccountViewModel accountViewModel);
        void EditAccount(AccountViewModel accountViewModel);
        void DeleteAccount(int id);
        void AddProduct(ProductViewModel productViewModel);
        void DeleteProduct(int id);
        List<ItemViewModel> GetInventoryItems();
        List<AccountViewModel> GetAccounts();
        IEnumerable<SearchItemViewModel> GetSearchItems();
        IEnumerable<ProductViewModel> GetProductViewModels();
    }
}
