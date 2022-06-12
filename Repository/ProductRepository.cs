using Capstone.Data;
using Capstone.Repository.IRepository;
using Capstone.Utilities;
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
                ProductBaseImage pbi = _context.BaseImages.Where(m => m.Product == item).FirstOrDefault();
                model.Id = item.Id;
                model.Description = item.Description;
                model.Image = pbi.Path;
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

        public void RemoveCartItem(UserViewModel user, string id)
        {
            User dbUser = _context.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            Product product = _context.Products.Where(p => p.Id.ToString().Equals(id)).FirstOrDefault();
            if (product != null)
            {
                CartProduct cartProduct = _context.CartProducts.Where(p => p.Product.Equals(product) && p.User == dbUser).FirstOrDefault();
                if (cartProduct != null)
                {
                    _context.CartProducts.Remove(cartProduct);
                }
            }
            _context.SaveChanges();
        }

        public string GetCartItem(UserViewModel user, string id)
        {
            User dbUser = _context.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            Product product = _context.Products.Where(p => p.Id.ToString().Equals(id)).FirstOrDefault();
            if (product != null)
            {
                return product.BaseName;
            }
            return "";
        }

        public void CheckoutOrder(UserViewModel user)
        {
            User dbUser = _context.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            var listOfProducts = _context.CartProducts.Where(m => m.User == dbUser).ToList();
            Order order = new Order() { DateCreated = DateTime.Now, User = dbUser };
            foreach (var item in listOfProducts)
            {
                Product product = _context.Products.Where(m=>m.CartProducts.Contains(item)).FirstOrDefault();
                ProductOrder productOrder = new ProductOrder();
                productOrder.Order = order;
                productOrder.Status = EnumsCollection.ProductOrderStatus.Pending.ToString();
                productOrder.Quantity = item.Quantity;
                productOrder.Product = product;
                _context.ProductOrders.Add(productOrder);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
