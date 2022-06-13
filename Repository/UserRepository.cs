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
                _context.Users.Add(new User() { Email = user.Email, Password = user.Password, StreetAddress = user.StreetAddress, Barangay = user.Barangay, Phone = user.Phone, Profile = user.Profile, Role = SessionKeys.UserAccessRoleDefault });
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
                ProductBaseImage pbi = _context.BaseImages.Where(m => m.Product == product).FirstOrDefault();
                cartItems.Add(new CartItemViewModel() { Id = product.Id, Image = (pbi == null) ? "" : product.BaseImage.Path, Name = product.BaseName, Price = product.BasePrice, Quantity = item.Quantity });
            }
            cart.Cart = cartItems;

            return cart;
        }

        public List<UserViewModel> GetAllUsers()
        {
            var dbUsers = _context.Users.ToList();
            List<UserViewModel> ListOfUsers = new List<UserViewModel>();
            foreach (var item in dbUsers)
            {
                ListOfUsers.Add((item != null) ? new UserViewModel()
                {
                    Id = item.Id,
                    Email = item.Email,
                    Password = item.Password,
                    Phone = item.Phone,
                    Barangay = item.Barangay,
                    Profile = item.Profile,
                    Role = item.Role,
                    StreetAddress = item.StreetAddress
                } : null);

            }
            return ListOfUsers;
        }

        public bool RemoveUser(string id)
        {
            User dbUser = _context.Users.Where(u => u.Id.ToString().Equals(id)).FirstOrDefault();
            if (dbUser != null)
            {
                _context.Users.Remove(dbUser);
            }
            _context.SaveChanges();
            User check = _context.Users.Where(u => u.Id.ToString().Equals(id)).FirstOrDefault();
            return check != null ? false : true;
        }

        public void CreateOrUpdate(UserViewModel user)
        {
            User u = _context.Users.Where(m => m.Id.ToString().Equals(user.Id.ToString()) && (m.Email == user.Email || m.Phone == user.Phone)).FirstOrDefault();
            if (u == null)
            {
                if(_context.Users.Where(m => m.Email.Equals(user.Email)).FirstOrDefault() == null && _context.Users.Where(m => m.Phone.Equals(user.Phone)).FirstOrDefault() == null)
                {
                    _context.Users.Add(new User() { Email = user.Email, Password = user.Password, StreetAddress = user.StreetAddress, Barangay = user.Barangay, Phone = user.Phone, Profile = user.Profile, Role = user.Role.ToString() });
                }
            }
            else
            {
                User uu = new User() { Id=u.Id, Email = user.Email, Password = user.Password, StreetAddress = user.StreetAddress, Barangay = user.Barangay, Phone = user.Phone, Profile = user.Profile, Role = user.Role.ToString() };
                _context.Entry(u).CurrentValues.SetValues(uu);
            }
            _context.SaveChanges();
        }
    }
}
