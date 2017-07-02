using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Phystones.Enum;
using MyUtility.Extensions;
using Phystones.Helper;

namespace Phystones
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("assets/{*pathInfo}");

            routes.MapRoute(
               name: "LocalizedDefault",
               url: "{lang}/{controller}/{action}",
               defaults: new { controller = "Home", action = "Index", lang = "vi-VN" },
               constraints: new { lang = "vi-vn|en-us" }
           );

            //routes.MapRoute(
            //  name: RouteName.JobDetail.Text(),
            //  url: "job/{textid}.html",
            //  defaults:
            //      new { controller = "Job", action = "JobDetail", textid = UrlParameter.Optional }
            //  );
            //routes.MapRoute(
            //  name: RouteName.WorkDetail.Text(),
            //  url: "work/{textid}.html",
            //  defaults:
            //      new { controller = "Job", action = "WorkDetail", textid = UrlParameter.Optional }
            //  );
            ////routes.MapRoute(
            ////  name: RouteName.ArticleDetail.Text(),
            ////  url: "article/{textid}.html",
            ////  defaults:
            ////      new { controller = "Job", action = "ArticleDetail", textid = UrlParameter.Optional }
            ////  );
            //routes.MapRoute(
            //  name: RouteName.PopupPortfolio.Text(),
            //  url: "popupPortfolio",
            //  defaults:
            //      new { controller = "Portfolio", action = "Content" }
            //  );
            //routes.MapRoute(
            // name: RouteName.Footer.Text(),
            // url: "FooterHtml",
            // defaults:
            //     new { controller = "Home", action = "Footer" }
            // );
            //routes.Add("ArticleDetail", new SeoFriendlyRoute("ArticleDetail/{id}",
            //    new RouteValueDictionary(new { controller = "Article", action = "ArticleDetail" }),
            //    new MvcRouteHandler()));

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "Phystones.Controllers" }

            //);
            //routes.MapRoute(
            //    name: "Article",
            //    url: "{controller}/{action}",
            //    defaults: new { controller = "Article", action = "Index", lang = "vi-VN" }
            //    );

        }
    }
}
