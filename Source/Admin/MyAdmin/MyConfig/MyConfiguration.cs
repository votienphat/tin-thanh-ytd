/**********************************************************************
 * Author: ThongNT
 * DateCreate: 06-25-2014 
 * Description: MyConfiguration  
 * ####################################################################
 * Author:......................
 * DateModify: .................
 * Description: ................
 * 
 *********************************************************************/
using System.Configuration;
using MyConfig.Cache;

namespace MyConfig
{
    public class MyConfiguration
    {
        private static MyConfigSection _instance;

        private static MyConfigSection Instance
        {
            get
            {
                return _instance ?? (_instance = (MyConfigSection)ConfigurationManager.GetSection("MyConfig"));
            }
        }

        public static DefaultElement Default
        {
            get { return Instance.DefaultElement; }
        }
        public static ArticleElement Article
        {
            get { return Instance.ArticleElement; }
        }
        public static WebSocketElement WebSocket
        {
            get { return Instance.WebSocketElement; }
        }

        public static UrlSocketCollection UrlSocketCollection
        {
            get { return Instance.UrlSocketCollection; }
        }

        public static NotificationElement Notification
        {
            get { return Instance.NotificationElement; }
        }

        public static RedisElement Redis
        {
            get { return Instance.RedisElement; }
        }

        public static CacheElement Cache
        {
            get { return Instance.CacheElement; }
        }
    }

}
