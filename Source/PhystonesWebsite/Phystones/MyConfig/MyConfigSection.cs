using System.Configuration;
using MyConfig.Cache;
using MyConfig.Content;

namespace MyConfig
{
    public class MyConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Default")]
        public DefaultElement DefaultElement
        {
            get { return (DefaultElement)this["Default"]; }
        }

        [ConfigurationProperty("Notification")]
        public NotificationElement NotificationElement
        {
            get { return (NotificationElement)this["Notification"]; }
        }

        [ConfigurationProperty("Article")]
        public ArticleElement ArticleElement
        {
            get { return (ArticleElement)this["Article"]; }
        }

        [ConfigurationProperty("Mail")]
        public MailElement MailElement
        {
            get { return (MailElement)this["Mail"]; }
        }

        [ConfigurationProperty("Cache")]
        public CacheElement CacheElement
        {
            get { return (CacheElement)this["Cache"]; }
        }
    }
}
