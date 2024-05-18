using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class AuthenticationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        // Check if the user is authenticated
        if (!HttpContext.Current.Request.Cookies.AllKeys.Contains("UserID"))
        {
            // Check if the current action is the login action to avoid redirection loop
            var action = filterContext.ActionDescriptor.ActionName;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (!(controller.Equals("Home", StringComparison.OrdinalIgnoreCase) && action.Equals("Login", StringComparison.OrdinalIgnoreCase)))
            {
                // User is not authenticated and not on the login page, redirect to the login page
                filterContext.Result = new RedirectResult("~/Home/Login");
                return;
            }
        }

        base.OnActionExecuting(filterContext);
    }
}
