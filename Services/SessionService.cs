using Capstone.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Capstone.Services
{
    public class SessionService : ISessionService
    {
        public string GetItems(string key, HttpContext httpContext)
        {
            return httpContext.Session.GetString(key);
        }

        public void RemoveItems(string key, HttpContext httpContext)
        {
            httpContext.Session.Remove(key);
        }

        public void SetItems(string key, string value, HttpContext httpContext)
        {
            if (string.IsNullOrEmpty(value))
                return;

            httpContext.Session.SetString(key, value);
        }
    }
}
