using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    public enum ResultCheckEnoughGold
    {
        [Description("Thất bại do không tìm thấy loại card này hoặc do isEnable = false")]
        CardTypeNotavailable = -1,
        [Description("Thất bại do không đủ gold")]
        NotEnoughGold = 0,
        [Description("Đủ gold để đổi")]
        Enough = 1
    }
}