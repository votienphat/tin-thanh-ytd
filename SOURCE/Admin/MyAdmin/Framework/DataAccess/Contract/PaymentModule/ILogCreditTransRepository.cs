using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;
namespace DataAccess.Contract.PaymentModule
{
    public interface ILogCreditTransRepository : IDaoRepository<LogCreditTran>
    {
        Api_LogCreditTrans_InsertDepositInApp_Result LogCreditTrans_InsertDepositInApp(int userId, string transId,
            decimal currentCoin, decimal coinTrans, int status, string description, string token, int cardType,
            string cardTypeName, decimal cardAmount, int intChannelID, int intPlatformId, string strVersionGame);

        int LogCreditTrans_updateData(int id, int status, string token, string description, decimal amount,
            decimal currentCoin, decimal coinTrans);
    }
}
