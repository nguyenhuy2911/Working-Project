using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ecommerce.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProductDetail",
                url: "san-pham/{alias}-{id}",
                defaults: new
                {
                    controller = "SanPham",
                    action = "Index",
                    id = UrlParameter.Optional,
                    alias = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Category",
                url: "danh-muc/{alias}/{branch}",
                defaults: new
                {
                    controller = "Category",
                    action = "Index",
                    alias = UrlParameter.Optional,
                    branch = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
