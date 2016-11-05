using System;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class LogCardOpenGiftBoxRepository : DaoRepository<UserEntities, LogCardOpenGiftBox>,
        ILogCardOpenGiftBoxRepository
    {
        public int Insert(int userId, int transId, int itemGameuserId, int cardType, int cardAmount, DateTime transDate,
            int result, string partnerMessage)
        {
            var firstOrDefault = Uow.Context.Out_LogCardOpenGiftBox_Insert(userId, transId, itemGameuserId, cardType, cardAmount,
                transDate, result, partnerMessage).FirstOrDefault();
            if (firstOrDefault != null)
                return
                    firstOrDefault.Value;
            return -1;
        }

        public int Update(int id, string transId, int userId, string serial, string pinCode,int carType, int result, string partnerMessage, int cardAmount)
        {
            var firstOrDefault = Uow.Context.Out_LogCardOpenGiftBox_Update(id, transId, userId, serial, pinCode, result, partnerMessage, carType, cardAmount).FirstOrDefault();
            if (
                firstOrDefault != null)
                return firstOrDefault.Value;
            return -1;
        }
    }
}
