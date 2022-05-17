using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Repository.IRepository
{
    public interface IAdminRepository
    {
        bool CheckAdminCredentials(string user, string pass);
    }
}
