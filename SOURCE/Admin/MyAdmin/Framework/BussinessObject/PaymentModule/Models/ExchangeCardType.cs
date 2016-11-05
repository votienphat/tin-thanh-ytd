using System.Collections.Generic;

namespace BussinessObject.PaymentModule.Models
{
    public class ExchangeCardType
    {
        public int Id { get; set; }
        public bool IsMaintenance { get; set; }
        public bool IsEnable { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string ImageLink { get; set; }
        public string ActiveImageLink { get; set; }
        public int OrderNo { get; set; }
        public List<ExchangeCardAmount> CardAmounts { get; set; }

        public ExchangeCardType()
        {
            CardAmounts = new List<ExchangeCardAmount>();
        }
    }
}