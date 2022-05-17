using Capstone.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Capstone.Utilities;
using Capstone.Services.IServices;

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
            }else
            {
                return View("Index");
            }
        }

        public IActionResult Login(string email, string pass)
        {
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
            if(userAccess.Equals(SessionKeys.UserAccessAdmin))
            {
                _sessionService.SetItems(SessionKeys.UserAccess, SessionKeys.UserAccessDefault, HttpContext);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
