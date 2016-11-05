using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contract.NotificationModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.LogManagementEntities;
namespace DataAccess.Repositories.NotificationModule
{
    public class NotificationRepository : DaoRepository<LogManagementEntities, Notification>, INotificationRepository
    {
        public bool AddNotification(int notificationReasonID, string Link, string Description, DateTime date)
        {
                return Uow.Context.Ins_Notification_AddNotification(notificationReasonID, Link, Description, date) > 0;
        }


        public Ins_NotificationReason_GetNotificationReasonByID_Result GetNotificationReasonById(int id)
        {
            return Uow.Context.Ins_NotificationReason_GetNotificationReasonByID(id).FirstOrDefault();
        }
    }
}
