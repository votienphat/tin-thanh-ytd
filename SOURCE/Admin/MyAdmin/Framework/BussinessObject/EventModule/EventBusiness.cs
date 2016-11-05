using System;
using System.Collections.Generic;
using System.Linq;
using BussinessObject.ContentModule.Contract;
using BussinessObject.Enums;
using BussinessObject.EventModule.Contract;
using BussinessObject.EventModule.Enum;
using BussinessObject.EventModule.Helper.Abstract;
using BussinessObject.EventModule.Models;
using DataAccess.Contract.ContentModule;
using DataAccess.Contract.EventModule;
using DataAccess.Contract.PaymentModule;
using DataAccess.Contract.UserModule;
using DataAccessRedis.Module.Contract;
using EntitiesObject.Entities.EventManagement;
using EntitiesObject.Entities.LogManagementEntities;
using MyUtility;
using MyUtility.Extensions;
using Newtonsoft.Json;

namespace BussinessObject.EventModule
{
    public class EventBusiness : IEventBusiness
    {
        #region Variables

        private readonly IEventRepository _eventRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ILeveGameLogRepository _leveGameLogRepository;
        private readonly IRichGameLogRepository _richGameLogRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IAwardMakeFriendRepository _awardMakeFriendRepository;
        private readonly IOfflineMessageBusiness _offlineMessage;
        private readonly IAccountRepository _accountRepository;
        private readonly IEventRedis _eventRedis;
        private readonly ILyckyEveryDayHelper _everyDayHelper;

        public EventBusiness(IEventRepository eventRepository, IArticleRepository articleRepository, ILeveGameLogRepository leveGameLogRepository,
            IWalletRepository walletRepository, IAwardMakeFriendRepository awardMakeFriendRepository, IOfflineMessageBusiness offlineMessage, IAccountRepository accountRepository, IRichGameLogRepository richGameLogRepository, IEventRedis eventRedis, ILyckyEveryDayHelper everyDayHelper)
        {
            _eventRepository = eventRepository;
            _articleRepository = articleRepository;
            _leveGameLogRepository = leveGameLogRepository;
            _walletRepository = walletRepository;
            _awardMakeFriendRepository = awardMakeFriendRepository;
            _offlineMessage = offlineMessage;
            _accountRepository = accountRepository;
            _richGameLogRepository = richGameLogRepository;
            _eventRedis = eventRedis;
            _everyDayHelper = everyDayHelper;
        }

        #endregion

        /// <summary>
        /// <para>Author:PhatVT</para>
        /// <para>DateCreated: 06/01/2016</para>
        /// <para>Chạy các khuyến mãi</para>
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 18/3/2016 Updated TaiNM
        /// Param promotionRequestModel
        /// 22/3/2016 Updated TaiNM
        /// return list EventPromotionResposeModel
        /// </history>
        public List<EventPromotionResposeModel> RunPromotion(PromotionRequestModel request)
        {
            var lstResults = new List<EventPromotionResposeModel>();
            //1: Đăng ký, 2: Đăng nhập, 3: Nạp gold
            var events = _eventRepository.GetInfo_ByRunOn(request.RunOn);
            bool IsKhuyenMai = false;

            foreach (var item in events)
            {
                var eventPromotionModel = new EventPromotionResposeModel
                {
                    Status = EventStatusCode.Failed
                };
                // Dựa vào mã sự kiện mà chạy đúng event
                switch (item.EventId)
                {
                    case (int)EventCodeEnum.EventInviteFriend:

                        #region Invite friend

                        eventPromotionModel.EventCode = EventCodeEnum.EventInviteFriend;

                        if (item.Status.HasValue && item.Status.Value > 0)//trạng thái đang hoạt động
                        {
                            var dataObj = JsonConvert.DeserializeObject<EventInviteFriendDataModel>(item.ConfigData);

                            //check
                            if (!_awardMakeFriendRepository.AwardMakeFriend_CheckExist(request.UserId,
                                request.UserFriendId))
                            {
                                if (_walletRepository.Wallet_AddOrSubtractGoldUser(request.UserId, dataObj.Gold,
                                    ReasonEnum.TangGoldMoiBan.Value(), request.Description, 0, request.IsMobile,
                                    request.LoginType, request.ClientTarget) > 0)
                                {
                                    eventPromotionModel.Status = EventStatusCode.Success;
                                    eventPromotionModel.GoldPromotion = dataObj.Gold;
                                    //insert log
                                    _awardMakeFriendRepository.AwardMakeFriend_Add(request.UserId, request.UserFriendId,
                                        dataObj.Gold, request.Description);

                                    //Gửi mail
                                    var displayName = _accountRepository.GetNameByUserId(request.UserFriendId);
                                    //Bạn nhận X xu do Y đã tham gia thành công Ifish
                                    //var msgContent = "Chào bạn,/nIfish gửi tặng bạn " + dataObj.Gold.ToCurrencyString() + " xu vì " + displayName + " đã tham gia Ifish!/nChúc bạn chơi game vui vẻ";
                                    //var msgContent = "Bạn nhận " + dataObj.Gold.ToCurrencyString() + " xu do " + displayName + " đã tham gia thành công Ifish";
                                    //var msgContent = string.Format("Mời thành công {0} tham gia iFish. Bạn được tặng {1} Xu", displayName, dataObj.Gold.ToCurrencyString());
                                    //_offlineMessage.SendMessage(0, request.UserId, msgContent, 1, 1, string.Empty, 1,
                                    //    string.Empty, true, 0);

                                    eventPromotionModel.Message = String.Format("Mời thành công {0} tham gia iFish. Bạn được tặng {1} Xu", displayName, dataObj.Gold.ToCurrencyString());

                                    // cap nhat gold cho user tren server game - Duynd - 05/05/2016
                                    _accountRepository.UpdateGoldUser(request.UserId, 2, dataObj.Gold);
                                    //#end
                                }
                                else
                                {
                                    eventPromotionModel.Status = EventStatusCode.Failed;
                                    eventPromotionModel.Message = EventStatusCode.Failed.Text();
                                }
                            }
                        }

                        #endregion

                        break;
                    case (int)EventCodeEnum.EventNapTienLanDau:

                        var dataObjs = JsonConvert.DeserializeObject<KhuyenMaiNapTienLanDau>(item.ConfigData);

                        //check
                        if (_eventRepository.Out_CheckKhuyenMaiNapTien_Valid(dataObjs.NumberCardPromotion, dataObjs.MinAmount, dataObjs.MaxAmount, request.UserId, request.Amount) ==
                            (int)EventStatusCode.Success)
                        {
                            if (_eventRepository.Out_E1610_KhuyenMaiNapTheLanDau_Insert(request.UserId, request.ChanelId, request.IpRequest, request.PlatformId, request.GameVersion, request.Amount)
                                == (int)EventStatusCode.Success)
                            {
                                var gold = request.Gold * dataObjs.Percent / 100;
                                if (_walletRepository.Wallet_AddOrSubtractGoldUser(request.UserId, gold,
                                   ReasonEnum.NapTienLanDau.Value(), request.Description, 0, request.IsMobile,
                                   request.LoginType, request.ClientTarget) > 0)
                                {
                                    eventPromotionModel.EventCode = EventCodeEnum.EventNapTienLanDau;
                                    eventPromotionModel.Status = EventStatusCode.Success;
                                    //Gửi mail
                                    //var displayName = _accountRepository.GetNameByUserId(request.UserId);
                                    //Bạn nhận X xu do Y đã tham gia thành công Ifish
                                    //var msgContent = "Chào bạn,/nIfish gửi tặng bạn " + dataObj.Gold.ToCurrencyString() + " xu vì " + displayName + " đã tham gia Ifish!/nChúc bạn chơi game vui vẻ";
                                    //var msgContent = "Bạn nhận " + dataObj.Gold.ToCurrencyString() + " xu do " + displayName + " đã tham gia thành công Ifish";

                                    //var msgContent = string.Format("Xin chào {0}. Bạn được tặng xu nạp tiền lần đầu {1} xu", displayName, gold.ToCurrencyString(false, false));
                                    eventPromotionModel.Message = String.Format("Bạn nhận được {0} khuyến mãi thẻ nạp đầu tiên", gold.ToCurrencyString(false, false));
                                    //_offlineMessage.SendMessage(0, request.UserId, msgContent, 1, 1, string.Empty, 1,
                                    //    string.Empty, true, 0);

                                    // cap nhat gold cho user tren server game - ThoaiND - 02/06/2016
                                    _accountRepository.UpdateGoldUser(request.UserId, 2, gold);

                                    eventPromotionModel.Coin = request.Gold + gold;
                                    //#end

                                    IsKhuyenMai = true;
                                }
                            }
                        }

                        break;
                    case (int)EventCodeEnum.KhuyenMaiTheNapTheoMenhGia:
                        if (IsKhuyenMai) // da nhan km the nap dau tien thi ko nhan km the theo manh gia
                            break;
                        //TrungLD : chan the bit khuyen mai theo menh gia - 10/06/2016
                        if (request.CardType.ToEnum<HqCardTypeEnum>() == HqCardTypeEnum.Bit)
                            break;
                        var dataOb = JsonConvert.DeserializeObject<ConfigValueCard>(item.ConfigData);

                        var rs = dataOb.MenhGiaModels.FirstOrDefault(x => x.CardAmount == request.Amount);
                        if (rs == null) break;
                        double percent = rs.PercentValue;
                        if (_eventRepository.Out_E1612_KhuyenMaiTheoMenhGia_Insert(request.UserId, request.ChanelId, request.PlatformId, request.GameVersion, request.Amount, request.CardType, percent, request.IpRequest)
                                == (int)EventStatusCode.Success)
                        {
                            var gold = request.Gold * (decimal)percent / 100;
                            if (_walletRepository.Wallet_AddOrSubtractGoldUser(request.UserId, gold,
                               ReasonEnum.NapTienTheoMenhGia.Value(), request.Description, 0, request.IsMobile,
                               request.LoginType, request.ClientTarget) > 0)
                            {
                                eventPromotionModel.EventCode = EventCodeEnum.KhuyenMaiTheNapTheoMenhGia;
                                eventPromotionModel.Status = EventStatusCode.Success;
                                //Gửi mail
                                //var displayName = _accountRepository.GetNameByUserId(request.UserId);
                                //Bạn nhận X xu do Y đã tham gia thành công Ifish
                                //var msgContent = "Chào bạn,/nIfish gửi tặng bạn " + dataObj.Gold.ToCurrencyString() + " xu vì " + displayName + " đã tham gia Ifish!/nChúc bạn chơi game vui vẻ";
                                //var msgContent = "Bạn nhận " + dataObj.Gold.ToCurrencyString() + " xu do " + displayName + " đã tham gia thành công Ifish";
                                eventPromotionModel.Message = String.Format("Bạn nhận được {0}  xu trong sự kiện khuyến mãi theo mệnh giá thẻ nạp.", gold.ToCurrencyString(false, false));
                                //var msgContent = string.Format("Xin chào {0}.Bạn được nhận {1} xu trong sự kiện khuyến mãi theo mệnh giá thẻ nạp", displayName, gold.ToCurrencyString(false, false));
                                //_offlineMessage.SendMessage(0, request.UserId, msgContent, 1, 1, string.Empty, 1,
                                //    string.Empty, true, 0);

                                // cap nhat gold cho user tren server game - ThoaiND - 02/06/2016
                                _accountRepository.UpdateGoldUser(request.UserId, 2, gold);

                                eventPromotionModel.Coin = request.Gold + gold;
                                //#end

                                IsKhuyenMai = true;
                            }

                        }
                        break;
                }

                lstResults.Add(eventPromotionModel);
            }

            return lstResults;
        }

        #region Private methods

        #endregion


        /// <summary>
        /// Lấy danh sách bài viết hiển thị trên game
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 18/2/2016 Create By MinhT
        /// </history>
        public List<ArticleResponseModel> GetArticle(int cate, int starindex, int pagelenght, out int totalrow)
        {
            var value = _articleRepository.GetArticle(cate, starindex, pagelenght, out totalrow);
            return value.Select(x => new ArticleResponseModel
            {
                ArticleID = x.ArticleID.GetValueOrDefault(),
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                CreateDate = x.CreateDate.GetVnDateTimeFormat(),
                PublicDate = x.PublicDate.GetVnDateTimeFormat()
                ,
                DateSort = x.PublicDate.GetValueOrDefault(),
                TextID = x.TextID,

            }).OrderByDescending(x => x.DateSort).ToList();
        }
        public List<ArticleResponseModel> GetTopArticle(int top)
        {
            var topArticle = _articleRepository.GetTopArticle(top);
            return topArticle.Select(x => new ArticleResponseModel
            {
                ArticleID = x.ArticleID,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                CreateDate = x.CreateDate.GetVnDateTimeFormat(),
                PublicDate = x.PublicDate.GetVnDateTimeFormat()
                ,
                DateSort = x.PublicDate.GetValueOrDefault()
                ,
                Status = x.HotOrNew.GetValueOrDefault(0)
                ,
                CategoryID = x.CategoryID
            }).OrderByDescending(x => x.DateSort).ToList();
        }


        /// <summary>
        /// Lấy link nội dung bài viết hiển thị trên game Theo ID
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        /// <history>
        /// 18/2/2016 Create By MinhT
        /// </history>
        public ArticleResponseModel GetLinkArticleDetail(int articleId)
        {

            var value = _articleRepository.GetArticleDetail(articleId);
            return new ArticleResponseModel
            {
                ArticleID = value.ArticleID,
                Title = value.Title,
                ShortDescription = value.ShortDescription,
                Body = value.Body,
                CreateDate = value.CreateDate.GetVnDateTimeFormat(),
                PublicDate = value.PublicDate.GetVnDateTimeFormat()
            };
        }
        public ArticleResponseModel GetArticleDetailBySeo(string textid)
        {

            var value = _articleRepository.GetArticleDetailsForClient(textid);
            return new ArticleResponseModel
            {
                ArticleID = value.ArticleID,
                Title = value.Title,
                ShortDescription = value.ShortDescription,
                Body = value.Body,
                CreateDate = value.CreateDate.GetVnDateTimeFormat(),
                PublicDate = value.PublicDate.GetVnDateTimeFormat(),
                CategoryID = value.CategoryID,
            };
        }

        /// <summary>
        /// lấy top level user
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 5/3/2016 Create By MinhT
        /// </history>
        public List<Out_LevelGameLog_GetLevelGame_Result> GetTopLevel(int top, int userId)
        {
            return _leveGameLogRepository.GetTopLevel(top, userId);
        }

        /// <summary>
        /// lấy top level user - Cache data on Redis
        /// Duynd - 16/05/2016
        /// </summary>
        public List<Out_LevelGameLog_GetLevelGame_Result> GetTopLevel(int top, int userId, DateTime dtmClearCacheRedis)
        {
            var listData = _eventRedis.GetTopLevel(top, userId, dtmClearCacheRedis);
            if (!listData.Any())
            {
                listData = GetTopLevel(top, userId);
                _eventRedis.SetTopLevel(top, userId, dtmClearCacheRedis, listData);
            }
            return listData;
        }

        /// <summary>
        /// lấy top kill boss
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 5/3/2016 Create By TrungLD
        /// </history>
        public List<Out_TopKillBoss_GetTop_Result> GetTopKillBoss(int top)
        {
            return _leveGameLogRepository.GetTopKillBoss(top);
        }


        public List<Out_E1602TopUserWin_GetByDate_Result> GetTopUserWin(DateTime reportDate, int top)
        {
            return _eventRepository.GetTopUserWin(reportDate, top);
        }

        public List<Out_E1604_TopAttackBoss_GetDataTop_Result> E1604_TopAttackBoss_GetDataTop(int top)
        {
            return _eventRepository.E1604_TopAttackBoss_GetDataTop(top);
        }

        /// <summary>
        /// Lấy top hoàn thành nhiệm vụ trong ngày
        /// </summary>
        /// <param name="top"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <history>
        /// 18/3/2016 Create By MinhT
        /// </history>
        public List<Out_TMission_Top_GetTop_Result> TMission_GetTopFinish(int top, DateTime date)
        {
            return _eventRepository.TMission_GetTopFinish(top, date);
        }


        public List<Out_Report_E1606_UserShootFish_GetDate_Result> TopUserFishCollection(int top, DateTime date)
        {
            return _eventRepository.GetTopUserFishCollection(top, date);
        }


        public List<Ins_FishCollection_GetTop_Result> GetTopFishCollection(int? top = null)
        {
            return _eventRepository.FishCollection_GetTop(top);
        }

        public List<Out_FishCollection_AcchiveUser_Result> GetFishCollection_AcchiveUser(int userid)
        {
            return _eventRepository.GetFishCollection_AcchiveUser(userid);
        }

        /// <summary>
        /// Top User mời qua facebook có tổng gold cao nhất
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        /// <history>
        /// 5/4/2016 Create By ThoaiND
        /// </history>

        public List<Out_E1607_TopFriendWinGold_Archives_Result> GetFriendWinGold_Acchive(int userid, DateTime dateBegin, DateTime dateEnd)
        {
            return _eventRepository.GetFriendWinGold_Acchive(userid, dateBegin, dateEnd);
        }

        public List<Out_E1607_TopFriendWinGold_GetTop_Result> GetTopFriendWinGold_GetTop(int? top = null)
        {
            return _eventRepository.GetTopFriendWinGold_GetTop(top);
        }

        public Out_EventPromotion_GetEventByCode_Result EventPromotion_GetEventByCode(string keyCode)
        {
            return _eventRepository.EventPromotion_GetEventByCode(keyCode);
        }

        public ConfigEventInviteFriendModel GetConfigInviteFriend(string keyCode)
        {
            var objResult = new ConfigEventInviteFriendModel();
            var objEvent = EventPromotion_GetEventByCode(keyCode);
            if (objEvent != null)
            {
                var configGold = JsonConvert.DeserializeObject<EventInviteFriendDataModel>(objEvent.ConfigData);
                objResult = new ConfigEventInviteFriendModel()
                {
                    DateBegin = objEvent.BeginTime.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss")
                    ,
                    DateEnd = objEvent.EndTime.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss")
                    ,
                    Status = true
                    ,
                    Title = objEvent.Title
                    ,
                    Gold = configGold.Gold.ToCurrencyString(false, false)
                };
            }

            return objResult;
        }

        // Top user co ban be moi facebook nhieu nhat
        public List<Ins_E1607_TopManyFriend_GetTopFriendFacebook_Result> GetTopFriendFacebook(int intTop)
        {
            return _eventRepository.GetTopFriendFacebook(intTop);
        }

        // Top danh sach ban be Facebook cua user
        public List<Ins_E1607_TopManyFriend_GetTopFriendFacebookOfUser_Result> GetTopFriendFacebookOfUser(int intUserID, int intPageSize, int intPosition)
        {
            return _eventRepository.GetTopFriendFacebookOfUser(intUserID, intPageSize, intPosition);
        }

        /// <summary>
        /// Lấy danh sách bạn bè của user mời từ fb có xu cao nhất
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="top"></param>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <history>
        /// 5/4/2016 Create By TaiNM
        /// </history>
        public List<Out_FriendRelationship_TopFriendFbGoldMax_Result> GetTopFriendFbGoldMax(int userId, int position, int pageSize, out int count)
        {
            return _eventRepository.GetTopFriendFbGoldMax(userId, position, pageSize, out count);
        }

        /// <summary>
        /// Lấy danh sách user có tổng xu của bạn bè (mời trên fb)  cao nhất
        /// </summary>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <history>
        /// 5/4/2016 Create By TaiNM
        /// </history>
        public List<Out_E1607_TopGoldFriend_GetData_Result> GetTopGoldFriend(int position, int pageSize, out int count)
        {
            return _eventRepository.GetTopGoldFriend(position, pageSize, out count);
        }

        /// <summary>
        /// Lay event - Duynd - 14/04/2016       
        /// </summary>
        /// <param name="intEventID"></param>
        /// <returns></returns>
        public Ins_EventPromotion_GetEventById_Result GetEventById(int intEventID)
        {
            return _eventRepository.GetEventById(intEventID);
        }

        /// <summary>
        /// Lấy danh sách đại gia trong game
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 25/4/2016 Create By TaiNM
        /// </history>
        public List<Out_RichGameLog_GetData_Result> GetTopRich(int top, int userId)
        {
            return _richGameLogRepository.GetTopRich(top, userId);
        }

        /// <summary>
        /// Lấy danh sách đại gia trong game
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 19/05/2016 Create By Duynd
        /// </history>
        public List<Out_RichGameLog_GetData_Result> GetTopRich(int top, int userId, DateTime dtmClearCacheRedis)
        {
            var listData = _eventRedis.GetTopRich(top, userId, dtmClearCacheRedis);
            if (!listData.Any())
            {
                listData = GetTopRich(top, userId);
                _eventRedis.SetTopRich(top, userId, dtmClearCacheRedis, listData);
            }
            return listData;
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/05/2016</para>
        /// <para>lấy top user bắn thắng</para>
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Out_E1602TopUserWin_GetTopByDate_Result> E1602TopUserWin_GetTopByDate(int top = 10)
        {
            return _eventRepository.E1602TopUserWin_GetTopByDate(top);
        }

        public List<Out_A_TPlayGame_ListHitFish_ByUser_EventBanCa_Result> Out_A_TPlayGame_ListHitFish_ByUser_EventBanCa_Result(int rowBegin, int rowEnd, int top, out int totalRow)
        {
            var value = _eventRepository.Out_A_TPlayGame_ListHitFish_ByUser_EventBanCa_Result(rowBegin, rowEnd, top, out totalRow).ToList();
            return value;
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 03/06/2016</para>
        /// <para>lấy event đang chạy theo kiểu event</para>
        /// </summary>
        /// <param name="typeEnum"></param>
        /// <returns></returns>
        public List<Out_EventPromotion_GetEventRunningByType_Result> EventPromotion_GetEventRunningByType(TypeEventPromotion typeEnum)
        {
            return _eventRepository.EventPromotion_GetEventRunningByType(typeEnum.Value());
        }

        public List<LuckyEverydayViewModel> LogUserRecieveLuckyEveryday_GetFullDataForAPI()
        {
            var repositoryData = _eventRepository.LogUserRecieveLuckyEveryday_GetFullDataForAPI();

            var response = repositoryData.GroupBy(x => x.EventDate).Select(x => new LuckyEverydayViewModel
            {
                EventDate = x.Key.GetValueOrDefault(),

                LuckyEverydayDateViewModel = repositoryData.Where(y => y.EventDate == x.Key.GetValueOrDefault()).GroupBy(y => y.PeriodOfTime).OrderBy(y => y.Key).Select(y => new LuckyEverydayDateViewModel
                {
                    LuckyEverydayPeriodTime = y.Key.ToEnum<LuckyEverydayPeriodTime>(),
                    PeriodTimeValue = y.FirstOrDefault().PeriodOfTimeValue,
                    LuckyEverydayPeriodTimeViewModel = repositoryData.Where(i => i.EventDate == y.FirstOrDefault().EventDate && i.PeriodOfTime == y.FirstOrDefault().PeriodOfTime).Select(k => new LuckyEverydayPeriodTimeViewModel
                    {
                        DisplayName = k.DisplayName,
                        Present = k.Present,
                        Value = k.Value.GetValueOrDefault()
                    }).ToList()
                }).ToList()
            }).ToList();

            var objEvent = EventPromotion_GetEventRunningByType(TypeEventPromotion.EventShowTop);
            var myObjEvent = objEvent.FirstOrDefault(x => x.EventId == 16);
            if (myObjEvent != null)
            {
                var beginTime = myObjEvent.BeginTime.GetValueOrDefault();
                while (beginTime.Date <= myObjEvent.EndTime.GetValueOrDefault().Date)
                {
                    var time = beginTime;
                    var flag = response.Where(x => x.EventDate.Date == time.Date).Select(x => x);
                    if (!flag.Any())
                    {
                        response.Add(new LuckyEverydayViewModel
                        {
                            EventDate = time
                        });
                    }
                    beginTime = beginTime.AddDays(1);
                }
            }

            return response.OrderBy(x => x.EventDate).ToList();
        }

        /// <summary>
        /// Lấy danh sách user trúng thưởng trong event may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        public List<LuckyEverydayMiniModel> LogUserRecieveLuckyEveryday_GetFullDataForAPI(DateTime dateReport, int periodOfTime, int pageSize, int pageIndex)
        {
            List<LuckyEverydayMiniModel> response;

            //Tạo key từ tham số truyền vào.
            var key = _everyDayHelper.CreateKey(dateReport, periodOfTime, pageSize, pageIndex);

            //Kiểm Lấy danh sách key từ redis, nếu chưa có thì vào SQL lấy
            var getKeFromRedis = _everyDayHelper.GetKey(key);

            if (getKeFromRedis.Any())
            {
                //Lay du lieu tu Redis
                response = _everyDayHelper.GetValue(key);
            }
            else
            {
                //Laasy duwx lieu tu SQL
                var rowBegin = (pageIndex - 1) * pageSize + 1;
                var rowEnd = rowBegin + pageSize -1;
                var value = _eventRepository.LogUserRecieveLuckyEveryday_GetFullDataForAPI(dateReport, periodOfTime,
                    rowBegin, rowEnd);
                if (value.Any())
                {
                    var luckyEverydayModel = value.Select(x => new LuckyEverydayModel
                    {
                        RowNumber = x.RowNumber.GetValueOrDefault(0),
                        DisplayName = x.DisplayName,
                        EventDate = x.EventDate.GetValueOrDefault(),
                        PeriodOfTimeValue = x.PeriodOfTimeValue,
                        Present = x.Present,
                        Value = x.Value.GetValueOrDefault(),
                        TotalRow = x.TotalRow.GetValueOrDefault()
                    }).ToList();

                    //Insert vao Redis, nếu sảy ra lỗi thì xóa key đó, lần sau insert
                    var isSuccess = _everyDayHelper.IsSuccessSetValue(key, luckyEverydayModel);
                    if (!isSuccess)
                    {
                        _everyDayHelper.DeleteKey(key);
                    }
                    response = value.Select(x => new LuckyEverydayMiniModel
                    {
                        RowNumber = x.RowNumber.GetValueOrDefault(),
                        DisplayName = x.DisplayName,
                        PeriodOfTimeValue = x.PeriodOfTimeValue,
                        Present = x.Present,
                        Value = x.Value.GetValueOrDefault(),
                        TotalRow = x.TotalRow.GetValueOrDefault()
                    }).ToList();
                }
                else
                {
                    response = Enumerable.Empty< LuckyEverydayMiniModel>().ToList();
                }
            }
            return response;
        }

        public List<LuckyEverydayEventDate> GetLuckyEverydayEventDate()
        {
            var value = _eventRepository.Out_E1609_LogConfigLuckyEveryday_GetDateEventEveryDay();
            var response = value.GroupBy(x => x.EventDate).Select(x => new LuckyEverydayEventDate
            {
                EventDate = x.Key.GetValueOrDefault(),
                PeriodOfTimes = value.Where(y=>y.EventDate == x.Key.GetValueOrDefault()).Select(y=>new PeriodOfTime
                {
                    PeriodTime = y.PeriodOfTime.GetValueOrDefault(),
                    PeriodTimeString = y.PeriodOfTime.GetValueOrDefault().ToEnum<LuckyEverydayPeriodTime>().Text(),
                    PeriodTimeValue = y.PeriodOfTimeValue
                }).ToList()
            }).ToList();
            return response;
        }
    }
}
