using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MyAdmin.Helper.Hubs
{
    [HubName("notificationHub")]
    public class 
        NotificationHub : Hub
    {
        private readonly NotificationTicker _notificationTicker;
        //private readonly INotificationPermissionRepository _notificationPermissionRepository=IoC.Resolve<INotificationPermissionRepository>();
        public NotificationHub() : this(NotificationTicker.Instance) { }
        public NotificationHub(NotificationTicker notificationTicker)
        {
            _notificationTicker = notificationTicker;
        }

        public override Task OnConnected()
        {
            //_userCount++;
            //var context = GlobalHost.ConnectionManager.GetHubContext<SampleHub>();
            //context.Clients.All.online(_userCount);

            var id = HubSessionManager.GetAdminId(Context);
            JoinRoom(id.ToString()); 
            return null;
        }
        public override Task OnReconnected()
        {
            //_userCount++;
            //var context = GlobalHost.ConnectionManager.GetHubContext<SampleHub>();
            //context.Clients.All.online(_userCount);

            var id = HubSessionManager.GetAdminId(Context);
            JoinRoom(id.ToString());
            return null;
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            //_userCount--;
            //var context = GlobalHost.ConnectionManager.GetHubContext<SampleHub>();
            //context.Clients.All.online(_userCount);

            var id = HubSessionManager.GetAdminId(Context);
            LeaveRoom(id.ToString());
            return null;
        }

        public void JoinGroup(string adminID)
        {
            //Get userID để lấy quyền
            //var name = _notificationBusiness.GetNotificationReasonByAdminId(SessionManager.SessionData.UserId);
            //JoinRoom(name.NotificationReason);
        }

        //public List<Notification> GetNotificationsByPermission()
        //{
        //    return _notificationTicker.GetNotificationsByTime();
        //}
        //[HubMethodName("sendMessage")]
        //public void SendMessage(string permissionID, string message)
        //{
        //    Clients.User(permissionID).send(message);
        //}

        public Task JoinRoom(string roomName)
        {
            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }
    }
}