using System.Configuration;

namespace MyConfig
{
    public class DefaultElement : ConfigurationElement
    {
        #region Encryption

        /// <summary>
        /// Có mã hóa dữ liệu truyền đi không
        /// </summary>
        [ConfigurationProperty("HasEncryptData", DefaultValue = false)]
        public bool HasEncryptData
        {
            get { return (bool)this["HasEncryptData"]; }
        }

        /// <summary>
        /// Key mã hóa AES256
        /// </summary>
        [ConfigurationProperty("EncryptKey", DefaultValue = "123456789123456789")]
        public string Aes256Key
        {
            get { return (string)this["EncryptKey"]; }
        }

        /// <summary>
        /// IV mã hóa AES256
        /// </summary>
        [ConfigurationProperty("Aes256Iv", DefaultValue = "123456789123456789")]
        public string Aes256Iv
        {
            get { return (string)this["Aes256Iv"]; }
        }

        #endregion

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
        [ConfigurationProperty("IsCheckPermission", DefaultValue = "false")]
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

        [ConfigurationProperty("ImageHost", DefaultValue = "http://10.17.0.20:8003/")]
        public string ImageHost
        {
            get { return (string)this["ImageHost"]; }
        }

        /// <summary>
        /// Có mở tính năng debug ko
        /// </summary>
        [ConfigurationProperty("EnableDebug", DefaultValue = false)]
        public bool EnableDebug
        {
            get { return (bool)this["EnableDebug"]; }
        }
    }
}
