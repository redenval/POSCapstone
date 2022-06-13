using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Capstone.Services.IServices;
using Capstone.Repository.IRepository;
using Capstone.Utilities;
using Capstone.ViewModels;
using Capstone.ActionFilters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Controllers
{
    public class AdminController : Controller
    {

        private readonly ISessionService _sessionService;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public AdminController(ISessionService sessionService, IUserRepository userRepository, IProductRepository productRepository)
        {
            _sessionService = sessionService;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            if(_sessionService.GetItems(SessionKeys.UserAccessStatus, HttpContext).Equals(SessionKeys.UserAccessStatusLoggedIn) && _sessionService.GetItems(SessionKeys.UserAccessRole, HttpContext).Equals(SessionKeys.UserAccessRoleAdmin))
            {
                return View();
            }
            else if(_sessionService.GetItems(SessionKeys.UserAccessStatus, HttpContext).Equals(SessionKeys.UserAccessStatusLoggedOut))
            {
                return Redirect("/Login");
            }
            else
            {
                return Redirect("/");
            }
        }

        public IActionResult ManageSales()
        {
            return View("ManageSales", _productRepository.GetAllUserOrders());
        }

        public IActionResult ManageOrder()
        {
            return View("ManageOrder", _productRepository.GetAllUserOrders());
        }

        public IActionResult ApproveOrder(string orderId)
        {
            _productRepository.ApproveOrder(orderId);
            return Json(new { Success = true});
        }
        public IActionResult ConfirmDelivery(string orderId)
        {
            _productRepository.ConfirmOrder(orderId);
            return Json(new { Success = true});
        }

        [ImportModelState]
        public IActionResult ManageAccount()
        {
            if(_sessionService.GetItems(SessionKeys.UserAccessStatus, HttpContext).Equals(SessionKeys.UserAccessStatusLoggedIn) && _sessionService.GetItems(SessionKeys.UserAccessRole, HttpContext).Equals(SessionKeys.UserAccessRoleAdmin))
            {
                ViewBag.BarangayList = FunctionHelper.GetBarangayList().Select((value, index) => new { value, index }).Select(x => new SelectListItem() { Value = x.index.ToString(), Text = x.value });
                ViewBag.RoleList = FunctionHelper.GetRoleList().Select((value, index) => new { value, index }).Select(x => new SelectListItem() { Value = x.index.ToString(), Text = x.value });
                ViewBag.ListOfUsers = _userRepository.GetAllUsers();
                ViewBag.Email = _sessionService.GetItems(SessionKeys.User, HttpContext); 
                return View();
            }
            else if(_sessionService.GetItems(SessionKeys.UserAccessStatus, HttpContext).Equals(SessionKeys.UserAccessStatusLoggedOut))
            {
                return Redirect("/Login");
            }
            else
            {
                return Redirect("/");
            }
        }

        [ExportModelState]
        public IActionResult CreateOrUpdate(UserViewModel user)
        {
            ModelState.Remove("Id");
            if (user.Email.Equals("admin@admin.com"))
            {
                ModelState.AddModelError("Email", "Cannot update admin email address");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.BarangayList = FunctionHelper.GetBarangayList().Select((value, index) => new { value, index }).Select(x => new SelectListItem() { Value = x.index.ToString(), Text = x.value });
                return Redirect("/Admin/ManageAccount");
            }

            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            string phoneNumber = user.Phone;

            if (!string.IsNullOrEmpty(user.Phone))
            {
                var parsedPhoneNumber = phoneNumberUtil.Parse(user.Phone.Replace(" ", ""), "PH");
                var formattedPhoneNumber = phoneNumberUtil.Format(parsedPhoneNumber, PhoneNumbers.PhoneNumberFormat.INTERNATIONAL);
                bool isValidPhoneNumber = phoneNumberUtil.IsValidNumber(parsedPhoneNumber);

                if (!isValidPhoneNumber)
                {
                    ModelState.AddModelError("Phone", "Please provide a valid phone number");
                    ViewBag.BarangayList = FunctionHelper.GetBarangayList().Select((value, index) => new { value, index }).Select(x => new SelectListItem() { Value = x.index.ToString(), Text = x.value });
                    return Redirect("/Admin/ManageAccount");
                }
                phoneNumber = formattedPhoneNumber;
            }
            _userRepository.CreateOrUpdate(new UserViewModel() { Id = user.Id, Email = user.Email, Password = user.Password, Role =  FunctionHelper.GetRoleList()[int.Parse(user.Role)], Barangay = FunctionHelper.GetBarangayList()[int.Parse(user.Barangay)], StreetAddress = user.StreetAddress, Phone = phoneNumber, Profile = user.Profile });
            return Redirect("/Admin/ManageAccount");
        }

        public IActionResult RemoveUser(string id)
        {
            return Json(new { Success = _userRepository.RemoveUser(id) });
        }
        public IActionResult GetBarangay(string barangay, string role)
        {
            var b = FunctionHelper.GetBarangayList().Select((value, index) => new { value, index }).Where(x=> x.value == barangay).FirstOrDefault();
            var r = FunctionHelper.GetRoleList().Select((value, index) => new { value, index }).Where(x=> x.value == role).FirstOrDefault();
            return Json(new { Success = true, Barangay = b.index, Role = r.index });
        }

    }
}