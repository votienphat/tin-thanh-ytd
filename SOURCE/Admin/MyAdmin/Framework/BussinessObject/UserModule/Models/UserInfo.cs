using System;

namespace BussinessObject.UserModule.Models
{
    /// <summary>
    /// Định nghĩa các thông tin user
    /// </summary>
    /// <history>
    /// 12/01/2016 PhatVT: Tạo mới
    /// </history>
    public class UserInfo
    {
        public int UserId { get; set; }
        public int PubUserId { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? IsActive { get; set; }
        public int OpenIdProvider { get; set; }
        public string Token { get; set; }
    }
}
