using System.Configuration;

namespace MyConfig
{
    public class DefaultElement : ConfigurationElement
    {
        /// <summary>
        /// Số dòng mặc định trong 1 trang
        /// </summary>
        [ConfigurationProperty("PageSize", DefaultValue = "10")]
        public int PageSize
        {
            get { return (int)this["PageSize"]; }
        }

        /// <summary>
        /// Có kiểm tra quyền không. Set false để bỏ qua bước kiểm tra
        /// </summary>
        [ConfigurationProperty("IsCheckPermission", DefaultValue = "true")]
        public bool IsCheckPermission
        {
            get { return (bool)this["IsCheckPermission"]; }
        }

        /// <summary>
        /// Full đường dẫn của API
        /// </summary>
        [ConfigurationProperty("ApiFullDomain", DefaultValue = "http://api.com/")]
        public string ApiFullDomain
        {
            get { return (string)this["ApiFullDomain"]; }
        }

        /// <summary>
        /// Full đường dẫn của API
        /// </summary>
        [ConfigurationProperty("ContentVersion", DefaultValue = "1")]
        public int ContentVersion
        {
            get { return (int)this["ContentVersion"]; }
        }

        /// <summary>
        /// <para>Author: TrungLD</para>
        /// <para>DateCreated: 19/12/2014</para>
        /// <para>userid cua admin tao bai viet su dung test: 6268112</para>
        /// </summary>
        [ConfigurationProperty("AdminId", DefaultValue = 11334)]
        public int AdminId
        {
            get { return (int)this["AdminId"]; }
        }

        [ConfigurationProperty("DomainOnline", DefaultValue = "http://10.17.0.20:8006/")]
        public string DomainOnline
        {
            get { return (string)this["DomainOnline"]; }
        }

        /// <summary>
        /// Author: TanPVD
        /// <para>Domain chua hinh cua user</para> 
        /// </summary>
        [ConfigurationProperty("ImageHost", DefaultValue = "http://10.17.0.20:8003/")]
        public string ImageHost
        {
            get { return (string)this["ImageHost"]; }

        }
        [ConfigurationProperty("AdminIdDefault", DefaultValue = 0)]
        public int AdminIdDefault
        {
            get { return (int)this["AdminIdDefault"]; }
        }
        [ConfigurationProperty("IsTest", DefaultValue = true)]
        public bool IsTest
        {
            get { return (bool)this["IsTest"]; }
        }
        [ConfigurationProperty("MaxCardAmountOnRound", DefaultValue = 2000000)]
        public int MaxCardAmountOnRound
        {
            get { return (int)this["MaxCardAmountOnRound"]; }
        }

        [ConfigurationProperty("IsReportAuWithOldVersion", DefaultValue = true)]
        public bool IsReportAuWithOldVersion
        {
            get { return (bool)this["IsReportAuWithOldVersion"]; }
        }

        [ConfigurationProperty("TotalMinutesAllowUpdateBoss", DefaultValue = 5)]
        public int TotalMinutesAllowUpdateBoss
        {
            get { return (int)this["TotalMinutesAllowUpdateBoss"]; }
        }
    }
}
