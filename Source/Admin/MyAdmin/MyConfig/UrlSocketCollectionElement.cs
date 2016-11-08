
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
    public class UrlSocketCollectionElement : ConfigurationElement
    {
        [ConfigurationProperty("Url", DefaultValue = "ws://192.168.5.43:8080")]
        public string Url
        {
            get { return (string)this["Url"]; }
        }
    }
    public class UrlSocketCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new UrlSocketCollectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UrlSocketCollectionElement)element).Url;
        }
    }
}
