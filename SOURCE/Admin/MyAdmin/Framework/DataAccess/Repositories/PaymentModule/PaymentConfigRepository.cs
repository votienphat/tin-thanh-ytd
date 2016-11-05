using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.PaymentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.PaymentModule
{
    public class PaymentConfigRepository : DaoRepository<UserEntities, PaymentConfig>, IPaymentConfigRepository
    {
        public Out_PaymentConfig_GetByAmount_Result GetCoin(decimal cardAmount, int cardType)
        {
            return Uow.Context.Out_PaymentConfig_GetByAmount(cardAmount, cardType).FirstOrDefault();
        }


        public List<Out_PaymentConfig_GetRateGoogleIAP_Result> GetGoogleIAP(int paymentType)
        {
            return Uow.Context.Out_PaymentConfig_GetRateGoogleIAP(paymentType).ToList();
        }

        public IEnumerable<Out_PaymentConfig_GetRateCard_Result> GetRateCard()
        {
            return Uow.Context.Out_PaymentConfig_GetRateCard().ToList();
        }

        /// <summary>
        /// Lấy danh sách config IsEnable = true theo channel và payment type
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        /// <history>
        /// 01/6/2016 Create by TaiNM
        /// </history>
        public List<Out_PaymentConfig_Channel_PaymentType_Result> GetPaymentConfig_Channel_PaymentType(int channelId,
            int paymentTypeId)
        {
            return Uow.Context.Out_PaymentConfig_Channel_PaymentType(channelId, paymentTypeId).ToList();
        }
    }
}
