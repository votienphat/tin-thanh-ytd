using System.ComponentModel;

namespace BusinessObject.Enums
{
    public enum NotificationEnum
    {
        [Description("Đổi thẻ")]
        DoiThe = 1
    }

    public enum CardTypeEnum
    {
        [Description("Mobifone")]
        MOBIFONE = 1,

        [Description("Viettel")]
        VIETTEL = 2,

        [Description("Vinaphone")]
        VINAPHONE = 3,

        [Description("Gate")]
        GATE = 5,

        [Description("Bit")]
        BIT = 4,
    }

    public enum LogCardTransEnum
    {
        ///// <summary>
        ///// Lỗi chưa xác định được.
        ///// </summary>
        //[Description("Unknown")]
        //Fail = 0,

        [Description("Thành công")]
        Success = 1,

        [Description("Thất bại.")]
        Failure = 2,

        /// <summary>
        /// Lỗi khi gọi qua đối tác(rớt mạng hoặc đối tác trả về sai)
        /// </summary>
        [Description("Đang chờ duyệt")]
        WaitApproval = 3,
        [Description("User Hủy đổi thưởng")]
        UserCancel = 4,
        [Description("Admin Hủy đổi thưởng")]
        AdminCancel = 5,
        [Description("Đã hoàn gold cho User")]
        DaHoanGold = 7,
        //[Description("Nạp thẻ thành công nhưng không nhận được gold")]
        //SuccessAllNotGold = 4,

        //[Description("Nạp thẻ thành công nhưng lỗi chiết khấu mời bạn")]
        //SuccessButFailPromotionInviteFriend = 5,

        //[Description("Nạp thẻ thành công nhưng lỗi khuyến mãi thêm phần trăm nạp thẻ")]
        //SuccessButFailPromotionPercentCard = 6,

        //[Description("Nạp thẻ thành công nhưng lỗi khuyến mãi mệnh giá thẻ")]
        //SuccessButFailPromotionValueCard = 7,

        //[Description("Nạp thẻ thành công nhưng lỗi tặng vip code")]
        //SuccessButFailPresentVipCode = 8,

        //[Description("Lỗi hệ thống")]
        //SystemError = 1001,

        //[Description("Nạp thẻ bị timeout")]
        //TimeOut = 1002
    }
    public enum CreditCardTypeEum
    {
        GoogleStore = 1000,
        AppleStore = 1001,
        WindowStore = 1003,
       [Description("-1")]
        All =  -1
    }
}