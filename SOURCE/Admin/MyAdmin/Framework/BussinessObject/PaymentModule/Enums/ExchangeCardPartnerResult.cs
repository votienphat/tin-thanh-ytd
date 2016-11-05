using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    public enum ExchangeCardPartnerResult
    {
        [Description("Lỗi kiểm tra kho thẻ trả về null")]
        LoiKiemTraKhoTheTraVeNull = -2,

        [Description("Lỗi đổi thẻ từ đối tác. Lỗi từ try cache. Timeout")]
        LoiDoiTheTuDoiTac = -1,

        // Danh sach enum cua doi tac

        [Description("Thành công")]
        Success = 0,

        [Description("Có lỗi xảy ra")]
        Failure = 1,

        [Description("Hệ thống đang bảo trì")]
        HeThongDangBaoTri = 2,

        [Description("Sai giao thức")]
        SaiGiaoThuc = 3,

        [Description("Ứng dụng đạt mức yêu cầu")]
        UngDungDatMucYeuCau = 4,

        [Description("IP không có quyền truy cập")]
        IpKhongCoQuyenTruyCap = 5,

        [Description("Yêu cầu phải từ https")]
        YeuCauPhaiTuHttps = 6,

        [Description("User is performing too many actions")]
        UserIsPerformingTooManyActions = 7,

        [Description("Application does not have permission for this action")]
        ApplicationDoesNotHavePermissionForThisAction = 8,

        [Description("This method is deprecated")]
        ThisMethodIsDeprecated = 9,

        [Description("This API version is deprecated")]
        ThisApiVersionIsDeprecated = 10,

        [Description("Invalid parameter. Incorrect input parameters")]
        InvalidParameterIncorrectInputParameters = 11,

        [Description("Unauthorized to use this function")]
        UnauthorizedToUseThisFunction = 12,

        [Description("Bạn đã đăng xuất")]
        BanDaDangXuat = 13,

        [Description("Đăng nhập thất bại, tài khoản và mật khẩu không chính xác")]
        DangNhapThatBaiTaiKhoanMatKhauKhongChinhXac = 14,

        [Description("Hệ thống đang xử lý. Vui lòng truy cập lại sau")]
        HeThongDangXuLy = 15,

        [Description("OrderID không hợp lệ")]
        OrderIdKhongHopLe = 20,

        [Description("OrderID đã tồn tại")]
        OrderIdKhongTonTai = 21,

        [Description("Loại thẻ không hợp lệ")]
        LoaiTheKhongHopLe = 22,

        [Description("Mã Pin không hợp lệ")]
        MaPinKhongHopLe = 23,

        [Description("Số Serial không hợp lệ")]
        SoSerialKhongHopLe = 24,

        [Description("Nhà mạng đang bảo trì")]
        NhaMangDangBaoTri = 25,

        [Description("Tài khoản bị khoá")]
        TaiKhoanBiKhoa = 26,

        [Description("Đã hết thời gian")]
        DaHetThoiGian = 27,

        [Description("Chữ ký không hợp lệ")]
        ChuKyKhongHopLe = 28,

        [Description("Module không hợp lệ")]
        ModuleKhongHopLe = 29,

        [Description("Action không hợp lệ")]
        ActionKhongHopLe = 30,

        [Description("Time không hợp lệ")]
        TimeKhongHopLe = 31,

        [Description("Price không hợp lệ")]
        PriceKhongHopLe = 32,

        [Description("App đã hết ngân sách kho thẻ, vui lòng liên hệ")]
        AppDaHetNganSachKhoThe = 33,

        [Description("Kho đang hết thẻ, vui lòng liên hệ")]
        KhoDangHetThe = 34,

        [Description("Không tồn tại giao dịch")]
        KhongTonTaiGiaoDich = 35,

        [Description("Số lượng thẻ không hợp lệ")]
        SoLuongTheKhongHopLe = 36,

        [Description("Bạn không có quyền truy cập kho thẻ này")]
        BanKhongCoQuyenTruyCapKhoTheNay = 37,

        [Description("Data không hợp lệ")]
        DataKhongHopLe = 38,

        [Description("Data mã hóa không hợp lệ")]
        DataMaKhoaKhongHopLe = 39,

        [Description("Dữ liệu mã hóa không hợp lệ")]
        DuLieuMaHoaKhongHopLe = 40,

        [Description("Thẻ đã được sử dụng")]
        TheDaDuocSuDung = 50,

        [Description("Thẻ đã bị khoá")]
        TheDaBiKhoa = 51,

        [Description("Thẻ đã hết hạn")]
        TheDaHetHan = 52,

        [Description("Thẻ chưa kích hoạt")]
        TheChuaKichHoat = 53,

        [Description("Thẻ không hợp lệ")]
        TheKhongHopLe = 54,

        [Description("Thẻ không tồn tại hoặc đã sử dụng")]
        TheKhongTonTai = 55,

        [Description("Mobiphone đang bảo trì")]
        MobiPhoneBaoTri = 56,

        [Description("Hệ thống đang quá tải")]
        HeThongQuaTai = 57,

        [Description("Có lỗi xẩy ra")]
        CoLoiXayRa = 58,

        [Description("CP không hợp lệ")]
        CpKhongHopLe = 59,

        [Description("Hệ thống đang bảo trì, vui lòng nạp lại sau")]
        HeThongBaoTri = 60,

        [Description("Mã thẻ chỉ được sử dụng để rút tiền mặt tại doithe.net")]
        MaTheChiDuocSuDungRutTienTaiDoiTheNet = 61,

        [Description("Vui lòng nhập tài khoản")]
        VuiLongNhapTaiKhoan = 100,

        [Description("Mật khẩu Tài khoản không hợp lệ")]
        MatKhauTaiKhoanKhongHopLe = 101,

        [Description("App key Tài khoản không hợp lệ")]
        AppKeyTaiKhoanKhongHopLe = 102,

        [Description(" key Tài khoản không hợp lệ")]
        SecretKeyTaiKhoanKhongHopLe = 103,

        [Description("App key hay secret key Tài khoản không hợp lệ")]
        AppKeyHoacSecretKeyKhongHopLe = 104,

        [Description("Đang nhập thất bại")]
        DangNhapThatBai = 105,

        [Description("Access token không hợp lệ")]
        AccessToKenKhongHopLe106 = 106,

        [Description("Access token không hợp lệ")]
        AccessToKenKhongHopLe107 = 107,

        [Description("ClientID không hợp lệ")]
        ClientIdKhongHopLe = 108,

        [Description(" App không hợp lệ hoặc đang bị khoá")]
        AppKhongHopLeHoacDangBiKhoa = 109,

        [Description("Target ID không hợp lệ")]
        TargetIdKhongHopLe = 110,

        [Description("Amount không hợp lệ")]
        AmountKhongHopLe = 120,

        [Description("Tài khoản không đủ tiền")]
        TaiKhoanKhongKhongDuTien = 121,

        [Description("ActionID không hợp lệ")]
        ActionIdKhongHopLe = 130,

        [Description("ActionID không hợp lệ")]
        ActionIdKhongHopLe131 = 131,

        [Description("Facebook Access token không hợp lệ")]
        FaceBookAccessTokenKhongHopLe = 200,

        [Description("Tài khoản không tồn tại")]
        TaiKhoanKhongTonTai = 201,

        [Description("Facebook ID không hợp lệ")]
        FacebookIdKhongHopLe = 202,

        [Description("Đăng nhập Facebook thất bại")]
        DangNhapFacebookThatBai = 203,

        [Description("Tài khoản không hợp lệ")]
        TaiKhoanKhongHopLe = 300,

        [Description("Mật khẩu không hợp lệ")]
        MatKhaiKhongHopLe = 301,

        [Description("Giới tính không hợp lệ")]
        GioiTinhKhongHopLe = 302,

        [Description("Ngày sinh không hợp lệ")]
        NgaySinhKhongHopLe = 303,

        [Description("Tài khoản đã tồn tại")]
        TaiKhoanDaTonTai = 304,

        [Description("Đăng ký không thành công")]
        DangKyKhongThanhCong = 305,

        [Description("Tài khoản không hợp lệ")]
        TaiKhoanKhongHopLe306 = 306,

        [Description("Mật khẩu không hợp lệ")]
        MatKhauKhongHopLe = 307,

        [Description("Tài khoản không tồn tại")]
        TaiKhoanKhongTonTai308 = 308,

        [Description("Tài khoản có dài 6-32 ký tự Thường và Số")]
        TaiKhoanCoDai632KyTuThuongVaSo = 309,

        [Description("UserID không hợp lệ")]
        UserIdKhongHopLe = 310,

        [Description("Tài khoản không tồn tại")]
        TaiKhoanKhongTonTai311 = 311,

        [Description("Tài khoản không Email")]
        TaiKhoanKhongEmail = 312,

        [Description("Email không hợp lệ")]
        EmailKhongHopLe = 320,

        [Description("Email đã tồn tại")]
        EmailDaTonTai = 400,

        [Description("Mật khẩu phải có chiều dài từ 6-32 ký tự")]
        MatKhauPhaiCoChieuDaiTu632KyTu = 400,

        [Description("Mật khẩu phải có chiều dài từ 6-32 ký tự")]
        MatKhauPhaiCoChieuDaiTu632KyTu401 = 401,

        [Description("Mật khẩu mới phải có chiều dài từ 6-32 ký tự")]
        MatKhauMoiPhaiCoChieuDaiTu632KyTu401 = 402,

        [Description("Mật khẩu không giống nhau")]
        MatKhauKhongGiongNhau = 403,

        [Description("Mật khẩu không giống nhau")]
        MatKhauKhongGiongNhau404 = 404,

        [Description("FacebookID không hợp lệ")]
        FacebookIdKhongHopLe410 = 410,

        [Description("CPKey không hợp lệ")]
        CpKeyKhongHopLe = 500,

        [Description("CPSecretKey không hợp lệ")]
        CpSecretKeyKhongHopLe = 501,

        [Description("CP không hợp lệ")]
        CpKhongHopLe502 = 502,

        [Description("CP không hợp lệ")]
        CpKhongHopLe503 = 503,

        [Description("CPCode không hợp lệ")]
        CpCodeKhongHopLe = 504,

        [Description("FromDate không hợp lệ")]
        FromDateKhongHopLe = 505,

        [Description("ToDate không hợp lệ")]
        ToDateKhongHopLe = 506,

        [Description("Sign không hợp lệ")]
        SignKhongHopLe = 510,

        [Description("Sign không hợp lệ")]
        SignKhongHopLe511 = 511,

        [Description("Đã hết thời gian, thời gian gọi:%time_request, thời gian API: %time_server")]
        DaHetThoiGianApi = 520,

        [Description("Algorithm không hợp lệ")]
        AlgorithmKhongHopLe = 530,

        [Description("Chữ ký không hợp lệ")]
        ChuKyKhongHopLe540 = 540,

        [Description("Lời yêu cầu đã hết thời gian")]
        LoiYeuCauDaHetThoiGian = 550,

        [Description("OrderID không hợp lệ")]
        OrderIdKhongHopLe560 = 560,

        [Description("Giao dịch đã hết hạn")]
        GiaoDichDaHetHan = 570,

        [Description("Code không hợp lệ")]
        CodeKhongHopLe = 900,

        [Description("Code không hợp lệ hoặc hết hạn")]
        CodeKhongHopLeHoacHetHan = 901,

        [Description("client_secret không hợp lệ")]
        ClientSecretKhongHopLe = 910,

        [Description("client_id không hợp lệ")]
        ClientIdKhongHopLe920 = 920,

        [Description("Tài khoản chưa đăng nhập")]
        TaiKhoanChuaDangNhap = 930,

        [Description("Access token có lỗi")]
        AccessTokenCoLoi = 931,

        [Description("Access token không hợp lệ")]
        AccessTokenKhongHopLe = 932,

        [Description("Data không hợp lệ")]
        DataKhongHopLe999 = 999,

        [Description("Lổi khác")]
        LoiKhac = 1000,

        [Description("App invalid or block")]
        AppInvaliedOrBlock = 1100,

        [Description("Request and Sign invalid")]
        RequestAndSignInvalid = 1200,

        [Description("Request invalid")]
        RequestInvalid = 1201,

        [Description("Error Authen")]
        ErrorAuthen = 1206,

        [Description("Time Expired")]
        TimeExpired = 1207,

        [Description("Coin invalid")]
        CoinInvalid = 1208,

        [Description("Error OrderID")]
        ErrorOrderId = 1209,

        [Description("OrderID exists")]
        OrderIdExists = 1210,

        [Description("Error AccessToken")]
        ErrorAccessToken = 1211,

        [Description("Error Token")]
        ErrorToken = 1212,

        [Description("CCoin not enough")]
        CCoinNotEnough = 1213,

        [Description("Cannot init Transaction")]
        CannotInitTransaction = 1214,

        [Description("Error Json Data")]
        ErrorJsonData = 1215,
    }
}


