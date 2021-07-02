using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShivamSingh_DotNetDeveloper
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Students",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Student", action = "AllStudent", id = UrlParameter.Optional }
            );
        }
    }
}
