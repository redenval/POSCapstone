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

        public bool CheckAdminCredentials(string user, string pass)
        {
            return _dbContext.Accounts.Where(x => x.Email.Equals(user) && x.Password.Equals(pass)).Count() > 0 ? true : false ;
        }

        public void DeleteInventoryItem(int id)
        {
            var item = _dbContext.Items.Where(x => x.Id == id).ToList();
            if(item.Count()>0)
            {
                _dbContext.Items.Remove(item.First());
                _dbContext.SaveChanges();
            }else
            {
                return;
            }
    
        }

        public List<InventoryViewModel> GetInventoryItems()
        {
            List<InventoryViewModel> inventory = new List<InventoryViewModel>();
            var items = _dbContext.Items;

            foreach(var item in items)
            {
                inventory.Add(new InventoryViewModel() { Id = item.Id, Name = item.Name, StockQuantity = item.StockQuantity });
            }

            return inventory;
        }
    }
}
