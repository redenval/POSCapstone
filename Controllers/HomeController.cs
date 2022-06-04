using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Capstone.Services.IServices;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Utilities;
using Capstone.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IUserRepository _userRepository;

        public HomeController(ISessionService sessionService, IUserRepository userRepository)
        {
            _sessionService = sessionService;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {

            ViewBag.BarangayList = StaticUtilities.GetBarangayList().Select((value, index) => new { value, index }).Select(x => new SelectListItem() { Value = x.index.ToString(), Text = x.value });
            return View();
        }
        
        public IActionResult RegisterUser(UserViewModel user)
        {
            _userRepository.Create(new UserViewModel() { Email = user.Email, Password = user.Password, Barangay = StaticUtilities.GetBarangayList()[int.Parse(user.Barangay)], StreetAddress = user.StreetAddress, Phone = user.Phone, Profile = user.Profile});
            return RedirectToAction("Index");
        }
    }
}
