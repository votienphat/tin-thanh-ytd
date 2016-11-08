using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessObject.MembershipModule.Models;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace BusinessObject.MembershipModule.Enums
{
    public class AccountModel
    {
    }

    public class EditGroupMemberAdminModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public bool Status { get; set; }
    }
     
    public class PermissionUserModel
    {
        public List<PageFunctionModel> ListPageFunction { get; set; }
        public List<Ins_MemberPermission_getPermissionUserByUserId_Result> ListPermission { get; set; }
    }

    public class ProvidePermissionUserModel
    {
        public int UserId { get; set; }
        public string ListPage { get; set; }
    }

    public class RulesPermissionUserModel
    {
        public int UserId { get; set; }
        public int Rules { get; set; }
        public int CurrentPage { get; set; }
        public bool PageDefault { get; set; }
    }

    public class MemberPermissionInfoModel
    {
        public MemberAdmin MemberInfo { get; set; }
        public List<GroupMember> GroupMember { get; set; }
    }

    public class AdminUserInfoModel
    {
        public AdminUserModel AdminUser { get; set; }
        public MemberAdmin UserInfo { get; set; }
        public List<Ins_MemberAdmin_GetListUser_Result> ListUser { get; set; }
    }

    public class MemberPermissionInfoModel1
    {
        public MemberAdmin MemberInfo { get; set; }
        public List<Ins_GroupMember_GetAllGroupMemberAdmin_Result> GroupMember { get; set; }
    }

    public class MemberPermissionModel
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public int GroupId { get; set; }
        public bool IsLock { get; set; }
    }

    public class AdminGroupModel
    {
        public int ID { get; set; }
        public int GroupId { get; set; }
        public int Rules { get; set; }
        public int Check { get; set; }
        public string GroupName { get; set; }
        public bool Visible { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEnable { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int MemberJoin { get; set; }
    }

    public class AdminUserModel
    {
        public int STT { get; set; }
        public int ID { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime Datecreated { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int UserID { get; set; }
        public string GroupName { get; set; }
        public int TotalRow { get; set; }
        public int Rules { get; set; }
        public int Check { get; set; }
        public int PageID { get; set; }
    }

    public class AdminLogTransferSearchModel
    {
        public bool IsIgnore { get; set; }

        public string PubUserId { get; set; }

        public int AdminId { get; set; }
        public string Admin { get; set; }

        public int TransferType { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }
        public string Keyword { get; set; }

        public int CaseExportExcel { get; set; }
    }

    public class InboxModel
    {
        public string ListUserID { get; set; }
        public string Delimiter { get; set; }
        public string ReasonContentUser { get; set; }
        public string IpAddress { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string Keyword { get; set; }
        public int Status { get; set; }
        public int IsRead { get; set; }
        public string ListMessID { get; set; }
    }

    public enum InboxStatusEnum
    {
        ////0: Chưa gửi, 1: Hiển thị, 2: User xóa, 3: Admin xóa
        [Description("Tất cả")]
        TatCa = -1,

        [Description("Ẩn")]
        An = 0,

        [Description("Hiển thị")]
        HienThi = 1,

        [Description("Admin Xóa")]
        AdminXoa = 3,

        [Description("User Xóa")]
        UserXoa = 2,
    }

    public enum InboxAlreadyEnum
    {
        [Description("Tất cả")]
        TatCa = -1,

        [Description("Đã đọc")]
        DaDoc = 1,

        [Description("Chưa đọc")]
        ChuaDoc = 0
    }

   
}