using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace UseCases.Services 
{
    public class AlertService(IHttpContextAccessor httpContextAccessor)
    {
        public void RaiseAlert(string message, AlertTypes type)
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Append("Alert", message);
            httpContextAccessor.HttpContext?.Response.Cookies.Append("AlertType", type.ToString());
        }

        public void RaiseSuccess(string message = "Success")
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Append("Alert", message);
            httpContextAccessor.HttpContext?.Response.Cookies.Append("AlertType", AlertTypes.Success.ToString());
        }
    }
}
