using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BussinessObject.ContentModule.Contract;
using BussinessObject.Enums;
using BussinessObject.Models;
using BussinessObject.PaymentModule.Contract;
using BussinessObject.PaymentModule.Enums;
using BussinessObject.PaymentModule.Models;
using DataAccess.Contract.PaymentModule;
using DataAccess.Contract.MainModule;
using DataAccess.Contract.UserModule;
using DataAccessRedis.Constants;
using DataAccessRedis.Module.Contract;
using EntitiesObject.Entities.UserEntities;
using EntitiesObject.Model.Payment;
using EntitiesObject.Model.PaymentConfig;
using MyUtility;
using MyUtility.Extensions;
using Newtonsoft.Json;
using IPaymentHelper = BussinessObject.Helper.Contract.IPaymentHelper;
using BussinessObject.EventModule.Enum;
using BussinessObject.EventModule.Models;
using BussinessObject.EventModule.Contract;

namespace BussinessObject.PaymentModule
{
    public class PaymentBusiness : IPaymentBusiness
    {
        #region Variables

        private readonly IAccountRepository _accountRepo;
        private readonly ILogCardTranRepository _logCardTranRepo;
        private readonly ILogCreditTransRepository _logCreditTransRepo;
        private readonly IPaymentConfigRepository _paymentConfigRepo;
        private readonly IMyConfigRepository _myConfigRepo;
        private readonly IWalletRepository _walletRepo;
        private static IPaymentHelper _paymentHelper;
        private readonly IPaymentLogRepository _paymentLog;
        private readonly IRankingDetailRepository _rankingDetailRepo;
        private readonly IOfflineMessageBusiness _offlineMessage;
        private readonly IEventBusiness _eventBusiness;
        private readonly ICommonRedis _commonRedis;

        #endregion

        #region Constructor

        public PaymentBusiness(ILogCardTranRepository logCardTranRepo, IPaymentConfigRepository paymentConfigRepo,
            IAccountRepository accoutRepo, IWalletRepository walletRepo, IPaymentHelper paymentHelper,
            ILogCreditTransRepository logCreditTransRepo, IExchangeUserRepository exchangeUser,
            IMyConfigRepository myConfig, IPaymentLogRepository paymentLogRepository,
            IRankingDetailRepository rankingDetailRepo, IOfflineMessageBusiness offlineMessage,
            IEventBusiness eventBusiness, ICommonRedis commonRedis)
        {
            _logCardTranRepo = logCardTranRepo;
            _paymentConfigRepo = paymentConfigRepo;
            _accountRepo = accoutRepo;
            _walletRepo = walletRepo;
            _paymentHelper = paymentHelper;
            _logCreditTransRepo = logCreditTransRepo;
            _myConfigRepo = myConfig;
            _paymentLog = paymentLogRepository;
            _rankingDetailRepo = rankingDetailRepo;
            _offlineMessage = offlineMessage;
            _eventBusiness = eventBusiness;

            _commonRedis = commonRedis;
        }

        #endregion

        #region Thông tin nạp gold

        /// <summary>
        /// Lấy danh sách loại thẻ nạp
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 16/12/2015 PhatVT: Create New
        /// </history>
        public PaymentCard GetPaymentCard(string paymentTypeCard)
        {
            return GetPayment(paymentTypeCard);
        }

        /// <summary>
        /// Lấy thông tin cấu hình nạp thẻ qua Google IAP
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 01/12/2015 MinhT: Create New
        /// 3/6/2016 Update TaiNM - add thêm channelId
        /// </history>
        public GoogleIAPModel GetGoogleIAP(int paymentType, int channelId)
        {
            var result = new GoogleIAPModel
            {
                IsMaintenance = true
            };
            #region kiểm tra xem card có đang bảo trì không
            var paymentConfig = GetPaymentCard(MyConfigKey.PaymentCard.Text());
            result.IsMaintenance = paymentConfig.IsMaintenance;
            result.Message = paymentConfig.Message;
            #endregion


            result.ListDataByChannel = GetPaymentConfig_Channel_PaymentType(channelId, paymentType);
            //result.ListData = _paymentConfigRepo.GetGoogleIAP(paymentType);
            return result;
        }

        #endregion

        #region Nạp gold

        /// <summary>
        /// Nạp thẻ
        /// ../12/2015 MinhT : Create New
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="serial"></param>
        /// <param name="pinCode"></param>
        /// <param name="cardTypeId"></param>
        /// <param name="iPAddress"></param>
        /// <param name="platformId"></param>
        /// <param name="hardwareId"></param>
        /// <param name="serviceConfig">Cấu hình gọi service</param>
        /// <param name="defaultAdminId"></param>
        /// <returns></returns>
        public ChargeCardResponse ChargeCard(int userId, string serial, string pinCode, int cardTypeId,
            string iPAddress, int platformId, string hardwareId, PaymentServiceConfig serviceConfig, int defaultAdminId, int intChannelID, string strAppVersion)
        {
            var result = new ChargeCardResponse();

            #region Chuẩn bị thông tin

            // Lấy và kiểm tra loại thẻ
            var cardType = GetCardType(cardTypeId, MyConfigKey.PaymentCard.Text());
            if (cardType == null || cardType.IsMaintenance)
            {
                result.Result = ChargeCardStatus.CardIsMaintenance;
                return result;
            }

            #endregion

            #region Ghi giao dịch nạp thẻ

            // Ghi log giao dịch
            var transStatus = TransactionStatus.New;
            int transLogId = _logCardTranRepo.Insert(userId, serial, pinCode, cardType.Id, iPAddress, platformId, (int)result.Result, intChannelID, strAppVersion);

            #endregion

            #region Lấy thông tin user theo ID

            var account = _accountRepo.GetAccountById(userId);
            if (account == null)
            {
                result.Result = ChargeCardStatus.NotExistUser;
                return result;
            }

            #endregion

            #region Gọi đối tác gạch thẻ

            var useCardModel = new UseCardModel
            {
                CardType = cardType.Id.ToEnum<CardTypeEnum>(),
                Serial = serial,
                PinCard = pinCode,
                PartnerTransId = MyUtility.StringCommon.GenTranId()
            };
            int cardAmount = 0;
            int partnerId = 0;
            string partnerTransId = string.Empty;
            string partnerMessage = string.Empty;
            const WalletTypeEnum walletType = WalletTypeEnum.MainWallet;
            bool serviceResult = false;

            try
            {
                var serviceResponse = _paymentHelper.UseCard(useCardModel, serviceConfig);
                //CommonLogger.PaymentLogger.Debug("ChargeCard | Data: " + JsonConvert.SerializeObject(serviceResponse));
                cardAmount = serviceResponse.Amount;
                partnerId = serviceResponse.PartnerId;
                partnerMessage = serviceResponse.Message;
                partnerTransId = serviceResponse.PartnerTransId;
                serviceResult = serviceResponse.Result;
            }
            catch (Exception ex)
            {
                /* Cap nhat lai giao dich la xay ra loi hoac timeout */
                var exceptionString = ex.StackTrace;
                transStatus = exceptionString.Contains("time out") || exceptionString.Contains("timeout")
                                    ? TransactionStatus.TimeOut
                                    : TransactionStatus.ErrorFromPartner;
            }

            #endregion

            #region Cập nhật lại giao dịch

            decimal coin = 0;

            if (!serviceResult)
            {
                result.Result = ChargeCardStatus.Failure;

                // Cập nhật lại Giao dịch ở Log
                transStatus = TransactionStatus.New;
                _logCardTranRepo.Update(userId, transLogId, partnerTransId, transStatus.Value(), cardAmount, partnerId, partnerMessage);

                return result;
            }
            //cardAmount = 50000;
            // Lấy cấu hình tỉ lệ, kiểm tra thẻ có đang bảo trì không?
            var paymentConfig = _paymentConfigRepo.GetCoin(cardAmount, cardType.Id);
            if (paymentConfig == null || !paymentConfig.Gold.HasValue)
            {
                transStatus = TransactionStatus.InvalidConfig;
                result.Result = ChargeCardStatus.SuccessButNotAddGold;

            }
            else
            {
                transStatus = TransactionStatus.Success;
                coin = paymentConfig.Gold.Value;
            }
            result.CoinTransfer = coin;
            result.Coin = coin;
            result.UserID = account.UserID;
            string messageContent = "Nạp thẻ thành công. " +
                                    "Bạn đã nạp: " + StringCommon.FormatCurrency(coin) +
                                    " xu.";
            if (transStatus == TransactionStatus.Success)
            {
                // Nạp gold vào ví cho User
                var newCoin = _walletRepo.UpdateCoin(userId, walletType.Value(), result.Coin);

                result.Coin = newCoin;
                result.Result = ChargeCardStatus.Success;

                // Cập nhật lại Giao dịch ở Log
                _logCardTranRepo.Update(userId, transLogId, partnerTransId, transStatus.Value(), cardAmount, partnerId, partnerMessage);

                #region Event nạp tiền
                var promotionResults = _eventBusiness.RunPromotion(new PromotionRequestModel
                   {
                       ClientTarget = ClientTargetEnum.Mobile.Value(),
                       Description = "Khuyen mai nap card",
                       IsMobile = true,
                       LoginType = ClientTargetEnum.Mobile.Value(),
                       RunOn = RunOnEnum.Deposit.Value(),
                       UserId = userId,
                       ChanelId = intChannelID,
                       GameVersion = strAppVersion,
                       PlatformId = platformId,
                       IpRequest = iPAddress,
                       Amount = cardAmount,
                       Gold = coin,
                       CardType = cardType.Id
                   });

                var evnaptien = promotionResults.Where(x => x.Status == EventStatusCode.Success).ToList();


                if (evnaptien.Any()) // lay gold va tn km
                {
                    foreach (var t in evnaptien)
                    {
                        messageContent += " \n" + t.Message;
                        result.Coin = result.Coin + t.Coin;
                    }
                    result.Message = messageContent;
                }
                #endregion

            }

            #endregion

            #region Chạy sự kiện

            #region Gởi tin nhắn cho user

            //1: Tin nhắn thông thường, 2: Tin nhắn đổi thẻ
            const int messageType = 2;
            //0: Chưa gửi, 1: Hiển thị, 2: User xóa, 3: Admin xóa
            const int status = 1;
            //true: do hệ thống gởi
            const bool isSystem = true;
            //Id message mà nó comment
            const int parentId = 0;

            var value = _offlineMessage.SendMessage(defaultAdminId, userId, messageContent, messageType, status, iPAddress, platformId, hardwareId, isSystem, parentId);
            #endregion

            #endregion

            return result;
        }

        #endregion

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/12/2015</para>
        /// <para>lay coin hien tai cua user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public decimal GetCoinUser(int userId)
        {
            return _walletRepo.GetCoin(userId);
        }

        #region Credit Trans Google

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Xu ly nap tien In App Purcharse</para>
        /// </summary>
        /// <returns></returns>
        public DepositInAppPurchaseResponseModel DepositInAppPurchase(DepositInAppPurchaseModel model)
        {
            var transId = Common.GetMd5Hash(model.UserId + "|" + model.PackageName + "|" + model.ProductId + "|" + model.Token);

            var depositLogInAppPurcharseModel = new DepositLogGoogleModel
            {
                UserId = model.UserId,
                CardAmount = 0,
                CardType = model.InAppPurchaseType, // card type la google
                CardTypeString = model.InAppPurchaseType.Text(), // "Google Wallet",
                Description = "Đang xử lý|" + model.GooglePinCard,
                GoogleModel = new DepositGoogleModel
                {
                    ProductId = model.ProductId,
                    Token = model.Token,
                    PackageName = model.PackageName,
                },
                PinCard = model.GooglePinCard,

                Result = ChargeGoldResultEnum.Pending,
                TargetIp = model.IpClient,
                TransId = transId,
                ChannelID = model.ChannelID,
                PlatformId = model.PlatformId,
                VersionGame = model.VersionGame
            };

            var goldUser = GetCoinUser(model.UserId);
            var depositLogGoogleResult = DepositLogGoogleResultEnum.SqlError;
            var cardTransId = LogCreditTrans_InsertDepositInApp(depositLogInAppPurcharseModel, ref depositLogGoogleResult);

            switch (depositLogGoogleResult)
            {
                case DepositLogGoogleResultEnum.TransactionSuccess:
                    return new DepositInAppPurchaseResponseModel
                    {
                        CoinUser = goldUser,
                        CoinDeposit = model.ProductValue,
                        StatusCode = ChargeGoldResultEnum.SuccessBefore,
                    };

                case DepositLogGoogleResultEnum.TransactionError:
                case DepositLogGoogleResultEnum.SqlError:
                    return new DepositInAppPurchaseResponseModel
                    {
                        CoinUser = goldUser,
                        CoinDeposit = 0,
                        StatusCode = ChargeGoldResultEnum.Error,
                    };

                case DepositLogGoogleResultEnum.NotExists:
                case DepositLogGoogleResultEnum.TransactionPending:
                    break;
            }

            switch (model.InAppPurchaseType)
            {
                case CreditCardTypeEum.GoogleStore:
                    #region nap tien google store

                    var rs = GoogleInAppPurcharse(model, cardTransId);

                    if (rs.StatusCode == ChargeGoldResultEnum.Success ||
                        rs.StatusCode == ChargeGoldResultEnum.SuccessButAddGoldFail)
                        return rs;

                    rs.CoinDeposit = model.ProductValue;
                    rs.CoinUser = goldUser;
                    return rs;

                    #endregion

                case CreditCardTypeEum.AppleStore:
                    #region nap tien google store

                    var aBc = GoogleInAppPurcharse(model, cardTransId);

                    if (aBc.StatusCode == ChargeGoldResultEnum.Success ||
                        aBc.StatusCode == ChargeGoldResultEnum.SuccessButAddGoldFail || aBc.StatusCode == ChargeGoldResultEnum.FailFromIap)
                        return aBc;

                    aBc.CoinDeposit = model.ProductValue;
                    aBc.CoinUser = goldUser;
                    return aBc;

                    #endregion
            }

            return new DepositInAppPurchaseResponseModel
            {
                StatusCode = ChargeGoldResultEnum.Error,
                CoinUser = goldUser,
                CoinDeposit = model.ProductValue
            };
        }

        public int LogCreditTrans_InsertDepositInApp(DepositLogGoogleModel model, ref DepositLogGoogleResultEnum depositLogGoogleResult)
        {
            //Logger.CommonLogger.DefaultLogger.Debug("{ userId: " + model.UserId
            //                                      + ", transId: " + model.TransId
            //                                      + ", productName: " + model.GoogleModel.PackageName
            //                                      + ", productId: " + model.GoogleModel.ProductId
            //                                      + ", token: " + model.GoogleModel.Token + " }");

            depositLogGoogleResult = DepositLogGoogleResultEnum.SqlError;
            //var resultTransaction = DepositLogGoogleResultEnum.SqlError.Value();

            var retInsert = _logCreditTransRepo.LogCreditTrans_InsertDepositInApp(model.UserId, model.TransId, model.CurrentCoin,
                    model.CoinTrans, model.Result.Value(), model.Description, model.Token, model.CardType.Value(),
                    model.CardTypeString, model.CardAmount, model.ChannelID, model.PlatformId.Value(), model.VersionGame);

            if (retInsert == null) return 0;

            var cardTransId = retInsert.CardTranId.HasValue ? retInsert.CardTranId.Value : 0;
            depositLogGoogleResult = retInsert.ResultTransaction.HasValue
                                         ? retInsert.ResultTransaction.Value.ToEnum<DepositLogGoogleResultEnum>()
                                         : DepositLogGoogleResultEnum.SqlError;

            return cardTransId;
        }

        public LogCreditTran GetOne_LogCreditTran(int id)
        {
            return _logCreditTransRepo.GetOne(id);
        }

        public bool Update_LogCreditTran(LogCreditTran entities)
        {
            return _logCreditTransRepo.Save(entities);
        }

        public bool LogCreditTrans_UpdateData(LogCreditTran entities)
        {
            return
                _logCreditTransRepo.LogCreditTrans_updateData(entities.ID, entities.Status.GetValueOrDefault(0), entities.Token,
                    entities.Description, entities.Amount.GetValueOrDefault(0), entities.CurrentCoin.GetValueOrDefault(0), entities.CoinTrans.GetValueOrDefault(0)) > 0;
        }
        #endregion

        #region cong tru gold
        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/12/2015</para>
        /// <para>cập nhật gold cho user và ghi log</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="goldTrans"></param>
        /// <param name="reasonId"></param>
        /// <param name="description"></param>
        /// <param name="actionId"></param>
        /// <param name="isMobile"></param>
        /// <param name="loginType"></param>
        /// <param name="clientTarget"></param>
        /// <param name="historyId"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public WalletExchangeStatus Wallet_AddOrSubtractGoldUser(int userId, decimal goldTrans, int reasonId, string description, int actionId,
            bool isMobile, int loginType, int clientTarget, ref int historyId, int adminId = 0)
        {
            var objReturn = WalletExchangeStatus.Failure;
            try
            {
                var objResult = _walletRepo.Wallet_AddOrSubtractGoldUser(userId, goldTrans, reasonId, description, actionId,
                   isMobile, loginType, clientTarget, adminId);
                if (objResult > 0)
                {
                    historyId = objResult;
                    objResult = 1;
                }
                objReturn = objResult.ToEnum<WalletExchangeStatus>();
            }
            catch (Exception)
            {
                objReturn = WalletExchangeStatus.SystemError;
            }

            return objReturn;

        }
        #endregion

        #region lịch sử gold User

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>lịch sử nạp gold của user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ChangeCardHistoryResponse PaymentLog_GetHistoryPagingByUserId(int userId,
            int pageNumber, int pageSize)
        {
            int totalRow;
            var listHistory = _paymentLog.PaymentLog_GetHistoryPagingByUserId(userId, pageNumber, pageSize, out totalRow).OrderByDescending(x => x.CreateDate).ToList();
            var response = listHistory.Select(x => new ChangeCardHistoryModel
            {
                RowNumber = Int32.Parse(x.RowNumber.GetValueOrDefault().ToString()),
                PaymentType = ((PaymentTypeEnum)x.PaymentType.GetValueOrDefault()).Text(),
                PaymentTypeId = x.PaymentType.GetValueOrDefault(0),
                CardName = x.PaymentType.GetValueOrDefault(0) == PaymentTypeEnum.GoogleIap.Value() ? "IAP" : ((ItemPaymentTypeEnum)x.ItemType.GetValueOrDefault()).Text(),
                GoldValue = x.Amount.GetValueOrDefault(0),
                DateCreated = x.CreateDate.GetValueOrDefault().GetVnDateFormat(),
                Serial = x.Serial
            }).ToList();
            return new ChangeCardHistoryResponse
            {
                TotalRows = totalRow,
                ListData = response
            };
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>lưu lịch sử nạp tiền</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paymentId"></param>
        /// <param name="platformId"></param>
        /// <param name="imei"></param>
        /// <param name="harwareid"></param>
        /// <param name="ipaddress"></param>
        /// <param name="status"></param>
        /// <param name="objId"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="itemType"></param>
        /// <returns>LogID</returns>
        public int PaymentLog_InsertData(int userId, int paymentId, int platformId, string imei, string harwareid,
            string ipaddress, int status, int objId, decimal paymentAmount, int itemType)
        {
            return _paymentLog.PaymentLog_InsertData(userId, paymentId, platformId, imei, harwareid, ipaddress,
                status, objId, paymentAmount, itemType);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>cập nhật log nạp gold</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool PaymentLog_UpdateData(int id, int status)
        {
            return _paymentLog.PaymentLog_UpdateData(id, status) > 0;
        }

        #endregion

        #region GetTopChargeCard

        ///// <summary>
        ///// 24/12/2015 MinhT : Create New
        ///// Lấy Top nộp card của User
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <returns></returns>
        //public List<Out_RankingDetail_GetTopChargeCard_Result> GetTopChargeCard(int start, int end)
        //{
        //    var result = _rankingDetailRepo.GetTopChargeCard(start, end).OrderBy(x => x.OrderNo).ToList();
        //    return result;
        //}

        /// <summary>
        /// Lấy Top nộp card của User
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        /// <history>
        /// 10/6/2016 Update by TaiNM
        /// </history>
        public List<TopChargeCardModel> GetTopChargeCard(int pageSize, int start, int end, DateTime dtmClearCacheRedis)
        {
            #region old

            //var result = _rankingDetailRepo.GetTopChargeCard(start, end).OrderBy(x => x.OrderNo);
            //if (result.Any())
            //    return result.Select(t => new TopChargeCardModel
            //    {
            //        Username = t.Username,
            //        RowNumber = t.OrderNo.HasValue ? t.OrderNo.Value : 0,
            //        TotalAmount = t.TotalAmount.HasValue ? (int)t.TotalAmount.Value : 0
            //    }).ToList();

            //return new List<TopChargeCardModel>();

            #endregion

            var strKey = RedisKeyConstants.GetTopChargeCard + ":" + pageSize;
            var jsonData = _commonRedis.GetData(strKey);
            var dataResults = new List<TopChargeCardModel>();
            if (!string.IsNullOrEmpty(jsonData))/*Get data*/
                dataResults = JsonConvert.DeserializeObject<List<TopChargeCardModel>>(jsonData);
            else/*Set data*/
            {
                var result = _rankingDetailRepo.GetTopChargeCard(0, pageSize).OrderBy(x => x.OrderNo);
                if (result.Any())
                    dataResults = result.Select(t => new TopChargeCardModel
                    {
                        Username = t.Username,
                        RowNumber = t.OrderNo.HasValue ? t.OrderNo.Value : 0,
                        TotalAmount = t.TotalAmount.HasValue ? (int)t.TotalAmount.Value : 0
                    }).ToList();
                _commonRedis.SetData(strKey, dtmClearCacheRedis, JsonConvert.SerializeObject(dataResults));
            }

            if (dataResults.Any())
                return dataResults.OrderByDescending(t => t.TotalAmount).Skip(start).Take(end).ToList();

            return new List<TopChargeCardModel>();
        }

        #endregion

        /// <summary>
        /// ../12/2015 MinhT : Create New
        /// Chuyển tiền ra vào rương
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="goldTransfer"></param>
        /// <param name="actionType"></param>
        /// <param name="ipAddress"></param>
        /// <param name="platformId"></param>
        /// <param name="hardwareId"></param>
        /// <returns></returns>
        public CofferModel DepositOrWithdrawCoffer(int userId, decimal goldTransfer, int actionType, string ipAddress, int platformId, string hardwareId)
        {
            int result;
            decimal walletNow;
            decimal cofferNow;
            _walletRepo.DepositOrWithdrawCoffer(userId, goldTransfer, actionType, ipAddress, platformId, hardwareId, out result, out walletNow, out cofferNow);

            return new CofferModel
            {
                Result = result,
                CofferNow = cofferNow,
                WalletNow = walletNow
            };
        }

        #region Private methods

        /// <summary>
        /// Lấy thông tin loại thẻ theo Id
        /// </summary>
        /// <param name="cardTypeId"></param>
        /// <param name="myConfigKey"></param>
        /// <returns></returns>
        private CardType GetCardType(int cardTypeId, string myConfigKey)
        {
            var payment = GetPayment(myConfigKey);
            CardType cardType = payment.CardTypes.FirstOrDefault(x => x.Id == cardTypeId);
            return cardType;
        }

        /// <summary>
        /// 12/1/201
        /// </summary>
        /// <param name="paymentCardType"></param>
        /// <returns></returns>
        private PaymentCard GetPayment(string paymentCardType)
        {
            #region Danh sách Card

            var config = _myConfigRepo.Get(paymentCardType);
            if (config == null || string.IsNullOrEmpty(config.Value))
            {
                return new PaymentCard
                {
                    IsMaintenance = true,
                    CardTypes = new List<CardType>()
                };
            }
            var paymentCard = JsonConvert.DeserializeObject<PaymentCard>(config.Value);
            if (paymentCard.CardTypes == null)
            {
                paymentCard.CardTypes = new List<CardType>();
            }

            // Xóa loại thẻ nếu nó không được sử dụng
            for (int i = 0; i < paymentCard.CardTypes.Count; i++)
            {
                if (!paymentCard.CardTypes[i].IsEnable)
                {
                    paymentCard.CardTypes.RemoveAt(i);
                    i--;
                }
                paymentCard.CardTypes[i].Name = ((CardTypeEnum)paymentCard.CardTypes[i].Id).Text();
            }
            //{"IsMaintenance":true,"Message":"Chức năng sẽ được online vào ngày 21/03/2016", "IsEnable": true, CardTypes: [{"Id": 1, "IsMaintenance":true,"IsEnable":true,"Message":"Chức năng sẽ được online vào ngày 21/03/2016","Name":"VMS", "OrderNo": 1, "ImageLink": "Images/Exchange/mobifoneoff.png", "ActiveImageLink": "Images/Exchange/mobifoneon.png"},{"Id": 2, "IsMaintenance":true,"IsEnable":true,"Message":"Chức năng sẽ được online vào ngày 21/03/2016","Name":"VNP", "OrderNo": 3, "ImageLink": "Images/Exchange/vinaphoneoff.png", "ActiveImageLink": "Images/Exchange/vinaphoneon.png"},{"Id": 3, "IsMaintenance":true,"IsEnable":true,"Message":"Chức năng sẽ được online vào ngày 21/03/2016","Name":"VTT", "OrderNo": 2, "ImageLink": "Images/Exchange/vietteloff.png", "ActiveImageLink": "Images/Exchange/viettelon.png"},{"Id": 5, "IsMaintenance":true,"IsEnable":false,"Message":"Chức năng sẽ được online vào ngày 21/03/2016","Name":"BIT", "OrderNo": 4, "ImageLink": "Images/Exchange/bitoff.png", "ActiveImageLink": "Images/Exchange/biton.png"},{"Id": 4, "IsMaintenance":true,"IsEnable":true,"Message":"Chức năng sẽ được online vào ngày 21/03/2016","Name":"GATE", "OrderNo": 5, "ImageLink": "Images/Exchange/gateoff.png", "ActiveImageLink": "Images/Exchange/gateon.png"}]}
            //{"IsMaintenance":true,"Message":"Chức năng sẽ được online vào ngày 21/03/2016", "IsEnable": true, CardTypes: [{"Id": 1, "IsMaintenance":false,"IsEnable":true,"Message":"Hệ thống đang bảo trì, bạn vui lòng quay lại sau","Name":"VMS", "OrderNo": 1, "ImageLink": "Images/Exchange/mobifoneoff.png", "ActiveImageLink": "Images/Exchange/mobifoneon.png"},{"Id": 2, "IsMaintenance":false,"IsEnable":true,"Message":"Hệ thống đang bảo trì, bạn vui lòng quay lại sau","Name":"VNP", "OrderNo": 3, "ImageLink": "Images/Exchange/vinaphoneoff.png", "ActiveImageLink": "Images/Exchange/vinaphoneon.png"},{"Id": 3, "IsMaintenance":false,"IsEnable":true,"Message":"Hệ thống đang bảo trì, bạn vui lòng quay lại sau","Name":"VTT", "OrderNo": 2, "ImageLink": "Images/Exchange/vietteloff.png", "ActiveImageLink": "Images/Exchange/viettelon.png"},{"Id": 5, "IsMaintenance":false,"IsEnable":false,"Message":"Hệ thống đang bảo trì, bạn vui lòng quay lại sau","Name":"BIT", "OrderNo": 4, "ImageLink": "Images/Exchange/bitoff.png", "ActiveImageLink": "Images/Exchange/biton.png"},{"Id": 4, "IsMaintenance":false,"IsEnable":true,"Message":"Hệ thống đang bảo trì, bạn vui lòng quay lại sau","Name":"GATE", "OrderNo": 5, "ImageLink": "Images/Exchange/gateoff.png", "ActiveImageLink": "Images/Exchange/gateon.png"}]}
            #endregion

            #region Tỉ lệ quy đổi
            //
            var temp = _paymentConfigRepo.GetRateCard();
            var listCardRates = temp.Select(item => new CardRate
            {
                Id = item.Id,
                CardType = item.CardType.GetValueOrDefault(),
                CardName = ((CardTypeEnum)item.CardType.GetValueOrDefault()).Text(),
                Amount = item.Amount.GetValueOrDefault(),
                Gold = item.Gold.GetValueOrDefault()
            }).ToList();

            paymentCard.CardRate = listCardRates;

            #endregion

            return paymentCard;
        }

        #region In App Purchase

        #region Google

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Nap tien Google In App Purcharse</para>
        /// </summary>
        /// <returns></returns>
        private DepositInAppPurchaseResponseModel GoogleInAppPurcharse(DepositInAppPurchaseModel model, int logCardTransId)
        {
            //var depositGoogleStatus = CheckTransaction(model);
            var depositGoogleStatus = new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.DangChoXuLy, string.Empty, string.Empty);
            DepositAppleResultModel depositAppleResult = null;
            if (model.PlatformId == PlatformIdEnum.Ios)
                depositGoogleStatus = CheckTransactionIos(model, ref depositAppleResult);
            else if (model.PlatformId == PlatformIdEnum.Android)
                depositGoogleStatus = CheckTransaction(model);
            switch (depositGoogleStatus.Item1)
            {
                case DepositCreditStatusEnum.ThongTinGiaoDichKhongDung:
                case DepositCreditStatusEnum.KhongLayDuocThongTinGiaoDich:
                    return new DepositInAppPurchaseResponseModel
                    {
                        StatusCode = ChargeGoldResultEnum.IapTransactionNotExists,
                    };

                case DepositCreditStatusEnum.ThatBai:
                    return new DepositInAppPurchaseResponseModel
                    {
                        StatusCode = ChargeGoldResultEnum.FailFromIap,
                    };

                case DepositCreditStatusEnum.DangChoXuLy:
                    return new DepositInAppPurchaseResponseModel
                    {
                        StatusCode = ChargeGoldResultEnum.IapPending,
                    };

                case DepositCreditStatusEnum.ThanhCong:
                    #region xu ly nap tien

                    #region kiem tra giao dich

                    var logPaypalTransactionItem = GetOne_LogCreditTran(logCardTransId);
                    if (logPaypalTransactionItem == null)
                    {
                        //Logger.CommonLogger.DefaultLogger.Debug("GoogleWallet: Khong lay duoc IdLog: logCardTransId = " + logCardTransId);
                        return new DepositInAppPurchaseResponseModel
                        {
                            StatusCode = ChargeGoldResultEnum.TransactionNotExists,
                        };
                    }

                    #endregion

                    #region ghi log nap gold

                    var paymentType = PaymentTypeEnum.GoogleIap.Value();
                    switch (model.PlatformId)
                    {
                        case PlatformIdEnum.Android:
                            paymentType = PaymentTypeEnum.GoogleIap.Value();
                            break;
                        case PlatformIdEnum.Ios:
                            paymentType = PaymentTypeEnum.IosIap.Value();
                            break;
                        case PlatformIdEnum.WindowsPhone:
                            paymentType = PaymentTypeEnum.WdpIap.Value();
                            break;
                        default:
                            paymentType = PaymentTypeEnum.GoogleIap.Value();
                            break;
                    }
                    var logPaymentId = PaymentLog_InsertData(model.UserId, paymentType,
                        model.PlatformId.Value(), model.DeviceId, model.HardwareId, model.IpClient, 0, 0,
                        model.ProductValue, paymentType);

                    #endregion

                    #region cong gold cho user

                    var historyId = 0;
                    var retAddGold = Wallet_AddOrSubtractGoldUser(model.UserId, model.ProductValue,
                        ReasonEnum.NapTienGoogleWallet.Value(), LogTransEnum.NapTienGoogleWallet.Text(),
                        LogTransEnum.NapTienGoogleWallet.Value(), true, model.ClientTarget.Value(),
                        model.ClientTarget.Value(), ref historyId);

                    #endregion

                    #region cap nhat giao dich nap tien

                    var kq = retAddGold == WalletExchangeStatus.Success;
                    var coinUser = GetCoinUser(model.UserId);
                    var responseData = new DepositInAppPurchaseResponseModel
                    {
                        CoinDeposit = model.ProductValue,
                        CoinUser = coinUser,
                        StatusCode = kq == false
                            ? ChargeGoldResultEnum.SuccessButAddGoldFail
                            : ChargeGoldResultEnum.Success,
                        ProductId = depositGoogleStatus.Item2
                    };

                    // Ket qua nap tien

                    logPaypalTransactionItem.Status = responseData.StatusCode.Value();
                    logPaypalTransactionItem.ReceiveGoldDate = DateTime.Now;
                    logPaypalTransactionItem.Token = string.Empty; //"Token=" + model.Token + "|" + model.GooglePinCard;
                    logPaypalTransactionItem.Description = depositGoogleStatus.Item3;
                    logPaypalTransactionItem.Amount = model.ProductValue;
                    logPaypalTransactionItem.CurrentCoin = coinUser;
                    logPaypalTransactionItem.CoinTrans = model.ProductAmount;
                    //Update_LogCreditTran(logPaypalTransactionItem);
                    LogCreditTrans_UpdateData(logPaypalTransactionItem);
                    #region cap nhat log tang gold

                    try
                    {
                        PaymentLog_UpdateData(logPaymentId, (kq ? 1 : 0));
                    }
                    catch (Exception ex)
                    {
                        Logger.CommonLogger.PaymentLogger.Error("Lỗi cập nhật logGold: " + ex);
                    }

                    #endregion

                    return responseData;

                    #endregion

                    #endregion

                default:
                    return new DepositInAppPurchaseResponseModel
                    {
                        StatusCode = ChargeGoldResultEnum.Pending,
                    };
            }
        }

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Kiem tra giao dich tu google</para>
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 22/4/2016 Tai Update - Return Object 3 tham số: Status, ProductId, 3, chuỗi apple trả về
        /// </history>
        private Tuple<DepositCreditStatusEnum, string, string> CheckTransaction(DepositInAppPurchaseModel model)
        {
            if (model.IsTest) return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThanhCong, "Test", string.Empty);
            if (string.IsNullOrEmpty(model.UrlRequest)) return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.KhongTruyCapDuocGoogle, string.Empty, string.Empty);

            // url request len google de kiem tra giao dich
            var urlRequest = string.Format(model.UrlRequest, model.PackageName, model.ProductId, model.Token);

            var authenGoogle = RefreshTokenGoogle(model);
            if (authenGoogle == null) return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.KhongTruyCapDuocGoogle, string.Empty, string.Empty);

            // goi request len google de kiem tra giao dich
            var dataString = NetworkCommon.CallRequest(urlRequest + "?access_token=" + authenGoogle.access_token, NetworkCommon.HttpRequestEnum.Get);

            // khong lay duoc thong tin giao dich
            if (dataString == string.Empty) return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThongTinGiaoDichKhongDung, string.Empty, string.Empty);

            DepositGoogleResultModel dataObject = null;

            // parse thong tin giao dich
            try
            {
                dataObject = JsonConvert.DeserializeObject<DepositGoogleResultModel>(dataString);
            }
            catch
            {
                return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThongTinGiaoDichKhongDung, string.Empty, string.Empty);
            }

            // khong parse duoc thong tin giao dich
            if (dataObject == null) return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThongTinGiaoDichKhongDung, string.Empty, string.Empty);

            // tra ve thanh cong khi giao dich duoc thuc hien
            return dataObject.PurchaseState.ToEnum<PurchaseStateGoogleEnum>() == PurchaseStateGoogleEnum.Purchased
                ? new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThanhCong, dataObject.product_id, dataString)
                : new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThatBai, string.Empty, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        /// <history>
        /// 22/4/2016 Tai Update - Return Object 3 tham số: Status, ProductId, 3, chuỗi apple trả về
        /// </history>
        public static Tuple<DepositCreditStatusEnum, string, string> CheckTransactionIos(DepositInAppPurchaseModel model, ref DepositAppleResultModel dataObject)
        {
            if (model.IsTest) return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThanhCong, "Test", string.Empty);
            // url request len google de kiem tra giao dich
            var urlRequest = string.Format(model.UrlRequest);

            try
            {

                // goi request len google de kiem tra giao dich
                var dataString = NetworkCommon.CallRequest(urlRequest, NetworkCommon.HttpRequestEnum.Post, "{\"receipt-data\":\"" + model.Token + "\"}");

                // khong lay duoc thong tin giao dich
                if (dataString == string.Empty) return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThongTinGiaoDichKhongDung, string.Empty, string.Empty);

                var jss = new JavaScriptSerializer();

                // parse thong tin giao dich
                try
                {
                    dataObject = jss.Deserialize<DepositAppleResultModel>(dataString);
                }
                catch
                {
                    return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThongTinGiaoDichKhongDung, string.Empty, string.Empty);
                }

                // khong parse duoc thong tin giao dich
                if (dataObject == null) return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThongTinGiaoDichKhongDung, string.Empty, string.Empty);

                // khong dung voi productid truyen len
                if (!model.ProductId.Equals(dataObject.receipt.product_id))
                    return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThongTinGiaoDichKhongDung, string.Empty, string.Empty);

                // tra ve thanh cong khi giao dich duoc thuc hien
                return dataObject.status == 0
                           ? new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThanhCong, dataObject.receipt.product_id, dataString)
                           : new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.ThatBai, string.Empty, string.Empty);
            }
            catch
            {
                return new Tuple<DepositCreditStatusEnum, string, string>(DepositCreditStatusEnum.KhongTruyCapDuocApple, string.Empty, string.Empty);
            }
        }

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Refresh lai token truy cap cua google</para>
        /// </summary>
        /// <returns></returns>
        private GoogleApiAccessToken RefreshTokenGoogle(DepositInAppPurchaseModel model)
        {
            if (string.IsNullOrEmpty(model.UrlRefreshToken)) return null;

            var postData = "grant_type=refresh_token"
                           + "&client_id=" + model.ClientId
                           + "&client_secret=" + model.ClientSecret
                           + "&refresh_token=" + model.RefreshToken;

            var dataString = NetworkCommon.CallRequest(model.UrlRefreshToken, NetworkCommon.HttpRequestEnum.Post, postData);

            // khong lay duoc access token
            if (dataString == string.Empty) return null;

            GoogleApiAccessToken dataObject = null;

            // parse thong tin giao dich
            try { dataObject = JsonConvert.DeserializeObject<GoogleApiAccessToken>(dataString); }
            catch { return null; }

            // tra ve thanh cong khi giao dich duoc thuc hien
            return dataObject;
        }

        #endregion

        #region Apple
        #endregion

        #endregion

        #endregion

        #region get payment config - TaiNM

        /// <summary>
        /// Lấy danh sách config IsEnable = true theo channel và payment type
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        /// <history>
        /// 01/6/2016 Create by TaiNM
        /// </history>
        public List<PaymentConfigModel> GetPaymentConfig_Channel_PaymentType(int channelId,
            int paymentTypeId)
        {
            var lst = new List<PaymentConfigModel>();

            var results = _paymentConfigRepo.GetPaymentConfig_Channel_PaymentType(channelId, paymentTypeId);
            if (results != null && results.Any())
                lst = results.Select(t => new PaymentConfigModel
                {
                    Id = t.Id,
                    Amount = t.Amount.HasValue ? t.Amount.Value : 0,
                    Gold = t.Gold.HasValue ? t.Gold.Value : 0,
                    PacketName = t.PacketName
                }).ToList();

            return lst;
        }

        #endregion

    }
}
