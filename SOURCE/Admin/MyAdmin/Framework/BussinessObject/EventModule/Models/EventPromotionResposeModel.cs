using System.ComponentModel;
using BussinessObject.EventModule.Enum;

namespace BussinessObject.EventModule.Models
{
    public class EventPromotionResposeModel
    {
        public EventCodeEnum EventCode { get; set; }

        public decimal GoldPromotion { get; set; }

        public decimal Coin { get; set; }

        public EventStatusCode Status { get; set; }

        public string Message { get; set; }
    }

    public enum EventStatusCode
    {
        [Description("Thất bại")]
        Failed = -1,

        [Description("Thành công")]
        Success = 0,

        [Description("Event chưa bắt đầu")]
        EventIsNotBegin = 1,

        [Description("Event đã kết thúc")]
        EventIsEnd = 2,

        [Description("Cộng gold không thành công")]
        ErrorWhenAddGold = 3,

        [Description("User không tham gia trong event")]
        UserNotinEvent = 4,

        [Description("Nạp thẻ thành công nhưng lỗi chiết khấu mời bạn")]
        SuccessButFailPromotionInviteFriend = 5,

        [Description("Nạp thẻ thành công nhưng lỗi khuyến mãi thêm phần trăm nạp thẻ")]
        SuccessButFailPromotionPercentCard = 6,

        [Description("Nạp thẻ thành công nhưng lỗi khuyến mãi mệnh giá thẻ")]
        SuccessButFailPromotionValueCard = 7,

        [Description("Nạp thẻ thành công nhưng lỗi khuyến mãi nạp thẻ giờ vàng")]
        SuccessButFailPromotionGoldTimeCard = 9
    }
}
