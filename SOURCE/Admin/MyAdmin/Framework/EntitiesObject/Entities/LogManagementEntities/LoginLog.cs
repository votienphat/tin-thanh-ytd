//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesObject.Entities.LogManagementEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoginLog
    {
        public int UserID { get; set; }
        public System.DateTime LoginDate { get; set; }
        public string Username { get; set; }
        public Nullable<int> OpenProviderID { get; set; }
        public string OpenUserID { get; set; }
        public string Token { get; set; }
        public Nullable<System.DateTime> TokenExpiredTime { get; set; }
        public string IMEI { get; set; }
        public Nullable<int> PlatformID { get; set; }
        public string HardwareID { get; set; }
        public string AppVersion { get; set; }
        public string IPAddress { get; set; }
    }
}
