using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace pavlikeMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           // routes.MapRoute(
           //    "Default",
           //    "AnaSayfa",
           //    new { controller = "Home", action = "Index" },
           //    new[] { "pavlikeMVC.Controllers" }
           //);
            routes.MapMvcAttributeRoutes();
        }
    }
}
