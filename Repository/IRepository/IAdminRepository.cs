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
        void DeleteInventoryItem(int id);
        List<InventoryViewModel> GetInventoryItems();
    }
}
