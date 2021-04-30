using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
         
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
               name: "Product Category",
               url: "san-pham/{meltatitle}-{id}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
           );
            routes.MapRoute(
             name: "Search",
             url: "Tim-kiem",
             defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
              name: "Product Detail",
              url: "chi-tiet/{meltatitle}-{id}",
              defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );
            routes.MapRoute(
              name: "Content Detail",
              url: "tin-tuc/{meltatitle}-{id}",
              defaults: new { controller = "Content", action = "Details", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );
            routes.MapRoute(
          name: "About",
          url: "gioi-thieu",
          defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
          namespaces: new[] { "OnlineShop.Controllers" }
      );
            routes.MapRoute(
        name: "News",
        url: "tin-tuc",
        defaults: new { controller = "Content", action = "LContent", id = UrlParameter.Optional },
        namespaces: new[] { "OnlineShop.Controllers" }
    );
            routes.MapRoute(
        name: "Contact",
        url: "lien-he",
        defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
        namespaces: new[] { "OnlineShop.Controllers" }
    );
            routes.MapRoute(
        name: "Home",
        url: "trang-chu",
        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
        namespaces: new[] { "OnlineShop.Controllers" }
    );
            routes.MapRoute(
  name: "Add Cart",
  url: "them-gio-hang",
  defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
  namespaces: new[] { "OnlineShop.Controllers" }
);
            routes.MapRoute(
name: "Cart",
url: "gio-hang",
defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
namespaces: new[] { "OnlineShop.Controllers" }
);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
        }
    }
}
