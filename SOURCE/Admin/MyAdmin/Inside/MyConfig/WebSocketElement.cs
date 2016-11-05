
/**********************************************************************
 * Author: ThongNT
 * DateCreate: 06-25-2014 
 * Description: Quan ly thong tin cau hinh chung cho project  
 * ####################################################################
 * Author:......................
 * DateModify: .................
 * Description: ................
 * 
 *********************************************************************/
using System.Configuration;

namespace MyConfig
{
    public class WebSocketElement : ConfigurationElement
    {
        [ConfigurationProperty("Key", DefaultValue = "@bcd-1x#E")]
        public string Key
        {
            get { return (string)this["Key"]; }
        }
        [ConfigurationProperty("IsMultiUrl", DefaultValue = "true")]
        public bool IsMultiUrl
        {
            get { return (bool)this["IsMultiUrl"]; }
        }
        [ConfigurationProperty("TotalMultiUrl", DefaultValue = "10")]
        public int TotalMultiUrl
        {
            get { return (int)this["TotalMultiUrl"]; }
        }

        [ConfigurationProperty("Url", DefaultValue = "ws://192.168.5.43:8080")]
        public string Url
        {
            get { return (string)this["Url"]; }
        }
    }
}
