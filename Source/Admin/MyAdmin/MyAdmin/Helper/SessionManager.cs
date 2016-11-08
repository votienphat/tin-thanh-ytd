using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessObject.MembershipModule;
using BusinessObject.MembershipModule.Contract;
using BussinessObject.MembershipModule.Contract;
using MyAdmin.Ioc;
using MyAdmin.Models.Account;
using Logger;

namespace MyAdmin.Helper
{
    public class SessionManager
    {
        #region Private Variables

        private const string UserSessionKey = "UserData";
        private static IMemberBusiness _memberBusiness = IoC.Resolve<IMemberBusiness>();

        #endregion

        #region Public Variables

        /// <summary>
        /// Thông tin user
        /// </summary>
        public static AdminAccount SessionData
        {
            get
            {
                if (HttpContext.Current.Session[UserSessionKey] == null)
                {
                    var userDataClient = SplitClientUser();

                    if (userDataClient == null)
                    {
                        return null;
                    }

                    if (userDataClient.UserId > 0)
                        LoadSession();
                }

                return HttpContext.Current.Session[UserSessionKey] == null
                    ? null
                    : (HttpContext.Current.Session[UserSessionKey] as AdminAccount);
            }
            set
            {
                HttpContext.Current.Session[UserSessionKey] = value;
            }
        }

        /// <summary>
        /// Quyền của user
        /// </summary>
        public static List<AdminPermission> Permissions
        {
            get
            {
                var account = SessionData;
                if (account != null)
                {
                    return account.Permissions ?? new List<AdminPermission>();
                }

                return new List<AdminPermission>();
            }
            set
            {
                var account = SessionData;
                if (account != null)
                {
                    account.Permissions = value;
                    SessionData = account;
                }
            }
        }

        /// <summary>
        /// Danh sách menu của user
        /// </summary>
        public static List<AdminMenu> Menus
        {
            get
            {
                var account = SessionData;
                if (account != null)
                {
                    return account.Menus ?? new List<AdminMenu>();
                }

                return new List<AdminMenu>();
            }
            set
            {
                var account = SessionData;
                if (account != null)
                {
                    account.Menus = value;
                    SessionData = account;
                }
            }
        }

        /// <summary>
        /// Kiểm tra user đã đăng nhập hay chưa
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return
                    SessionData != null && SessionData.UserId > 0 &&
                HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Chứng thực cho user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="rememberMe"></param>
        /// <param name="dateExp"></param>
        public static void AuthenticateRequest(int userId, string token, bool rememberMe, DateTime? dateExp = null)
        {
            const string userRole = "user";
            var dateExpire = dateExp ?? (rememberMe
                ? DateTime.Now.AddMinutes(60 * 24 * 30)
                : DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout));
            var data = string.Format("{0}|{1}", userId, token);

            var ticket = new FormsAuthenticationTicket(1, data, DateTime.Now, dateExpire, true, userRole);
            var cookiestr = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr) { Expires = ticket.Expiration };
            if (rememberMe)
                cookie.Expires = ticket.Expiration;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Domain = FormsAuthentication.CookieDomain;
            HttpContext.Current.Response.Cookies.Add(cookie);

            LoadSession(data);
        }

        /// <summary>
        /// Author: ThongNT
        /// <para>Xoa Current Session - Sign Out</para>
        /// </summary>
        public static void SignOut()
        {
            // Update het han token cho user
            if (SessionData != null)
            {
                SessionData = null;
                FormsAuthentication.SignOut();
            }

            // Xóa danh sách quyền trong session
            Permissions = null;
        }

        /// <summary>
        /// Kiểm tra xem user có quyền thực hiện tính năng không
        /// </summary>
        /// <param name="methodName">Tên page, gồm controller và action</param>
        /// <returns></returns>
        public static bool HasRight(string methodName)
        {
            bool result = false;
            try
            {
                // Nếu user đã đăng nhập thì mới kiểm tra quyền
                if (IsAuthenticated && SessionData != null)
                {
                    var permissions = Permissions;

                    // Để đồng bộ giữa các method có dấu / và không có dấu /
                    // set thêm biến có dấu / nếu như methodName chưa có để kiểm tra cả 2
                    var splitMethodName = methodName;
                    if (methodName.StartsWith("/"))
                    {
                        splitMethodName = methodName.Substring(1);
                    }
                    result = permissions != null &&
                             (permissions.Exists(
                                 x => !string.IsNullOrEmpty(x.PageLink) &&
                                     (x.PageLink.Equals(methodName, StringComparison.OrdinalIgnoreCase) ||
                                     x.PageLink.Equals(splitMethodName, StringComparison.OrdinalIgnoreCase))));
                }
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("HasRight", ex);
            }

            return result;
        }

        /// <summary>
        /// Kiểm tra xem user có quyền thực hiện tính năng không
        /// </summary>
        /// <param name="pageId">Id của page, action</param>
        /// <returns></returns>
        public static bool HasRight(int pageId)
        {
            bool result = false;
            try
            {
                // Nếu user đã đăng nhập thì mới kiểm tra quyền
                if (IsAuthenticated && SessionData != null)
                {
                    result = Permissions.Exists(x => x.PageId == pageId);
                }
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("HasRight", ex);
            }

            return result;
        }

        /// <summary>
        /// Refresh để lấy lại danh sách quyền của user
        /// </summary>
        /// <returns></returns>
        public static List<AdminPermission> RefreshPermissions(int? userId = null)
        {
            var currentUserId = 0;

            if (userId.HasValue)
            {
                currentUserId = userId.Value;
            }
            else
            {
                var account = SessionData;
                if (account != null)
                {
                    currentUserId = account.UserId;
                }
            }
            if (currentUserId <= 0)
            {
                return new List<AdminPermission>();
            }

            var pages = _memberBusiness.GetPermissionByUser(currentUserId);

            // Lưu danh sách quyền
            List<AdminPermission> permissions = pages.Select(x => new AdminPermission
            {
                PageId = x.ID,
                PageLink = x.Link,
                PageName = x.Name,
                ParentId = x.ParentID,
                AppId = x.AppID,
                OrderNo = x.SortNum
            })
                .ToList();

            // Lưu danh sách menu
            // cái này khác quyền ở chỗ không lấy action
            List<AdminPermission> menuPages = pages.Where(x => x.PageType == 1)
                .Select(x => new AdminPermission
                {
                    PageId = x.ID,
                    PageLink = x.Link,
                    PageName = x.Name,
                    ParentId = x.ParentID,
                    AppId = x.AppID,
                    OrderNo = x.SortNum
                })
                .ToList();

            Permissions = permissions;
            Menus = GetSubMenus(menuPages);

            return permissions;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Author: ThongNT
        /// <para>Xoa tat ca session cua user (Khong signout)</para>
        /// </summary>
        public static void ClearAllSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        private static InfoUserClient SplitClientUser(string infoClient = null)
        {
            infoClient = infoClient ?? HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(infoClient))
            {
                return null;
            }

            var items = infoClient.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var data = new InfoUserClient
            {
                UserId = items.Any() ? int.Parse(items[0]) : 0,
                TokenId = items.Length > 1 ? items[1] : null
            };

            return data.UserId > 0 && !string.IsNullOrEmpty(data.TokenId) ? data : null;
        }

        /// <summary>
        /// Author: ThongNT
        /// <para>Load thong ti user tu database vao sesion data</para>
        /// </summary>
        /// <param name="clientData">Client data from cookie</param>
        private static void LoadSession(string clientData = null)
        {
            var data = SplitClientUser(clientData);

            if (data != null)
            {
                var userInfo = _memberBusiness.GetAdminByID(data.UserId);
                if (userInfo != null)
                {
                    SessionData = new AdminAccount
                    {
                        NickName = userInfo.NickName,
                        UserId = userInfo.ID,
                        Email = userInfo.Email,
                        TokenExp = data.TokenExp,
                        TokenId = data.TokenId,
                        GroupName = userInfo.GroupName,
                        GroupId = userInfo.GroupID,
                        FullName = userInfo.FullName
                    };
                    RefreshPermissions(userInfo.ID);
                }
            }
        }

        /// <summary>
        /// Đệ quy phân cấp menu
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private static List<AdminMenu> GetSubMenus(List<AdminPermission> pages, int? parentId = 0)
        {
            HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
            var subPages = pages
                .Where(m => m.ParentId == parentId)
                .Select(x => new AdminMenu
                {
                    PageId = x.PageId,
                    PageName = x.PageName,
                    ParentId = x.ParentId,
                    AppId = x.AppId,
                    OrderNo = x.OrderNo,
                    SubMenus = new List<AdminMenu>(),
                    PageLink = UrlHelper.GenerateContentUrl("~/" + x.PageLink, httpContext)
                })
                .OrderBy(x => x.OrderNo)
                .ToList();

            foreach (var page in subPages)
            {
                page.SubMenus = pages.Any(m => m.ParentId == page.PageId) ? GetSubMenus(pages, page.PageId) : new List<AdminMenu>();
            }

            return subPages;
        }

        #endregion
    }
}