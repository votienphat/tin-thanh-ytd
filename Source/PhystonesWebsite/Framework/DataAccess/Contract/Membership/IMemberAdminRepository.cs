using System;
using System.Collections.Generic;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace DataAccess.Contract.Membership
{
    public interface IMemberAdminRepository : IDaoRepository<MemberAdmin>
    {
        /// <summary> 
        /// Lấy thông tin user theo nickname
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        /// <history>
        /// 17/12/2015 PhatVT: Create new
        /// </history>
        Ins_MemberAdmin_checkLogin_Result GetByNickname(string nickname);

        /// <summary>
        /// Lấy thông tin user theo ID
        /// </summary>
        /// <param name="userID"></param>
        /// <history>
        /// 17/12/2015 PhatVT: Create new
        /// </history>
        Ins_MemberAdmin_getInfoMemberAdmin_Result GetByID(int userID);

        /// <summary>
        /// Cập nhật token của user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="token"></param>
        /// <param name="tokenExpiredTime"></param>
        /// <history>
        /// 17/12/2015 PhatVT: Create new
        /// </history>
        bool UpdateToken(int userID, string token, DateTime tokenExpiredTime);

        #region Administrator
        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 18/12/2014</para>
        /// <para>lấy thông tin user dựa theo nickname</para>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<Ins_MemberAdmin_checkLogin_Result> GetInfoMemberByNickName(string userName, string tokentUser);

        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 19/12/2014</para>
        /// <para>cập nhật token cho user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newToken"></param>
        /// <param name="tokentExp"></param>
        /// <returns></returns>
        int MemberAdmin_updateTokenUser(int userId, string newToken, DateTime tokentExp);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2014</para>
        /// <para>cập nhật thời han cho token</para>
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="dateExp"></param>
        /// <param name="token">token lấy từ client</param>
        /// <returns></returns>
        bool MemberAdmin_updateTokenExp(int userid, DateTime dateExp, string token);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2014</para>
        /// <para>Kiểm tra quyền hạn</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        int MemberPermission_checkPermissionPage(int userId, string strUrl);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 23/12/2014</para>
        /// <para>lấy danh sách page được phép truy cập của user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<Ins_MemberPermission_getPagePermission_Result> MemberPermission_getPagePermission(int userId,
            int appId);

        /// <summary>
        /// Lay danh sach user admin
        /// </summary>
        /// <returns></returns>
        List<MemberAdmin> GetAllMemberAdmin();

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 19/01/2015</para>
        /// <para>lấy danh sách page được phép truy cập của user</para>
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="keyId"></param>
        /// <returns></returns>
        Ins_AdminConfigData_getConfigByAdminID_Result AdminConfigData_getConfigByAdminID(int adminId,
            int keyId, int areaId);

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
        bool UpdateAdminConfigData(int adminId, int keyId, string configData, string note, int areaId);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 29/01/2015</para>
        /// <para>Lấy danh sách nhóm admin</para>
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<GroupMember> GetGroupMemberAdmin(int pageIndex, int pageSize, ref int totalPage);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>save trạng thái của group admin</para>
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateStatusGroupMemberAdmin(int groupId, bool status);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>update tên của group admin</para>
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        bool UpdateNameGroupMemberAdmin(int groupId, string groupName);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>insert group member admin</para>
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool InsertGroupMemberAdmin(string groupName, bool status);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>lấy toàn bộ danh sách function page</para>
        /// </summary>
        /// <returns></returns>
        List<PageFunction> GetPages();

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>Lấy danh sách member admin</para>
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        List<Ins_MemberAdmin_getListMember_Result> MemberAdmin_getListMember(int pageIndex, int pageSize,
            string keySearch);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>Lấy danh sách quyen han cua user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Ins_MemberPermission_getPermissionUserByUserId_Result> GetpermissionPageByUserId(int userId);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 26/02/2015</para>
        /// <para>lấy page theo id</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PageFunction GetPageFunctionById(int id);

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
        bool UpdatePageFunction(int pageId, int parentId, string pageName, int funcPage, string link,
            string linkUse, int referPage, int pageType, int userIdUpdate, bool isEnable, bool isTargetBlank);

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
        /// <returns></returns>
        Ins_PageFunction_InsertPage_Result InsertPageFucntion(int parentId, string pageName, int status,
            int funcPage
            , string link, string linkUse, int pageType, bool isEnable, bool isTarget);

        /// <summary>
        /// Author:TrungLD
        /// DateCreated: 26/02/2015
        /// lấy quyền đã cấp cho user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<MemberPermission> GetPermissionUser(int userId);

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
        int MemberPermission_InsertPermissionMember(int userId, string listPage, string delimiter,
            bool defaultPage, int rule);
        
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
        int MemberPermission_UpdateRuleUser(int userId, int pageId, int rules, bool defaultPage);

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
        int MemberAdmin_SaveMemberAdmin(int userId, string nickname, string fullname, string email
            , string password, string passsalt, bool isLock, int groupId);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated:02/03/2015</para>
        /// <para>lấy toàn bộ danh sách Group admin</para>
        /// </summary>
        /// <returns></returns>
        List<GroupMember> GetAllGroupMemberAdmin();

        List<Ins_GroupMember_GetAllGroupMemberAdmin_Result> GetAllGroupMemberAdmin1();
        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2015-04-11</para>
        /// <para>Description: Lay thong tin PageFunction theo link</para>
        /// </summary>
        /// <returns></returns>
        PageFunction GetPageFunctionByLink(string urlLink);

        /// <summary>
        /// TanPVD: 2015-05-11
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// Thay đổi password
        bool ChangePasswordMemberAdmin(int userId, string password);

        /// <summary>
        /// <para>Author: PhatVT</para>
        /// <para>Ngày tạo: 21/05/2015</para>
        /// <para>Lấy danh sách quyền của user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<string> GetPermissions(int userId);

       // MemberAdmin GetInfoMemberAdmin(int userId);

        #endregion

        #region TanPVD - Administrator - Cấp quyền cho user

        /// <summary>
        /// TanPVD: 2015-06-01
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="visible"></param>
        /// <param name="rowStart"></param>
        /// <param name="rowEnd"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        /// Danh sách user admin
        List<Ins_MemberAdmin_GetListUser_Result> MemberAdmin_GetListUser(int rowStart, int rowEnd, int orderBy, int orderDirection, out int totalRow);

        /// <summary>
        /// TanPVD: 2015-06-01
        /// </summary>
        /// <returns></returns>
        /// Danh sách tạo Quyền cho user
        List<Ins_PageFunction_ListMenuUser_Result> PageFunction_ListMenuUser(int userId);

        /// <summary>
        /// TanPVD: 2015-06-10
        /// </summary>
        /// <returns></returns>
        /// Danh sách cấp Quyền cho nhiều user
        List<Ins_PageFunction_ListMenuPageForGroup_Result> PageFunction_ListMenuPageForGroup(int userId);

        /// <summary>
        /// Tanpvd: 2015-06-01
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userID"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        ///  cập nhật quyền cho user
        bool UpdateAndInsertUserPermission(int id, int userID, int rules, int check);

        /// <summary>
        /// TanPVD: 2015-05-26
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        /// lấy thông tin user bởi tên
        MemberAdmin AdminUserManagerGetInfoByUserName(string userName);


        #endregion
        
        #region Administrator - Chọn 1 quyền rồi cấp cho nhiều user
        /// <summary>
        /// TanPVD:2015-05-22
        /// </summary>
        /// <returns></returns>
        /// lấy danh sách thành viên đã có quyền hoặc chưa có quyền
        List<Ins_MemberAdmin_GetListMemberByPermisstionUser_Result> GetListMemberByPermisstionUser(int pageId, int rules,
                                                                                                   int rowStart,
                                                                                                   int rowEnd,
                                                                                                   int orderBy,
                                                                                                   int orderDirection,
                                                                                                   out int totalRow);

        #endregion

        #region Clone quyền

        /// <summary>
        /// TanPVD:2015-07-06
        /// </summary>
        /// <param name="userIdClone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// clone quyền cho user
        bool MemberPermission_CloneUser(int userIdClone, int userId);


        /// <summary>
        /// TanPVD:2015-07-06
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// clone quyền của nhóm cho user
        bool GroupPermission_CloneGroup(List<int> userIds, int groupId);

        #endregion

        #region Xóa quyền user

        /// <summary>
        /// TanPVD:2015-07-07
        /// </summary>
        /// <param name="userIdClone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Xóa quyền user
        bool MemberPermission_Delete(int userId, int pageId, int type);

        #endregion

        #region TanPVD - Administrator - Quản lý nhóm
        /// <summary>
        /// TanPVD: 2015-05-26
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// update trạng thái page
        bool UpdateAdminStatusPageFunction(int ID, bool status);
        #endregion

        List<Ins_MemberAdmin_GetListUserAdmin_Result> GetListUserAdmin(int areaId);
    }
}
