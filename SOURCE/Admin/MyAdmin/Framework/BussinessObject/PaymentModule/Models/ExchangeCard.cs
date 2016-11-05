using System.Collections.Generic;
using System.ComponentModel;
using BussinessObject.Helper;

namespace BussinessObject.PaymentModule.Models
{
    public class ExchangeCard
    {
        public bool IsMaintenance { get; set; }
        public string Message { get; set; }
        public List<ExchangeCardType> CardTypes { get; set; }
    }
    public class ExchangeCardCancel
    {
        public int Result { get; set; }
        public decimal WalletAmount { get; set; }
    }

    public class ExchangeCardRequest
    {
        public PaymentServiceConfig PaymentService { get; set; }
        public SocketModel Socket { get; set; }

        [DisplayName("CardAmount")]
        public int CardAmount { get; set; }

        [DisplayName("CardType")]
        public int CardType { get; set; }

        [DisplayName("UserID")]
        public int UserId { get; set; }

        [DisplayName("IPAddress")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 1 - Android, 2 - iOS, 3 - Windows Phone
        /// </summary>
        [DisplayName("PlatformID")]
        public int PlatformId { get; set; }

        [DisplayName("HardwareID")]
        public string HardwareId { get; set; }

        public bool IsAutoExchange { get; set; }

        public decimal MaxEchanged { get; set; }

        public decimal MinExchanged { get; set; }

        public int DefaultAdminId { get; set; }

        public int RuleTimePlayGame { get; set; }
        public int ChannelID { get; set; }        
    }
}