using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

    }

    public class RedirectUnauthorizedToLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // If user is not authenticated, redirect to the login page
                filterContext.Result = new RedirectResult("~/Home/Login");
            }
        }
    }
}
