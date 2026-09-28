using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UseCases.Services.LocalizationServices.Interfaces;

namespace UseCases.Services.LocalizationServices
{
    public class UILocalizationService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : IUILocalizationService
    {
        void IUILocalizationService.ChangeUILanguage(string language)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Delete("UILanguage");
            httpContextAccessor.HttpContext.Response.Cookies.Append("UILanguage", language);
        }

        string IUILocalizationService.GetUILanguage()
        {
            var value = httpContextAccessor.HttpContext.Request.Cookies["UILanguage"];
            return (string.IsNullOrWhiteSpace(value) ? configuration["DefaultUILanguage"] : value) 
                ?? throw new Exception("DefaultUILanguage not set in configuration");
        }
    }
}
