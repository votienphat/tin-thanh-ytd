using System;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface ILogCardOpenGiftBoxRepository : IDaoRepository<LogCardOpenGiftBox>
    {
        int Insert(int userId, int transId, int itemGameuserId, int cardType, int cardAmount, DateTime transDate,
            int result, string partnerMessage);

        int Update(int id, string transId, int userId, string serial, string pinCode, int carType, int result, string partnerMessage, int cardAmount);
    }
}
