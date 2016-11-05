using System;
using System.Web;
using System.Web.Mvc;
using BusinessObject.Enums;
using BusinessObject.Helper;
using BusinessObject.MembershipModule.Contract;
using BusinessObject.MembershipModule.Enums;
using MyAdmin.ActionFilter;
using MyAdmin.Helper;
using MyAdmin.Models;
using MyAdmin.Models.Account;
using MyUtility.Extensions;

namespace MyAdmin.Controllers
{
    public class AccountController : BaseController
    {
        #region Variables

        private IMemberBusiness _memberBusiness;
        public AccountController(IMemberBusiness memberBusiness)
        {
            _memberBusiness = memberBusiness;
        }

        #endregion

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (SessionManager.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            var response = new BaseResponse
            {
                Code = ResponseCode.Failed.Value()
            };
            if (ModelState.IsValid)
            {
                var result = _memberBusiness.Login(model.Username, model.Password);
                response.Code = result.Result.Value();
                if (result.Result == MembershipCode.Success && result.User.ID.HasValue)
                {
                    SessionManager.AuthenticateRequest(result.User.ID.Value, result.User.TokenID, model.RememberMe, result.User.TokenExp);
                    var returnUrl = HttpUtility.UrlDecode(Request.QueryString.Get("returnUrl"));
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return new JsonResult() { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    return Redirect(returnUrl);
                }
            }
            ViewBag.Status = response.Code;
            return View();
        }
        [HeaderAuthorizeFilter(IsCheckPermission = false)]
        public ActionResult LogOut()
        {
            SessionManager.SignOut();
            return RedirectToAction("Login");
        }

        [HeaderAuthorizeFilter(IsCheckPermission = false)]
        public ActionResult Refresh()
        {
            string url = "/";
            if(HttpContext.Request != null && HttpContext.Request.Url != null && !string.IsNullOrEmpty(HttpContext.Request.Url.GetLeftPart(UriPartial.Authority)))
                url = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
            SessionManager.RefreshPermissions();
            return Redirect(url);
        }

        /// <summary>
        /// Lấy danh sách menu mà user có thể sử dụng
        /// </summary>
        /// <returns></returns>
        [HeaderAuthorizeFilter(IsCheckPermission = false)]
        public JsonResult GetMenus()
        {
            var pages = SessionManager.Menus;
            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        [HeaderAuthorizeFilter(IsCheckPermission = false)]
        public JsonResult RefreshPermissions()
        {
            SessionManager.RefreshPermissions();
            return null;
        }

        [HeaderAuthorizeFilter(IsCheckPermission = false)]
        public ActionResult GetUserProfile()
        {
            var adminInfo = _memberBusiness.MemberAdmin_getInfoMemberAdmin(SessionManager.SessionData.UserId);
            var result = new AdminMemberInfo
            {
                FullName = adminInfo.FullName,
                Email = adminInfo.Email,
                GroupName = adminInfo.GroupName,
                NickName = adminInfo.NickName,
                NamePage = adminInfo.NamePage
            };

            return View(result);
        }
        
        [HeaderAuthorizeFilter(IsCheckPermission = false)]
        public JsonResult ChangePassword(string oldPassword,string newPassword)
        {
            Session["Status"] = null;
            var userName = SessionManager.SessionData.NickName;
            var adminID = SessionManager.SessionData.UserId;
            var response = new BaseResponse
            {
                Code = ResponseCode.Failed.Value()
            };

            if (ModelState.IsValid)
            {
                // Gọi hàm đăng nhập
                var result = _memberBusiness.Login(userName, oldPassword);

                // Nếu đăng nhập đc thì tiến hành đổi password
                if (result.Result == MembershipCode.Success && result.User.ID.HasValue)
                {
                    var passwordSalt = result.User.PasswordSalt;
                    var passwordEncrpt = BoCommon.EncodePassword(newPassword, passwordSalt);
                    var resultChangePassword = _memberBusiness.ChangePasswordMemberAdmin(adminID, passwordEncrpt);
                    if (resultChangePassword)
                    {
                        response.Code = Convert.ToInt32(resultChangePassword);
                        return Json(new{result=true}, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            //Session["Status"] = "Sai mật khẩu";
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("GetUserProfile", "Account", new { userID = adminID });
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoginPopup(LoginModel model)
        {
            var response = new BaseResponse
            {
                Code = ResponseCode.Failed.Value()
            };

            if (ModelState.IsValid)
            {
                // Gọi hàm đăng nhập
                var result = _memberBusiness.Login(model.Username, model.Password);
                response.Code = result.Result.Value();
                // Nếu đăng nhập thành công thì lưu session
                if (result.Result == MembershipCode.Success && result.User.ID.HasValue)
                {
                    SessionManager.AuthenticateRequest(result.User.ID.Value, result.User.TokenID, model.RememberMe, result.User.TokenExp);
                    response = new BaseResponse
                    {
                        Code = ResponseCode.Success.Value()
                    };
                }
            }
            return new JsonResult() { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}