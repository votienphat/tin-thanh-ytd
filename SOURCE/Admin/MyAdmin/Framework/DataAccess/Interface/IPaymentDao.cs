using DataAccess.Dao.User;
using EntitiesObject.Entities.UserEntities;
using System.Data.Objects;

namespace DataAccess.Interface
{
    public interface IPaymentDao : IBaseFactories<Payment>
    {
        /// <summary>
        /// Ghi Log khi bắt đầu nạp tiền
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="serial"></param>
        /// <param name="pinCode"></param>
        /// <param name="cardType"></param>
        /// <param name="iPAddress"></param>
        /// <param name="platformId"></param>
        /// <history>
        /// 15/12/2015 MinhT
        /// </history>
        Out_LogCardTrans_Insert_Result CardTransLog_Insert(int userId, string serial, string pinCode, int cardType, string iPAddress, int platformId);

        int Payment_Insert(int userId, string serial, string pinCode, int cardType, string iPAddress, int platform, int partnerId, string partnerMessage, int cardAmount, int walletType, int result, string transId);
    }
}
