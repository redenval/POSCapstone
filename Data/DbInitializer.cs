using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Accounts.Add(new Entities.Account() { FirstName = "Admin", LastName = "Admin", AccountLevel = 0, Email = "Admin@Admin.com", Password="Admin", PhoneNumber = "00000000000" });
            context.SaveChanges();
        }
    }
}