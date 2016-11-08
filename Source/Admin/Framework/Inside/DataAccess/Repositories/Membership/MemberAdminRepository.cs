using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Transactions;
using DataAccess.Contract.Membership;
using DataAccess.Entity;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace DataAccess.Repositories.Membership
{
    public class MemberAdminRepository : DaoRepository<MetroMembershipEntities, MemberAdmin>, IMemberAdminRepository
    {
        public Ins_MemberAdmin_checkLogin_Result GetByNickname(string nickname)
        {
            return Uow.Context.Ins_MemberAdmin_checkLogin(nickname, string.Empty).FirstOrDefault();
        }
        public Ins_MemberAdmin_getInfoMemberAdmin_Result GetByID(int userID)
        {
            return Uow.Context.Ins_MemberAdmin_getInfoMemberAdmin(userID).FirstOrDefault();
        }

        public bool UpdateToken(int userID, string token, DateTime tokenExpiredTime)
        {
            return Uow.Context.Ins_MemberAdmin_updateTokenUser(userID, token, tokenExpiredTime) > 0;
        }

        #region Administrator - TrungLD old
        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 18/12/2014</para>
        /// <para>lấy thông tin user dựa theo nickname</para>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Ins_MemberAdmin_checkLogin_Result> GetInfoMemberByNickName(string userName, string tokentUser)
        {
            return Uow.Context.Ins_MemberAdmin_checkLogin(userName, tokentUser).ToList();
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
            return Uow.Context.Ins_MemberAdmin_updateTokenUser(userId, newToken, tokentExp);
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
            var objReturn = Uow.Context.Ins_MemberAdmin_updateTokenExp(dateExp, userid, token);
            return objReturn > 0;
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
            var objReturn = Uow.Context.Ins_MemberPermission_checkPermissionPage(userId, strUrl).FirstOrDefault();
            return objReturn.HasValue ? objReturn.Value : 0;
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
            return Uow.Context.Ins_MemberPermission_getPagePermission(userId, appId).ToList();
        }

        /// <summary>
        /// Lay danh sach user admin
        /// </summary>
        /// <returns></returns>
        public List<MemberAdmin> GetAllMemberAdmin()
        {
            var result = Uow.Context.MemberAdmins.ToList()
                .OrderBy(m => m.ID);

            return result.ToList();
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
            int keyId)
        {
            return Uow.Context.Ins_AdminConfigData_getConfigByAdminID(adminId, keyId).FirstOrDefault();
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
        public bool UpdateAdminConfigData(int adminId, int keyId, string configData, string note)
        {
            var objResult = Uow.Context.Ins_AdminConfigData_insertConfigData(adminId, keyId, configData, note);
            return objResult > 0;
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
            var allList = from x in Uow.Context.GroupMembers select x;
            totalPage = allList.Any() ? allList.Count() : 0;
            return allList.OrderByDescending(c => c.Datecreated).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
            var listGroup = Uow.Context.GroupMembers.First(x => x.ID == groupId);
            listGroup.Visible = status;
            return Uow.Context.SaveChanges() > 0;
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
            int iResult;
            try
            {
                if (Uow.Context.GroupMembers.Any(c => c.ID != groupId && c.GroupName == groupName))
                {
                    iResult = -1;
                }
                else
                {
                    var listGroup = Uow.Context.GroupMembers.First(x => x.ID == groupId);
                    listGroup.GroupName = groupName;
                    iResult = Uow.Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult > 0;
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
            int iResult;
            try
            {
                if (Uow.Context.GroupMembers.Any(c => c.GroupName == groupName))
                {
                    iResult = -1;
                }
                else
                {
                    Uow.Context.GroupMembers.Add(new GroupMember
                    {
                        GroupName = groupName,
                        Visible = status,
                        Datecreated = DateTime.Now
                    });
                    iResult = Uow.Context.SaveChanges();
                }

            }
            catch (Exception)
            {
                iResult = -1;
            }
            return iResult > 0;
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/01/2015</para>
        /// <para>lấy toàn bộ danh sách function page</para>
        /// </summary>
        /// <returns></returns>
        public List<PageFunction> GetPages()
        {
            return (from x in Uow.Context.PageFunctions select x).ToList();
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
            return Uow.Context.Ins_MemberAdmin_getListMember(keySearch, pageIndex, pageSize).ToList();
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
            return Uow.Context.Ins_MemberPermission_getPermissionUserByUserId(userId).ToList();
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
            var objReturn = (from x in Uow.Context.PageFunctions
                             where x.ID == id
                             select x
                ).FirstOrDefault();
            return objReturn;
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
        public bool UpdatePageFunction(int pageId, int parentId, string pageName, int funcPage, string link, string linkUse, int referPage, int pageType, int userIdUpdate, bool isEnable, bool isTargetBlank)
        {
            int iResult;
            try
            {
                if (Uow.Context.PageFunctions.Any(c => c.ID != pageId && c.Name == pageName))
                {
                    iResult = -1;
                }
                else
                {
                    var listPage = Uow.Context.PageFunctions.First(x => x.ID == pageId);
                    listPage.FunctionPage = funcPage;
                    listPage.ParentID = parentId;
                    listPage.IsEnable = isEnable;
                    listPage.IsTargetBlank = isTargetBlank;
                    listPage.Link = link;
                    listPage.LinkUse = linkUse;
                    listPage.Name = pageName;
                    listPage.PageReferID = referPage;
                    listPage.PageType = pageType;
                    listPage.UpdatedBy = userIdUpdate;
                    listPage.UpdatedDate = DateTime.Now;
                    iResult = Uow.Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult > 0;
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
        /// <returns></returns>
        public Ins_PageFunction_InsertPage_Result InsertPageFucntion(int parentId, string pageName, int status, int funcPage
            , string link, string linkUse, int pageType, bool isEnable, bool isTarget)
        {
            return
                Uow.Context.Ins_PageFunction_InsertPage(parentId, pageName, status, link, linkUse, funcPage,
                    isEnable, isTarget, pageType).FirstOrDefault();
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
            return (from x in Uow.Context.MemberPermissions
                    where x.UserID == userId
                    select x
                ).ToList();
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
        public int MemberPermission_InsertPermissionMember(int userId, string listPage, string delimiter,
            bool defaultPage, int rule)
        {
            var objResult = Uow.Context.Ins_MemberPermission_InsertPermissionMember(userId, listPage, defaultPage, rule,
                    delimiter).FirstOrDefault();
            return objResult.HasValue ? objResult.Value : 0;
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
        public int MemberPermission_UpdateRuleUser(int userId, int pageId, int rules, bool defaultPage)
        {
            var objResult =
                Uow.Context.Ins_MemberPermission_UpdateRuleUser(userId, pageId, rules, defaultPage)
                    .FirstOrDefault();
            return objResult.HasValue ? objResult.Value : 0;
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
        public int MemberAdmin_SaveMemberAdmin(int userId, string nickname, string fullname, string email
            , string password, string passsalt, bool isLock, int groupId)
        {
            var objResult = Uow.Context.Ins_MemberAdmin_SaveMemberAdmin(userId, nickname, fullname, email,
                password, passsalt, isLock, groupId).FirstOrDefault();
            return objResult.HasValue ? objResult.Value : 0;
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated:02/03/2015</para>
        /// <para>lấy toàn bộ danh sách Group admin</para>
        /// </summary>
        /// <returns></returns>
        public List<GroupMember> GetAllGroupMemberAdmin()
        {
            var allList = from x in Uow.Context.GroupMembers select x;
            return allList.ToList();
        }

        public List<Ins_GroupMember_GetAllGroupMemberAdmin_Result> GetAllGroupMemberAdmin1()
        {
            var objResult = Uow.Context.Ins_GroupMember_GetAllGroupMemberAdmin().ToList();
            return objResult;
        }

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2015-04-11</para>
        /// <para>Description: Lay thong tin PageFunction theo link</para>
        /// </summary>
        /// <returns></returns>
        public PageFunction GetPageFunctionByLink(string urlLink)
        {
            //using (var tran = Uow.BeginTransaction(IsolationLevel.ReadUncommitted))
            //{
                var result = Uow.Context.PageFunctions
                    .Where(p => p.Link == urlLink);
                var ret = result.FirstOrDefault();
                //tran.Complete();
                return ret;
            //}
        }

        /// <summary>
        /// TanPVD: 2015-05-11
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// Thay đổi password
        public bool ChangePasswordMemberAdmin(int userId, string password)
        {
            int iResult;
            try
            {
                //if (Uow.Context.MemberAdmins.Any(c => c.ID != userId))
                //{
                //    iResult = -1;
                //}
                //else
                {
                    var listInfor = Uow.Context.MemberAdmins.First(x => x.ID == userId);
                    listInfor.Password = password;
                    iResult = Uow.Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult > 0;
        }

        /// <summary>
        /// <para>Author: PhatVT</para>
        /// <para>Ngày tạo: 21/05/2015</para>
        /// <para>Lấy danh sách quyền của user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetPermissions(int userId)
        {
            return
                Uow.Context.Ins_MemberAdmin_GetPermission(userId).ToList();
        }
        #endregion

        #region TanPVD - Administrator - Cấp quyền cho user

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
            var opTotalRow = new ObjectParameter("TotalRow", typeof(int));
            var list = Uow.Context.Ins_MemberAdmin_GetListUser(rowStart, rowEnd, orderBy, orderDirection, opTotalRow).ToList();
            totalRow = opTotalRow.Value == null ? 0 : (int)opTotalRow.Value;
            return list;
        }

        /// <summary>
        /// TanPVD: 2015-06-01
        /// </summary>
        /// <returns></returns>
        /// Danh sách tạo Quyền cho user
        public List<Ins_PageFunction_ListMenuUser_Result> PageFunction_ListMenuUser(int userId)
        {
            var list = Uow.Context.Ins_PageFunction_ListMenuUser(userId).ToList();
            return list;
        }

        /// <summary>
        /// TanPVD: 2015-06-10
        /// </summary>
        /// <returns></returns>
        /// Danh sách cấp Quyền cho nhiều user
        public List<Ins_PageFunction_ListMenuPageForGroup_Result> PageFunction_ListMenuPageForGroup(int userId)
        {
            var list = Uow.Context.Ins_PageFunction_ListMenuPageForGroup(userId).ToList();
            return list;
        }

        /// <summary>
        /// Tanpvd: 2015-06-01
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userID"></param>
        /// <param name="rules"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        ///  cập nhật quyền cho user
        public bool UpdateAndInsertUserPermission(int id, int userID, int rules, int check)
        {
            var iResult = 0;
            try
            {
                iResult = Uow.Context.Ins_GroupPermission_UpdateAndInsertUserPermission(id, userID, rules, check);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult > 0;
        }

        /// <summary>
        /// TanPVD: 2015-05-26
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// lấy thông tin user bởi tên
        public MemberAdmin AdminUserManagerGetInfoByUserName(string userName)
        {
            var list = Uow.Context.MemberAdmins.First(x => x.NickName == userName);
            return list;
        }
        #endregion

        #region Administrator - Chọn 1 quyền rồi cấp cho nhiều user
        /// <summary>
        /// TanPVD:2015-05-22
        /// </summary>
        /// <returns></returns>
        /// lấy danh sách thành viên đã có quyền hoặc chưa có quyền
        public List<Ins_MemberAdmin_GetListMemberByPermisstionUser_Result> GetListMemberByPermisstionUser(int pageId, int rules, int rowStart, int rowEnd, int orderBy, int orderDirection, out int totalRow)
        {
            var opTotalRow = new ObjectParameter("TotalRow", typeof(int));
            var list = Uow.Context.Ins_MemberAdmin_GetListMemberByPermisstionUser(pageId, rules, rowStart, rowEnd, orderBy, orderDirection, opTotalRow).ToList();
            totalRow = opTotalRow.Value == null ? 0 : (int)opTotalRow.Value;
            return list;
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
            var iResult = 0;
            try
            {
                iResult = Uow.Context.Ins_MemberPermission_CloneUser(userIdClone, userId);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult > 0;
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
            using (Uow)
            {
                var iResult = 0;
                try
                {
                    foreach (var userid in userIds)
                    {
                        if (userid != 0)
                        {
                            iResult = Uow.Context.Ins_GroupPermission_CloneGroup(groupId, userid);
                        }
                    }
                }
                catch (Exception ex)
                {
                    iResult = -1;
                }
                return iResult > 0;
            }
        }

        #endregion

        #region Xóa quyền user
        /// <summary>
        /// TanPVD:2015-07-07
        /// </summary>
        /// <param name="userIdClone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Xóa quyền user
        public bool MemberPermission_Delete(int userId, int pageId, int type)
        {
            var iResult = 0;
            try
            {
                iResult = Uow.Context.Ins_Permission_Delete(userId, pageId, type);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult > 0;
        }
        #endregion

        #region TanPVD - Administrator - Quản lý nhóm
        /// <summary>
        /// TanPVD: 2015-05-26
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// update trạng thái page
        public bool UpdateAdminStatusPageFunction(int ID, bool status)
        {
            using (Uow)
            {
                var iResult = 0;
                try
                {
                    var listInfor = Uow.Context.PageFunctions.First(x => x.ID == ID);
                    listInfor.IsEnable = status;
                    iResult = Uow.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    iResult = -1;
                }
                return iResult > 0;
            }
        }

        public List<Ins_MemberAdmin_GetListUserAdmin_Result> GetListUserAdmin()
        {
            return Uow.Context.Ins_MemberAdmin_GetListUserAdmin().ToList();
        }

        #endregion
    }
}