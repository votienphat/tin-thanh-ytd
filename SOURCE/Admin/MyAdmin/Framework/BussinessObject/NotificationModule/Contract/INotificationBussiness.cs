using BussinessObject.Enums;
using EntitiesObject.Entities.LogManagementEntities;
using System;

namespace BussinessObject.NotificationModule.Contract
{
    public interface INotificationBusiness
    {
        bool AddNotification(NotificationTypeEnum notificationType, bool isEnable, string link, string description, DateTime date);

        void ThreadAddNotification(NotificationTypeEnum notificationType, bool isEnable, string link, string description, DateTime date);

        Ins_NotificationReason_GetNotificationReasonByID_Result GetNotificationReasonById(int id);
    }
}
