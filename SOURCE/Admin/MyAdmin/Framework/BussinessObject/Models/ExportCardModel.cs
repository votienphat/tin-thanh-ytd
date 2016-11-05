using BussinessObject.PaymentModule.Enums;

namespace BussinessObject.Models
{
    public class ExportCardModel
    {
        public string CardPin { get; set; }
        public string CardSerial { get; set; }
        public string CardType { get; set; }
        public int Amount { get; set; }
        public long ExportId { get; set; }
        public ExportCardStatus ExportCardStatusExport { get; set; }
    }
}
