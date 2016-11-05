using System.ComponentModel;

namespace BusinessObject.MembershipModule.Enums
{
    public enum PageFunctionEnum
    {
        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Action cap nhat khuyen mai</para>
        /// </summary>
        DieuChinhKhuyenMaiUpdate = 24,

        /// <summary>
        /// Khóa user
        /// 26/1/2016 MinhT Create New
        /// </summary>
        LockUser = 1529,

        /// <summary>
        /// Mở khóa user
        /// 26/1/2016 MinhT Create New
        /// </summary>
        UnLockUser = 1530,

        /// <summary>
        /// Thay đổi Password Login
        /// 26/1/2016 MinhT Create New
        /// </summary>
        ChangePass = 1535,

        /// <summary>
        /// Kick User
        /// 26/1/2016 MinhT Create New
        /// </summary>
        KickUser = 1526,

        /// <summary>
        /// Thay đổi thông tin profile của user
        /// 26/1/2016 MinhT Create New
        /// </summary>
        EditProfileUser = 1534,

        /// <summary>
        /// Duyệt đổi thẻ
        /// 17/2/2016 MinhT Create New
        /// </summary>
        ApprovalExchangeCard = 523,

        /// <summary>
        /// Hủy đổi thẻ
        /// 17/2/2016 MinhT Create New
        /// </summary>
        CancelExchangeCard = 524,

        /// <summary>
        /// Nạp gold cho user
        /// 17/2/2016 MinhT Create New
        /// </summary>
        AddGold = 525,

        /// <summary>
        /// Trừ gold cho user
        /// 17/2/2016 MinhT Create New
        /// </summary>
        AbtractGold = 526,

        /// <summary>
        /// Cập nhật thông tin vật phẩm
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateItem = 527,

        /// <summary>
        /// Cập nhật số lượng rớt vật phẩm mỗi ngày
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateQuantityItem = 528,

        /// <summary>
        /// Cập nhật trạng thái vật phẩm
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateStatusItem = 529,

        /// <summary>
        /// Tạo vật phẩm mới
        /// 17/2/2016 MinhT Create New
        /// </summary>
        CreateNewItem = 530,

        /// <summary>
        ///  Tạo mới BroadCast
        /// 17/2/2016 MinhT Create New
        /// </summary>
        CreateBroadCast = 531,

        /// <summary>
        ///  Xóa BroadCast
        /// 17/2/2016 MinhT Create New
        /// </summary>
        DeteleBroadCast = 532,

        /// <summary>
        ///  Update BroadCast
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateBroadCast = 533,

        /// <summary>
        ///  tạo bài viết mới
        /// 17/2/2016 MinhT Create New
        /// </summary>
        CreateArticle = 534,

        /// <summary>
        ///  Update bài viết
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateArticle = 535,

        /// <summary>
        ///  Update mệnh giá thẻ nạp và result cho LogCardTrans
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateCardAmountAndResult = 536,

        /// <summary>
        ///  Update trạng thái Farmer
        /// 22/3/2016 MinhT Create New
        /// </summary>
        UpdateFarmer = 537,

        /// <summary>
        ///  Admin hủy đổi thẻ
        /// 22/3/2016 MinhT Create New
        /// </summary>
        AdminCancelApprovalExchange = 538,

        /// <summary>
        ///  Admin hoàn gold
        /// 22/3/2016 MinhT Create New
        /// </summary>
        AdminHoanGold = 539,
        /// <summary>
        ///  Thêm mới gian hàng
        /// 22/3/2016 MinhT Create New
        /// </summary>
        AddNewKiot = 540,
        /// <summary>
        ///  Cập nhật IsDisable gian hàng
        /// 22/3/2016 MinhT Create New
        /// </summary>
        UpdateIsDisableKiot = 541,
        /// <summary>
        ///  Cập nhật gian hàng
        /// 22/3/2016 MinhT Create New
        /// </summary>
        UpdateKiot = 542,
        /// <summary>
        ///  Xóa gian hàng
        /// 22/3/2016 MinhT Create New
        /// </summary>
        DeleteKiot = 543,

        /// <summary>
        ///  Update trạng thái bài viết
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateStatusArticle = 1536,
        /// <summary>
        ///  Tạo popup khuyến mãi
        /// 17/2/2016 MinhT Create New
        /// </summary>
        CreatePopupPromotion = 1537,
        /// <summary>
        ///  Edit popup khuyến mãi
        /// 17/2/2016 MinhT Create New
        /// </summary>
        EditPopupPromotion = 1538,
        /// <summary>
        ///  Delete popup khuyến mãi
        /// 17/2/2016 MinhT Create New
        /// </summary>
        DeleteEditPopupPromotion = 1539,

        /// <summary>
        /// Tặng gold chơi liên tục
        /// 17/2/2016 MinhT Create New
        /// </summary>
        EventTangGoldOnline = 1540,

             /// <summary>
        /// Tặng gold chơi hàng ngày
        /// 17/2/2016 MinhT Create New
        /// </summary>
        EventTangGoldHangNgay = 1541,

        /// <summary>
        /// Tặng gold theo nhiệm vụ
        /// 17/2/2016 MinhT Create New
        /// </summary>
        EventTangGoldTheoNhiemVu = 1542,
        /// <summary>
        /// Đăng nhập tặng gold
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateEventTangGold = 1543,

        /// <summary>
        /// Cập nhật cá Boss
        /// 17/2/2016 MinhT Create New
        /// </summary>
        UpdateFish = 1544,
        /// <summary>
        /// Shutdown tất cả server
        /// 17/2/2016 MinhT Create New
        /// </summary>
        ShutdownAllMaster = 1545,
        /// <summary>
        /// Shutdown 1 server
        /// 17/2/2016 MinhT Create New
        /// </summary>
        Shutdown1Server = 1546,

        /// <summary>
        /// Gửi inbox cho user
        /// 17/2/2016 TanPVD Create New
        /// </summary>
        SendInbox = 1547,
        /// <summary>
        /// Thêm mới cấu hình rớt vật phẩm
        /// 17/2/2016 MinhT Create New
        /// </summary>
         
        [Description("Tạo mới cấu hình rớt hộp quà")]
        AddNewConfigFallGiftBox = 1548,
        /// <summary>
        /// Update cấu hình rớt vật phẩm
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Cập nhật cấu hình rớt hộp quà")]
        UpdateConfigFallGiftBox = 1549,
        /// <summary>
        /// Delete cấu hình rớt vật phẩm
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Xóa cấu hình rớt hộp quà")] 
        DeleteConfigFallGiftBox = 1550,

        /// <summary>
        /// Cập nhật cấu hình server
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Cập nhật cấu hình server")]
        UpdateConfigServer = 1551,
        /// <summary>
        /// Cập nhật cấu hình server
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Xóa channel")]
        DeleteChannel = 1552,
        /// <summary>
        /// Cập nhật kho
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Cập nhật kho")]
        UpdateStock = 1553,

        /// <summary>
        /// Cập nhật thông tin Skill
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Cập nhật thông tin Skill")]
        UpdateSkillInfor = 1554,


        /// <summary>
        /// Cập nhật cấu hình may mắn hằng ngày
        /// 17/2/2016 MinhT Create New
        /// </summary>
        [Description("Cập nhật cấu hình may mắn hằng ngày")]
        UpdateLuckyEveryday = 1555,

        /// <summary>
        /// Cập nhật cấu hình danh sách Stock
        /// 21/2/2016 ThoaiND Update
        /// </summary>
        [Description("Cập nhật cấu hình Stock")]
        UpdateConfigListStock = 1556,

        /// <summary>
        /// Thêm mới cấu hình danh sách Stock
        /// 22/2/2016 ThoaiND Create New
        /// </summary>
        [Description("Thêm mới cấu hình Stock")]
        InsertConfigListStock = 1557,

        /// <summary>
        /// Xóa cấu hình danh sách Stock
        /// 22/2/2016 ThoaiND Delete
        /// </summary>
        [Description("Xóa cấu hình Stock")]
        DeleteConfigListStock = 1558,

        /// <summary>
        /// Cập nhật cấu hình chi tiết Stock
        /// 21/2/2016 ThoaiND Update
        /// </summary>
        [Description("Cập nhật chi tiết Stock")]
        UpdateConfigDetailStock = 1556,

        /// <summary>
        /// Thêm mới cấu hình chi tiết Stock
        /// 22/2/2016 ThoaiND Create New
        /// </summary>
        [Description("Thêm mới chi tiết Stock")]
        InsertConfigDetailStock = 1557,

        /// <summary>
        /// Xóa cấu hình chi tiết Stock
        /// 22/2/2016 ThoaiND Delete
        /// </summary>
        [Description("Xóa chi tiết Stock")]
        DeleteConfigDetailStock = 1558,

        /// <summary>
        /// Cập nhật cấu hình bảo trì đổi thưởng
        /// 23/6/2016 ThoaiND Update
        /// </summary>
        [Description("Cập nhật cấu hình bảo trì đổi thưởng")]
        UpdateConfigExchangeCard = 1559,

        /// <summary>
        /// Cập nhật cấu hình bảo trì thẻ nạp
        /// 23/6/2016 ThoaiND Update
        /// </summary>
        [Description("Cập nhật cấu hình bảo trì thẻ nạp")]
        UpdateConfigPaymentCard = 1560,
    }
}