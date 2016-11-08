using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessObject.MembershipModule.Contract;
using BusinessObject.MembershipModule.Enums;
using BusinessObject.MembershipModule.Models;
using BussinessObject.Helper;
using BussinessObject.MembershipModule.Contract;
using EntitiesObject.Entities.MetroMembershipEntities;
using InsideBanCa.Models.Account;
using MyAdmin.Controllers;
using MyAdmin.Helper;
using MyAdmin.Helper.DataTables;
using MyUtility;
using MyUtility.Extensions;

namespace MyAdmin.Areas.MembershipModule.Controllers
{
    public class MembershipController : BaseController
    {
        private readonly IMemberBusiness _memberBusiness;
        private readonly IPageBusiness _pageBusiness;
        private readonly ILogActionAdminBusiness _logActionAdminBusiness;
        public MembershipController(IMemberBusiness memberBusiness, IPageBusiness pageBusiness, ILogActionAdminBusiness logActionAdminBusiness)
        {
            _memberBusiness = memberBusiness;
            _pageBusiness = pageBusiness;
            _logActionAdminBusiness = logActionAdminBusiness;
        }

        #region Quản lý page

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>hiển thị danh sách tất cả page và action</para>
        /// </summary>
        /// <returns></returns>
        public ActionResult PageFunctionManager()
        {
            return View();
        }

        public JsonResult GetPages()
        {
            var pages = _pageBusiness.GetPages();
            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>cập nhật lại page admin</para>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdatePageAdmin(PageFunctionModel models)
        {
            var objResult = _memberBusiness.UpdatePageFunction(models.PageId, models.ParentId.GetValueOrDefault(0), models.PageName,
                models.FunctionPage,
                models.Link, models.LinkUse, models.ReferPage, models.PageType, SessionManager.SessionData.UserId,
                models.IsEnable, models.IsTargetBlank);
            return Json(new
            {
                Status = objResult,
                Message = objResult ? "Update Thành công." : "Update thất bại, có thể tên page bị trùng."
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertPageAdmin(PageFunctionModel models)
        {
            var newId = 0;
            string message;
            var objResult = _memberBusiness.InsertPageFucntion(models.ParentId.GetValueOrDefault(0), models.PageName, 0,
                models.FunctionPage, models.Link, models.LinkUse, models.PageType, models.IsEnable, models.IsTargetBlank, ref newId);
            switch (objResult)
            {
                case ResultPageFunctionEnum.Success:
                    message = "Insert thành công.";
                    break;
                case ResultPageFunctionEnum.Fail:
                    message = "Insert thất bại, xin thử lại sau.";
                    break;
                case ResultPageFunctionEnum.NameExists:
                    message = "Tên page đã được sử dụng trước đó, thay đổi tên page khác";
                    break;
                case ResultPageFunctionEnum.SqlErorr:
                    message = "hệ thông dữ liệu gặp sự cố, xin thử lại sau.";
                    break;
                case ResultPageFunctionEnum.SystemErorr:
                    message = "Hệ thông bị lỗi, xin thử lại sau.";
                    break;
                default:
                    message = "Insert thất bại, xin thử lại sau";
                    break;
            }
            return Json(new
            {
                Status = objResult == ResultPageFunctionEnum.Success,
                Message = message,
                Data = newId
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Trang cấp quyền cho user

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// show các quyền
        public ActionResult AdminPermissionForManyUser(AdminUserModel model)
        {
            return View(model);
        }

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// danh sách cấp Quyền cho nhiều user
        public JsonResult AdminGetPermissionForManyUser(AdminUserModel model)
        {
            var pages = _pageBusiness.GetPages(true);

            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// show danh sách user
        public ActionResult AdminUserForManyUser(AdminUserModel model)
        {
            return PartialView(model);
        }

        #endregion

        #region Quản lý thông tin user

        public ActionResult AdminUserManager(AdminUserModel model)
        {
            return View(model);
        }

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="dataTablesParam"></param>
        /// <returns></returns>
        /// Lấy danh sách user
        [HttpPost]
        public JsonResult GetListUser(DataTablesParam dataTablesParam)
        {
            var response = new DataTablesData
            {
                aaData = new object[0],
                sEcho = dataTablesParam.Draw,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0
            };
            var pageSize = dataTablesParam.Length;
            if (pageSize <= 0)
            {
                return Json(response);
            }

            var totalRow = 0;
            int pageIndex = pageSize > 0
                ? (int)Math.Ceiling((decimal)dataTablesParam.Start / dataTablesParam.Length)
                : 0;
            pageIndex++;
            int startPos = (pageIndex - 1) * pageSize + 1;
            int endPos = startPos + pageSize - 1;
            var logs = new List<Ins_MemberAdmin_GetListUser_Result>();

            if (pageSize > 0 && pageIndex > 0)
            {
                var orderBy = dataTablesParam.OrderColumn;
                var orderDescending = !dataTablesParam.IsAscOrdering ? 0 : 1;
                logs = _memberBusiness.MemberAdmin_GetListUser(orderBy, orderDescending, startPos, endPos, out totalRow);
            }

            var url = Url.Action("PopupAdminUserManager", "Membership");
            var list = logs.Select(l => new
            {
                l.STT,
                CreatedDate = l.Datecreated != null ? l.Datecreated.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                LastLoginDate = l.LastLoginDate != null ? l.LastLoginDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                NickName = "<a href='#' onclick=\"OpenPopup('Quản lý User','" + url + "?ID=" + l.ID + "', 850); return false;\" title=''>" + l.NickName + "</a>",
                Email = "<a href='#' onclick=\"OpenPopup('Quản lý User','" + url + "?ID=" + l.ID + "', 850); return false;\" title=''>" + l.Email + "</a>",
                l.ID,
                l.Avatar,
                l.FullName,
                l.GroupID,
                l.IsLockedOut,
                l.GroupName
            }).ToArray();

            response.sEcho = dataTablesParam.Draw;
            response.iTotalRecords = totalRow;
            response.iTotalDisplayRecords = totalRow;
            response.aaData = list.Cast<object>().ToArray();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Danh sách tạo quyền
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AdminPermissionUser(AdminUserModel model)
        {
            int total;
            var listUser = _memberBusiness.MemberAdmin_GetListUser(2, 1, 1, 1000, out total);
            var ar = _memberBusiness.GetInfoMemberAdmin(model.ID);
            var listModel = new AdminUserInfoModel
            {
                AdminUser = model,
                UserInfo = ar,
                ListUser = listUser
            };

            return PartialView(listModel);
        }

        //public ActionResult PopupAdminUserManager(AdminUserModel adminUserModel)
        //{
        //    var modelResponse = new PopubAdminUserManagerViewModel();

        //    if (adminUserModel.ID > 0) { 
        //        var ar = _memberBusiness.GetInfoMemberAdmin(adminUserModel.ID);
        //        adminUserModel.UserID = ar.ID;
        //        adminUserModel.NickName = ar.NickName;
        //    }
        //    modelResponse.AdminUserModel = adminUserModel;

        //    //get info add, update user
        //    var listInfo = _memberBusiness.GetInfoMemberAdmin(adminUserModel.UserID);
        //    //var listGroup = _memberBusiness.GetAllGroupMemberAdmin();
        //    var listGroup1 = _memberBusiness.GetAllGroupMemberAdmin1();
        //    var memberPermissionModel = new MemberPermissionInfoModel1
        //    {
        //        GroupMember = listGroup1
        //        ,
        //        MemberInfo = listInfo
        //    };
        //    modelResponse.MemberPermissionInfoModel1 = memberPermissionModel;

        //    // Danh sách tạo quyền
        //    int total;
        //    var listUser = _memberBusiness.MemberAdmin_GetListUser(2, 1, 1, 1000, out total);
        //    var arTemp = _memberBusiness.GetInfoMemberAdmin(adminUserModel.ID);
        //    var listModel = new AdminUserInfoModel
        //    {
        //        AdminUser = adminUserModel,
        //        UserInfo = arTemp,
        //        ListUser = listUser
        //    };
        //    modelResponse.AdminUserInfoModel = listModel;
        //    return PartialView("PopupAdminUserManagerV2", modelResponse);
        //}

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// danh sách tạo Quyền cho user
        public JsonResult AdminGetPermissionUser(AdminUserModel model)
        {
            string str = "";
            string sTitle = "";
            var logs = _memberBusiness.PageFunction_ListMenuPageForGroup(model.UserID);

            if (logs.Any())
            {
                int i = 1;
                foreach (var item in logs)
                {
                    var view = "";
                    var editView = "";
                    var viewDelete = "";
                    var viewEditDelete = "";
                    var delete = "";
                    if (item.FunctionPageType != null && item.IsEnable.HasValue && item.IsEnable.Value)
                    {
                        if (item.FunctionPageType == 1) //view
                        {
                            view = String.Format(@"<div class='classView'>  
                                                      <input  value='{1}' type='radio' class='btn btn-success btn-xs'  id='classView_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", (item.Rules == 1 ? "checked" : string.Empty), item.ID);

                            delete = (item.Rules == 1) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' type='button' class='btn btn-danger btn-xs'  id='classView_{1}' rules='1' name='classxoa_{1}' {0} onclick='MemberPermission_Delete({1});' />
                                                      <label for='classView_{1}'></label>
                                                    </div>", (item.Rules == 1 ? "checked" : string.Empty), item.ID)) : "";

                        }
                        else if (item.FunctionPageType == 2) //edit
                        {
                            view = String.Format(@"<div class='classView'>  
                                                      <input  value='{1}' type='radio' class='btn btn-success btn-xs'  id='classView_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", (item.Rules == 1 ? "checked" : string.Empty), item.ID);

                            editView = String.Format(@"<div class='classView'>  
                                                      <input value='{1}' type='radio' class='btn btn-success btn-xs'  id='classEditView_{1}' rules='3' name='classView_{1}' {0} />
                                                      <label for='classEditView_{1}'></label>
                                                    </div>", (item.Rules == 3 ? "checked" : string.Empty), item.ID);

                            delete = (item.Rules == 1) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) :
                                     (item.Rules == 3) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='3' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) : "";
                        }
                        else if (item.FunctionPageType == 4) //delete
                        {
                            view = String.Format(@"<div class='classView'>  
                                                      <input  value='{1}' type='radio' class='btn btn-success btn-xs' type='button' id='classView_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", (item.Rules == 1 ? "checked" : string.Empty), item.ID);

                            editView = String.Format(@"<div class='classView'>  
                                                      <input value='{1}' type='radio' class='btn btn-success btn-xs' type='button' id='classEditView_{1}' rules='3' name='classView_{1}' {0} />
                                                      <label for='classEditView_{1}'></label>
                                                    </div>", (item.Rules == 3 ? "checked" : string.Empty), item.ID);

                            viewDelete = String.Format(@"<div class='classView'>  
                                                      <input value='{1}' type='radio' class='btn btn-success btn-xs' type='button' id='classViewDelete_{1}' rules='5' name='classView_{1}' {0} />
                                                      <label for='classViewDelete_{1}'></label>
                                                    </div>", (item.Rules == 5 ? "checked" : string.Empty), item.ID);

                            viewEditDelete = String.Format(@"<div class='classView'>  
                                                      <input value='{1}' type='radio' class='btn btn-success btn-xs' type='button' id='classViewEditDelete_{1}' rules='7' name='classView_{1}' {0} />
                                                      <label for='classViewEditDelete_{1}'></label>
                                                    </div>", (item.Rules == 7 ? "checked" : string.Empty), item.ID);
                            delete = (item.Rules == 1) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) :
                                     (item.Rules == 3) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='3' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) :
                                    (item.Rules == 5) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='5' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) :
                                    (item.Rules == 7) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='7' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) : "";
                        }
                    }
                    else
                    {
                        if (item.FunctionPageType == 1 && item.IsEnable.HasValue && item.IsEnable.Value) //view
                        {
                            view = String.Format(@"<div class='classView'>  
                                                      <input  value='{1}' type='radio' class='btn btn-success btn-xs'  id='classView_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", (item.Rules == 1 ? "checked" : string.Empty), item.ID);

                            delete = (item.Rules == 1) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' type='button' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs'  id='classxoa_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", (item.Rules == 1 ? "checked" : string.Empty), item.ID)) : "";

                        }
                        else if (item.FunctionPageType == 2) //edit
                        {
                            view = String.Format(@"<div class='classView'>  
                                                      <input  value='{1}' type='radio' class='btn btn-success btn-xs'  id='classView_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", (item.Rules == 1 ? "checked" : string.Empty), item.ID);

                            editView = String.Format(@"<div class='classView'>  
                                                      <input value='{1}' type='radio' class='btn btn-success btn-xs'  id='classEditView_{1}' rules='3' name='classView_{1}' {0} />
                                                      <label for='classEditView_{1}'></label>
                                                    </div>", (item.Rules == 3 ? "checked" : string.Empty), item.ID);

                            delete = (item.Rules == 1) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) :
                                     (item.Rules == 3) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' class='btn btn-danger btn-xs' type='button' id='classView_{1}' rules='3' name='classxoa_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) : "";
                        }
                        else if (item.FunctionPageType == 4) //delete
                        {
                            view = String.Format(@"<div class='classView'>  
                                                      <input  value='{1}' type='radio' class='btn btn-success btn-xs' type='button' id='classView_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", (item.Rules == 1 ? "checked" : string.Empty), item.ID);

                            editView = String.Format(@"<div class='classView'>  
                                                      <input value='{1}' type='radio' class='btn btn-success btn-xs' type='button' id='classEditView_{1}' rules='3' name='classView_{1}' {0} />
                                                      <label for='classEditView_{1}'></label>
                                                    </div>", (item.Rules == 3 ? "checked" : string.Empty), item.ID);

                            viewDelete = String.Format(@"<div class='classView'>  
                                                      <input value='{1}' type='radio' class='btn btn-success btn-xs' type='button' id='classViewDelete_{1}' rules='5' name='classView_{1}' {0} />
                                                      <label for='classViewDelete_{1}'></label>
                                                    </div>", (item.Rules == 5 ? "checked" : string.Empty), item.ID);

                            viewEditDelete = String.Format(@"<div class='classView'>  
                                                      <input value='{1}' type='radio' class='btn btn-success btn-xs' type='button' id='classViewEditDelete_{1}' rules='7' name='classView_{1}' {0} />
                                                      <label for='classViewEditDelete_{1}'></label>
                                                    </div>", (item.Rules == 7 ? "checked" : string.Empty), item.ID);
                            delete = (item.Rules == 1) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='1' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) :
                                     (item.Rules == 3) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='3' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) :
                                    (item.Rules == 5) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' class='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='5' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) :
                                    (item.Rules == 7) ? (String.Format(@"<div class='classView'>  
                                                      <input  value='Xóa' pageId='{1}' onclick='MemberPermission_Delete({1});' sclass='btn btn-danger btn-xs' type='button' id='classxoa_{1}' rules='7' name='classView_{1}' {0} />
                                                      <label for='classView_{1}'></label>
                                                    </div>", string.Empty, item.ID)) : "";
                        }
                    }
                    if (item.IsEnable.HasValue && item.IsEnable.Value)
                    {
                        var isEnable = String.Format(@"<div class='classStatus'>  
                                                      <input value='{1}' type='checkbox' id='classStatus_{1}' value='None' name='classStatus' {0} />
                                                      <label for='classStatus_{1}'></label>
                                                    </div>",
                            (item.IsEnable.Value ? "checked" : string.Empty), item.ID);


                        //1: chi xem | 3: xem va edit | 5 : xem va xoa | 7 : xem, sua va xoa
                        str += "<tr role='row' class='odd'>" +
                               "<td class=' align-left'>" + i++ + "</td>" +
                               "<td class=' align-left'>" + item.ID + "</td>" +
                               "<td>" + item.Title + "</td>" +
                            /*"<td class=' align-center'>" + isEnable + "</td>" +*/
                               "<td class=' align-left'>" + view + "</td>" +
                               "<td class=' align-left'>" + editView + "</td>" +
                               "<td class=' align-left'>" + viewDelete + "</td>" +
                               "<td class=' align-left'>" + viewEditDelete + "</td>" +
                               "<td class=' align-left'>" + delete + "</td>" +
                               "</tr>";
                    }
                }
            }

            return Json(new { Success = 0, Data = str, Title = sTitle }, JsonRequestBehavior.AllowGet);


        }

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// cập nhật quyền cho user
        public JsonResult UpdateAndInsertUserPermission(AdminUserModel model)
        {
            // lấy userid khi mới thêm vào
            int userId = model.UserID == 0 ? _memberBusiness.AdminUserManagerGetInfoByUserName(model.NickName).ID : model.UserID;

            if (_memberBusiness.UpdateAndInsertUserPermission(model.PageID, userId, model.Rules, model.Check))
            {
                return Json(new { Success = 0, Message = "Cập nhật quyền cho user thành công." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = 1, Message = "Cập nhật quyền cho user không thành công." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// get info add, update user
        public ActionResult GetUserInsertUpdate(int userId)
        {
            var listInfo = _memberBusiness.GetInfoMemberAdmin(userId);
            //var listGroup = _memberBusiness.GetAllGroupMemberAdmin();
            var listGroup1 = _memberBusiness.GetAllGroupMemberAdmin1();
            var memberPermissionModel = new MemberPermissionInfoModel1
            {
                GroupMember = listGroup1
                ,
                MemberInfo = listInfo
            };
            return PartialView("AdminInsertUpdateUser", memberPermissionModel);
        }

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// add, update user
        [HttpPost]
        public JsonResult SaveUserAdmin(MemberPermissionModel model)
        {
            string message;
            var objResult = ResultMemberPermissionEnum.Fail;

            if (!ModelState.IsValid)
            {
                message = "Dữ liệu không hợp lệ.";
            }
            else
            {
                var passwordSalt = Guid.NewGuid().ToString();
                if (model.UserId > 0)
                {
                    var listInfo = _memberBusiness.GetInfoMemberAdmin(model.UserId);
                    if (listInfo != null)
                    {
                        passwordSalt = listInfo.PasswordSalt;
                    }
                    else
                    {
                        return Json(new
                        {
                            Status = false
                            ,
                            Message = "Lỗi dữ liệu member, thử f5 lại trước khi tạo/edit user."
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                ////////// TanPVD - edit
                var passwordEncrpt = "";
                if (!String.IsNullOrEmpty(model.Password))
                {
                    //passwordEncrpt = MyUtility.Common.sha256_hash(MyUtility.Common.MD5_encode(model.Password) + passwordSalt);

                    //TanPVD edit: 2016-03-08
                    passwordEncrpt = BoCommon.EncodePassword(model.Password, passwordSalt);
                }
                //////////

                objResult = _memberBusiness.MemberAdmin_SaveMemberAdmin(model.UserId, model.NickName,
                    model.FullName, model.Email, passwordEncrpt, passwordSalt, model.IsLock, model.GroupId);
                switch (objResult)
                {
                    case ResultMemberPermissionEnum.EmailExists:
                        message = "Email đã được sử dụng.";
                        break;
                    case ResultMemberPermissionEnum.Fail:
                        message = "Lưu thông tin thành viên thất bại.";
                        break;
                    case ResultMemberPermissionEnum.NickNameExists:
                        message = "NickName đã được sử dụng.";
                        break;
                    case ResultMemberPermissionEnum.SqlErorr:
                        message = "Hệ thông dữ liệu gặp sự cố, xin thử lại sau.";
                        break;
                    case ResultMemberPermissionEnum.Success:
                        message = "Lưu thông tin thành viên thành công.";
                        break;
                    case ResultMemberPermissionEnum.SystemErorr:
                        message = "Hệ thống bị lỗi, xin thử lại sau.";
                        break;
                    default:
                        message = "Lưu thông tin thành viên thất bại.";
                        break;
                }
            }

            return Json(new
            {
                Status = objResult == ResultMemberPermissionEnum.Success
                ,
                Message = message
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// cập nhật trạng thái cho page
        public JsonResult UpdateAdminStatusPageFunction(AdminGroupModel model)
        {
            if (_memberBusiness.UpdateAdminStatusPageFunction(model.ID, model.IsEnable))
            {
                return Json(new { Success = 0, Data = "Cập nhật trạng thái thành công." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = 1, Data = "Cập nhật trạng thái không thành công." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClonePermisstionUser(int userIdClone, int userId)
        {
            if (_memberBusiness.MemberPermission_CloneUser(userIdClone, userId))
            {
                return Json(new { Success = 0, Data = "", Message = "Clone quyền thành công." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = 1, Data = "", Message = "Clone quyền không thành công." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// TanPVD:2015-07-07
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// Xóa quyền user
        public JsonResult MemberPermission_Delete(int userId, int pageId, int type)
        {
            if (_memberBusiness.MemberPermission_Delete(userId, pageId, type))
            {
                return Json(new { Success = 0, Data = "", Message = "Xóa quyền thành công." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = 1, Data = "", Message = "Xóa quyền không thành công." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lịch sử khóa nick user
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 1/2016 Create By VuTVT
        /// </history>
        public ActionResult GetUserLockedLog()
        {
            return View();
        }
        #endregion

        #region Administrator - TrungLD old
        public ActionResult GroupAdminManager()
        {
            return PartialView();
        }
        public JsonResult GetGroupAdminManager()
        {
            var totalCount = 0;
            var list = _memberBusiness.GetGroupMemberAdmin(1, 111, ref totalCount);

            var result = list.Select(c => new
            {
                c.ID
               ,
                GroupName = String.Format(@"<a href='javascript:void(0)' class='groupName' rel='{0}' >{1}</span>", c.ID, c.GroupName)
               ,
                Status = String.Format(@"<div class='slideThree'>  
                              <input value='{1}' type='checkbox' id='slideThree_{1}' value='None' name='check' {0} />
                              <label for='slideThree_{1}'></label>
                            </div>", (c.Visible.HasValue && c.Visible.Value ? "checked" : string.Empty), c.ID)
               ,
                DateCreated = String.Format(@"{0:dd/MM/yyyy}", c.Datecreated)
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>thay doi trang thai cua group admin</para>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatusGroupAdmin(EditGroupMemberAdminModel model)
        {
            var objResult = _memberBusiness.UpdateStatusGroupMemberAdmin(model.GroupId, model.Status);
            string mess = objResult ? "Update Thành Công." : "Update Thất Bại. Xin thử lại sau.";
            return Json(new { Success = objResult, Message = mess }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>thay doi ten cua group admin</para>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditGroupAdmin(EditGroupMemberAdminModel model)
        {
            var objResult = _memberBusiness.UpdateNameGroupMemberAdmin(model.GroupId, model.GroupName);
            string mess = objResult ? "Update Thành Công." : "Update Thất Bại. Xin thử lại sau.";
            return Json(new { Success = objResult, Message = mess }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>insert group admin</para>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertGroupAdmin(EditGroupMemberAdminModel model)
        {
            model.Status = true; //luon hien thi group
            var objResult = _memberBusiness.InsertGroupMemberAdmin(model.GroupName, model.Status);
            string mess = objResult ? "Insert Thành Công." : "Insert Thất Bại. Xin thử lại sau.";
            return Json(new { Success = objResult, Message = mess }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPageFunctionManager(List<PageFunction> menu, int? parentid = 0)
        {
            var listFunctionPage = _pageBusiness.GetPages();
            var result = listFunctionPage.Select(x => new
            {
                text = x.PageName
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>Lấy danh sách page để cấp quyền</para>
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissionMember()
        {
            var listFunctionPage = _pageBusiness.GetPages().Where(r => r.PageType == 1).ToList();
            return PartialView("UserPermission", listFunctionPage);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>hiển thị danh sách tất cả member admin</para>
        /// </summary>
        /// <param name="dataTablesParam"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFilterMemberAdmin(JqueryDataTableParam dataTablesParam)
        {
            var response = new DataTablesData
            {
                aaData = new object[0],
                sEcho = 1,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0
            };
            var pageSize = dataTablesParam.iDisplayLength;
            if (pageSize <= 0)
            {
                return Json(response);
            }

            int pageIndex = pageSize > 0
                ? (int)Math.Ceiling((decimal)dataTablesParam.iDisplayStart / pageSize)
                : 0;
            pageIndex++;

            if (string.IsNullOrEmpty(dataTablesParam.sSearch))
                dataTablesParam.sSearch = "";
            var list = _memberBusiness.MemberAdmin_getListMember(pageIndex, pageSize, dataTablesParam.sSearch);
            if (list.Any())
            {
                // ReSharper disable once CoVariantArrayConversion
                response.aaData = list.Select(c => new
                {
                    c.ID,
                    Email =
                        String.Format(@"<a class='chooseItem' href='javascript:void(0);' ref='{0}' >{1}</a>", c.ID,
                            c.Email),
                    FullName =
                        String.Format(@"<a class='chooseItem' href='javascript:void(0);' ref='{0}' >{1}</a>", c.ID,
                            c.FullName),
                    c.GroupName,
                    LastLogin =
                        c.LastLoginDate.HasValue ? String.Format(@"{0:dd/MM/yyyy}", c.LastLoginDate) : string.Empty,
                    NickName =
                        String.Format(@"<a class='chooseItem' href='javascript:void(0);' ref='{0}' >{1}</a>", c.ID,
                            c.NickName),
                    c.RowNumber,
                    DateCreated =
                        c.CreateDated.HasValue ? String.Format(@"{0:dd/MM/yyyy}", c.CreateDated) : string.Empty,
                    EditMember =
                        String.Format(
                            @"<a class='editMember' href='javascript:void(0);' ref='{0}' data-title='{1}' data-url='{2}' >Edit</a>",
                            c.ID, "Edit Account Manager", Url.Action("GetUserInfo", "Membership", new { userId = c.ID }))
                }).ToArray();
                var totalCount = list[0].totalCount.HasValue && list[0].totalCount.Value > 0 ? list[0].totalCount.Value : 0;
                response.sEcho = dataTablesParam.sEcho;
                response.iTotalRecords = list.Count > 0 ? totalCount : 0;
                response.iTotalDisplayRecords = response.iTotalRecords;
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            return Json(response);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>lấy danh sách page admin và permission của user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult ShowPermissionUser(int userId)
        {
            var modelPermission = new PermissionUserModel
            {
                ListPageFunction = _pageBusiness.GetPages()
                ,
                ListPermission = _memberBusiness.GetpermissionPageByUserId(userId)
            };
            return PartialView("_permissionPage", modelPermission);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>lấy chi tiết quyền từng page</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPageFunctionById(int id)
        {
            var objResult = _memberBusiness.GetPageFunctionById(id);

            return Json(new { Status = objResult != null, Data = objResult }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPermissionUser(int userId)
        {
            var objResult = _memberBusiness.GetPermissionUser(userId);
            return Json(new
            {
                Status = objResult.Any(),
                Data = objResult
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertPermissionAdmin(ProvidePermissionUserModel models)
        {
            string message;
            var objResult = _memberBusiness.MemberPermission_InsertPermissionMember(models.UserId,
                models.ListPage, ",", false, 1);
            switch (objResult)
            {
                case ResultPageFunctionEnum.Success:
                    message = "Insert thành công.";
                    break;
                case ResultPageFunctionEnum.Fail:
                    message = "Insert thất bại, xin thử lại sau.";
                    break;
                case ResultPageFunctionEnum.SqlErorr:
                    message = "hệ thông dữ liệu gặp sự cố, xin thử lại sau.";
                    break;
                case ResultPageFunctionEnum.SystemErorr:
                    message = "Hệ thông bị lỗi, xin thử lại sau.";
                    break;
                default:
                    message = "Insert thất bại, xin thử lại sau";
                    break;
            }
            return Json(new
            {
                Status = objResult == ResultPageFunctionEnum.Success
                ,
                Message = message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateRuleUser(RulesPermissionUserModel models)
        {
            var objResult = _memberBusiness.MemberPermission_UpdateRuleUser(models.UserId, models.CurrentPage,
                models.Rules, models.PageDefault);
            string message;
            switch (objResult)
            {
                case ResultPageFunctionEnum.Success:
                    message = "Update thành công.";
                    break;
                case ResultPageFunctionEnum.Fail:
                    message = "Update thất bại, xin thử lại sau.";
                    break;
                case ResultPageFunctionEnum.SqlErorr:
                    message = "Hệ thông dữ liệu gặp sự cố, xin thử lại sau.";
                    break;
                case ResultPageFunctionEnum.SystemErorr:
                    message = "Hệ thông bị lỗi, xin thử lại sau.";
                    break;
                default:
                    message = "Update thất bại, xin thử lại sau";
                    break;
            }
            return Json(new
            {
                Status = objResult == ResultPageFunctionEnum.Success
                ,
                Message = message
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetUserInfo(int userId)
        {
            var listInfo = _memberBusiness.GetInfoMemberAdmin(userId);
            var listGroup = _memberBusiness.GetAllGroupMemberAdmin();
            var memberPermissionModel = new MemberPermissionInfoModel
            {
                GroupMember = listGroup
                ,
                MemberInfo = listInfo
            };
            return PartialView("_accountManager", memberPermissionModel);
        }

        [HttpPost]
        public JsonResult SaveMemberAdmin(MemberPermissionModel model)
        {
            string message;
            var objResult = ResultMemberPermissionEnum.Fail;

            if (!ModelState.IsValid)
            {
                message = "Dữ liệu không hợp lệ.";
            }
            else
            {
                var passwordSalt = Guid.NewGuid().ToString();
                if (model.UserId > 0)
                {
                    var listInfo = _memberBusiness.GetInfoMemberAdmin(model.UserId);
                    if (listInfo != null)
                    {
                        passwordSalt = listInfo.PasswordSalt;
                        model.Password = string.IsNullOrEmpty(model.Password) ? listInfo.Password : model.Password;
                    }
                    else
                    {
                        return Json(new
                        {
                            Status = false
                            ,
                            Message = "Lỗi dữ liệu member, thử f5 lại trước khi tạo/edit user."
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                var passwordEncrpt = MyUtility.Common.sha256_hash(MyUtility.Common.MD5_encode(model.Password) + passwordSalt);
                objResult = _memberBusiness.MemberAdmin_SaveMemberAdmin(model.UserId, model.NickName,
                    model.FullName, model.Email, passwordEncrpt, passwordSalt, model.IsLock, model.GroupId);
                switch (objResult)
                {
                    case ResultMemberPermissionEnum.EmailExists:
                        message = "Email đã được sử dụng.";
                        break;
                    case ResultMemberPermissionEnum.Fail:
                        message = "Lưu thông tin thành viên thất bại.";
                        break;
                    case ResultMemberPermissionEnum.NickNameExists:
                        message = "NickName đã được sử dụng.";
                        break;
                    case ResultMemberPermissionEnum.SqlErorr:
                        message = "Hệ thông dữ liệu gặp sự cố, xin thử lại sau.";
                        break;
                    case ResultMemberPermissionEnum.Success:
                        message = "Lưu thông tin thành viên thành công.";
                        break;
                    case ResultMemberPermissionEnum.SystemErorr:
                        message = "Hệ thống bị lỗi, xin thử lại sau.";
                        break;
                    default:
                        message = "Lưu thông tin thành viên thất bại.";
                        break;
                }
            }

            return Json(new
            {
                Status = objResult == ResultMemberPermissionEnum.Success
                ,
                Message = message
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="dataTablesParam"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// lấy danh sách thành viên đã có quyền hoặc chưa có quyền 
        [HttpPost]
        public ActionResult AdminGetListUserForManyUser(JqueryDataTableParam dataTablesParam, AdminUserModel model)
        {
            var response = new DataTablesData
            {
                aaData = new object[0],
                sEcho = dataTablesParam.sEcho,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0
            };
            var pageSize = dataTablesParam.iDisplayLength;
            if (pageSize <= 0)
            {
                return Json(response);
            }

            var totalRow = 0;
            int pageIndex = pageSize > 0
                ? (int)Math.Ceiling((decimal)dataTablesParam.iDisplayStart / dataTablesParam.iDisplayLength)
                : 0;
            pageIndex++;
            int startPos = (pageIndex - 1) * pageSize + 1;
            int endPos = startPos + pageSize - 1;
            var logs = new List<Ins_MemberAdmin_GetListMemberByPermisstionUser_Result>();

            if (pageSize > 0 && pageIndex > 0)
            {
                var orderBy = dataTablesParam.SortCollumn;
                var orderDescending = dataTablesParam.SortDir == "desc" ? 0 : 1;
                logs = _memberBusiness.GetListMemberByPermisstionUser(model.PageID, model.Rules, startPos, endPos, orderBy, orderDescending, out totalRow);
            }

            var list = logs.Select(l => l.CreateDate != null ? new
            {
                l.STT,
                l.Avatar,
                l.Email,
                l.GroupID,
                l.ID,
                l.LastLoginDate,
                l.NickName,
                CreateDate = l.CreateDate.Value.GetVnDateFormat(),
                IsLockedOut = l.IsLockedOut == false ? "<i class='glyphicon glyphicon-ok green'></i>" : "<i class='glyphicon glyphicon-remove'></i>",
                Option = String.Format(@"<input type='checkbox' {0} id='id_" + l.ID + "' groupId='" + l.GroupID + "' userId='" + l.ID + "' class='cb-status' onclick='UpdateAndInsertUserPermissionForManyUser(" + l.ID + ")'>", (l.Rules != 0 ? "checked" : string.Empty)),
                CheckGroup = l.GroupID != 0 ? "<i class='glyphicon glyphicon-ok green'></i>" : "<i class='glyphicon glyphicon-remove'></i>",

                //Lấy thông tin nhóm GroupName = l.GroupID != 0 ? _membe.AdminGroupManagerGetInfoByID(l.GroupID.Value).FirstOrDefault().GroupName : ""
                GroupName = "Nhóm tạm"
            } : null).ToArray();

            response.sEcho = dataTablesParam.sEcho;
            response.iTotalRecords = totalRow;
            response.iTotalDisplayRecords = totalRow;
            response.aaData = list.Cast<object>().ToArray();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// cập nhật quyền cho từng user -- edit lại action UpdateAndInsertUserPermission
        public JsonResult UpdateAndInsertUserPermissionForManyUser(AdminUserModel model)
        {
            // lấy userid khi mới thêm vào
            var userId = model.UserID == 0 ? _memberBusiness.AdminUserManagerGetInfoByUserName(model.NickName).ID : model.UserID;

            return Json(_memberBusiness.UpdateAndInsertUserPermission(model.PageID, userId, model.Rules, model.Check) 
                        ? new { Success = 0, Message = "Cập nhật quyền cho user thành công." } 
                        : new { Success = 1, Message = "Cập nhật quyền cho user không thành công." }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Clone quyền
        /// <summary>
        /// TanPVD
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// update trạng thái
        public ActionResult ClonePermisstionGroup(string ids, AdminGroupModel model)
        {
            var list = ids.Split(',').Select(c => c.ToInt()).ToList();
            return Json(_memberBusiness.GroupPermission_CloneGroup(list, model.GroupId));
        }

        #endregion

        #region Xóa quyền user
        #endregion
    }
}