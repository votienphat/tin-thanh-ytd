using System.Configuration;

namespace MyConfig
{
    public class NotificationElement : ConfigurationElement
    {
        /// <summary>
        /// Thời gian để kiểm tra notify mới, tính bằng giây
        /// </summary>
        [ConfigurationProperty("IntervalTime", DefaultValue = 5)]
        public int IntervalTime
        {
            get { return (int)this["IntervalTime"]; }
        }

        /// <summary>
        /// Có mở chế độ notify không
        /// </summary>
        [ConfigurationProperty("IsEnabled", DefaultValue = true)]
        public bool IsEnabled
        {
            get { return (bool)this["IsEnabled"]; }
        }

        /// <summary>
        /// Thời gian để hiển thị khi có nhiều notify cùng lúc
        /// </summary>
        [ConfigurationProperty("DisplayIntervalTime", DefaultValue = 3)]
        public int DisplayIntervalTime
        {
            get { return (int)this["DisplayIntervalTime"]; }
        }
    }
}
