using BussinessObject.PaymentModule.Enums;

namespace BussinessObject.PaymentModule.Models
{
    public class ExchangeConfig
    {
        public int UserId { get; set; }
        /// <summary>
        /// Có vượt khỏi số tiền được phép đổi tối đa trong ngày không
        /// </summary>
        public bool IsOverAmount { get; set; } 

        /// <summary>
        /// Có chuyển cho Admin duyệt không
        /// </summary>
        public bool IsApproval { get; set; }

        /// <summary>
        /// Nếu isOverAmount = true thì roundId = 1
        /// </summary>
        public int RoundId { get; set; }

        public ExChangeCardStatus Result { get; set; }
        public string Message { get; set; }

       
    }
}