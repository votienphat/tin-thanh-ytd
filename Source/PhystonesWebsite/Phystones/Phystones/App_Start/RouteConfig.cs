using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyUtility.Extensions;
using Phystones.Helper;
using Phystones.Models.Enum;

namespace Phystones
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("assets/{*pathInfo}");

            routes.MapRoute(
              name: RouteName.ArticleDetail.Text(),
              url: "article/{textid}.html",
              defaults:
                  new { controller = "Article", action = "ArticleDetail", textid = UrlParameter.Optional }
              );

            routes.MapRoute(
              name: GlobalHelper.GetEnglishRouteName(RouteName.Work),
              url: "work",
              defaults:
                  new { controller = "Work", action = "Index" }
              );
            routes.MapRoute(
              name: GlobalHelper.GetVietnameseRouteName(RouteName.Work),
              url: "du-an",
              defaults:
                  new { controller = "Work", action = "Index" }
              );

            routes.MapRoute(
              name: GlobalHelper.GetEnglishRouteName(RouteName.About),
              url: "about",
              defaults:
                  new { controller = "About", action = "Index" }
              );
            routes.MapRoute(
              name: GlobalHelper.GetVietnameseRouteName(RouteName.About),
              url: "doi-ngu",
              defaults:
                  new { controller = "About", action = "Index" }
              );

            routes.MapRoute(
              name: GlobalHelper.GetEnglishRouteName(RouteName.Article),
              url: "blog",
              defaults:
                  new { controller = "Feed", action = "Index" }
              );
            routes.MapRoute(
              name: GlobalHelper.GetVietnameseRouteName(RouteName.Article),
              url: "bai-viet",
              defaults:
                  new { controller = "Feed", action = "Index" }
              );
            routes.MapRoute(
              name: GlobalHelper.GetEnglishRouteName(RouteName.ArticleDetail),
              url: "blog/{textid}",
              defaults:
                  new { controller = "Article", action = "ArticleDetail" }
              );
            routes.MapRoute(
              name: GlobalHelper.GetVietnameseRouteName(RouteName.ArticleDetail),
              url: "bai-viet/{textid}",
              defaults:
                  new { controller = "Article", action = "ArticleDetail" }
              );

            routes.MapRoute(
              name: GlobalHelper.GetEnglishRouteName(RouteName.Register),
              url: "register",
              defaults:
                  new { controller = "Register", action = "Index" }
              );
            routes.MapRoute(
              name: GlobalHelper.GetVietnameseRouteName(RouteName.Register),
              url: "chu-ky-so",
              defaults:
                  new { controller = "Register", action = "Index" }
              );

            routes.MapRoute(
              name: GlobalHelper.GetEnglishRouteName(RouteName.Contact),
              url: "contact",
              defaults:
                  new { controller = "Contact", action = "Index" }
              );
            routes.MapRoute(
              name: GlobalHelper.GetVietnameseRouteName(RouteName.Contact),
              url: "lien-he",
              defaults:
                  new { controller = "Contact", action = "Index" }
              );


            routes.MapRoute(
                name: "LocalizedDefault",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", lang = "vi-VN", id = UrlParameter.Optional },
                constraints: new { lang = "vi-vn|en-us" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", lang = "vi-VN", id = UrlParameter.Optional },
                namespaces: new[] { "Phystones.Controllers" }
            );
        }
    }
}
