using System.ComponentModel;

namespace BusinessObject.MembershipModule.Enums
{
    public enum ActionAdminType
    {
        
        /// <summary>
        /// Thay đổi Password
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Thay đổi Password")]
        ChangePass = 1535,
        /// <summary>
        /// Kick User
        /// 26/1/2016 MinhT Create New
        /// </summary>
        [Description("Kick User")]
        KickUser = 1526,
        /// <summary>
        /// Khóa user
        /// 26/1/2016 MinhT Create New
        /// </summary>
        [Description("Khóa User")]
        LockUser = 1529,
        /// <summary>
        /// Duyệt đổi thẻ
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Duyệt đổi thẻ")]
        ApprovalExchangeCard = 523,
       

        /// <summary>
        /// Hủy đổi thẻ
        /// 17/2/2016 MinhT Create New
        /// </summary>
         [Description("Hủy đổi thẻ")]
        CancelExchangeCard = 524,

        /// <summary>
        /// Nạp gold cho user
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Nạp gold")]
        AddGold = 525,
        /// <summary>
        /// Trừ gold cho user
        /// 17/2/2016 MinhT Create New
        /// </summary>
         [Description("Trừ gold")]
        AbtractGold = 526,
        

        /*[Description("Nạp Gold")]
        NapGold = 1,

        [Description("Trừ Gold")]
        TruGold = 2,

        [Description("Nạp Sao")]
        NapSao = 3,

        [Description("Trừ Sao")]
        TruSao = 4,

        [Description("Trả Gold do lỗi hệ thống")]
        TraGoldLoiHeThong = 5,
        
        [Description("Khác")]
        Khac = 6,

        [Description("Tặng Gold User")]
        TangGoldUser = 7,

        [Description("Thu hồi Gold do vi phạm")]
        ThuHoiGoldViPham = 8,

        [Description("Thu Hồi Gold do nạp sai")]
        ThuHoiGoldNapSai = 9,

        [Description("Tặng Sao cho user")]
        Khac = 10,

        [Description("Tặng sao do giao dịch lỗi")]
        Khac = 11,

        [Description("Trả Gold do lỗi thẻ")]
        Khac = 12,

        [Description("Thu hồi sao do đánh bồ")]
        Khac = 13,

        [Description("Trả Gold do lỗi nạp NganLuong")]
        Khac = 14,

        [Description("Thu hồi sao hack nick")]
        Khac = 15,

        [Description("Khác")]
        Khac1 = 16,

        [Description("Khác")]
        Khac1 = 16,

        [Description("Khác")]
        Khac1 = 16,

        [Description("Khác")]
        Khac1 = 16,

        [Description("Khác")]
        Khac1 = 16,

        [Description("Khác")]
        Khac1 = 16,

        [Description("Khác")]
        Khac1 = 16,

        [Description("Khác")]
        Khac1 = 16,

        Nạp thẻ
        Nạp Google IAP
        Chơi game
        Đổi thẻ
        Thắng trong game
        Chuyển tiền vào rương
        Rút tiền ra khỏi rương
        User hủy đổi thưởng
        Hoàn Gold Duyệt Thẻ
        Tặng Gold Đăng Ký
        Tặng Gold Đăng Nhập
        Tặng xu chơi game liên tục
        Admin Trừ gold
        Admin Chuyển gold
        Bắn cá boss
        Mua item
        Bán item
        Hoàn thành nhiệm vụ
        Sử dụng item
        Event Tặng Xu đăng nhập hằng ngày
        Xu triệt tiêu từ đấu trường
        Tặng Gold mời bạn
        Trừ xu trong sao để đổi thưởng
        Cộng xu tích lũy được vào sao
        Tặng tiền cho bạn
        Nhận tiền từ bạn*/
    }
}