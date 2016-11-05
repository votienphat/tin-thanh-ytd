using System;
using System.Collections.Generic;
using System.Linq;
using BussinessObject.Enums;
using BussinessObject.Models;
using BussinessObject.UserModule.Contract;
using DataAccess.Contract.MainModule;
using EntitiesObject.Entities.UserEntities;
using MyUtility.Extensions;
using Newtonsoft.Json;

namespace BussinessObject.UserModule
{
    public class MyConfigBusiness : IMyConfigBusiness
    {
        #region Variables

        private readonly IMyConfigRepository _myConfigRepo;

        #endregion

        public MyConfigBusiness(IMyConfigRepository myConfigRepo)
        {
            _myConfigRepo = myConfigRepo;
        }

        public MyConfigModel GetMyConfigGetConfigByKey(int platFormId, string version, int chanelid = (int)ChanelIdEnum.IFish)
        {
            try
            {
                // get ip & port tu DBGame
                var isAndroid = false;
                var isIos = false;
                switch (platFormId.ToEnum<PlatformIdEnum>())
                {
                    case PlatformIdEnum.Android:
                        isAndroid = true;
                        break;
                    case PlatformIdEnum.Ios:
                        isIos = true;
                        break;
                }
                var valueServer = _myConfigRepo.GetIpPortServer(isAndroid, isIos);

                // get from table Myconfig
                var model = new MyConfigModel();
                const string configApi = "ConfigAPI";
                var myConfig = _myConfigRepo.GetMyConfigGetConfigByKey(configApi);
                if (myConfig != null)
                {
                    var valueConfig = myConfig.Select(x => x.Value).FirstOrDefault();
                    if (!string.IsNullOrEmpty(valueConfig))
                    {
                        var rs = JsonConvert.DeserializeObject<List<ChanelApiModel>>(valueConfig);

                        var items = rs.ToList().FindAll(x => x.ChanelId == chanelid
                            && x.ChanelData.Any(t => t.FlatFormId == platFormId.ToString())
                            && x.ChanelData.Any(t => t.Versions.Any(a => a.GameVersion == version))).ToList();

                        var rand = new Random();
                        var index = rand.Next(0, items.Count);
                        index = index >= items.Count ? items.Count - 1 : index;
                        var item = index < 0 || index >= items.Count ? items.FirstOrDefault() : items[index];
                        if (item != null)
                        {
                            //var versionModel = item.Versions.FirstOrDefault(t => t.GameVersion == version);
                            var chaneldata = item.ChanelData.FirstOrDefault(t => t.FlatFormId == platFormId.ToString());
                            if (chaneldata != null)
                            {
                                var versionModel = chaneldata.Versions.FirstOrDefault(t => t.GameVersion == version);
                                if (versionModel != null)
                                {
                                    //rand = new Random();
                                    //var index1 = 0;
                                    //if (versionModel.GameServers.Count > 1)
                                    //    index1 = rand.Next(0, versionModel.GameServers.Count - 1);
                                    //var gameServerModel = versionModel.GameServers[index1];

                                    model = new MyConfigModel
                                    {
                                        ChanelId = item.ChanelId,
                                        ChanelName = item.ChanelName,
                                        LinkForum = chaneldata.LinkForum,
                                        LinkFanpage = chaneldata.LinkFanpage,
                                        AppsflyerId = chaneldata.AppsflyerId,
                                        ApiUrl = chaneldata.ApiUrl,
                                        PhoneSupport = chaneldata.PhoneSupport,
                                        GameVersion = versionModel.GameVersion,
                                        Message = versionModel.Message,
                                        MustUpgrade = versionModel.MustUpgrade,
                                        IsExchangeCard = versionModel.IsExchangeCard,
                                        LinkDownload = versionModel.LinkDownload,
                                        EnableIap = versionModel.EnableIap,
                                        AllowSignUp = versionModel.AllowSignUp,
                                        InviteReward = versionModel.InviteReward,
                                        IsIpForeign = versionModel.IsIpForeign,
                                        IsChargeCard = versionModel.IsChargeCard,
                                        IsReview = versionModel.IsReview,
                                        FlatformName = chaneldata.FlatformName,
                                        ForceUpdate = versionModel.ForceUpdate,
                                        IsEnableMarket = versionModel.IsEnableMarket
                                    };
                                    var outCallDbGameGetServerResult = valueServer.FirstOrDefault();
                                    if (outCallDbGameGetServerResult != null)
                                    {
                                        model.GamePortServer = outCallDbGameGetServerResult.Port.ToString();
                                        model.GameIpServer = outCallDbGameGetServerResult.IP;
                                    }
                                    switch (platFormId.ToEnum<PlatformIdEnum>())
                                    {
                                        case PlatformIdEnum.Android:
                                            model.VersionAndroid = versionModel.Version;
                                            model.AndroidLink = versionModel.LinkDownload;
                                            break;
                                        case PlatformIdEnum.Ios:
                                            model.VersionIos = versionModel.Version;
                                            model.IosLink = versionModel.LinkDownload;
                                            break;
                                        case PlatformIdEnum.WindowsPhone:
                                            model.VersionWp = versionModel.Version;
                                            model.WpLink = versionModel.LinkDownload;
                                            break;
                                    }
                                }
                            }
                        }

                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                Logger.CommonLogger.DefaultLogger.Debug(string.Format("Gọi Store get my config Bị lỗi, Giá trị trả về: {0}", ex));
                return null;
            }

        }

        /// <summary>
        /// Lấy chi tiết my config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <history>
        /// 2/6/2016 Create by TaiNM
        /// </history>
        public Out_MyConfig_GetCardConfig_Result Get_ByKey(string key)
        {
            return _myConfigRepo.Get(key);
        }

    }
}
