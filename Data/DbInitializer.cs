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

            context.Users.Add(new User() { Email = "admin@admin.com", Password = "qwe123", Role = "Default", Phone ="+639876543219"});

            Product slimGallon = new Product();
            slimGallon.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = slimGallon };
            slimGallon.BaseName = $"Slim Gallon (Container Only)";
            slimGallon.Description = "Slim Gallon is the standard sized container good for everyone.";
            slimGallon.BasePrice = 130.00;
            slimGallon.Category = Utilities.ProductCategory.WithNoWater;
            slimGallon.IsActive = true;
            context.Products.Add(slimGallon);

            Product slimGallonWithWater = new Product();
            slimGallonWithWater.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = slimGallonWithWater };
            slimGallonWithWater.BaseName = $"Slim Gallon (With Water)";
            slimGallonWithWater.Description = "Slim Gallon is the standard sized container good for everyone. This is filled with water.";
            slimGallonWithWater.BasePrice = 150.00;
            slimGallonWithWater.Category = Utilities.ProductCategory.WithWater;
            slimGallonWithWater.IsActive = true;
            context.Products.Add(slimGallonWithWater);

            Product roundGallon = new Product();
            roundGallon.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = roundGallon };
            roundGallon.BaseName = $"Round Gallon (Container Only)";
            roundGallon.BasePrice = 100.00;
            roundGallon.Category = Utilities.ProductCategory.WithNoWater;
            roundGallon.IsActive = true;
            context.Products.Add(roundGallon);

            Product roundGallonWithWater = new Product();
            roundGallonWithWater.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = roundGallonWithWater };
            roundGallonWithWater.BaseName = $"Round Gallon (With Water)";
            roundGallonWithWater.BasePrice = 120.00;
            roundGallonWithWater.Category = Utilities.ProductCategory.WithWater;
            roundGallonWithWater.IsActive = true;
            context.Products.Add(roundGallonWithWater);

            Product waterBottle350ml = new Product();
            waterBottle350ml.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = waterBottle350ml };
            waterBottle350ml.BaseName = $"Water Bottle 350 mL";
            waterBottle350ml.BasePrice = 4.50;
            waterBottle350ml.Category = Utilities.ProductCategory.Small;
            waterBottle350ml.IsActive = true;
            context.Products.Add(waterBottle350ml);

            Product waterBottle500ml = new Product();
            waterBottle500ml.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = waterBottle500ml };
            waterBottle500ml.BaseName = $"Water Bottle 500 mL";
            waterBottle500ml.BasePrice = 6;
            waterBottle500ml.Category = Utilities.ProductCategory.Medium;
            waterBottle500ml.IsActive = true;
            context.Products.Add(waterBottle500ml);

            Product waterBottle1L = new Product();
            waterBottle1L.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = waterBottle1L };
            waterBottle1L.BaseName = $"Water Bottle 1 L";
            waterBottle1L.BasePrice = 10;
            waterBottle1L.Category = Utilities.ProductCategory.Large;
            waterBottle1L.IsActive = true;
            context.Products.Add(waterBottle1L);
            
            Product bigCap = new Product();
            bigCap.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = bigCap };
            bigCap.BaseName = $"Big Cap";
            bigCap.BasePrice = 10;
            bigCap.Category = Utilities.ProductCategory.Large;
            bigCap.IsActive = true;
            context.Products.Add(bigCap);

            Product smallCap = new Product();
            smallCap.BaseImage = new ProductBaseImage() { Path = "~/images/noimage.png", Product = smallCap };
            smallCap.BaseName = $"Small Cap";
            smallCap.BasePrice = 5;
            smallCap.Category = Utilities.ProductCategory.Small;
            smallCap.IsActive = true;
            context.Products.Add(smallCap);

            context.SaveChanges();
        }
    }
}