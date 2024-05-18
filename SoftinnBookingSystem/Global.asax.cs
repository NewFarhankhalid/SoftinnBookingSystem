using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Optimization;
using System.Web.Routing;

namespace SoftinnBookingSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            General.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public class FilterConfig
        {
            public static void RegisterGlobalFilters(GlobalFilterCollection filters)
            {
                filters.Add(new HandleErrorAttribute());
                filters.Add(new AuthenticationFilterAttribute()); // Apply the custom authentication filter
            }
        }


    }
}
