
namespace EntitiesObject.Models.PaymentConfigModel
{
    public class PaymentConfigModel
    {
        public int Id { get; set; }

        public int PaymentType { get; set; }

        public double Amount { get; set; }

        public decimal Gold { get; set; }

        public bool IsEnable { get; set; }

        public int ChannelId { get; set; }

        public PaymentConfigModel()
        {
            Id = 0;
            PaymentType = 0;
            Amount = 0;
            Gold = 0;
            IsEnable = false;
            ChannelId = 0;
        }
    }
}
