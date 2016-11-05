using System.Collections.Generic;
using System.ComponentModel;

namespace BussinessObject.Models
{
    public class MyConfigModel
    {
        public MyConfigModel()
        {

            GameIpServer = string.Empty;
            GamePortServer = string.Empty;
            VersionWp = string.Empty;
            VersionAndroid = string.Empty;
            VersionIos = string.Empty;
            ForceUpdate = string.Empty;
            Message = string.Empty;
            MustUpgrade = false;
            IsExchangeCard = false;
            IsChargeCard = false;
            ServerPort = string.Empty;
            WpLink = string.Empty;
            IosLink = string.Empty;
            AndroidLink = string.Empty;
            LinkFanpage = string.Empty;
            ApiUrl = string.Empty;
            PhoneSupport = string.Empty;
            AppsflyerId = string.Empty;
            LinkForum = string.Empty;
            EnableIap = false;
            AllowSignUp = false;
            InviteReward = 0;
            IsIpForeign = false;
            IsReview = false;
        }

        public int ChanelId { get; set; }
        public string ChanelName { get; set; }

        public string GameIpServer { get; set; }

        public string GamePortServer { get; set; }

        [Description("versionWp")]
        public string VersionWp { get; set; }

        [Description("versionAndroid")]
        public string VersionAndroid { get; set; }

        [Description("versionIOS")]
        public string VersionIos { get; set; }

        [Description("forceUpdate")]
        public string ForceUpdate { get; set; }

        [Description("message")]
        public string Message { get; set; }

        [Description("mustUpgrade")]
        public bool MustUpgrade { get; set; }
        public bool IsExchangeCard { get; set; }

        public bool IsChargeCard { get; set; }
        public string ServerPort { get; set; }

        [Description("WPLink")]
        public string WpLink { get; set; }

        [Description("iOSLink")]
        public string IosLink { get; set; }

        [Description("androidLink")]
        public string AndroidLink { get; set; }
        public string LinkFanpage { get; set; }
        public string ApiUrl { get; set; }
        public string PhoneSupport { get; set; }
        public string AppsflyerId { get; set; }
        public string LinkForum { get; set; }
        public bool EnableIap { get; set; }
        public bool AllowSignUp { get; set; }

        public int InviteReward { get; set; }
        public bool IsIpForeign { get; set; }
        public bool IsReview { get; set; }
        public string FlatformName { get; set; }
        public string GameVersion { get; set; }
        public string LinkDownload { get; set; }
        public string Port { get; set; }
        public bool IsEnableMarket { get; set; }
    }

    public class ChanelApiModel
    {
        public int ChanelId { get; set; }
        public string ChanelName { get; set; }
        public List<ConfigApiModel> ChanelData { get; set; }
    }

    public class ConfigApiModel
    {
        public string FlatFormId { get; set; }

        public string LinkForum { get; set; }

        public string LinkFanpage { get; set; }

        public string AppsflyerId { get; set; }

        public string ApiUrl { get; set; }

        public string PhoneSupport { get; set; }

        public List<Versions> Versions { get; set; }

        public string FlatformName { get; set; }
    }

    public class Versions
    {
        public string GameVersion { get; set; }

        public string Version { get; set; }

        public string ForceUpdate { get; set; }

        public string Message { get; set; }

        public bool MustUpgrade { get; set; }

        public bool IsExchangeCard { get; set; }

        public string LinkDownload { get; set; }

        public bool EnableIap { get; set; }

        public bool AllowSignUp { get; set; }

        public int InviteReward { get; set; }

        public bool IsIpForeign { get; set; }

        public bool IsChargeCard { get; set; }
        public bool IsReview { get; set; }
        public bool IsEnableMarket { get; set; }

        public List<GameServers> GameServers { get; set; }
    }

    public class GameServers
    {
        public string Ip { get; set; }

        public string Port { get; set; }
    }
}
