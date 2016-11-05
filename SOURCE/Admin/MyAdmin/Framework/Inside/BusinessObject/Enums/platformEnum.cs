using System.ComponentModel;

namespace BusinessObject.Enums
{

    public enum ChannelIdEnum
    {
        [Description("Tất Cả")]
        All = -1,

        //[Description("P111")]
        //P111 = 1,

        //[Description("Eway")]
        //DanhBaiOnlineEway = 2,

        //[Description("Appota")]
        //Appota = 3,

        //[Description("MWork")]
        //DanhBaiOnlineMWork = 4
    }

    /// <summary>
    /// Tao enum Channel
    /// Duynd - 11/05/2016
    /// P/S: Khong biet cai tren co su dung duoc hay khong nen tao cai moi :D
    /// </summary>
    public enum ChannelEnum
    {
        [Description("iFish")]
        IFish = 0,
        [Description("Siêu thị")]
        SupperMarket = 1
    }

    public enum PlatformIdEnum
    {
        [Description("Tất cả")]
        All = -1,

        [Description("Android")]
        Android = 1,

        [Description("iOS")]
        Ios = 2,

        [Description("Windows Phone")]
        WindownPhone = 3,
    }

    public enum OpenProviderIdEnum
    {
        [Description("Tất cả")]
        All = -1,

        [Description("Thường - Đăng ký thường")]
        Normal = 0,

        [Description("Facebook - ĐK qua Facebook")]
        Facebook = 1,
        [Description("Google - ĐK qua Google")]
        Google = 2,
        [Description("Yahoo - ĐK qua Yahoo")]
        Yahoo = 3,
    }
}