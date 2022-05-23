using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Data;
using Capstone.Models;
using Capstone.Repository.IRepository;
using Capstone.Data.Entities;

namespace Capstone.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _dbContext;
        public AdminRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAccount(AccountViewModel accountViewModel)
        {

            var count = _dbContext.Accounts.Where(x => x.FirstName.Equals(accountViewModel.FirstName) && x.LastName.Equals(accountViewModel.LastName)).Count();
            if (count > 0)
                return;

            _dbContext.Accounts.Add(new Account() { AccountLevel = accountViewModel.AccountLevel, FirstName = accountViewModel.FirstName, 
            LastName = accountViewModel.LastName, Email = accountViewModel.Email, Password = accountViewModel.Password, PhoneNumber = accountViewModel.PhoneNumber});
            _dbContext.SaveChanges();
        }

        public void AddInventoryItem(ItemViewModel itemViewModel)
        {
            var count = _dbContext.Items.Where(x => x.Name.Equals(itemViewModel.Name)).Count();
            if (count > 0)
                return;

            _dbContext.Items.Add(new Item() {  Name = itemViewModel.Name, StockQuantity = itemViewModel.StockQuantity });
            _dbContext.SaveChanges();
        }

        public void AddProduct(ProductViewModel productViewModel)
        {
            if (productViewModel == null) return;

            Product product = new Product() { Name = productViewModel.Name, Price = productViewModel.Price};
            List<ProductItem> pl = new List<ProductItem>();
            foreach (var item in productViewModel.Items)
            {
                ProductItem p = new ProductItem() { Product = product, Quantity = item.Quantity, Item = _dbContext.Items.Where(x => x.Id == item.Item.Id).FirstOrDefault() };
                pl.Add(p);
            }

            product.ProductItems = pl;
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public bool CheckAdminCredentials(string user, string pass)
        {
            return _dbContext.Accounts.Where(x => x.Email.Equals(user) && x.Password.Equals(pass)).Count() > 0 ? true : false ;
        }

        public void DeleteAccount(int id)
        {
            var account = _dbContext.Accounts.Where(x => x.Id == id).ToList();
            if (account.Count() <= 0)
            {
                return;
            }

            _dbContext.Accounts.Remove(account.First());
            _dbContext.SaveChanges();
        }

        public void DeleteInventoryItem(int id)
        {
            var item = _dbContext.Items.Where(x => x.Id == id).ToList();
            if(item.Count()<=0)
            {
                return;
            }

            bool isPresent = _dbContext.ProductItems.Where(x => x.Item == item.First()).ToList().Count() > 0;

            if(isPresent)
            {
                return;
            }
            else
            {
                _dbContext.Items.Remove(item.First());
                _dbContext.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.Where(x => x.Id == id).ToList();
            if(product.Count()<=0)
            {
                return;
            }

            var productItems = _dbContext.ProductItems.Where(x => x.Product.Id == product.FirstOrDefault().Id).ToList();
            foreach(var item in productItems)
            {
                _dbContext.ProductItems.Remove(item);
                _dbContext.SaveChanges();
            }

            _dbContext.Products.Remove(product.FirstOrDefault());
            _dbContext.SaveChanges();
        }

        public void EditAccount(AccountViewModel accountViewModel)
        {
            var count = _dbContext.Accounts.Where(x => x.Id == accountViewModel.Id).Count();
            if (count <= 0)
            {
                return;
            }

            Account account = _dbContext.Accounts.Where(x => x.Id == accountViewModel.Id).FirstOrDefault();
            account.FirstName = accountViewModel.FirstName;
            account.LastName = accountViewModel.LastName;
            account.Email = accountViewModel.Email;
            account.Password = accountViewModel.Password;
            account.PhoneNumber = accountViewModel.PhoneNumber;
            account.AccountLevel = accountViewModel.AccountLevel;

            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }

        public void EditInventoryItem(ItemViewModel itemViewModel)
        {
            var count = _dbContext.Items.Where(x => x.Id == itemViewModel.Id).Count();
            if (count <= 0)
            {
                return;
            }

            Item item = _dbContext.Items.Where(x => x.Id == itemViewModel.Id).FirstOrDefault();
            item.Name = itemViewModel.Name;
            item.StockQuantity = itemViewModel.StockQuantity;

            _dbContext.Items.Update(item);
            _dbContext.SaveChanges();
        }

        public List<AccountViewModel> GetAccounts()
        {
            List<AccountViewModel> userAccounts = new List<AccountViewModel>();
            var accounts = _dbContext.Accounts;

            foreach (var account in accounts)
            {
                userAccounts.Add(new AccountViewModel() { Id = account.Id, FirstName = account.FirstName, LastName = account.LastName, Email = account.Email, AccountLevel = account.AccountLevel, Password = account.Password, PhoneNumber = account.PhoneNumber});
            }

            return userAccounts;
        }

        public List<ItemViewModel> GetInventoryItems()
        {
            List<ItemViewModel> inventory = new List<ItemViewModel>();
            var items = _dbContext.Items;

            foreach(var item in items)
            {
                inventory.Add(new ItemViewModel() { Id = item.Id, Name = item.Name, StockQuantity = item.StockQuantity });
            }

            return inventory;
        }

        public IEnumerable<ProductViewModel> GetProductViewModels()
        {
            return _dbContext.Products.Select(x => new ProductViewModel() { Id = x.Id, Name = x.Name, Items = x.ProductItems, Price = x.Price }).AsEnumerable();
        }

        public IEnumerable<SearchItemViewModel> GetSearchItems()
        {
            IEnumerable<SearchItemViewModel> searchItems;
            searchItems = _dbContext.Items.Select(x => new SearchItemViewModel() { Id = x.Id, ItemName = x.Name}).AsEnumerable();
            return searchItems;
        }
    }
}
