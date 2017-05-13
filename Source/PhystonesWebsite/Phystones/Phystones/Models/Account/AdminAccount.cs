using System;
using System.Collections.Generic;

namespace Phystones.Models.Account
{
    public class AdminAccount
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public string TokenId { get; set; }
        public DateTime? TokenExp { get; set; }
        public List<AdminPermission> Permissions { get; set; }
        public List<AdminMenu> Menus { get; set; }

        public AdminAccount()
        {
            Permissions = new List<AdminPermission>();
            Menus = new List<AdminMenu>();
        }
    }
}