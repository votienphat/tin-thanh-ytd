using System;
using BussinessObject.Enums;
using BussinessObject.PaymentModule.Enums;

namespace BussinessObject.Models
{
    public class UseCardModel
    {
        public CardTypeEnum CardType { get; set; }

        public string Serial { get; set; } 

        public string PinCard { get; set; } 

        /// <summary>
        /// ID Giao dịch với đối tác
        /// </summary>
        public string TransId { get; set; }
        public string PartnerTransId { get; set; }
        public int Amount { get; set; }
        /// <summary>
        /// Message gởi về từ đối tác
        /// </summary>
        public string ResponseText { get; set; }
        /// <summary>
        /// Nội dung tin nhắn gởi cho User
        /// </summary>
        public string OfflineMessage { get; set; }
        public DateTime CreateDate { get; set; }
        public int HqCardResponseE { get; set; }
    }
}
