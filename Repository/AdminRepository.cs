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

        public void AddInventoryItem(ItemViewModel itemViewModel)
        {
            var count = _dbContext.Items.Where(x => x.Name.Equals(itemViewModel.Name)).Count();
            if (count > 0)
                return;

            _dbContext.Items.Add(new Item() {  Name = itemViewModel.Name, StockQuantity = itemViewModel.StockQuantity });
            _dbContext.SaveChanges();
        }

        public bool CheckAdminCredentials(string user, string pass)
        {
            return _dbContext.Accounts.Where(x => x.Email.Equals(user) && x.Password.Equals(pass)).Count() > 0 ? true : false ;
        }

        public void DeleteInventoryItem(int id)
        {
            var item = _dbContext.Items.Where(x => x.Id == id).ToList();
            if(item.Count()<=0)
            {
                return;
            }

            _dbContext.Items.Remove(item.First());
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
    }
}
