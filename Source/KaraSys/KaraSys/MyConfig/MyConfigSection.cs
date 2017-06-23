using System.Configuration;
using MyConfig.Cache;

namespace MyConfig
{
    public class MyConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Default")]
        public DefaultElement DefaultElement
        {
            get { return (DefaultElement)this["Default"]; }
        }
        [ConfigurationProperty("WebSocket")]
        public WebSocketElement WebSocketElement
        {
            get { return (WebSocketElement)this["WebSocket"]; }
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

        [ConfigurationProperty("UrlSocketCollection")]
        public UrlSocketCollection UrlSocketCollection
        {
            get { return (UrlSocketCollection)this["UrlSocketCollection"]; }
        }

        [ConfigurationProperty("Redis")]
        public RedisElement RedisElement
        {
            get { return (RedisElement)this["Redis"]; }
        }

        [ConfigurationProperty("Cache")]
        public CacheElement CacheElement
        {
            get { return (CacheElement)this["Cache"]; }
        }
    }
}
