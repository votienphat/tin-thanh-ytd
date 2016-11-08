using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using MyAdmin.Ioc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MyConfig;

namespace MyAdmin.Helper.Hubs
{
    public class NotificationTicker
    {
        #region Variables

        // Singleton instance
        private readonly static Lazy<NotificationTicker> _instance = new Lazy<NotificationTicker>(() =>
         new NotificationTicker(GlobalHost.ConnectionManager.GetHubContext<NotificationHub>()));

        private DateTime _formTime;
        private DateTime _toTime;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(MyConfiguration.Notification.IntervalTime);

        // Thời gian chờ mặc định
        private readonly static int PeriodSecond = MyConfiguration.Notification.DisplayIntervalTime;

        public static NotificationTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubContext _context
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        private NotificationTicker(IHubContext context)
        {
            _context = context;
            var thread = new Thread(StartNotification) { IsBackground = true };
            thread.Start();
        }

        #endregion

        #region Methods

        private void StartNotification()
        {
            while (true)
            {
                Thread.Sleep(_updateInterval);
            }
        }
        #endregion
    }
}