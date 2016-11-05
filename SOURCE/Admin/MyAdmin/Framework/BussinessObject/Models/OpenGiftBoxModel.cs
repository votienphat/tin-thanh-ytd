using BussinessObject.PaymentModule.Enums;
using BussinessObject.PaymentModule.Models;

namespace BussinessObject.Models
{
    public class OpenGiftBoxModel 
    {
        public UseCardModel UseCardModel { get; set; }

        public int GiftBoxEnum { get; set; }

        public string Result { get; set; }
        public string ItemName { get; set; }
        public string ImagePath { get; set; }
        public string MessagePopup { get; set; }
        public string MessageMail { get; set; }
        public string ManhGhep { get; set; }
        public int UserId { get; set; }
    }
}
