using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VanTaiSystem.Ioc;
using Logger;
using BussinessObject.MembershipModule.Contract;
using MyConfig;
using MyUtility;
using VanTaiSystem.Modules.Base.Enums;
using VanTaiSystem.Modules.Base.Models;
using VanTaiSystem.Modules.Membership.Models;

namespace VanTaiSystem.Helper
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

            return true;
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

            //var pages = _memberBusiness.GetPermissionByUser(currentUserId);

            List<AdminPermission> permissions = new List<AdminPermission>();
            //// Lưu danh sách quyền
            //List<AdminPermission> permissions = pages.Select(x => new AdminPermission
            //{
            //    PageId = x.ID,
            //    PageLink = x.Link,
            //    PageName = x.Name,
            //    ParentId = x.ParentID,
            //    AppId = x.AppID,
            //    OrderNo = x.SortNum
            //})
            //    .ToList();

            //// Lưu danh sách menu
            //// cái này khác quyền ở chỗ không lấy action
            //List<AdminPermission> menuPages = pages.Where(x => x.PageType == 1)
            //    .Select(x => new AdminPermission
            //    {
            //        PageId = x.ID,
            //        PageLink = x.Link,
            //        PageName = x.Name,
            //        ParentId = x.ParentID,
            //        AppId = x.AppID,
            //        OrderNo = x.SortNum
            //    })
            //    .ToList();

            //Permissions = permissions;
            //Menus = GetSubMenus(menuPages);

            return permissions;
        }

        #endregion

        #region Private Methods

        /// <summary>
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

        #region Validation Sign

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static ApiStatusCode ValidateSign(ApiBaseRequest request, int userId, params string[] parameters)
        {
            return ValidateRequest(request.Sign, userId, request.ApiName, request.Version, parameters);
        }

        /// <summary>
        /// Kiểm tra chữ ký
        /// </summary>
        /// <returns></returns>
        public static ApiStatusCode ValidateSign(string signClient, int userId, string version, string apiName, params string[] parameters)
        {
            return ValidateRequest(signClient, userId, apiName, version, parameters);
        }

        /// <summary>
        /// Kiểm tra chữ ký khi login
        /// </summary>
        /// <returns></returns>
        public static ApiStatusCode ValidateLoginSign(ApiBaseRequest request, params string[] parameters)
        {
            return ValidateLogin(request.Sign, request.Version, request.ApiName, parameters);
        }

        /// <summary>
        /// Kiểm tra chữ ký khi login
        /// </summary>
        /// <returns></returns>
        public static ApiStatusCode ValidateLoginSign(string signClient, string version, string apiName, params string[] parameters)
        {
            return ValidateLogin(signClient, version, apiName, parameters);
        }

        /// <summary>
        /// Kiểm tra chữ ký khi login
        /// </summary>
        /// <returns></returns>
        public static ApiStatusCode ValidateLogin(string signClient, string version, string apiName, params string[] parameters)
        {
            if (string.IsNullOrEmpty(signClient))
            {
                return ApiStatusCode.InvalidSign;
            }

            var signOrganic = parameters.Aggregate((s, str) => s + str);
            signOrganic = apiName + signOrganic + version;

            var signEncrypt = Common.GetMd5Hash(signOrganic).ToLower();
            return !signClient.Equals(signEncrypt) ? ApiStatusCode.InvalidSign : ApiStatusCode.Success;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static ApiStatusCode ValidateRequest(string signClient, int userId, string apiName, string version, params string[] parameters)
        {
            //    CommonLogger.DefaultLogger.Debug("signClient: " + signClient + "|userId: " + userId + "|versionAppClient: " +
            //                                     versionAppClient + "|deviceIdClient: " + deviceIdClient +
            //                                     "|platformID: " + platformID + "|apiName: " + apiName + "|parameters: " +
            //                                     JsonConvert.SerializeObject(parameters));

            if (string.IsNullOrEmpty(signClient))
            {
                return ApiStatusCode.InvalidSign;
            }

            var sessionInfo = GetSessionData(userId, version);
            if (sessionInfo == null)
                return ApiStatusCode.NotLogin;

            if (sessionInfo.TokenExpire < DateTime.Now)
                return ApiStatusCode.TokenExpire;

            var signOrganic = parameters.Length > 0 ? parameters.Aggregate((s, str) => s + str) : string.Empty;
            signOrganic = apiName + signOrganic + userId + version + sessionInfo.Token;

            var signEncrypt = Common.GetMd5Hash(signOrganic);

            return !signClient.Equals(signEncrypt, StringComparison.OrdinalIgnoreCase) ? ApiStatusCode.InvalidSign : ApiStatusCode.Success;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static string GetSignForTest(ApiBaseRequest request, int userId, params string[] parameters)
        {
            if (MyConfiguration.Default.EnableDebug)
            {
                var sessionInfo = GetSessionData(userId, request.Version);
                if (sessionInfo == null || sessionInfo.TokenExpire < DateTime.Now)
                    return string.Empty;

                var signOrganic = parameters.Length > 0 ? parameters.Aggregate((s, str) => s + str) : string.Empty;
                signOrganic = request.ApiName + signOrganic + userId + request.Version + sessionInfo.Token;

                var signEncrypt = Common.GetMd5Hash(signOrganic);

                return signEncrypt;
            }

            return string.Empty;
        }

        public static string GetSignLogin(ApiBaseRequest request, params string[] parameters)
        {
            if (MyConfiguration.Default.EnableDebug)
            {
                var signOrganic = parameters.Aggregate((s, str) => s + str);
                signOrganic = request.ApiName + signOrganic + request.Version;

                var signEncrypt = Common.GetMd5Hash(signOrganic).ToLower();
                return signEncrypt;
            }

            return string.Empty;
        }

        /// <summary>
        /// Tạo chuỗi mã hóa
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="lastLogin"></param>
        /// <returns></returns>
        private static string GetSign(string token, string userId, DateTime lastLogin)
        {
            var unixTime = lastLogin.GetUnixTimeStamp();
            return Common.GetMd5Hash(string.Format("{0}|{1}|{2}", token, userId, unixTime));
        }

        /// <summary>
        /// Kiểm tra chữ ký
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="lastLogin"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private static bool IsValidSign(string token, string userId, DateTime lastLogin, string sign)
        {
            var unixTime = lastLogin.GetUnixTimeStamp();
            var currentSign = Common.GetMd5Hash(string.Format("{0}|{1}|{2}", token, userId, unixTime));
            return currentSign.Equals(sign);
        }
        private static SessionInfo GetSessionData(int userId, string version)
        {
            var sessionKey = "Session_" + userId;

            try
            {
                var sessionInfo = HttpRuntime.Cache[sessionKey] ?? LoadSessionFromDatabase(userId, version);
                return sessionInfo as SessionInfo;
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("GetSessionData", ex);
                return null;
            }
        }

        public static SessionInfo LoadSessionFromDatabase(int userId, string version)
        {
            SessionInfo sessionInfo = null;
            //var userMobileInfo = _memberBusiness.GetToken(userId);
            //var userMobileInfo = _memberBusiness.GetToken(userId);
            //if (userMobileInfo != null)
            //{
            //    sessionInfo = new SessionInfo
            //    {
            //        UserId = userMobileInfo.ID,
            //        Token = userMobileInfo.TokenID,
            //        TokenExpire = userMobileInfo.TokenExp.GetValueOrDefault(),
            //    };
            //}

            return sessionInfo;
        }

        #endregion

        #endregion
    }
}