using System.Configuration;
using System.Threading;
using System.Web.Configuration;
using MyUtility.Extensions;
using Phystones.Models.Enum;

namespace Phystones.Helper
{
    public class GlobalHelper
    {
        public const string Vietnamese = "vi-VN";
        public const string English = "en-US";

        public static string CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.Name;
            }
        }
        public static string DefaultCulture
        {
            get
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                GlobalizationSection section = (GlobalizationSection)config.GetSection("system.web/globalization");
                return section.UICulture;
            }
        }

        public static string GetRouteName(RouteName routeName)
        {
            return CurrentCulture + routeName.Text();
        }

        public static string GetEnglishRouteName(RouteName routeName)
        {
            return English + routeName.Text();
        }

        public static string GetVietnameseRouteName(RouteName routeName)
        {
            return Vietnamese + routeName.Text();
        }
    }
}