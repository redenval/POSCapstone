using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Data;
using Capstone.Repository.IRepository;

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
    }
}
