using BussinessObject.Models;
using BussinessObject.PaymentModule.Enums;

namespace BussinessObject.PaymentModule.Models
{
    public class ExchangeCardResponse
    {
        public ExChangeCardStatus Result { get; set; }
        public string Serial { get; set; }
        public string PinCode { get; set; }
        public string Message { get; set; }
        public string TransId { get; set; }

        public ExchangeCardPartnerResult PartnerResult { get; set; }
        public string PartnerMessage { get; set; }
    }

}