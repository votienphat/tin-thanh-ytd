using BussinessObject.Models;
using BussinessObject.PaymentModule.Models;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.Helper.Contract
{
    public interface IPaymentHelper 
    {
        UseCardResponse UseCard(UseCardModel model, PaymentServiceConfig serviceConfig);

        ExchangeCardResponse DoiTheMoHopQua(int amount, string transId, int cardTypeId,
            PaymentServiceConfig serviceConfig, int userId, out UseCardModel cardInfo);

        bool IsSandboxApple(string receiptData);
    }
}
