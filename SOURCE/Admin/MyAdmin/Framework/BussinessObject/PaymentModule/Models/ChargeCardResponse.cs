using BussinessObject.Enums;
using BussinessObject.PaymentModule.Enums;

namespace BussinessObject.PaymentModule.Models
{
    public class ChargeCardResponse
    {
        public ChargeCardStatus Result { get; set; }
        public int UserID { get; set; }
        public decimal CoinTransfer { get; set; }
        public decimal Coin { get; set; }
        public string Message { get; set; }

        public ChargeCardResponse()
        {
            Result = ChargeCardStatus.Failure;
        }
    }
}