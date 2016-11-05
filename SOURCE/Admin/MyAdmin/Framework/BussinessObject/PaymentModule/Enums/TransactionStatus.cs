
namespace BussinessObject.PaymentModule.Enums
{
    /// <summary>
    /// 0: Thất bại
    /// 1: Thành Công
    /// 2: Mới
    /// 3: TimeOut
    /// 4: Thẻ không tồn tại hoặc không hợp lệ
    /// 5: Lỗi gạch thẻ từ đối tác
    /// 6: Cấu hình tỉ lệ không hợp lệ
    /// </summary>
    public enum TransactionStatus
    {
        Failure = 0,
        Success = 1,
        New = 2,
        TimeOut = 3,
        InvalidCard = 4,
        ErrorFromPartner = 5,
        InvalidConfig = 6
    }

    public enum WalletExchangeStatus
    {
        Failure = 0,
        Success = 1,
        SqlError = -1001,
        SystemError = -1002,
        EnoughGoldForSubtract =-1
    }
}


