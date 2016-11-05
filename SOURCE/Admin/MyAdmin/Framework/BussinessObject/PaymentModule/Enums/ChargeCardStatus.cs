
using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    /// <summary>
    /// 0: Thất bại
    /// 1: Thành Công
    /// 2: Thẻ không tồn tại hoặc không hợp lệ
    /// 3: Chưa áp dụng cho loại thẻ này
    /// 4: User không tồn tại
    /// 5: Gạch thẻ thành công nhưng add gold không thành công
    /// </summary>
    /// 
    public enum ChargeCardStatus
    {
        /// <summary>
        /// Card không hợp lệ
        /// </summary>
        [Description("Card không hợp lệ")]
        InvalidCard = 0,
        /// <summary>
        /// Thành công
        /// </summary>
        [Description("Thành công.")]
        Success = 1,
        /// <summary>
        /// Thất bại
        /// </summary>
        [Description("Thất bại")]
        Failure = 2,
        /// <summary>
        /// Hệ thống Card đang bảo trì
        /// </summary>
        [Description("Hệ thống Card đang bảo trì")]
        CardIsMaintenance = 3,
        /// <summary>
        /// User không tồn tại
        /// </summary>
        [Description("User không tồn tại")]
        NotExistUser = 4,
        /// <summary>
        /// Thành công nhưng không thể Update Gold
        /// </summary>
        [Description("Thành công nhưng không thể Update Gold")]
        SuccessButNotAddGold = 5,
        /// <summary>
        /// Không đủ gold để đổi thẻ cào
        /// </summary>
        [Description("Không đủ gold để đổi")]
        NotEnoughCoin = 6,
        /// <summary>
        /// Hết thẻ trong kho đối tác
        /// </summary>
        [Description("Hết thẻ trong kho")]
        CardOver = 7,
        /// <summary>
        /// User đang trong game.
        /// </summary>
        [Description("User đang trong game.")]
        UserInGame = 8,

        [Description("Lỗi trong quá trình đổi gold, liên hệ bộ phận hỗ trợ để xử lý.")]
        LoiDoiGold = 9,

        [Description("Quá trình đổi thẻ đã hoàn tất. Không thể thực hiện thao tác.")]
        Done = 10,
    }

    /// <summary>
    /// 0: Card không hợp lệ
    /// 1: Thành Công
    /// 2: Thất bại
    /// 3: Chưa áp dụng cho loại thẻ này
    /// 4: User không tồn tại
    /// 5: Gạch thẻ thành công nhưng add gold không thành công
    /// </summary>
    /// 
    public enum ExChangeCardStatus
    {
        /// <summary>
        /// Card không hợp lệ
        /// </summary>
        [Description("Card không hợp lệ")]
        InvalidCard = 0,
        /// <summary>
        /// Thành công
        /// </summary>
        [Description("Đổi thẻ thành công. Vui lòng kiểm tra hộp thư.")]
        Success = 1,
        /// <summary>
        /// Thất bại
        /// </summary>
        [Description("Thất bại")]
        Failure = 2,
        /// <summary>
        /// Chờ duyệt
        /// </summary>
        [Description("Chờ duyệt")]
        Approvaling = 3,
        /// <summary>
        /// User đã hủy
        /// </summary>
        [Description("User đã hủy")]
        UserCancel = 4,
        /// <summary>
        /// Admin hủy
        /// </summary>
        [Description("Admin hủy")]
        AdminCancel = 5,
        /// <summary>
        /// Hệ thống Card đang bảo trì
        /// </summary>
        [Description("Hệ thống đổi thẻ đang bảo trì")]
        CardIsMaintenance = 13,
        /// <summary>
        /// User không tồn tại
        /// </summary>
        [Description("User không tồn tại")]
        NotExistUser = 14,
        /// <summary>
        /// Thành công nhưng không thể Update Gold
        /// </summary>
        [Description("Thành công nhưng không thể Update Gold")]
        SuccessButNotAddGold = 15,
        /// <summary>
        /// Không đủ gold để đổi thẻ cào
        /// </summary>
        [Description("Xu hiện tại của bạn không đủ để tiến hành đổi thưởng. Vui lòng xem chi tiết tại mục Đổi thưởng")]
        NotEnoughCoin = 16,
        /// <summary>
        /// Hết thẻ trong kho đối tác
        /// </summary>
        [Description("Hết thẻ trong kho")]
        CardOver = 17,
        /// <summary>
        /// User đang trong game.
        /// </summary>
        [Description("Bạn vui lòng thoát khỏi game để thực hiện thao tác này.")]
        UserInGame = 18,

        [Description("Lỗi trong quá trình đổi xu, liên hệ bộ phận hỗ trợ để xử lý.")]
        LoiDoiGold = 19,

        [Description("Quá trình đổi thẻ đã hoàn tất. Không thể thực hiện thao tác.")]
        Done = 110,
        [Description("Bạn đã hết hạn mức đối thẻ trong ngày. Vui lòng tiến hành đổi thẻ vào ngày mai.")]
        OverExchanged = 111,
        [Description("Yêu cầu đổi thẻ thành công. Hệ thống sẽ gửi thông tin thẻ cào về hộp thư của bạn sau ít phút.")]
        WaitApproval = 112,
        [Description("Sai mệnh giá.")]
        ErrorCardAmount = 113,
        [Description("Trừ xu thành công, chờ đổi thẻ với đối tác.")]
        TruGoldThanhCongChoDoiTheVoiDoiTac = 114,
        [Description("Tài khoản thẻ đã sử dụng hết hạn mức.")]
        TaiKhoanTheDaSuDungHetHanMuc = 115,
        [Description("Lỗi đổi thẻ từ đối tác.")]
        LoiDoiTheTuDoiTac = 116,
        [Description("Lỗi kiểm tra kho thẻ trả về null.")]
        LoiKiemTraKhoTheTraVeNull = 117,
        [Description("Trừ xu user thất bại.")]
        TruGoldThatBai = 118
    }
}


