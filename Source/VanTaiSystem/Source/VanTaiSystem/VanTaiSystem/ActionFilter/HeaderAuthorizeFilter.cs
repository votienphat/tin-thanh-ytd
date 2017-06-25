using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;
using VanTaiSystem.Modules.Base.Enums;
using MyConfig;
using VanTaiSystem.Helper;

namespace VanTaiSystem.ActionFilter
{

    public class HeaderAuthorizeFilter : ActionFilterAttribute
    {
        public bool IsCheckPermission;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Nếu method cho phép Anonymous thì không cần kiểm tra
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false))
            {
                return;
            }

            var url = string.Empty;
            var routeValues = filterContext.HttpContext.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("controller"))
                url = (string)routeValues["controller"];
            if (routeValues.ContainsKey("action"))
                url += "/" + (string)routeValues["action"];

            var area = filterContext.RouteData.DataTokens["area"] == null
                ? string.Empty
                : filterContext.RouteData.DataTokens["area"].ToString();
            if (!string.IsNullOrEmpty(area))
            {
                url = area + "/" + url;
            }

            //if (!SessionManager.IsAuthenticated)
            //{
            //    if (filterContext.HttpContext.Request.IsAjaxRequest())
            //    {
            //        var result = new JsonResult
            //        {
            //            Data = new
            //            {
            //                Status = ApiStatusCode.Unauthorized
            //            }
            //        };
            //        filterContext.Result = result;
            //    }
            //    else
            //    {
            //        filterContext.Result =
            //            new RedirectResult(FormsAuthentication.LoginUrl + "?returnUrl=" + HttpUtility.UrlEncode("/" + url));
            //    }
            //}
            //else
            //{
            //    var hasRight = SessionManager.HasRight(url);
            //    if (!hasRight && MyConfiguration.Default.IsCheckPermission && IsCheckPermission)
            //    {
            //        if (filterContext.HttpContext.Request.IsAjaxRequest())
            //        {
            //            var result = new JsonResult
            //            {
            //                Data = new
            //                {
            //                    Status = ApiStatusCode.Unauthorized
            //                }
            //            };
            //            filterContext.Result = result;
            //        }
            //        else
            //        {
            //            var customErrorsSection = (CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors");
            //            var page403 = customErrorsSection.Errors.Get("403");
            //            filterContext.Result = new RedirectResult(page403 == null ? "~/Home" : page403.Redirect);
            //        }
            //    }
            //}
        }
    }

    internal class Http403Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            // Set the response code to 403.
            context.HttpContext.Response.StatusCode = 403;
        }
    }

    public class MetroOverwriteHeaderAuthorizeFilter : HeaderAuthorizeFilter, IOverrideFilter
    {
        public Type FiltersToOverride { get { return typeof(IActionFilter); } }
    }
}