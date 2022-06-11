using Capstone.Data;
using Capstone.Repository.IRepository;
using Capstone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public void SubtractCartItem(UserViewModel user, string id)
        {
            User dbUser = _context.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            Product product = _context.Products.Where(p => p.Id.ToString().Equals(id)).FirstOrDefault();
            if (product != null)
            {
                CartProduct cartProduct = _context.CartProducts.Where(p => p.Product.Equals(product) && p.User == dbUser).FirstOrDefault();
                if (cartProduct != null)
                {
                    if (cartProduct.Quantity >= 2)
                    {
                        cartProduct.Quantity--;
                    }
                    else
                    {
                        _context.CartProducts.Remove(cartProduct);
                    }

                }
            }
            _context.SaveChanges();
        }

        public void AddCartItem(UserViewModel user, string id)
        {
            User dbUser = _context.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            Product product = _context.Products.Where(p => p.Id.ToString().Equals(id)).FirstOrDefault();
            if (product != null)
            {
                CartProduct cartProduct = _context.CartProducts.Where(p => p.Product.Equals(product) && p.User == dbUser).FirstOrDefault();
                if (cartProduct != null)
                {
                    cartProduct.Quantity++;
                }
                else
                {
                    _context.CartProducts.Add(new CartProduct() { Product = product, Quantity = 1, User = dbUser });
                }

            }
            _context.SaveChanges();
        }

        public List<ProductViewModel> GetProducts()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            List<Product> dbProduct = _context.Products.Where(p => p.IsActive == true).ToList();
            foreach (var item in dbProduct)
            {
                ProductViewModel model = new ProductViewModel();
                model.Id = item.Id;
                model.Description = item.Description;
                if (item.PromoStatus == true)
                {
                    model.Name = item.PromoName;
                    model.Price = item.PromoPrice;
                }
                else
                {
                    model.Name = item.BaseName;
                    model.Price = item.BasePrice;
                }
                products.Add(model);
            }

            return products;
        }
    }
}
