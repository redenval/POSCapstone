using System;
using System.Linq;
using Capstone.Data;
using Capstone.ViewModels;
using Capstone.Repository.IRepository;
using Capstone.Utilities;
using System.Collections.Generic;

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
            if (IsExist(user) == null)
            {
                _context.Users.Add(new User() { Email = user.Email, Password = user.Password, StreetAddress = user.StreetAddress, Barangay = user.Barangay, Phone = user.Phone, Profile = user.Profile, Role = SessionKeys.UserAccessRoleDefault});
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public UserViewModel IsExist(UserViewModel user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email.ToLower().Equals(user.Email.ToLower()));
            return (dbUser != null) ? new UserViewModel()
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                Password = dbUser.Password,
                Phone = dbUser.Phone,
                Barangay = dbUser.Barangay,
                Profile = dbUser.Profile
            ,
                Role = dbUser.Role,
                StreetAddress = dbUser.StreetAddress
            } : null;
        }
        public bool IsEmailExist(string email)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
            return (dbUser != null) ? true : false;
        }

        public bool IsPhoneNumberExist(string phone)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Phone.Equals(phone));
            return (dbUser != null) ? true : false;
        }

        public void Update(int id, UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUserLogin(string email, string password)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()) && u.Password.Equals(password));
            return (dbUser != null) ? true : false;
        }

        public UserViewModel GetUser(string email)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
            return (dbUser != null) ? new UserViewModel()
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                Password = dbUser.Password,
                Phone = dbUser.Phone,
                Barangay = dbUser.Barangay,
                Profile = dbUser.Profile,
                Role = dbUser.Role,
                StreetAddress = dbUser.StreetAddress
            } : null;
        }

        public CartViewModel GetCartViewModel(string email)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
            CartViewModel cart = new CartViewModel();
            cart.user = GetUser(email);
            List<CartItemViewModel> cartItems = new List<CartItemViewModel>();
            List<CartProduct> cartProducts = _context.CartProducts.Where(u => u.User == dbUser).ToList();
            foreach (var item in cartProducts)
            {
                Product product = _context.Products.Where(m => m.CartProducts.Contains(item)).FirstOrDefault();
                cartItems.Add(new CartItemViewModel() { Id = product.Id, Image = (product.BaseImage == null) ? "" : product.BaseImage.Path, Name = product.BaseName, Price = product.BasePrice, Quantity = item.Quantity});
            }
            cart.Cart = cartItems;

            return cart;
        }
    }
}
