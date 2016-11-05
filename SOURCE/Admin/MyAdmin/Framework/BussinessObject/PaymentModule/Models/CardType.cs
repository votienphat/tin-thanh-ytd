namespace BussinessObject.PaymentModule.Models
{
    public class CardType
    {
        public int Id { get; set; }
        public bool IsMaintenance { get; set; }
        public bool IsEnable { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string ActiveImageLink { get; set; }
        public int OrderNo { get; set; }
    }
}