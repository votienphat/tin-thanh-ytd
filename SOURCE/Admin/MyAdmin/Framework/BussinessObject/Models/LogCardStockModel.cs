
namespace BussinessObject.Models
{
    public class LogCardStockModel
    {
        public string TransExportId { get; set; }
        public int CardType { get; set; }
        public int CardAmount { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public string CardSerial { get; set; }
        public string CardPin { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public int AdminId { get; set; }
        public string ClientTarget { get; set; }
        public string ClientIp { get; set; }
        public string ClientAgent { get; set; }
        public int ReasonId { get; set; }
        public string ReasonName { get; set; }
        public string CardTypeName { get; set; }
    }
}
