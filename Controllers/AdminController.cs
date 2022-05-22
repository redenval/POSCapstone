using Capstone.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Capstone.Utilities;
using Capstone.Services.IServices;
using Capstone.Models;

namespace Capstone.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ISessionService _sessionService;

        public AdminController(IAdminRepository adminRepository, ISessionService sessionService)
        {
            _adminRepository = adminRepository;
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;

            if (userAccess.Equals(SessionKeys.UserAccessDefault))
            {
                return View("Login");
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Login(string email, string pass)
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;

            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
                return View("Index");

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
                return View("Login");

            if (_adminRepository.CheckAdminCredentials(email.Trim(), pass.Trim()))
            {
                _sessionService.SetItems(SessionKeys.UserAccess, SessionKeys.UserAccessAdmin, HttpContext);
                return View("Index");
            }
            else
                return View("Login");
        }

        public IActionResult Logout()
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                _sessionService.SetItems(SessionKeys.UserAccess, SessionKeys.UserAccessDefault, HttpContext);
            }
            return RedirectToAction("Index", "Home");
        }

        #region Product
        public IActionResult Product()
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;

            if(userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                return View("Product");
            }
            else
            {
                return View("Login");
            }
        }
        #endregion


        #region Account
        public IActionResult Account()
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                return View("Account");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Admin/Account/Add")]
        public IActionResult AccountAdd(AccountViewModel model)
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                _adminRepository.AddAccount(model);
                return View("Account");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Admin/Account/Edit")]
        public IActionResult AccountEdit(AccountViewModel model)
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                _adminRepository.EditAccount(model);
                return View("Account");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Admin/Account/Delete/{id}", Name = "AccountDelete")]
        public IActionResult AccountDelete(int id)
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                _adminRepository.DeleteAccount(id);
                return View("Account");
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion


        #region Inventory
        public IActionResult Inventory()
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                return View("Inventory");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Admin/Inventory/Add")]
        public IActionResult InventoryAdd(ItemViewModel model)
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                _adminRepository.AddInventoryItem(model);
                return View("Inventory");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Admin/Inventory/Edit")]
        public IActionResult InventoryEdit(ItemViewModel model)
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                _adminRepository.EditInventoryItem(model);
                return View("Inventory");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Admin/Inventory/Delete/{id}", Name = "InventoryDelete")]
        public IActionResult InventoryDelete(int id)
        {
            var userAccess = _sessionService.GetItems(SessionKeys.UserAccess, HttpContext) ?? SessionKeys.UserAccessDefault;
            if (userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                _adminRepository.DeleteInventoryItem(id);
                return View("Inventory");
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
