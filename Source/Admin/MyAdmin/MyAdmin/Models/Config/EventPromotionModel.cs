using System;
using System.Collections.Generic;

namespace MyAdmin.Models.Config
{
    public class EventPromotionModel : RequestEventPromotionModel
    {
        public object ResposeData { get; set; }
    }

    public class RequestEventPromotionModel
    {
        public int EventId { get; set; }

        public int GameConfigId { get; set; }
        public string DateBegin { get; set; }
        public string DateEnd { get; set; }
        public string BeginDisplayTime { get; set; }
        public string EndDisplayTime { get; set; }
        public bool IsEnable { get; set; }
        public string ConfigData { get; set; }
        public DateTime DateBeginShow { get; set; }
        public DateTime DateEndShow { get; set; }
        public string NameShowWeb { get; set; }
        public int TypePromotion { get; set; }
        public decimal MinValue { get; set; }
        public int EventRunOn { get; set; }

        public int NumberIpLimit { get; set; }
        public int NumberDeviceLimit { get; set; }
        public bool IsCheckProtectInfo { get; set; }
        public bool IsCheckCardGold { get; set; }
        public int OrderNumber { get; set; }

        public string LinkTitle { get; set; }

        public int MissionTimer { get; set; }

        public RequestEventPromotionModel()
        {
            DateBegin = DateTime.Now.ToString();
            DateEnd = DateTime.Now.ToString();
            DateBeginShow = DateTime.Now;
            DateEndShow = DateTime.Now;
            BeginDisplayTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            EndDisplayTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            IsEnable = false;
            EventRunOn = 0;
            OrderNumber = 0;
        }
    }

    public class RequestEventAwardUpLevel : RequestEventPromotionModel
    {
        public int GameId { get; set; }
        public int LevelNum { get; set; }
        public decimal GoldAward { get; set; }
    }
    public class ConfigRegisterModel
    {
        public int Card { get; set; }
        public int MaxIP { get; set; }
        public int MaxDevice { get; set; }
    }

    public class ConfigUpdateInfo
    {
        public int Amount { get; set; }
        public int MaxIP { get; set; }
    }

    public class ConfigInviteFriend
    {
        public double Percent { get; set; }
        public double Gold { get; set; } 
    }

    public class ConfigChargeCard
    {
        public int MaxTheKM { get; set; }
        public int EventId { get; set; }
        public List<ConfigPercent> Data { get; set; }
    }

    public class ConfigPercent
    {
        public int Index { get; set; }
        public int Percent { get; set; }
    }
    
    public class ConfigEventHoanTienRequest
    {
        public int GameId { get; set; }
        public decimal Gold { get; set; }
        public float ReturnRate { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
    }

#region nạp thẻ giờ vàng
    public class ConfigValueGoldCard
    {
        public List<ItemAwardGoldCard> GoldCardModels { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int DefaultPromotion { get; set; }
    }
    public class ItemAwardGoldCard
    {
        public int NumberCard { get; set; }
        public double PercentValue { get; set; }
    }
#endregion

    #region tuần lễ vàng
    public class ConfigValueTuanLeVang
    {
        public List<ItemAwardTuanLeVang> TuanLeVangModels { get; set; }
        public int DefaultPromotion { get; set; }
    }
    public class ItemAwardTuanLeVang
    {
        public decimal NumberValue { get; set; }
        public decimal GoldGift { get; set; } 
    }
    #endregion

    public class TangGoldModel
    {
        public int EventId { get; set; }
        public string DateBegin { get; set; }
        public string DateEnd { get; set; }
        public bool IsEnable { get; set; }
        public int TypePromotion = 1;
        public decimal MinValueFB { get; set; }
        public decimal MinValue { get; set; }
        public string DateBeginShow { get; set; }
        public string DateEndShow { get; set; }

        public string NameShowWeb { get; set; }
        public string ConfigData { get; set; }
        public int EventRunOn { get; set; }

        public int NumberIpLimitFB { get; set; }
        public int NumberIpLimit { get; set; }
        public int NumberDeviceLimitFB { get; set; }
        public int NumberDeviceLimit { get; set; }
        public bool IsCheckProtectInfo { get; set; }
        public bool IsCheckCardGold { get; set; }

        public string LinkTitle { get; set; }
    }

    public class DsGameModel
    {
        public int GameId { get; set; }
    }

    #region Tang Gold User

        public class TangGoldConfigModel
        {
            public decimal MinValue { get; set; }
            public int NumberIpLimit { get; set; }
            public int NumberDeviceLimit { get; set; }
            public bool IsCheckProtectInfo { get; set; }
            public bool IsCheckCardGold { get; set; }
        }
    #endregion
    #region Tang Gold Lan Dau

    public class NapGoldLanDauConfigModel
    {
        public decimal MinValue { get; set; }
        public int NumberIpLimit { get; set; }
        public int NumberDeviceLimit { get; set; }
        public bool IsCheckProtectInfo { get; set; }
        public bool IsCheckCardGold { get; set; }


        public int DayRegister { get; set; }
        public int NumberCardPromotion { get; set; }
        public int NumberMatchGame { get; set; }
        public int Percent { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
    }
    #endregion

    #region Tang gold dang ky
    public class GiftGoldRegister
    {
        public decimal ValueGoldFb { get; set; }
        public decimal ValueGold { get; set; }
        public int LimitDeviceFb { get; set; }
        public int LimitDevice { get; set; }
        public int LimitIPFb { get; set; }
        public int LimitIP { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

    }
    #endregion

    #region Tang gold Online Lien Tuc
    public class OnlineValues
    {
        public List<ItemValueOnline> GiaTriTheoGioModels { get; set; }
    }

    public class ItemValueOnline
    {
        public int OnlineTime { get; set; }

        public int OnlineTimeMoney { get; set; }
    }
#endregion

    #region Tang gold Hang Ngay
    public class OnlineDayModel
    {
        public List<ItemValueDay> OnlineDays { get; set; }
    }

    public class ItemValueDay
    {
        public int QMaxDay { get; set; }

        public int AddCore { get; set; }
    }
    #endregion

    #region Khuyen Mai Nap Tien Lan Dau
    public class KhuyenMaiNapTienLanDau
    {
        public int DayRegister { get; set; }
        public int NumberCardPromotion { get; set; }
        public int Percent { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
    }
    #endregion

    public class ItemAwardValueCard
    {
        public int CardAmount { get; set; }
        public double PercentValue { get; set; }
    }

    public class ConfigValueCard
    {
        public List<ItemAwardValueCard> MenhGiaModels { get; set; }
    }

    #region Cấu hình bảo trì đổi thẻ
    public class ConfigExchangeCardMaintenance
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
    }
    #endregion

#region Config Server
    public class ConfigServerInsideModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public int ChanelId { get; set; }
        public int Type { get; set; } //Loại update hay thêm mới
        public ConfigServerInsideModel()
        {
            Key = "ConfigAPI";
        }
    }

    public class ValueConfigServerModel
    {
        public string VersionWp { get; set; }
        public string VersionAndroid { get; set; }
        public string VersionIOS { get; set; }
        public string ForceUpdate { get; set; }
        public string Message { get; set; }
        public bool MustUpgrade { get; set; }
        public bool IsExchangeCard { get; set; }
        public string AndroidLink { get; set; }
        public string IOSLink { get; set; }
        public string WPLink { get; set; }
        public string LinkFanpage { get; set; }
        public string PhoneSupport { get; set; }
        public string AppsflyerId { get; set; }
        public string ApiUrl { get; set; }
        public bool IsChargeCard { get; set; }
        public string LinkForum { get; set; }
        public bool EnableIap { get; set; }
        public bool AllowSignUp { get; set; }
        public int InviteReward { get; set; }
        public bool IsIpForeign { get; set; }

    }

    public class IPServerModel
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }

    public class GameIpServerModel
    {
        public List<IPServerModel> GameIpServer { get; set; }
    }
#endregion
}