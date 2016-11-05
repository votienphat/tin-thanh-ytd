using System.Threading.Tasks;
using MyAdmin.Ioc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MyConfig;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using MyUtility.Extensions;
using BusinessObject.Enums;

namespace MyAdmin.Helper.Hubs
{
    [HubName("pushNotificationHub")]
    public class 
        PushNotificationHub : Hub
    {       
        private DateTime _formTime;
        private DateTime _toTime;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(MyConfiguration.Notification.IntervalTime);

        // Thời gian chờ mặc định
        private readonly static int PeriodSecond = MyConfiguration.Notification.DisplayIntervalTime;

        private IHubContext _context
        {
            get;
            set;
        }

        public PushNotificationHub()
        {
            _context = GlobalHost.ConnectionManager.GetHubContext<PushNotificationHub>();
        }

        public void PushAllNotification(int notyType)
        {
            PushNotification(notyType);
        }


        private void PushNotification(int notyType)
        {
            if (MyConfiguration.Notification.IsEnabled)
            {
                }
            }
        }
    }
