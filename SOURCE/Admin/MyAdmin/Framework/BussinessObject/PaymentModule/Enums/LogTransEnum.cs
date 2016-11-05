using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    public enum LogTransEnum
    {
        [Description("Nạp gold qua thẻ cào")]
        NapGoldTheCao = 1,

        [Description("Khuyến mãi 100% qua thẻ cào")]
        NapGoldTheCaoKhuyenMai100 = 2,

        [Description("Nạp gold qua ngân lượng")]
        NapGoldNganLuong = 3,

        [Description("Khuyến mãi nạp gold qua ngân lượng")]
        NapGoldNganLuongKhuyenMai = 4,

        [Description("Khuyến mãi nạp gold qua sms")]
        NapGoldSms = 5,

        [Description("Khuyến mãi nạp gold qua sms")]
        NapGoldSmsKhuyenMai = 6,

        [Description("Khuyến mãi tiếp sức qua thẻ cào")]
        NapGoldTheCaoKhuyenMaiTiepSuc = 7,

        [Description("Khuyến mãi tích lũy qua thẻ cào")]
        NapGoldTheCaoKhuyenMaiTihcLuy = 8,

        [Description("Khuyến mãi chọn lọc qua thẻ cào")]
        NapGoldTheCaoKhuyenMaiChonLoc = 9,

        [Description("Khuyến mãi ngày vàng qua thẻ cào")]
        NapGoldTheCaoKhuyenMaiNgayVang = 10,

        [Description("Lì xì tết")]
        LiXiTet = 11,

        [Description("Chuyển tiền vào tài khoản vào Ví ngân hàng")]
        ChuyenGoldTuTaiKhoanVaoViNganHang = 12,

        [Description("Chuyển tiền từ Ví ngân hàng vào tài khoản")]
        ChuyenGoldTuViNganHangVaoTaiKhoan = 13,

        [Description("Tặng Gold cho User Eway đăng nhập trên Mwork")]
        CongGoldEvent03 = 14,

        [Description("Đổi gold ra thẻ cào")]
        DoiGoldRaTheCao = 15,

        [Description("Đổi sao ra gold")]
        DoiSaoRaGold = 16,

        [Description("Đổi gold ra quà")]
        DoiGoldRaVatPham = 17,

        [Description("Mua Gold đai lý")]
        NapGoldDaiLy = 18,

        [Description("Khuyến mãi mua gold đại lý")]
        NapGoldDaiLyKhuyenMai = 19,

        [Description("Khuyến mãi mua gold đại lý chạm mốc")]
        NapGoldDaiLyKhuyenMaiMuaGoldChamMoc = 20,

        [Description("Tri ân Vip")]
        TriAnVip = 21,

        [Description("Nạp tiền bằng Ví Google")]
        NapTienGoogleWallet = 22,

        [Description("Nạp tiền qua Apple Store")]
        NapTienAppleStore = 23,

        [Description("Tặng Gold khi user cập nhật thông tin tài khoản")]
        TangGoldKhiCapNhatTaiKhoan = 25,

        [Description("Tặng Gold khi user đăng ký tài khoản")]
        TangGoldKhiDangKy = 26,
    }
}