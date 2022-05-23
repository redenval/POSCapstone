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

            for (int i = 0; i < 50; i++)
            {
                context.Items.Add(new Entities.Item() { Name = (i + 1) + "L Water Bottle", StockQuantity = ((i + 1) * 5) * 99 / (i+1) });
                context.SaveChanges();
            }

            context.Accounts.Add(new Entities.Account() { FirstName = "Admin", LastName = "Admin", AccountLevel = 0, Email = "Admin@Admin.com", Password="Admin", PhoneNumber = "00000000000" });
            context.SaveChanges();

            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {

                List<Entities.ProductItem> prods = new List<Entities.ProductItem>();
                for (int j = 0; j < rnd.Next(10); j++)
                {
                    int r = rnd.Next(context.Items.Count());
                    prods.Add(new Entities.ProductItem() { Item = context.Items.ToList()[r], Quantity = r});
                }

                context.Products.Add(new Entities.Product()
                {
                    Name = "Product #" + (i + 1),
                    Price = 200.00 * (i + 0.17),
                    ProductItems = prods
                });

                context.SaveChanges();
            }
        }
    }
}