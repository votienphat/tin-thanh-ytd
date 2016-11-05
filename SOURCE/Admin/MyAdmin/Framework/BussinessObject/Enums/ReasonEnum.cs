using System.ComponentModel;

namespace BussinessObject.Enums
{
    public enum ReasonEnum
    {

        /// <summary>
        /// Nap tien bang Google wallet
        /// </summary>
        NapTienGoogleWallet = 2,

        TangGoldMoiBan = 22,

        [Description("Trừ xu trong sao để đổi thưởng")]
        SubtractStar = 23,

        [Description("Cộng xu tích lũy được vào sao")]
        AddStar = 24,

        [Description("Trừ tiền quảng cáo bán sản phẩm từ chợ trời")]
        SubtractCoinPromotionKiot = 27,

        [Description("Trừ tiền xóa sản phẩm khỏi kiot từ chợ trời")]
        SubtractRemoveItemGameKiot = 30,

        [Description("Xuất thẻ cào cho user nhận quà")]
        ExportCard = 32,

        [Description("Nhập thẻ cào vào kho")]
        ImportCard = 33,

        [Description("Tặng gold khi hết thẻ cào")]
        OpenGiftGold = 34,

        [Description("Tặng gold nạp tiền lần đầu")]
        NapTienLanDau = 36,

        [Description("Tặng gold nạp tiền theo mệnh giá")]
        NapTienTheoMenhGia = 37,

        [Description("Hoàn gold khi đổi thẻ không thành công")]
        HoanGoldDoiTheKhongThanhCong = 38,
    }
}