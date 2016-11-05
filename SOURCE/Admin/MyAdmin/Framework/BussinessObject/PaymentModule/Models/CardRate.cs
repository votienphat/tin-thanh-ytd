
using BussinessObject.Enums;

namespace BussinessObject.PaymentModule.Models
{
    public class CardRate
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public int CardType { get; set; }
        public decimal Amount { get; set; }
        public decimal Gold { get; set; }
    }
    
}