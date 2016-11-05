using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAdmin.Models.Account
{
    public class AdminMemberInfo
    {
        public int ID { get; set; }
        public int? ChannelId { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool? IsLockedOut { get; set; }
        public DateTime? CreateDated { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangeDated { get; set; }
        public DateTime? LastLogoutDate { get; set; }
        public int? FailedPasswordAttempCount { get; set; }
        public int? GroupID { get; set; }
        public string LastIPAddress { get; set; }
        public string LastAgent { get; set; }
        public string TokenID { get; set; }
        public DateTime? TokenExp { get; set; }
        public string GroupName { get; set; }
        public string Link { get; set; }
        public string LinkUse { get; set; }
        public string NamePage { get; set; }
    }
}