using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;

namespace KaraSys.Helper.WebUtility
{
    public static class WebUtility
    {
        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 22/12/2014</para>
        /// tạo antiforgerytoken for post ajax
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString AntiForgeryTokenForAjaxPost(this HtmlHelper helper)
        {
            var antiForgeryInputTag = helper.AntiForgeryToken().ToString();
            // Above gets the following: <input name="__RequestVerificationToken" type="hidden" value="PnQE7R0MIBBAzC7SqtVvwrJpGbRvPgzWHo5dSyoSaZoabRjf9pCyzjujYBU_qKDJmwIOiPRDwBV1TNVdXFVgzAvN9_l2yt9-nf4Owif0qIDz7WRAmydVPIm6_pmJAI--wvvFQO7g0VvoFArFtAR2v6Ch1wmXCZ89v0-lNOGZLZc1" />
            var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
            var tokenValue = removedStart.Replace(@""" />", "");
            if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
                throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");
            return new MvcHtmlString(string.Format(@"{0}:""{1}""", "__RequestVerificationToken", tokenValue));
        }

        public static string Controller(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string Action(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }

        /// <summary>
        /// Lay Ip truc tiep khong uu tien Ip proxy
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddressDirect()
        {
            HttpContext ct = HttpContext.Current;
            string sIPAddress = string.Empty;
            try
            {
                sIPAddress = ct.Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(sIPAddress))
                {
                    return ct.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else
                {
                    string[] ipArray = sIPAddress.Split(new char[] { ',' });
                    return ipArray[0];
                }
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// PhatVT: Lấy domain [không có subdomain]
        /// </summary>
        /// <returns></returns>
        public static string GetDomain(string url)
        {
            string domain = string.Empty;
            try
            {
                string[] hostParts = new Uri(url).Host.Split('.');
                domain = String.Join(".", hostParts.Skip(Math.Max(0, hostParts.Length - 2)).Take(2));
            }
            catch{}
            return domain;
        }

        /// <summary>
        /// PhatVT: Lấy domain [có subdomain]
        /// </summary>
        /// <returns></returns>
        public static string GetFullDomain(string url)
        {
            string domain = string.Empty;
            try
            {
                domain = new Uri(url).Host;
            }
            catch { }
            return domain;
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 24/01/2015</para>
        /// <para>Hàm dùng chung cho paging</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByProperty"></param>
        /// <param name="isAscendingOrder"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public static IQueryable<T> PagedResult<T, TResult>(IQueryable<T> query, int pageNum, int pageSize,
                Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount)
        {
            if (pageSize <= 0) pageSize = 20;

            //Total result count
            rowsCount = query.Count();

            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            //Calculate nunber of rows to skip on pagesize
            int excludedRows = (pageNum - 1) * pageSize;

            query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);

            //Skip the required rows for the current page and take the next records of pagesize count
            return query.Skip(excludedRows).Take(pageSize);
        }

        public static KeyValuePair<string, object> GetRouteName(string keyGet)
        {
            var t = new ViewContext();
            return t.RouteData.Values.FirstOrDefault(x => x.Key == keyGet);
        }

        public static string RenderToString(this PartialViewResult partialView)
        {
            var httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                throw new NotSupportedException("An HTTP context is required to render the partial view to a string");
            }

            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                using (var tw = new HtmlTextWriter(sw))
                {
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, tw), tw);
                }
            }

            return sb.ToString();
        }
        public static string ReplaceDomain(string url, string newDomain)
        {
            try
            {
                var newUrl = new UriBuilder(url);
                var uri = new Uri(url);
                newUrl = new UriBuilder(uri) { Host = newDomain };
                url = newUrl.Uri.AbsoluteUri;
            }
            catch { }
            return url;
        }


        public static bool GetLocationIpAddress(string apiGetJsonIp,string nodeGet,string valueCompare,string ipaddress)
        {
            try
            {
                //WebClient client = new WebClient();
                //string jsonstring = client.DownloadString(apiGetJsonIp);
                //dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
                //var countryCode = dynObj.countryCode;
                //return countryCode.ToString().Equals(valueCompare);
                //Initializing a new xml document object to begin reading the xml file returned
                XmlDocument doc = new XmlDocument();
                doc.Load(apiGetJsonIp);
                XmlNodeList nodeValue = doc.GetElementsByTagName(nodeGet);
                return nodeValue[0].InnerText.Equals(valueCompare);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
