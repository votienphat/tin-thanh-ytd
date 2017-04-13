using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject.MembershipModule.Enums;
using BussinessObject.Helper;
using BussinessObject.MembershipModule.Contract;
using BussinessObject.MembershipModule.Models.Response;
using DataAccess.Contract.Membership;
using EntitiesObject.Entities.MetroMembershipEntities;
using MyUtility.Extensions;
using Newtonsoft.Json;

namespace BussinessObject.MembershipModule
{
    public class MemberBusiness : IMemberBusiness
    {
        #region Varriables

        private readonly IMemberPermissionRepository _memberPermissionRepo;
        private readonly IMemberAdminRepository _memberAdminRepo;
        //private readonly IMemberBusiness _memberBusiness;

        #endregion

        #region Constructor

        public MemberBusiness(IMemberPermissionRepository memberPermissionRepo, IMemberAdminRepository memberAdminRepo)
        {
            _memberPermissionRepo = memberPermissionRepo;
            _memberAdminRepo = memberAdminRepo;
        }

        #endregion

        /// <summary>
        /// Lấy danh sách quyền của user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 16/12/2015 PhatVT: Create new
        /// </history>
        public List<Ins_MemberPermission_GetPermissionByUser_Result> GetPermissionByUser(int userId)
        {
            return _memberPermissionRepo.GetPermissionByUser(userId);
        }

        /// <summary>
        /// Lấy thông tin user theo ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 17/12/2015 PhatVT: Create new
        /// </history>
        public Ins_MemberAdmin_getInfoMemberAdmin_Result GetAdminByID(int userId)
        {
            return _memberAdminRepo.GetByID(userId);
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginResponse Login(string username, string password)
        {
            var response = new LoginResponse();

            var memberAdmin = _memberAdminRepo.GetByNickname(username);

            // Kiểm tra tài khoản có tồn tại không
            if (memberAdmin == null || !memberAdmin.ID.HasValue)
            {
                response.Result = MembershipCode.UnexistedAccount;
                return response;
            }

            // Kiểm tra tài khoản có tồn tại không
            if (memberAdmin.IsLockedOut != false)
            {
                response.Result = MembershipCode.LockedAccount;
                return response;
            }

            var encodePassword = BoCommon.EncodePassword(password, memberAdmin.PasswordSalt);

            // Kiểm tra mật khẩu
            if (!encodePassword.Equals(memberAdmin.Password))
            {
                response.Result = MembershipCode.Failed;
                return response;
            }

            // Tạo token mới và lưu vào db
            var newToken = Guid.NewGuid().ToString();

            // Lấy thời gian hết hạn, mặc định là 1 ngày
            var dateExpire = DateTime.Now.AddDays(1);

            // Cập nhật token
            var userId = memberAdmin.ID.Value;
            _memberAdminRepo.UpdateToken(userId, newToken, dateExpire);

            // Set các thông tin trả về của user, lấy danh sách quyền của user
            memberAdmin.TokenID = newToken;
            memberAdmin.TokenExp = dateExpire;
            response.User = memberAdmin;

            response.Result = MembershipCode.Success;
            return response;
        }

        #region Administrator - TrungLD old
        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 18/12/2014</para>
        /// <para>lấy thông tin user dựa theo nickname</para>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="tokentUser"></param>
        /// <returns></returns>
        public List<Ins_MemberAdmin_checkLogin_Result> GetInfoMemberByNickName(string userName, string tokentUser)
        {
            return _memberAdminRepo.GetInfoMemberByNickName(userName, tokentUser);
        }

        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 19/12/2014</para>
        /// <para>cập nhật token cho user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newToken"></param>
        /// <param name="tokentExp"></param>
        /// <returns></returns>
        public int MemberAdmin_updateTokenUser(int userId, string newToken, DateTime tokentExp)
        {
            return _memberAdminRepo.MemberAdmin_updateTokenUser(userId, newToken, tokentExp);
        }

        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 19/12/2014</para>
        /// <para>lấy thông tin của user dựa theo userid</para>
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public Ins_MemberAdmin_getInfoMemberAdmin_Result MemberAdmin_getInfoMemberAdmin(int userid)
        {
            return _memberAdminRepo.GetByID(userid);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2014</para>
        /// <para>cập nhật thời han cho token</para>
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="dateExp"></param>
        /// <param name="token">token lấy từ client</param>
        /// <returns></returns>
        public bool MemberAdmin_updateTokenExp(int userid, DateTime dateExp, string token)
        {
            return _memberAdminRepo.MemberAdmin_updateTokenExp(userid, dateExp, token);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2014</para>
        /// <para>Kiểm tra quyền hạn</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public int MemberPermission_checkPermissionPage(int userId, string strUrl)
        {
            return _memberAdminRepo.MemberPermission_checkPermissionPage(userId, strUrl);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 23/12/2014</para>
        /// <para>lấy danh sách page được phép truy cập của user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<Ins_MemberPermission_getPagePermission_Result> MemberPermission_getPagePermission(int userId, int appId)
        {
            return _memberAdminRepo.MemberPermission_getPagePermission(userId, appId);
        }

        /// <summary>
        /// Lay danh sach user admin
        /// </summary>
        /// <returns></returns>
        public List<MemberAdmin> GetAllMemberAdmin()
        {
            return _memberAdminRepo.GetAllMemberAdmin();
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 19/01/2015</para>
        /// <para>lấy danh sách page được phép truy cập của user</para>
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="keyId"></param>
        /// <returns></returns>
        public Ins_AdminConfigData_getConfigByAdminID_Result AdminConfigData_getConfigByAdminID(int adminId,
            int keyId, int areaId)
        {
            return _memberAdminRepo.AdminConfigData_getConfigByAdminID(adminId, keyId, areaId);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 19/01/2015</para>
        /// <para>tạo config Data cho admin</para>
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="keyId"></param>
        /// <param name="configData"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public bool UpdateAdminConfigData(int adminId, int keyId, string configData, string note, int areaId)
        {
            return _memberAdminRepo.UpdateAdminConfigData(adminId, keyId, configData, note, areaId);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 29/01/2015</para>
        /// <para>Lấy danh sách nhóm admin</para>
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalPage"></param>
        /// <returns></returns>
        public List<GroupMember> GetGroupMemberAdmin(int pageIndex, int pageSize, ref int totalPage)
        {
            return _memberAdminRepo.GetGroupMemberAdmin(pageIndex, pageSize, ref totalPage);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>save trạng thái của group admin</para>
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatusGroupMemberAdmin(int groupId, bool status)
        {
            return _memberAdminRepo.UpdateStatusGroupMemberAdmin(groupId, status);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>update tên của group admin</para>
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool UpdateNameGroupMemberAdmin(int groupId, string groupName)
        {
            return _memberAdminRepo.UpdateNameGroupMemberAdmin(groupId, groupName);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>insert group member admin</para>
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool InsertGroupMemberAdmin(string groupName, bool status)
        {
            return _memberAdminRepo.InsertGroupMemberAdmin(groupName, status);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>Lấy danh sách member admin</para>
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        public List<Ins_MemberAdmin_getListMember_Result> MemberAdmin_getListMember(int pageIndex, int pageSize,
            string keySearch)
        {
            return _memberAdminRepo.MemberAdmin_getListMember(pageIndex, pageSize, keySearch);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>Lấy danh sách quyen han cua user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Ins_MemberPermission_getPermissionUserByUserId_Result> GetpermissionPageByUserId(int userId)
        {
            return _memberAdminRepo.GetpermissionPageByUserId(userId);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 26/02/2015</para>
        /// <para>lấy page theo id</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PageFunction GetPageFunctionById(int id)
        {
            return _memberAdminRepo.GetPageFunctionById(id);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 26/02/2015</para>
        /// <para>cập nhật thông tin của page</para>
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="parentId"></param>
        /// <param name="pageName"></param>
        /// <param name="funcPage"></param>
        /// <param name="link"></param>
        /// <param name="linkUse"></param>
        /// <param name="referPage"></param>
        /// <param name="pageType"></param>
        /// <param name="userIdUpdate"></param>
        /// <param name="isEnable"></param>
        /// <param name="isTargetBlank"></param>
        /// <returns></returns>
        public bool UpdatePageFunction(int pageId, int parentId, string pageName, int funcPage, string link
            , string linkUse, int referPage, int pageType, int userIdUpdate, bool isEnable, bool isTargetBlank)
        {
            return _memberAdminRepo.UpdatePageFunction(pageId, parentId, pageName, funcPage, link, linkUse, referPage,
                pageType, userIdUpdate, isEnable, isTargetBlank);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 26/02/2015</para>
        /// <para>insert thông tin của page</para>
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="pageName"></param>
        /// <param name="status"></param>
        /// <param name="funcPage"></param>
        /// <param name="link"></param>
        /// <param name="linkUse"></param>
        /// <param name="pageType"></param>
        /// <param name="isEnable"></param>
        /// <param name="isTarget"></param>
        /// <param name="newId"></param>
        /// <returns></returns>
        public ResultPageFunctionEnum InsertPageFucntion(int parentId, string pageName, int status,
            int funcPage
            , string link, string linkUse, int pageType, bool isEnable, bool isTarget, ref int newId)
        {
            var iResult = 0;
            try
            {
                var objResult = _memberAdminRepo.InsertPageFucntion(parentId, pageName, status, funcPage, link,
                linkUse
                , pageType, isEnable, isTarget);
                if (objResult != null && objResult.pageID.HasValue)
                {
                    iResult = objResult.result.GetValueOrDefault();
                    newId = objResult.pageID.GetValueOrDefault();
                }
            }
            catch (Exception)
            {
                iResult = -1001;
            }

            return iResult.ToEnum<ResultPageFunctionEnum>();
        }

        /// <summary>
        /// Author:TrungLD
        /// DateCreated: 26/02/2015
        /// lấy quyền đã cấp cho user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<MemberPermission> GetPermissionUser(int userId)
        {
            return _memberAdminRepo.GetPermissionUser(userId);
        }

        /// <summary>
        /// Author:TrungLD
        /// DateCreated: 27/02/2015
        /// cấp quyền nhanh quyền cho user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listPage"></param>
        /// <param name="delimiter"></param>
        /// <param name="defaultPage"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public ResultPageFunctionEnum MemberPermission_InsertPermissionMember(int userId, string listPage, string delimiter,
            bool defaultPage, int rule)
        {
            int iResult;
            try
            {
                iResult = _memberAdminRepo.MemberPermission_InsertPermissionMember(userId, listPage, delimiter,
                defaultPage, rule);
            }
            catch (Exception)
            {
                iResult = -1001;
            }
            return iResult.ToEnum<ResultPageFunctionEnum>();
        }

        /// <summary>
        /// Author:TrungLD
        /// DateCreated: 27/02/2015
        /// Cập nhật quyền hạn trên page cho user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageId"></param>
        /// <param name="rules"></param>
        /// <param name="defaultPage"></param>
        /// <returns></returns>
        public ResultPageFunctionEnum MemberPermission_UpdateRuleUser(int userId, int pageId, int rules, bool defaultPage)
        {
            int iResult;
            try
            {
                iResult = _memberAdminRepo.MemberPermission_UpdateRuleUser(userId, pageId, rules, defaultPage);
            }
            catch (Exception)
            {
                iResult = -1001;
            }
            return iResult.ToEnum<ResultPageFunctionEnum>();
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 02/03/2015</para>
        /// lấy thông tin admin thông qua userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MemberAdmin GetInfoMemberAdmin(int userId)
        {
            return _memberAdminRepo.GetOne(userId);
        }

        /// <summary>
        /// Author:TrungLD
        /// DateCreated: 02/03/2015
        /// tạo hoặc edit user đồng thời cấp quyền theo group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="nickname"></param>
        /// <param name="fullname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="passsalt"></param>
        /// <param name="isLock"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ResultMemberPermissionEnum MemberAdmin_SaveMemberAdmin(int userId, string nickname, string fullname, string email
            , string password, string passsalt, bool isLock, int groupId)
        {
            int iResult;
            try
            {
                iResult = _memberAdminRepo.MemberAdmin_SaveMemberAdmin(userId, nickname, fullname, email,
                    password, passsalt, isLock, groupId);
            }
            catch (Exception)
            {
                iResult = -1001;
            }
            return iResult.ToEnum<ResultMemberPermissionEnum>();
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated:02/03/2015</para>
        /// <para>lấy toàn bộ danh sách Group admin</para>
        /// </summary>
        /// <returns></returns>
        public List<GroupMember> GetAllGroupMemberAdmin()
        {
            return _memberAdminRepo.GetAllGroupMemberAdmin();
        }
        public List<Ins_GroupMember_GetAllGroupMemberAdmin_Result> GetAllGroupMemberAdmin1()
        {
            return _memberAdminRepo.GetAllGroupMemberAdmin1();
        }

        public bool ChangePasswordMemberAdmin(int userId, string password)
        {
            return _memberAdminRepo.ChangePasswordMemberAdmin(userId, password);
        }

        /// <summary>
        /// <para>Author: PhatVT</para>
        /// <para>Ngày tạo: 21/05/2015</para>
        /// <para>Lấy danh sách các page, action mà user có quyền sử dụng</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetPermissions(int userId)
        {
            return _memberAdminRepo.GetPermissions(userId);
        }
        #endregion

        #region Quản lý thông tin user

        /// <summary>
        /// TanPVD: 2015-06-01
        /// </summary>
        /// <param name="rowStart"></param>
        /// <param name="rowEnd"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        /// Danh sách user admin
        public List<Ins_MemberAdmin_GetListUser_Result> MemberAdmin_GetListUser(int rowStart, int rowEnd, int orderBy, int orderDirection, out int totalRow)
        {
            return _memberAdminRepo.MemberAdmin_GetListUser(rowStart, rowEnd, orderBy, orderDirection, out totalRow);
        }

        /// <summary>
        /// TanPVD: 2015-06-01
        /// </summary>
        /// <returns></returns>
        /// Danh sách tạo Quyền cho user
        public List<Ins_PageFunction_ListMenuUser_Result> PageFunction_ListMenuUser(int userId)
        {
            return _memberAdminRepo.PageFunction_ListMenuUser(userId).ToList();
        }

        /// <summary>
        /// TanPVD: 2015-06-10
        /// </summary>
        /// <returns></returns>
        /// Danh sách cấp Quyền cho nhiều user
        public List<Ins_PageFunction_ListMenuPageForGroup_Result> PageFunction_ListMenuPageForGroup(int userId)
        {
            return _memberAdminRepo.PageFunction_ListMenuPageForGroup(userId).ToList();
        }

        /// <summary>
        /// Tanpvd: 2015-06-01
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="rules"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        ///  cập nhật quyền cho user
        public bool UpdateAndInsertUserPermission(int id, int userId, int rules, int check)
        {
            return _memberAdminRepo.UpdateAndInsertUserPermission(id, userId, rules, check);
        }

        /// <summary>
        /// TanPVD: 2015-05-26
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// lấy thông tin user bởi tên
        public MemberAdmin AdminUserManagerGetInfoByUserName(string userName)
        {
            return _memberAdminRepo.AdminUserManagerGetInfoByUserName(userName);
        }
        #endregion

        #region Administrator - Chọn 1 quyền rồi cấp cho nhiều user
        /// <summary>
        /// TanPVD:2015-05-22
        /// </summary>
        /// <returns></returns>
        /// Lấy danh sách thành viên
        public List<Ins_MemberAdmin_GetListMemberByPermisstionUser_Result> GetListMemberByPermisstionUser(int pageId, int rules, int rowStart, int rowEnd, int orderBy, int orderDirection, out int totalRow)
        {
            return _memberAdminRepo.GetListMemberByPermisstionUser(pageId, rules, rowStart, rowEnd, orderBy, orderDirection, out totalRow);
        }
        #endregion

        #region Clone quyền

        /// <summary>
        /// TanPVD:2015-07-06
        /// </summary>
        /// <param name="userIdClone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// clone quyền cho user
        public bool MemberPermission_CloneUser(int userIdClone, int userId)
        {
            return _memberAdminRepo.MemberPermission_CloneUser(userIdClone, userId);
        }

        /// <summary>
        /// TanPVD:2015-07-06
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// clone quyền của nhóm cho user
        public bool GroupPermission_CloneGroup(List<int> userIds, int groupId)
        {
            return _memberAdminRepo.GroupPermission_CloneGroup(userIds, groupId);
        }
        #endregion

        #region Xóa quyền user

        /// <summary>
        /// TanPVD:2015-07-07
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// Xóa quyền user
        public bool MemberPermission_Delete(int userId, int pageId, int type)
        {
            return _memberAdminRepo.MemberPermission_Delete(userId, pageId, type);
        }
        #endregion

        #region TanPVD - Administrator - Quản lý nhóm
        /// <summary>
        /// TanPVD: 2015-05-26
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// update trạng thái page
        public bool UpdateAdminStatusPageFunction(int id, bool status)
        {
            return _memberAdminRepo.UpdateAdminStatusPageFunction(id, status);
        }
        #endregion

        public List<Ins_MemberAdmin_GetListUserAdmin_Result> GetListUserAdmin(int areaId)
        {
            return _memberAdminRepo.GetListUserAdmin(areaId);
        }
    }
}