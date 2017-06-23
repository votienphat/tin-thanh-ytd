using System.Configuration;

namespace MyConfig
{
    public class ArticleElement : ConfigurationElement
    {
        [ConfigurationProperty("BaseUrl")]
        public string BaseUrl
        {
            get { return (string)this["BaseUrl"]; }
        }
        [ConfigurationProperty("BaseDir")]
        public string BaseDir
        {
            get { return (string)this["BaseDir"]; }
        }
        [ConfigurationProperty("MapPath")]
        public string MapPath
        {
            get { return (string)this["MapPath"]; }
        }

       
    }
}
