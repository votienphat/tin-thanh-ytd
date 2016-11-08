using System.Configuration;

namespace MyConfig.Cache
{
    public class CacheElement : ConfigurationElement
    {
        /// <summary>
        /// Đơn vị là phút. Mặc định là 60
        /// </summary>
        [ConfigurationProperty("Default", DefaultValue = "60")]
        public int Default
        {
            get { return (int)this["Default"]; }
        }


        /// <summary>
        /// Đơn vị là phút
        /// </summary>
        [ConfigurationProperty("Cache30M", DefaultValue = "30")]
        public int Cache30M
        {
            get { return (int)this["Cache30M"]; }
        }

        /// <summary>
        /// Đơn vị là phút
        /// </summary>
        [ConfigurationProperty("Cache15M", DefaultValue = "15")]
        public int Cache15M
        {
            get { return (int)this["Cache15M"]; }
        }

        /// <summary>
        /// Đơn vị là ngày. Mặc định là 1 ngày
        /// </summary>
        [ConfigurationProperty("Cache1Day", DefaultValue = "1")]
        public int Cache1Day
        {
            get { return (int)this["Cache1Day"]; }
        }
    }
}
