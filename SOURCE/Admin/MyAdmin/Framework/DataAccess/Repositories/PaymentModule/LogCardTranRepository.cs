using System.Linq;
using DataAccess.Contract.PaymentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.PaymentModule
{
    public class LogCardTranRepository : DaoRepository<UserEntities, LogCardTran>, ILogCardTranRepository
    {
        public int Insert(int userId, string serial, string pinCode, int cardType, string iPAddress, int platformId,int result, int intChannelID, string strVersionGame)
        {
            return Uow.Context.Out_LogCardTrans_Insert(userId, serial, pinCode, cardType, iPAddress, platformId, result, intChannelID, strVersionGame).FirstOrDefault().GetValueOrDefault();
        }


        public bool Update(int userId, int transId, string partnerTransId, int status, int cardAmount, int partnerId, string partnerMessage)
        {
            var result =
                Uow.Context.Out_LogCardTrans_Update(userId,transId, partnerTransId, status, cardAmount, partnerId,
                    partnerMessage).FirstOrDefault();
            return result > 0;
        }
    }
}
