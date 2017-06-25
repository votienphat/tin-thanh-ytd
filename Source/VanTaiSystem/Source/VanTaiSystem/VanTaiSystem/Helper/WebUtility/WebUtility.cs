using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using MyConfig;

namespace VanTaiSystem.Helper.WebUtility
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
        /// Lấy domain [không có subdomain]
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
        /// Lấy domain [có subdomain]
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
        public static int GetFristSortColumns(List<int> listCol)
        {
            if (listCol != null && listCol.Count > 0)
            {
                return listCol[0];
            }
            return 0;
        }
        public static string GetFristSortDir(List<string> listDir)
        {
            if (listDir != null && listDir.Count > 0)
            {
                return listDir[0];
            }
            return "asc";
        }

        public static string GetTextID(string title, int maxLength)
        {
            title = UnicodeToAscii(title);
            if (title.Length > maxLength)
                title = title.Substring(0, maxLength).Trim();

            if (title.EndsWith("-"))
                title = title.Substring(0, title.Length - 1);

            return title;
        }

        public static String UnicodeToAscii(String strUnicode)
        {
            var strB = new StringBuilder(strUnicode);

            string[] unicodeChar = {
                "\u1EF9", "\u1EF8", "\u1EF7", "\u1EF6", "\u1EF5", "\u1EF4",
                "\u1EF3", "\u1EF2", "\u1EF1", "\u1EF0", "\u1EEF", "\u1EEE", "\u1EED", "\u1EEC", "\u1EEB",
                "\u1EEA", "\u1EE9", "\u1EE8", "\u1EE7", "\u1EE6", "\u1EE5", "\u1EE4", "\u1EE3", "\u1EE2",
                "\u1EE1", "\u1EE0", "\u1EDF", "\u1EDE", "\u1EDD", "\u1EDC", "\u1EDB", "\u1EDA", "\u1ED9",
                "\u1ED8", "\u1ED7", "\u1ED6", "\u1ED5", "\u1ED4", "\u1ED3", "\u1ED2", "\u1ED1", "\u1ED0",
                "\u1ECF", "\u1ECE", "\u1ECD", "\u1ECC", "\u1ECB", "\u1ECA", "\u1EC9", "\u1EC8", "\u1EC7",
                "\u1EC6", "\u1EC5", "\u1EC4", "\u1EC3", "\u1EC2", "\u1EC1", "\u1EC0", "\u1EBF", "\u1EBE",
                "\u1EBD", "\u1EBC", "\u1EBB", "\u1EBA", "\u1EB9", "\u1EB8", "\u1EB7", "\u1EB6", "\u1EB5",
                "\u1EB4", "\u1EB3", "\u1EB2", "\u1EB1", "\u1EB0", "\u1EAF", "\u1EAE", "\u1EAD", "\u1EAC",
                "\u1EAB", "\u1EAA", "\u1EA9", "\u1EA8", "\u1EA7", "\u1EA6", "\u1EA5", "\u1EA4", "\u1EA3",
                "\u1EA2", "\u1EA1", "\u1EA0", "\u01B0", "\u01AF", "\u01A1", "\u01A0", "\u0169", "\u0168",
                "\u0129", "\u0128", "\u0111", "\u0103", "\u0102", "\u00FD", "\u00FA", "\u00F9", "\u00F5",
                "\u00F4", "\u00F3", "\u00F2", "\u00ED", "\u00EC", "\u00EA", "\u00E9", "\u00E8", "\u00E3",
                "\u00E2", "\u00E1", "\u00E0", "\u00DD", "\u00DA", "\u00D9", "\u00D5", "\u00D4", "\u00D3",
                "\u00D2", "\u0110", "\u00CD", "\u00CC", "\u00CA", "\u00C9", "\u00C8", "\u00C3", "\u00C2",
                "\u00C1", "\u00C0"
        };

            string[] asciiChar = {
                "y", "Y", "y", "Y", "y", "Y", "y", "Y", "u", "U", "u",
                "U", "u", "U", "u", "U", "u", "U", "u", "U", "u", "U", "o", "O",
                "o", "O", "o", "O", "o", "O", "o", "O", "o", "O", "o", "O", "o",
                "O", "o", "O", "o", "O", "o", "O", "o", "O", "i", "I", "i", "I", "e",
                "E", "e", "E", "e", "E", "e", "E", "e", "E", "e", "E", "e", "E", "e",
                "E", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A",
                "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "u", "U",
                "o", "O", "u", "U", "i", "I", "d", "a", "A", "y", "u", "u", "o", "o", "o",
                "o", "i", "i", "e", "e", "e", "a", "a", "a", "a", "Y", "U", "U", "O", "O",
                "O", "O", "D", "I", "I", "E", "E", "E", "A", "A", "A", "A"
        };

            for (int i = 0; i < asciiChar.Length; i++)
                strB.Replace(unicodeChar[i], asciiChar[i]);

            string strInput = strB.ToString().ToLower();
            strB = new StringBuilder();

            foreach (char c in strInput)
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || (c == '-'))
                    strB.Append(c);
                else
                    strB.Append("-");

            return Regex.Replace(strB.ToString(), @"-+", "-");
        }
        public static string ContentVersion(this UrlHelper urlHelper, string relativePath)
        {
            return urlHelper.Content(relativePath) + "?v=" + MyConfiguration.Default.ContentVersion;
        }
    }
}
