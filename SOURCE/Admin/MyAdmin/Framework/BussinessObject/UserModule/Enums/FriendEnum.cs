using System.ComponentModel;

namespace BussinessObject.UserModule.Enums
{
    public enum FriendEnum
    {
        
    }

    public enum GiftGoldForFriendEnum
    {
        [Description("Đã gửi tặng xu cho thành viên này.")]
        DaTangChoNguoiNay = -1
        ,

        [Description("Vượt giới hạn tặng trong ngày.")]
        VuotGioiHanTang = -2
        ,

        [Description("Không đủ tiền để gửi.")]
        KhongDuTienDeGui = -3

        ,

        [Description("Loi SQL.")]
        LoiSql = -1000

        ,

        [Description("Loi System.")]
        LoiHeThong = -1001
        ,

        [Description("Thanh cong.")]
        ThanhCong = 1
         ,

        [Description("That bai.")]
        ThatBai = 0
    }

}