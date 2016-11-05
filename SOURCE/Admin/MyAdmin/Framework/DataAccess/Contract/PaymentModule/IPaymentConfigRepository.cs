using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.PaymentModule
{
    public interface IPaymentConfigRepository : IDaoRepository<PaymentConfig>
    {
        Out_PaymentConfig_GetByAmount_Result GetCoin(decimal cardAmount, int cardType);


        // ReSharper disable once InconsistentNaming
        List<Out_PaymentConfig_GetRateGoogleIAP_Result> GetGoogleIAP(int paymentType);

        IEnumerable<Out_PaymentConfig_GetRateCard_Result> GetRateCard();

        /// <summary>
        /// Lấy danh sách config IsEnable = true theo channel và payment type
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        /// <history>
        /// 01/6/2016 Create by TaiNM
        /// </history>
        List<Out_PaymentConfig_Channel_PaymentType_Result> GetPaymentConfig_Channel_PaymentType(int channelId,
            int paymentTypeId);
    }
}
