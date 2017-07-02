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
using MyConfig.Content;

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

        public static NotificationElement Notification
        {
            get { return Instance.NotificationElement; }
        }

        public static MailElement Mail
        {
            get { return Instance.MailElement; }
        }

        public static CacheElement Cache
        {
            get { return Instance.CacheElement; }
        }
    }

}
