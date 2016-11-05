using BusinessObject.MembershipModule.Enums;

namespace MyAdmin.Areas.MembershipModule.Models
{
    public class PopubAdminUserManagerViewModel
    {
        public AdminUserModel AdminUserModel { get; set; }
        public MemberPermissionInfoModel1 MemberPermissionInfoModel1 { get; set; }
        public AdminUserInfoModel AdminUserInfoModel { get; set; }
    }
}