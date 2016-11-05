using System.ComponentModel;

namespace BussinessObject.EventModule.Enum
{
    public enum RunOnEnum
    {
        [Description("Sự kiện chạy khi đăng nhập")]
        Login = 1,

        [Description("Sự kiện chạy khi đăng ký")]
        Register = 2,

        [Description("Nạp gold")]
        Deposit = 3,
    }

    public enum TypeEventPromotion
    {
        [Description("Event khuyến mãi")]
        EventKhuyenMai = 1,

        [Description("Event show top trong game")]
        EventShowTop =2
    }
}
