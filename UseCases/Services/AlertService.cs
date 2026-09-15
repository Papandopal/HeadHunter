using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace UseCases.Services 
{
    public class AlertService(IHttpContextAccessor httpContextAccessor)
    {
        public void RaiseAlert(string message, AlertTypes type)
        {
            httpContextAccessor.HttpContext?.Items.Add("Alert", message);
            httpContextAccessor.HttpContext?.Items.Add("AlertType", type);
        }

        public void RaiseSuccess(string message = "Success")
        {
            httpContextAccessor.HttpContext?.Items.Add("Alert", message);
            httpContextAccessor.HttpContext?.Items.Add("AlertType", AlertTypes.Success);
        }
    }
}
