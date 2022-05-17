using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Services.IServices
{
    public interface ISessionService
    {
        void SetItems(string key, string value ,HttpContext httpContext);
        string GetItems(string key, HttpContext httpContext);
        void RemoveItems(string key, HttpContext httpContext);
    }
}
