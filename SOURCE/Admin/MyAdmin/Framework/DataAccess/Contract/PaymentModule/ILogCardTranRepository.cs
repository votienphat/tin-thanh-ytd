using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;
namespace DataAccess.Contract.PaymentModule
{
    public interface ILogCardTranRepository : IDaoRepository<LogCardTran>
    {
        int Insert(int userId, string serial, string pinCode, int cardType,
            string iPAddress, int platformId, int result, int intChannelID, string strVersionGame);

        bool Update(int userId, int transId, string partnerTransId, int status, int cardAmount, int partnerId, string partnerMessage);
    }
}
