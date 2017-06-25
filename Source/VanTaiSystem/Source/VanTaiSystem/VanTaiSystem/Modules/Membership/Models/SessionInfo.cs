using System;

namespace VanTaiSystem.Modules.Membership.Models
{
    public class SessionInfo
    {
        public string Version { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime TokenExpire { get; set; }
    }
}