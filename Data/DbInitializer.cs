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

            context.Users.Add(new User() { Email = "admin@admin.com", Password = "qwe123", Role = "Default", Phone ="09652279274"});

            for (int i = 0; i < 10; i++)
            {
                Product product = new Product()
                {
                    BaseName = $"Water {i+1}L",
                    BasePrice = 1.00 * (i+1),
                    Category = (Utilities.ProductCategory)(i % 1),
                    IsActive = true,
                };
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}