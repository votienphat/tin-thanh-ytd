using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    public enum ExportCardStatus
    {
        [Description("Không đủ thẻ")]
        NotEnoughCard = -1,

        [Description("Thành công")]
        Success = 1,

        [Description("Thất bại")]
        Failed = 0,

        [Description("Lỗi SQL")]
        SqlError = -1002,

        [Description("Lỗi hệ thống")]
        SystemError = -1001
    }
}
