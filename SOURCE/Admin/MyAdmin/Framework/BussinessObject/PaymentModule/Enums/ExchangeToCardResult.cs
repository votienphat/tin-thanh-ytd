using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    public enum ExchangeToCardResult
    {
        [Description("Thất bại")]
        Failed = 2,

        [Description("Thành công")]
        Success = 1,

        [Description("Không tìm thấy loại Card/ Card không hợp lệ")]
        NotFoundCardType = 3,
    }
}