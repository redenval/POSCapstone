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

            foreach (var item in listOfProducts)
            {
                _context.CartProducts.Remove(item);
            }

            _context.SaveChanges();
        }

        public int GetTotalCartItem(UserViewModel user)
        {
            User dbUser = _context.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            int count = 0;
            if(dbUser != null)
            {
                var listOfProducts = _context.CartProducts.Where(m => m.User == dbUser).ToList();
                foreach (var item in listOfProducts)
                {
                    count += item.Quantity;
                }
            }
            return count;
        }

        public ViewOrderViewModel GetUserOrders(UserViewModel user)
        {
            User dbUser = _context.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            var listOfOrders = _context.Orders.Where(m => m.User == dbUser).ToList();
            ViewOrderViewModel viewOrder = new ViewOrderViewModel();
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var item in listOfOrders)
            {
                var listOfProductOrders = _context.ProductOrders.Where(m => m.Order == item).ToList();
                List<ProductOrderViewModel> productOrderViewModels = new List<ProductOrderViewModel>();
                double total = 0.00;
                foreach (var products in listOfProductOrders)
                {
                    Product product = _context.Products.Where(m => m.ProductOrders.Contains(products)).FirstOrDefault();
                    string imagePath = _context.BaseImages.Where(m => m.Product == product).FirstOrDefault().Path;
                    productOrderViewModels.Add(new ProductOrderViewModel()
                    {
                        Id = products.Id,
                        Image = imagePath,
                        Description = product.Description,
                        Name = product.BaseName,
                        Price = product.BasePrice,
                        Status = products.Status,
                        Quantity = products.Quantity,
                        TotalPrice = (product.BasePrice * products.Quantity)
                    });
                    total += product.BasePrice * products.Quantity;
                }
                OrderViewModel order = new OrderViewModel();
                order.Id = item.Id;
                order.Date = "DATE: " + item.DateCreated.ToShortDateString() + " TIME: " + item.DateCreated.ToShortTimeString();
                order.ProductOrders = productOrderViewModels;
                order.Total = total;
                orderViewModels.Add(order);
            }
            orderViewModels.Reverse();
            viewOrder.Orders = orderViewModels;
            return viewOrder;
        }

        public ManageOrderViewModel GetAllUserOrders()
        {
            var listOfOrders = _context.Orders.ToList();
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var item in listOfOrders)
            {
                User dbUser = _context.Users.Where(m => m.Orders.Contains(item)).FirstOrDefault();
                var listOfProductOrders = _context.ProductOrders.Where(m => m.Order == item).ToList();
                List<ProductOrderViewModel> productOrderViewModels = new List<ProductOrderViewModel>();
                double total = 0.00;
                foreach (var products in listOfProductOrders)
                {
                    Product product = _context.Products.Where(m => m.ProductOrders.Contains(products)).FirstOrDefault();
                    string imagePath = _context.BaseImages.Where(m => m.Product == product).FirstOrDefault().Path;
                    productOrderViewModels.Add(new ProductOrderViewModel()
                    {
                        Id = products.Id,
                        Image = imagePath,
                        Description = product.Description,
                        Name = product.BaseName,
                        Price = product.BasePrice,
                        Status = products.Status,
                        Quantity = products.Quantity,
                        TotalPrice = (product.BasePrice * products.Quantity)
                    });
                    total += product.BasePrice * products.Quantity;
                }
                OrderViewModel order = new OrderViewModel();
                order.Id = item.Id;
                order.Date = "DATE: " + item.DateCreated.ToShortDateString() + " TIME: " + item.DateCreated.ToShortTimeString();
                order.ProductOrders = productOrderViewModels;
                order.Total = total;
                if(dbUser != null)
                    order.Profile = new ProfileViewModel() { Address = $"{dbUser.StreetAddress} {dbUser.Barangay}", EmailAddress = dbUser.Email, PhoneNumber = dbUser.Phone} ;
                orderViewModels.Add(order);
            }
            orderViewModels.Reverse();

            List<OrderViewModel> pendingOrders = orderViewModels.Where(x=>x.ProductOrders.Any(m=>m.Status.Equals("Pending"))).ToList();
            List<OrderViewModel> shippedOrders = orderViewModels.Where(x=>!x.ProductOrders.Any(m=>m.Status.Equals("Pending")) && x.ProductOrders.Any(m=>m.Status.Equals("Shipped"))).ToList();
            List<OrderViewModel> deliveredOrders = orderViewModels.Where(x=>!x.ProductOrders.Any(m=>m.Status.Equals("Pending")) && !x.ProductOrders.Any(m=>m.Status.Equals("Shipped")) && x.ProductOrders.Any(m=>m.Status.Equals("Delivered"))).ToList();
            return new ManageOrderViewModel() { PendingOrders = pendingOrders, ShippedOrders = shippedOrders, DeliveredOrders = deliveredOrders};
        }

        public void ApproveOrder(string orderId)
        {
            Order order = _context.Orders.Where(x => x.Id.ToString().Equals(orderId)).FirstOrDefault();
            if(order != null)
            {
                var ListOfProductOrders = _context.ProductOrders.Where(m => m.Order == order);
                foreach (var item in ListOfProductOrders)
                {
                    item.Status = "Shipped";
                }
            }
            _context.SaveChanges();
        }
        public void ConfirmOrder(string orderId)
        {
            Order order = _context.Orders.Where(x => x.Id.ToString().Equals(orderId)).FirstOrDefault();
            if(order != null)
            {
                var ListOfProductOrders = _context.ProductOrders.Where(m => m.Order == order);
                foreach (var item in ListOfProductOrders)
                {
                    item.Status = "Delivered";
                }
            }
            _context.SaveChanges();
        }
    }
}
