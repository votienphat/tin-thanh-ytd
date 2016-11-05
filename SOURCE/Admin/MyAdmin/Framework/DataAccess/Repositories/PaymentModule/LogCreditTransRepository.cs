using System.Linq;
using DataAccess.Contract.PaymentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.PaymentModule
{
    public class LogCreditTransRepository : DaoRepository<UserEntities, LogCreditTran>,ILogCreditTransRepository
    { 
        public Api_LogCreditTrans_InsertDepositInApp_Result LogCreditTrans_InsertDepositInApp(int userId, string transId,
            decimal currentCoin, decimal coinTrans, int status, string description, string token, int cardType,
            string cardTypeName, decimal cardAmount, int intChannelID, int intPlatformId, string strVersionGame)
        {
            return
                Uow.Context.Api_LogCreditTrans_InsertDepositInApp(userId, transId, currentCoin, coinTrans, status,
                    description, token, cardType, cardTypeName, cardAmount, intChannelID, intPlatformId, strVersionGame).FirstOrDefault();
        }

        public int LogCreditTrans_updateData(int id, int status, string token, string description, decimal amount,
            decimal currentCoin, decimal coinTrans)
        {
            return Uow.Context.Out_LogCreditTrans_updateData(id, status, token, description, amount, currentCoin,
                coinTrans);
        }
    }
}
