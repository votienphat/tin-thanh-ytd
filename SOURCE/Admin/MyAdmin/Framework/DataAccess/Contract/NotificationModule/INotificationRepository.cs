using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.LogManagementEntities;
using System;

namespace DataAccess.Contract.NotificationModule
{
    public interface INotificationRepository : IDaoRepository<Notification>
    {
        bool AddNotification(int notificationReasonID, string Link, string Description, DateTime date);
        Ins_NotificationReason_GetNotificationReasonByID_Result GetNotificationReasonById(int id);
    }
   
}
