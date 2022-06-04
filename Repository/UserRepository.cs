using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Data;
using Capstone.Models;
using Capstone.Repository.IRepository;
using Capstone.Utilities;

namespace Capstone.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(UserViewModel user)
        {
            if(!IsExist(user))
            {
                _context.Users.Add(new User() { Email = user.Email, Password = user.Password, StreetAddress = user.StreetAddress, Barangay = user.Barangay, Phone = user.Phone, Profile = user.Profile, Role = SessionKeys.UserAccessRoleDefault });
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(UserViewModel user)
        {
            return (_context.Users.Where(x => x.Email.Equals(user.Email)).ToList().Count() > 0 ? true : false);
        }

        public void Update(int id, UserViewModel user)
        {
            throw new NotImplementedException();
        }
    }
}
