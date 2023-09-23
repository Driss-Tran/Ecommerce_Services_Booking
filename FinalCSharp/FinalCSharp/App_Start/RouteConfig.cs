using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinalCSharp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Register", "{type}",
                new { controller = "Login", action = "Register" },
                new RouteValueDictionary
                {
                    {"type","dang-ky" }
                },
                namespaces: new[] { "FinalCSharp.Controllers" });

            routes.MapRoute("Login", "{type}",
                new { controller = "Login", action = "Index"},
                new RouteValueDictionary
                {
                    {"type","dang-nhap" }
                },
                namespaces: new[] { "FinalCSharp.Controllers" });

            routes.MapRoute("Product", "{type}/{meta}",
                new{controller = "Product", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type","dich-vu" }
                },
                namespaces :new[] {"FinalCSharp.Controllers"});

            routes.MapRoute("ShoppingCart", "{type}",
                new { controller = "ShoppingCart", action = "Index"},
                new RouteValueDictionary
                {
                    {"type","gio-hang" }
                },
                namespaces: new[] { "FinalCSharp.Controllers" });

            routes.MapRoute("CheckOut", "{type}",
                new { controller = "ShoppingCart", action = "CheckOut" },
                new RouteValueDictionary
                {
                    {"type","thanh-toan" }
                },
                namespaces: new[] { "FinalCSharp.Controllers" });

            routes.MapRoute("Detail", "{type}/{name}/{meta}",
               new { controller = "Product", action = "Detail", meta = UrlParameter.Optional ,name = UrlParameter.Optional },
               new RouteValueDictionary
               {
                    {"type","dich-vu" },

               },
               namespaces: new[] { "FinalCSharp.Controllers" });

            routes.MapRoute("NewsDetail", "{type}/{meta}",
               new { controller = "News", action = "NewsDetail", meta = UrlParameter.Optional },
               new RouteValueDictionary
               {
                    {"type","tin-tuc" },

               },
               namespaces: new[] { "FinalCSharp.Controllers" });





            routes.MapRoute("Contact", "{type}",
                new { controller = "Home", action = "Contact" },
                new RouteValueDictionary
                {
                    {"type","lien-lac" }
                },
                namespaces: new[] { "FinalCSharp.Controllers" }
                );


            routes.MapRoute("About", "{type}",
                new {controller = "Home",action = "About"},
                new RouteValueDictionary
                {
                    {"type","chung-toi" }
                },
                namespaces : new[] {"FinalCSharp.Controllers"} 
                );
            routes.MapRoute("TempIndex", "{type}",
                new { controller = "Home", action = "Index" },
                new RouteValueDictionary
                {
                    {"type","trang-chu" }
                },
                namespaces: new[] { "FinalCSharp.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FinalCSharp.Controllers" }
            );


        }
    }
}
