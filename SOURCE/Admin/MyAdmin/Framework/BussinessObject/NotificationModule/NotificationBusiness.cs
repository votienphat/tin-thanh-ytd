using System;
using BussinessObject.Enums;
using BussinessObject.NotificationModule.Contract;
using DataAccess.Contract.NotificationModule;
using Logger;
using MyUtility.Extensions;
using System.Threading;
using EntitiesObject.Entities.LogManagementEntities;

namespace BussinessObject.NotificationModule
{
    public class NotificationBusiness : INotificationBusiness
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationBusiness(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        //public bool AddNotification(Notification notification)
        //{
        //    return _notificationRepository.AddNotification(notification);
        //}


        public bool AddNotification(NotificationTypeEnum notificationType, bool isEnable, string link, string description, DateTime date)
        {
            if (isEnable)
            {
                try
                {
                    return _notificationRepository.AddNotification(notificationType.Value(), link, description, date);
                }
                catch (Exception e)
                {
                    CommonLogger.DefaultLogger.Error("Add notification", e);
                    return false;
                }
            }
            return true;
        }

        public void ThreadAddNotification(NotificationTypeEnum notificationType, bool isEnable, string link, string description, DateTime date)
        {
            Thread noty = new Thread(() => AddNotification(notificationType, isEnable, link, description, date));
            noty.Start();
        }


        public Ins_NotificationReason_GetNotificationReasonByID_Result GetNotificationReasonById(int id)
        {
            return _notificationRepository.GetNotificationReasonById(id);
        }
    }
}
