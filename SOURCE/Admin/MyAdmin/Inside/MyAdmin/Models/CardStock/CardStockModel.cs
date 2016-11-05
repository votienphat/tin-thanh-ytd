
namespace MyAdmin.Models.CardStock
{
    public class CardStockModel
    {
        public string CardType { get; set; }

        public string CardSerial { get; set; }

        public string CardPin { get; set; }

        public int Amount { get; set; }

        public CardStockModel()
        {
            CardType = string.Empty;
            CardSerial = string.Empty;
            CardPin = string.Empty;
            Amount = 0;
        }
    }
}