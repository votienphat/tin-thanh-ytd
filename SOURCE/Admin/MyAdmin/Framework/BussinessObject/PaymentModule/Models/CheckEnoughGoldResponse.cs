using BussinessObject.PaymentModule.Enums;

namespace BussinessObject.PaymentModule.Models
{
    public class CheckEnoughGoldResponse 
    {
        public ExChangeCardStatus ExChangeCardStatus { get; set; }
        public decimal EssentialGold { get; set; }
        public string Message { get; set; }
        public CheckEnoughGoldResponse ()
        {
            ExChangeCardStatus = ExChangeCardStatus.Failure;
            EssentialGold = 0;
        }
    }
}