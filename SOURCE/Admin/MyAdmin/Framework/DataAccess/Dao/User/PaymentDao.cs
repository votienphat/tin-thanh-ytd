using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.EF;
using DataAccess.Interface;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Dao.User
{
    public class PaymentDao : DaoFactories<UserEntities, Payment>, IPaymentDao
    {
        public Out_LogCardTrans_Insert_Result CardTransLog_Insert(int userId, string serial, string pinCode, int cardType, string iPAddress, int platformId)
        {
            using (Uow)
            {
                return Uow.Context.Out_LogCardTrans_Insert(userId, serial, pinCode, cardType, iPAddress, platformId).FirstOrDefault();
            }
        }



        public int Payment_Insert(int userId, string serial, string pinCode, int cardType, string iPAddress, int platform, int partnerId, string partnerMessage, int cardAmount, int walletType,int result, string transId)
        {
            using (Uow)
            {
                return Uow.Context.Out_Payment(userId, serial, pinCode, cardType, iPAddress, platform, partnerId, partnerMessage, cardAmount, walletType, result, transId);
            }
        }

        
    }
}
