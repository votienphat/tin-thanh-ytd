using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BussinessObject.EventModule.Enum
{
    public enum EventPromotionEnum
    {
        [Description("Tặng gold đăng nhập")]
        EventTangGold = 1,

        [Description("Khuyến mãi mời bạn")]
        EventInviteFriend = 2,

        [Description("Tặng gold chơi liên tục")]
        EventTangGoldOnline= 3,

        [Description("Tặng gold hàng ngày")]
        EventTangGoldHangNgay = 4,

        [Description("Tặng gold theo nhiệm vụ")]
        EventTangGoldTheoNhiemVu = 5,

        [Description("Hiển thị top bộ sưu tập cá")]
        EventTopFishCollection = 7,

        [Description("Cấu hình đăng kí tặng gold")]
        EventCauHinhDangKyTangGold = 6,
                    
        [Description("Hiển thị top mời bạn Facebook")]
        EventTopFriendFacebook = 8,

        [Description("Hiển thị top săn cá boss")]
        EventTopSanCaBoss = 9,

        [Description("Hiển thị top bạn bè Facebook có gold cá nhân nhiều nhất")]
        EventTopFriendFacebookGoldMax = 10,

        [Description("Hiển thị top bạn bè Facebook thắng gold nhiều nhất")]
        EventTopFriendWinGold = 11,

        [Description("Hiển thị top bạn bè Facebook thắng gold nhiều nhất")]
        EventTopThangLon = 12,

        [Description("Hiển thị top hoàn thành nhiệm vụ")]
        EventTopHoanThanhNhiemVu = 13,
        [Description("Hiển thị Event may mắn hằng ngày")]
        EventMayManHangNgay = 16,
        [Description("Hiển thị top bắn cá")]
        EventTopBanCa = 17,

        [Description("Đua top level")]
        EventTopLevel = 19
        
    }
}
