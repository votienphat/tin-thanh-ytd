using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject.ContentModule.Contract;
using BussinessObject.Enums;
using BussinessObject.Helper;
using BussinessObject.Models;
using BussinessObject.PaymentModule.Contract;
using BussinessObject.PaymentModule.Enums;
using BussinessObject.PaymentModule.Models;
using BussinessObject.UserModule.Contract;
using BussinessObject.UserModule.Enums;
using DataAccess.Contract.MainModule;
using DataAccess.Contract.PaymentModule;
using DataAccess.Contract.UserModule;
using DataAccess.Helper;
using DataAccessRedis.Constants;
using DataAccessRedis.Module.Contract;
using EntitiesObject.Entities.UserEntities;
using EntitiesObject.Model.Payment;
using HqCardckStockApi;
using HqCardckStockApi.Model;
using Logger;
using MyUtility;
using MyUtility.Extensions;
using Newtonsoft.Json;
using ExchangeConfig = BussinessObject.PaymentModule.Models.ExchangeConfig;

namespace BussinessObject.PaymentModule
{
    public class ExchangeBusiness : IExchangeBusiness
    {
        #region Variables + Constructor

        private readonly IExchangeConfigRepository _exchangeConfigRepo;
        private readonly IMyConfigRepository _myConfigRepo;
        private readonly IExchangeUserRepository _exchangeUser;
        private readonly IRankingDetailRepository _rankingDetailRepo;
        private readonly IWalletRepository _walletRepo;
        private readonly IPaymentConfigRepository _paymentConfigRepo;
        private readonly IOfflineMessageBusiness _offlineMessage;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IFarmerRepository _farmerRepository;
        private readonly IWalletStarRepository _starRepository;
        private readonly ICommonRedis _commonRedis;

        public ExchangeBusiness(IExchangeConfigRepository exchangeConfigRepo, IMyConfigRepository myConfigRepo, IExchangeUserRepository exchangeUserParam, IRankingDetailRepository rankingDetailRepo,
            IWalletRepository walletRepo, IPaymentConfigRepository paymentConfigRepo, IOfflineMessageBusiness offlineMessage, IAccountBusiness accountBusiness, IFarmerRepository farmerRepository,
            IWalletStarRepository starRepository, ICommonRedis commonRedis)
        {
            _exchangeConfigRepo = exchangeConfigRepo;
            _myConfigRepo = myConfigRepo;
            _exchangeUser = exchangeUserParam;
            _rankingDetailRepo = rankingDetailRepo;
            _walletRepo = walletRepo;
            _paymentConfigRepo = paymentConfigRepo;
            _offlineMessage = offlineMessage;
            _accountBusiness = accountBusiness;
            _farmerRepository = farmerRepository;
            _starRepository = starRepository;
            _commonRedis = commonRedis;
        }
        #endregion

        public ExchangeCard GetExchangeCard()
        {
            return GetExchange();
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
            #region danh sách Card
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
            #endregion

            #region tỉ lệ quy đổi
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

        private ExchangeCard GetExchange()
        {
            var config = _myConfigRepo.Get(MyConfigKey.ExchangeCard.Text());
            if (config == null || string.IsNullOrEmpty(config.Value))
            {
                return new ExchangeCard
                {
                    IsMaintenance = true,
                    CardTypes = new List<ExchangeCardType>()
                };
            }

            var exchangeCard = JsonConvert.DeserializeObject<ExchangeCard>(config.Value);
            if (exchangeCard.CardTypes == null)
            {
                exchangeCard.CardTypes = new List<ExchangeCardType>();
            }

            // Lấy các mệnh giá được đổi thẻ
            var cardAmounts = _exchangeConfigRepo.GetByType(ExchangeTypeEnum.Card.Value());

            // Xóa loại thẻ nếu nó không được sử dụng
            // đồng thời thêm các mệnh giá vào loại thẻ
            for (int i = 0; i < exchangeCard.CardTypes.Count; i++)
            {
                if (!exchangeCard.CardTypes[i].IsEnable)
                {
                    exchangeCard.CardTypes.RemoveAt(i);
                    i--;
                }
                else
                {
                    var exchangeCardType = exchangeCard.CardTypes[i];
                    exchangeCardType.CardAmounts = cardAmounts
                        .Where(
                            x =>
                                x.CardType == exchangeCardType.Id && x.Amount.GetValueOrDefault(0) > 0 &&
                                x.Gold.GetValueOrDefault(0) > 0)
                        .Select(x => new ExchangeCardAmount
                        {
                            ActiveImageLink = x.ActiveImageLink,
                            Amount = x.Amount.GetValueOrDefault(0),
                            Gold = x.Gold.GetValueOrDefault(0),
                            ImageLink = x.ImageLink
                        })
                        .ToList();
                }
            }

            return exchangeCard;
        }

        #endregion

        #region Đổi thưởng

        /// <summary>
        /// Đổi gold ra thẻ
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="cardTypeId"></param>
        /// <param name="cardAmount"></param>
        /// <param name="ipAddress"></param>
        /// <param name="platformId"></param>
        /// <param name="hardwareId"></param>
        /// <param name="serviceConfig"></param>
        /// <param name="maxEchange"></param>
        /// <param name="isApplyRule"></param>
        /// <param name="defaultAdminId"></param>
        /// <param name="socket"></param>
        /// <returns></returns>
        /// <history>
        /// 21/12/2015: Created by MinhT
        /// </history>
        public ExchangeCardResponse ExchangeCard(string token, int userId, int cardTypeId, int cardAmount, string ipAddress,
            int platformId, string hardwareId, PaymentServiceConfig serviceConfig, decimal maxEchange, bool isApplyRule, int defaultAdminId, SocketModel socket, int intChannelID)
        {
            var result = new ExchangeCardResponse
            {
                Result = ExChangeCardStatus.Failure
            };

            #region Kiểm tra hệ thống đổi card

            var cardType = GetCardType(cardTypeId, MyConfigKey.ExchangeCard.Text());
            if (cardType == null || cardType.IsMaintenance)
            {
                result.Result = ExChangeCardStatus.CardIsMaintenance;
                result.Message = cardType != null && !string.IsNullOrEmpty(cardType.Message) ? cardType.Message : ExChangeCardStatus.CardIsMaintenance.Text();
                return result;
            }
            #endregion

            //true = vượt quá
            var isOverAmount = false;
            //true = đã đổi
            var isApproval = false;
            //Nếu isOverAmount = true thì roundId = 1
            var roundId = 0;

            #region áp dụng rule + cho ra kết quả có chờ admin duyệt (result => IsApproval)
            if (isApplyRule)
            {
                bool isExchanged;
                decimal totalGoldExchanged;
                _exchangeUser.GetTotalGoldAndIsExchanged(userId, cardAmount, out totalGoldExchanged, out isExchanged);

                #region kiểm tra xem có quá tổng số tiền cho phép

                if (totalGoldExchanged > maxEchange)
                {
                    isOverAmount = true;
                }
                #endregion

                #region cài đặt nội dung tin nhắn nếu vượt quá tổng số tiền và chưa đổi mệnh giá đó trước đây

                if (isOverAmount)
                {
                    isApproval = true;
                    roundId = 1;
                    result.Message = ExChangeCardStatus.OverExchanged.Text();
                }
                if (!isExchanged && !isOverAmount)
                {
                    isApproval = true;
                    roundId = 0;
                    result.Message = ExChangeCardStatus.WaitApproval.Text();
                }
                #endregion
            }

            #endregion

            #region Trừ gold trong ví

            const int paymentType = 1;
            const WalletTypeEnum walletType = WalletTypeEnum.MainWallet;
            //Lấy Result và Gold sau khi đã quy đổi
            var myResult = _walletRepo.ExchangeToCard(userId, cardTypeId, paymentType, cardAmount, walletType.Value());

            switch ((ExchangeToCardResult)myResult.Result)
            {
                case ExchangeToCardResult.Success:
                    //Gọi socket để Client biết gold đã thay đổi
                    var websocketResponse = GameServerHelper.Call(socket, WsPacket.UpdateGoldUser, new { UserId = userId });
                    if (websocketResponse != SocketResponeMode.ThanhCong.Value())
                    {
                        Logger.CommonLogger.DefaultLogger.Debug("Trừ gold thành công. Nhưng Websocket chưa update gold.");
                        //result.Message = "Trừ gold thành công. Nhưng Websocket chưa update gold.";
                    }
                    break;
                case ExchangeToCardResult.Failed:
                    result.Result = ExChangeCardStatus.NotEnoughCoin;
                    result.Message = ExChangeCardStatus.NotEnoughCoin.Text();
                    return result;
                case ExchangeToCardResult.NotFoundCardType:
                    result.Result = ExChangeCardStatus.ErrorCardAmount;
                    result.Message = ExChangeCardStatus.ErrorCardAmount.Text();
                    return result;
                default:
                    result.Result = ExChangeCardStatus.LoiDoiGold;
                    result.Message = ExChangeCardStatus.LoiDoiGold.Text();
                    return result;
            }

            #endregion

            #region Ghi đổi thưởng vào ExchangeCardLog + chuyen cho admin aproval neu IsApproval = true
            //result vẫn để ở trạng thái chưa thành công. 

            int logId;
            _exchangeUser.AddNew(userId, cardTypeId, ((CardTypeEnum)cardTypeId).Text(), cardAmount, ipAddress, platformId, hardwareId, result.Result.Value(), isApproval, out logId, roundId, result.Message, intChannelID);
            #endregion

            var cardInfo = new UseCardModel();

            #region IsApproval = false ( ko chờ admin duyệt) => đổi ra thẻ
            //Đang hardcode paymentType và walletType
            if (!isApproval)
            {
                #region Đổi thẻ với đối tác

                var doithevoidoitac = DoiTheVoiDoiTac(cardAmount, cardTypeId, serviceConfig, myResult, userId,
                    out cardInfo);

                #endregion

                //Đang hardCode  partnerID.

                #region Update đổi thưởng

                const int partnerId = 0;
                var c = _exchangeUser.Update(doithevoidoitac.Result.Value(), cardInfo.Serial, cardInfo.PinCard,
                    partnerId, cardInfo.PartnerTransId, cardInfo.ResponseText, cardInfo.HqCardResponseE != 0, logId,
                    myResult.Gold.GetValueOrDefault(), intChannelID);

                #endregion

                result = doithevoidoitac;

                if (result.Result != ExChangeCardStatus.Success)
                {
                    result.Message = ExChangeCardStatus.LoiDoiGold.Text();
                }
            }



            #endregion

            #region gởi tin nhắn cho user
            //1: Tin nhắn thông thường, 2: Tin nhắn đổi thẻ, 3: Tin nhắn đổi thưởng
            const int messageType = 3;
            //0: Chưa gửi, 1: Hiển thị, 2: User xóa, 3: Admin xóa
            const int status = 1;
            //true: do hệ thống gởi
            const bool isSystem = true;
            //Id message mà nó comment
            const int parentId = 0;

            if (string.IsNullOrEmpty(cardInfo.OfflineMessage))
            {
                cardInfo.OfflineMessage = result.Message;
            }
            var value = _offlineMessage.SendMessage(defaultAdminId, userId, cardInfo.OfflineMessage, messageType, status, ipAddress, platformId, hardwareId, isSystem, parentId);
            #endregion

            #region thành công + trả về kết quả

            return result;

            #endregion
        }

        /// <summary>
        /// Đổi thưởng V2.
        /// Kiểm tra đủ tiền
        /// Kiểm tra giới hạn đổi trong ngày
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="cardTypeId"></param>
        /// <param name="cardAmount"></param>
        /// <param name="ipAddress"></param>
        /// <param name="platfornId"></param>
        /// <param name="hardwareId"></param>
        /// <param name="serviceConfig"></param>
        /// <param name="maxEchanged"></param>
        /// <param name="defaultAdminId"></param>
        /// <param name="socket"></param>
        /// <param name="isAutoExchange"></param>
        /// <history>
        /// 21/3/2016 Create By MinhT
        /// </history>
        /// <returns></returns>
        public ExchangeCardResponse ExchangeCard_V2(string token, int userId, int cardTypeId, int cardAmount, string ipAddress,
            int platfornId, string hardwareId, PaymentServiceConfig serviceConfig, decimal maxEchanged, int defaultAdminId, SocketModel socket, bool isAutoExchange, int intChannelID)
        {
            var response = new ExchangeCardResponse
            {
                Result = ExChangeCardStatus.Failure,
                Message = ExChangeCardStatus.Failure.Text()
            };

            //kiểm tra đang chơi game
            #region kiểm tra đang chơi game
            var isPlayingGame = _accountBusiness.IsPlayingGame(userId);
            if (isPlayingGame)
            {
                response.Result = ExChangeCardStatus.Failure;
                response.Message = ExChangeCardStatus.UserInGame.Text();
                return response;
            }
            #endregion

            #region kiểm tra role

            // Kiểm tra đủ tiền không
            var checkEnoughGoldResponse = CheckEnoughGold(userId, cardTypeId, cardAmount);
            //nạp kết quả trả về
            response.Result = checkEnoughGoldResponse.ExChangeCardStatus;
            response.Message = checkEnoughGoldResponse.Message;
            if (response.Result != ExChangeCardStatus.Success)
            {
                return response;
            }

            var exchangeConfig = new ExchangeConfig();
            //kiểm tra có phải là farmer không
            exchangeConfig = CheckFarmer(exchangeConfig, userId);
            //kiểm tra có vượt hạn mức tối đa và đã mệnh giá cardAmount trước đó chưa
            exchangeConfig = CheckOverAndExchangeBefore(exchangeConfig, userId, cardAmount, maxEchanged);
            //nạp kết quả
            response.Result = exchangeConfig.Result;
            response.Message = exchangeConfig.Message;

            #endregion

            #region Trừ gold trong ví

            decimal subtractInNormal = 0;
            decimal subtractInCoffer = 0;
            var isAbtractStarSuccess = SubtractGoldExchangeCard(userId, checkEnoughGoldResponse.EssentialGold,
                out subtractInNormal, out subtractInCoffer);

            int logId;

            string descriptionLogExchange;
            if (isAbtractStarSuccess)
            {
                var websocketResponse = GameServerHelper.Call(socket, WsPacket.UpdateGoldUser, new { UserId = userId });
                if (websocketResponse != SocketResponeMode.ThanhCong.Value())
                {
                    //response.Message = "Đổi thưởng thành công. Vui lòng xem tin nhắn.";
                }
            }
            else
            {
                response.Result = ExChangeCardStatus.LoiDoiGold;
                response.Message = ExChangeCardStatus.LoiDoiGold.Text();
                exchangeConfig.IsApproval = false;
                descriptionLogExchange = "Trừ gold User Thất bại";
                _exchangeUser.AddNew(userId, cardTypeId, ((CardTypeEnum)cardTypeId).Text(), cardAmount, ipAddress,
                    platfornId, hardwareId, ExChangeCardStatus.Failure.Value(), exchangeConfig.IsApproval, out logId,
                    exchangeConfig.RoundId, descriptionLogExchange, intChannelID);
                return response;
            }
            #endregion
            if (!isAutoExchange)
            {
                exchangeConfig.IsApproval = true;
                response.Result = ExChangeCardStatus.WaitApproval;
                response.Message = ExChangeCardStatus.WaitApproval.Text();
            }




            #region Ghi đổi thưởng vào LogExchange + chuyen cho admin aproval neu IsApproval = true
            //result vẫn để ở trạng thái chưa thành công. 
            descriptionLogExchange = string.IsNullOrEmpty(response.Message) ? "Đã trừ gold thành công. Bước tiếp sẽ tiến hành đổi thẻ với đối tác" : response.Message;
            _exchangeUser.AddNew(userId, cardTypeId, ((CardTypeEnum)cardTypeId).Text(), cardAmount, ipAddress,
                platfornId, hardwareId, ExChangeCardStatus.Failure.Value(), exchangeConfig.IsApproval, out logId,
                exchangeConfig.RoundId, descriptionLogExchange, intChannelID);

            var cardInfo = new UseCardModel();

            if (!exchangeConfig.IsApproval)
            {
                #region Đổi thẻ với đối tác


                response = DoiTheVoiDoiTac_V2(cardAmount, cardTypeId, serviceConfig, userId, logId,
                    out cardInfo);

                #endregion

                //Đang hardCode  partnerID.

                #region Update đổi thưởng

                const int partnerId = 0;
                var c = _exchangeUser.Update(response.Result.Value(), cardInfo.Serial, cardInfo.PinCard,
                    partnerId, cardInfo.PartnerTransId, cardInfo.ResponseText, cardInfo.HqCardResponseE != 0, logId,
                    checkEnoughGoldResponse.EssentialGold, intChannelID);

                #endregion

                #region Trừ gold trong ví V2 - Tạm thời không dùng
                //trừ xu, gọi đối tác gạch thẻ
                /*

                //Lấy Result và Gold sau khi đã quy đổi
                decimal subtractInNormal = 0;
                decimal subtractInCoffer = 0;
                var isAbtractStarSuccess = SubtractGoldExchangeCard(userId, checkEnoughGoldResponse.EssentialGold, out subtractInNormal, out subtractInCoffer);
                if (isAbtractStarSuccess)
                {
                    var websocketResponse = GameServerHelper.Call(socket, WsPacket.UpdateGoldUser, new { UserId = userId });
                    if (websocketResponse != SocketResponeMode.ThanhCong.Value())
                    {
                        response.Message = "Đổi thưởng thành công. Vui lòng xem tin nhắn.";
                    }
                    #region Đổi thẻ với đối tác


                    response = DoiTheVoiDoiTac_V2(cardAmount, cardTypeId, serviceConfig, userId,
                        out cardInfo);

                    #endregion

                    //Đang hardCode  partnerID.

                    #region Update đổi thưởng

                    const int partnerId = 0;
                    var c = _exchangeUser.Update(response.Result.Value(), cardInfo.Serial, cardInfo.PinCard,
                        partnerId, cardInfo.PartnerTransId, cardInfo.ResponseText, cardInfo.HqCardResponseE != 0, logId,
                        checkEnoughGoldResponse.EssentialGold);

                    #endregion

                    if (response.Result != ExChangeCardStatus.Success)
                    {
                        response.Result = ExChangeCardStatus.LoiDoiGold;
                        response.Message = ExChangeCardStatus.LoiDoiGold.Text();
                    }
                }
                else
                {
                    response.Result = ExChangeCardStatus.LoiDoiGold;
                    response.Message = ExChangeCardStatus.LoiDoiGold.Text();
                    return response;
                }

                */
                #endregion
            }
            #endregion

            // Gởi Tin nhắn cho User

            #region gởi tin nhắn cho user

            var sendParam = new InitialParamSendmessage();
            const int parentId = 0;
            if (string.IsNullOrEmpty(cardInfo.OfflineMessage))
            {
                if (response.Result == ExChangeCardStatus.WaitApproval)
                {
                    var messageContent = new StringBuilder();
                    messageContent.AppendFormat("Yêu cầu đổi thưởng thẻ {0} mệnh giá {1} đã được tiếp nhận.", cardTypeId.ToEnum<CardTypeEnum>().Text(),
                        cardAmount.ToCurrencyString(false, false));
                    //messageContent.Append("Yêu cầu đổi thưởng thẻ ");
                    //messageContent.Append(((CardTypeEnum)cardTypeId).Text());
                    //messageContent.Append(" mệnh giá ");
                    //messageContent.Append(cardAmount);
                    //messageContent.Append(" đã được tiếp nhận.");
                    cardInfo.OfflineMessage = messageContent.ToString();

                    Logger.CommonLogger.DefaultLogger.Debug(messageContent.ToString());
                }
                else
                {
                    cardInfo.OfflineMessage = response.Message;
                }
            }
            var value = _offlineMessage.SendMessage(defaultAdminId, userId, cardInfo.OfflineMessage, sendParam.MessageType, sendParam.Status, ipAddress, platfornId, hardwareId, sendParam.IsSysTem, parentId);
            #endregion

            // return 
            return response;
        }

        public ExchangeCardResponse ExchangeCard_V3(ExchangeCardRequest request)
        {
            var response = new ExchangeCardResponse
            {
                Result = ExChangeCardStatus.Failure,
                Message = ExChangeCardStatus.Failure.Text()
            };
            //kiểm tra đang chơi game
            #region Bonus: kiem tra da cap nhat sdt chua

            if (!_accountBusiness.IsUpdatePhoneUser(request.UserId))
            {
                response.Result = ExChangeCardStatus.Failure;
                response.Message = "Vui lòng cập nhật số điên thoại trong thông tin cá nhân để tiến hành đổi thưởng.";
                return response;
            }
            #endregion
            #region B1: kiểm tra đang chơi game
            var isPlayingGame = _accountBusiness.IsPlayingGame(request.UserId);
            if (isPlayingGame)
            {
                response.Result = ExChangeCardStatus.UserInGame;
                response.Message = ExChangeCardStatus.UserInGame.Text();
                return response;
            }
            #endregion

            // Kiểm tra đủ tiền không
            #region B2: check co du tien k
            var checkEnoughGoldResponse = CheckEnoughGold(request.UserId, request.CardType, request.CardAmount);
            //nạp kết quả trả về
            response.Result = checkEnoughGoldResponse.ExChangeCardStatus;
            response.Message = checkEnoughGoldResponse.Message;
            if (response.Result != ExChangeCardStatus.Success)
            {
                response.Result = checkEnoughGoldResponse.ExChangeCardStatus;
                response.Message = checkEnoughGoldResponse.ExChangeCardStatus.Text();
                return response;
            }
            #endregion

            #region B3: check han muc hang ngay cua user
            //decimal totalGoldExchanged;
            bool isExchanged = false;

            //var isOverRankExchange = GetMaxExchangeAmount(request.UserId, request.CardAmount, request.MaxEchanged,
            //    request.MinExchanged, ref isExchanged);
            var isOverRankExchange = GetMaxExchangeAmount_v2(request.UserId, request.CardAmount, request.RuleTimePlayGame, request.MaxEchanged,
               request.MinExchanged, ref isExchanged);
            if (isOverRankExchange)
            {
                response.Result = ExChangeCardStatus.OverExchanged;
                response.Message = ExChangeCardStatus.OverExchanged.Text();
                return response;
            }
            #endregion
            int logId;
            var isApproval = false;

            #region B4: check cho phep chay auto khong

            decimal subtractInNormal;
            decimal subtractInCoffer;
            bool isAbtractStarSuccess;
            if (request.IsAutoExchange)
            {
                #region B5: check co fai Farmer khong
                var isFarmer = _farmerRepository.CheckIsFarmer(request.UserId);
                //khong fai la farmer thi tiep tuc kiem tra va the da duoc duyet truoc do thi se cho fep tra the tu dong
                if (!isFarmer /* && isExchanged */)
                {
                    var cardInfo = new UseCardModel();
                    // tru tien user
                    #region Trừ gold trong ví

                    //subtractInNormal = 0;
                    //subtractInCoffer = 0;
                    isAbtractStarSuccess = SubtractGoldExchangeCard(request.UserId, checkEnoughGoldResponse.EssentialGold,
                        out subtractInNormal, out subtractInCoffer);

                    #endregion
                    if (isAbtractStarSuccess) // tru tien thanh cong
                    {
                        #region ghi log
                        _exchangeUser.AddNew(request.UserId, request.CardType, request.CardType.ToEnum<CardTypeEnum>().Text(), request.CardAmount, request.IpAddress,
                                  request.PlatformId, request.HardwareId, ExChangeCardStatus.TruGoldThanhCongChoDoiTheVoiDoiTac.Value(), isApproval, out logId,
                                  0, ExChangeCardStatus.TruGoldThanhCongChoDoiTheVoiDoiTac.Text(), request.ChannelID);
                        #endregion

                        // Gọi socket cập nhật gold
                        GameServerHelper.Call(request.Socket, WsPacket.UpdateGoldUser, new { UserId = request.UserId });

                        #region B6: goi doi tac tra the
                        response = DoiTheVoiDoiTac_V2(request.CardAmount, request.CardType, request.PaymentService, request.UserId, logId,
                       out cardInfo);
                        const int partnerId = 0;
                        var partnerMessage =
                            string.Format("{0}. {1}", response.Result.Text(), cardInfo.ResponseText);
                        // loi doi the doi tac thi ghi nhan log
                        if (response.Result != ExChangeCardStatus.Success)
                        {
                            _exchangeUser.Update(response.Result.Value(), cardInfo.Serial, cardInfo.PinCard,
                                partnerId, cardInfo.PartnerTransId, partnerMessage, cardInfo.HqCardResponseE != 0, logId,
                            checkEnoughGoldResponse.EssentialGold, request.ChannelID);

                            response.Serial = string.Empty;
                            response.PinCode = string.Empty;
                            response.Message = "Đổi thẻ thất bại. Vui lòng liên hệ HTTT để biết thêm chi tiết.";
                            response.Result = response.Result;
                        }
                        else // thành công
                        {
                            const int parentId = 0;

                            // cap nhat lai log doi the
                            _exchangeUser.Update(response.Result.Value(), cardInfo.Serial, cardInfo.PinCard,
                                partnerId, cardInfo.PartnerTransId, partnerMessage, cardInfo.HqCardResponseE != 0, logId,
                                checkEnoughGoldResponse.EssentialGold, request.ChannelID);
                            //gui thu cho user
                            var sendParam = new InitialParamSendmessage();
                            response.Message = cardInfo.OfflineMessage;
                            try
                            {
                                var value = _offlineMessage.SendMessage(request.DefaultAdminId, request.UserId, cardInfo.OfflineMessage,
                                    sendParam.MessageType, sendParam.Status, request.IpAddress, request.PlatformId, request.HardwareId,
                                    sendParam.IsSysTem, parentId);
                            }
                            catch (Exception ex)
                            {
                                Logger.CommonLogger.DefaultLogger.Error(
                                    "Lỗi gửi thư sau khi gọi đối tác và trừ gold thành công: " + ex);
                            }
                            response.Serial = cardInfo.Serial;
                            response.PinCode = cardInfo.PinCard;
                            response.Message = cardInfo.OfflineMessage;
                            response.Result = ExChangeCardStatus.Success;
                        }
                        #endregion
                    }
                    else // tru gold loi
                    {
                        response.Serial = string.Empty;
                        response.PinCode = string.Empty;
                        response.Message = ExChangeCardStatus.TruGoldThatBai.Text();
                        response.Result = ExChangeCardStatus.TruGoldThatBai;
                    }

                    return response;


                }
                #endregion
            }
            #endregion

            #region truong hop phai duyet doi thuong
            #region Trừ gold trong ví

            //subtractInNormal = 0;
            //subtractInCoffer = 0;
            isAbtractStarSuccess = SubtractGoldExchangeCard(request.UserId, checkEnoughGoldResponse.EssentialGold,
                out subtractInNormal, out subtractInCoffer);
            #endregion
            if (!isAbtractStarSuccess) // tru tien thanh cong
            {
                response.Message = "Đổi xu thất bại. Vui lòng thử lại sau hoặc liên hệ HTTT để biết thêm chi tiết.";
                response.Result = ExChangeCardStatus.TruGoldThatBai;
                return response;
            }
            else // tru gold thanh cong xu ly tiep cho duyet 
            {
                var messageContent = new StringBuilder();
                messageContent.AppendFormat("Yêu cầu đổi thưởng thẻ {0} mệnh giá {1} đã được tiếp nhận.", request.CardType.ToEnum<CardTypeEnum>().Text(),
                    request.CardAmount.ToCurrencyString(false, false));

                response.Message = messageContent.ToString();
                response.Result = ExChangeCardStatus.Success;

                #region ghi nhan duyet the
                try
                {
                    GameServerHelper.Call(request.Socket, WsPacket.UpdateGoldUser, new { UserId = request.UserId });
                }
                catch (Exception ex)
                {
                    Logger.CommonLogger.DefaultLogger.Error(
                                    "Lỗi cập nhật gold : " + ex);
                }
                isApproval = true; // insert sẵn admin 1 duyệt
                _exchangeUser.AddNew(request.UserId, request.CardType, request.CardType.ToEnum<CardTypeEnum>().Text(), request.CardAmount, request.IpAddress,
                  request.PlatformId, request.HardwareId, ExChangeCardStatus.Approvaling.Value(), isApproval, out logId,
                  0, "Chờ duyệt đổi thẻ.", request.ChannelID);

                #region gởi tin nhắn cho user

                var sendParam = new InitialParamSendmessage();
                const int parentId = 0;


                var value = _offlineMessage.SendMessage(request.DefaultAdminId, request.UserId,
                    messageContent.ToString(), sendParam.MessageType, sendParam.Status, request.IpAddress,
                    request.PlatformId, request.HardwareId, sendParam.IsSysTem, parentId);

                #endregion

                #endregion
            }


            #endregion
            return response;
        }

        /// <summary>
        /// Doi the V4
        /// Duynd - 20/06/2016
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExchangeCardResponse ExchangeCard_V4(ExchangeCardRequest request)
        {
            var response = new ExchangeCardResponse
            {
                Result = ExChangeCardStatus.Failure,
                Message = ExChangeCardStatus.Failure.Text()
            };

            //kiểm tra đang chơi game
            #region Bonus: kiem tra da cap nhat sdt chua

            if (!_accountBusiness.IsUpdatePhoneUser(request.UserId))
            {
                response.Result = ExChangeCardStatus.Failure;
                response.Message = "Vui lòng cập nhật số điên thoại trong thông tin cá nhân để tiến hành đổi thưởng.";
                return response;
            }
            #endregion

            #region B1: kiểm tra đang chơi game
            var isPlayingGame = _accountBusiness.IsPlayingGame(request.UserId);
            if (isPlayingGame)
            {
                response.Result = ExChangeCardStatus.UserInGame;
                response.Message = ExChangeCardStatus.UserInGame.Text();
                return response;
            }
            #endregion

            // Kiểm tra đủ tiền không
            #region B2: check co du tien k
            var checkEnoughGoldResponse = CheckEnoughGold(request.UserId, request.CardType, request.CardAmount);
            //nạp kết quả trả về
            response.Result = checkEnoughGoldResponse.ExChangeCardStatus;
            response.Message = checkEnoughGoldResponse.Message;
            if (response.Result != ExChangeCardStatus.Success)
            {
                response.Result = checkEnoughGoldResponse.ExChangeCardStatus;
                response.Message = checkEnoughGoldResponse.ExChangeCardStatus.Text();
                return response;
            }
            #endregion

            #region B3: check han muc hang ngay cua user
            //decimal totalGoldExchanged;
            bool isExchanged = false;

            //var isOverRankExchange = GetMaxExchangeAmount(request.UserId, request.CardAmount, request.MaxEchanged,
            //    request.MinExchanged, ref isExchanged);
            var isOverRankExchange = GetMaxExchangeAmount_v2(request.UserId, request.CardAmount, request.RuleTimePlayGame, request.MaxEchanged,
               request.MinExchanged, ref isExchanged);
            if (isOverRankExchange)
            {
                response.Result = ExChangeCardStatus.OverExchanged;
                response.Message = ExChangeCardStatus.OverExchanged.Text();
                return response;
            }
            #endregion

            int logId;
            var isApproval = false;

            #region B4: check cho phep chay auto khong

            decimal subtractInNormal;
            decimal subtractInCoffer;
            bool isAbtractStarSuccess;
            if (request.IsAutoExchange)
            {
                #region B5: check co fai Farmer khong
                var isFarmer = _farmerRepository.CheckIsFarmer(request.UserId);
                //khong fai la farmer thi tiep tuc kiem tra va the da duoc duyet truoc do thi se cho fep tra the tu dong
                if (!isFarmer /* && isExchanged */)
                {
                    var cardInfo = new UseCardModel();

                    // tru tien user
                    #region Trừ gold trong ví

                    //subtractInNormal = 0;
                    //subtractInCoffer = 0;
                    isAbtractStarSuccess = SubtractGoldExchangeCard(request.UserId, checkEnoughGoldResponse.EssentialGold,
                        out subtractInNormal, out subtractInCoffer);

                    #endregion

                    if (isAbtractStarSuccess) // tru tien thanh cong
                    {
                        #region ghi log
                        _exchangeUser.AddNew(request.UserId, request.CardType, request.CardType.ToEnum<CardTypeEnum>().Text(), request.CardAmount, request.IpAddress,
                                  request.PlatformId, request.HardwareId, ExChangeCardStatus.TruGoldThanhCongChoDoiTheVoiDoiTac.Value(), isApproval, out logId,
                                  0, ExChangeCardStatus.TruGoldThanhCongChoDoiTheVoiDoiTac.Text(), request.ChannelID);
                        #endregion

                        // Gọi socket cập nhật gold
                        GameServerHelper.Call(request.Socket, WsPacket.UpdateGoldUser, new { UserId = request.UserId });

                        #region B6: goi doi tac tra the
                        //response = DoiTheVoiDoiTac_V2(request.CardAmount, request.CardType, request.PaymentService, request.UserId, logId, out cardInfo);

                        // Doi the vs doi tac V4 - Duynd - 20/06/2016
                        response = DoiTheVoiDoiTac_V4(request.CardAmount, request.CardType, request.PaymentService, request.UserId, logId, out cardInfo);

                        const int partnerId = 0;
                        var partnerMessage = string.Format("{0}. {1}", response.PartnerMessage, cardInfo.ResponseText);

                        if (response.Result == ExChangeCardStatus.Success) // thành công
                        {
                            const int parentId = 0;

                            // cap nhat lai log doi the
                            _exchangeUser.Update(response.Result.Value(), cardInfo.Serial, cardInfo.PinCard,
                                partnerId, cardInfo.PartnerTransId, partnerMessage, cardInfo.HqCardResponseE != 0, logId,
                                checkEnoughGoldResponse.EssentialGold, request.ChannelID);

                            //gui thu cho user
                            var sendParam = new InitialParamSendmessage();
                            response.Message = cardInfo.OfflineMessage;
                            try
                            {
                                var value = _offlineMessage.SendMessage(request.DefaultAdminId, request.UserId, cardInfo.OfflineMessage,
                                    sendParam.MessageType, sendParam.Status, request.IpAddress, request.PlatformId, request.HardwareId,
                                    sendParam.IsSysTem, parentId);
                            }
                            catch (Exception ex)
                            {
                                CommonLogger.DefaultLogger.Error(
                                    "Lỗi gửi thư sau khi gọi đối tác và trừ gold thành công: " + ex);
                            }

                            response.Serial = cardInfo.Serial;
                            response.PinCode = cardInfo.PinCard;
                            response.Message = cardInfo.OfflineMessage;
                            response.Result = ExChangeCardStatus.Success;
                        }
                        else // loi doi the doi tac thi ghi nhan log
                        {
                            string strRefun = string.Empty;
                            if (response.Result != ExChangeCardStatus.LoiDoiTheTuDoiTac) //loi list danh sách từ đối tác, loại trừ lỗi timeout
                            {
                                decimal decGoldTrans = checkEnoughGoldResponse.EssentialGold;

                                // Hoan gold lai cho user
                                if (_walletRepo.Wallet_AddOrSubtractGoldUser(request.UserId, decGoldTrans,
                                    ReasonEnum.HoanGoldDoiTheKhongThanhCong.Value(),
                                    "Hoàn gold khi đổi thẻ không thành công", 0, true,
                                    LoginTypeEnum.Mobile.Value(), request.PlatformId) > 0)
                                    strRefun = string.Format(". Hoàn {0} xu thành công", decGoldTrans.ToCurrencyString(false, false));
                                else
                                    strRefun = string.Format(". Hoàn {0} xu thất bại", decGoldTrans.ToCurrencyString(false, false));

                                // cap nhat gold cho user tren server game - Duynd - 05/05/2016
                                Task task = _accountBusiness.UpdateGoldUser(request.UserId, UpdateGoldServerGameEnum.Update.Value(), decGoldTrans);
                                //#end
                            }

                            _exchangeUser.Update(response.Result.Value(), cardInfo.Serial, cardInfo.PinCard,
                                partnerId, cardInfo.PartnerTransId, partnerMessage + strRefun, cardInfo.HqCardResponseE != 0, logId,
                            checkEnoughGoldResponse.EssentialGold, request.ChannelID);

                            response.Serial = string.Empty;
                            response.PinCode = string.Empty;
                            response.Message = "Đổi thẻ thất bại. Vui lòng liên hệ HTTT để biết thêm chi tiết.";
                            response.Result = response.Result;
                        }
                        #endregion
                    }
                    else // tru gold loi
                    {
                        response.Serial = string.Empty;
                        response.PinCode = string.Empty;
                        response.Message = ExChangeCardStatus.TruGoldThatBai.Text();
                        response.Result = ExChangeCardStatus.TruGoldThatBai;
                    }

                    return response;


                }
                #endregion
            }
            #endregion

            #region truong hop phai duyet doi thuong
            #region Trừ gold trong ví

            //subtractInNormal = 0;
            //subtractInCoffer = 0;
            isAbtractStarSuccess = SubtractGoldExchangeCard(request.UserId, checkEnoughGoldResponse.EssentialGold,
                out subtractInNormal, out subtractInCoffer);
            #endregion
            if (!isAbtractStarSuccess) // tru tien thanh cong
            {
                response.Message = "Đổi xu thất bại. Vui lòng thử lại sau hoặc liên hệ HTTT để biết thêm chi tiết.";
                response.Result = ExChangeCardStatus.TruGoldThatBai;
                return response;
            }
            else // tru gold thanh cong xu ly tiep cho duyet 
            {
                var messageContent = new StringBuilder();
                messageContent.AppendFormat("Yêu cầu đổi thưởng thẻ {0} mệnh giá {1} đã được tiếp nhận.", request.CardType.ToEnum<CardTypeEnum>().Text(),
                    request.CardAmount.ToCurrencyString(false, false));

                response.Message = messageContent.ToString();
                response.Result = ExChangeCardStatus.Success;

                #region ghi nhan duyet the
                try
                {
                    GameServerHelper.Call(request.Socket, WsPacket.UpdateGoldUser, new { UserId = request.UserId });
                }
                catch (Exception ex)
                {
                    Logger.CommonLogger.DefaultLogger.Error(
                                    "Lỗi cập nhật gold : " + ex);
                }
                isApproval = true; // insert sẵn admin 1 duyệt
                _exchangeUser.AddNew(request.UserId, request.CardType, request.CardType.ToEnum<CardTypeEnum>().Text(), request.CardAmount, request.IpAddress,
                  request.PlatformId, request.HardwareId, ExChangeCardStatus.Approvaling.Value(), isApproval, out logId,
                  0, "Chờ duyệt đổi thẻ.", request.ChannelID);

                #region gởi tin nhắn cho user

                var sendParam = new InitialParamSendmessage();
                const int parentId = 0;


                var value = _offlineMessage.SendMessage(request.DefaultAdminId, request.UserId,
                    messageContent.ToString(), sendParam.MessageType, sendParam.Status, request.IpAddress,
                    request.PlatformId, request.HardwareId, sendParam.IsSysTem, parentId);

                #endregion

                #endregion
            }


            #endregion

            return response;
        }

        /// <summary>
        /// xác nhận user đã vượt hạn mức đổi thẻ 1 ngày chưa.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardAmount"></param>
        /// <param name="maxEchanged"></param>
        /// <param name="minExchange"></param>
        /// <param name="isExchanged"></param>
        /// <returns></returns>
        private bool GetMaxExchangeAmount(int userId, int cardAmount, decimal maxEchanged, decimal minExchange, ref bool isExchanged)
        {
            var resultReturn = false;
            decimal totalGoldExchanged = 0;
            decimal rankExhange;
            decimal sumPaymentCard = 0;
            var objExchangeRank = new Out_LogExchange_GetTotalGoldAndIsExchanged_v2_Result();
            // lấy hạn mức đã đổi dưới db
            try
            {
                objExchangeRank = GetTotalGoldAndIsExchanged_V2(userId, cardAmount);
                if (objExchangeRank != null)
                {
                    sumPaymentCard = objExchangeRank.totalPaymentCard.GetValueOrDefault(0);
                    totalGoldExchanged = objExchangeRank.TotalAmountExchange.GetValueOrDefault(0);
                    isExchanged = objExchangeRank.IsExchangedThisCardAmount.GetValueOrDefault(false);
                }
            }
            catch (Exception ex)
            {
                //neu loi se cho la vuot han muc va chua doi the truoc do.
                isExchanged = false;
                resultReturn = true;
                Logger.CommonLogger.PaymentLogger.Error("Lỗi lấy hạn mức từ db: " + ex);
            }
            /* tinh gioi han theo user co nap card 
            if (sumPaymentCard <= 0) // nếu user chưa nạp thẻ thì hạn mức sẽ là hạn mức tổi thiểu
            {
                rankExhange = minExchange;
            }else if (sumPaymentCard >= maxEchanged) // nếu tổng tiền nạp của user lớn hơn hạn mức tối đa thì lấy hạn mức tối đa
            {
                rankExhange = maxEchanged;
            }
            else // hạn mức sẽ lấy tổng tiền nạp thẻ của user trong khoảng min và max hạn mức
            {
                rankExhange = sumPaymentCard;
            }
            Logger.CommonLogger.MobileLogger.Error(string.Format("han muc the: totalGoldExchanged: {0} | cardAmount: {1} | = {2}", totalGoldExchanged, cardAmount, (totalGoldExchanged + cardAmount)));
             * */
            rankExhange = maxEchanged;
            if ((totalGoldExchanged + cardAmount) > rankExhange)
            {
                resultReturn = true;
            }
            return resultReturn;
        }

        /// <summary>
        /// xác nhận user đã vượt hạn mức đổi thẻ 1 ngày chưa.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardAmount"></param>
        /// <param name="maxEchanged"></param>
        /// <param name="minExchange"></param>
        /// <param name="isExchanged"></param>
        /// <returns></returns>
        private bool GetMaxExchangeAmount_v2(int userId, int cardAmount, int ruleTimePlay, decimal maxEchanged, decimal minExchange, ref bool isExchanged)
        {
            var resultReturn = false;
            decimal totalGoldExchanged = 0;
            decimal rankExhange;
            //decimal sumPaymentCard = 0;
            var objExchangeRank = new Out_LogExchange_GetTotalGoldAndIsExchanged_v2_Result();
            // lấy hạn mức đã đổi dưới db
            try
            {
                objExchangeRank = GetTotalGoldAndIsExchanged_V2(userId, cardAmount);
                if (objExchangeRank != null)
                {
                    //sumPaymentCard = objExchangeRank.totalPaymentCard.GetValueOrDefault(0);
                    totalGoldExchanged = objExchangeRank.TotalAmountExchange.GetValueOrDefault(0);
                    isExchanged = objExchangeRank.IsExchangedThisCardAmount.GetValueOrDefault(false);
                }
            }
            catch (Exception ex)
            {
                //neu loi se cho la vuot han muc va chua doi the truoc do.
                isExchanged = false;
                resultReturn = true;
                Logger.CommonLogger.PaymentLogger.Error("Lỗi lấy hạn mức từ db: " + ex);
            }
            #region kiem tra thoi gian choi cua user
            //user online trên 2 -3 tiếng thì 3tr/ day
            var timePlayGameOnday = 0;
            try
            {
                timePlayGameOnday = _accountBusiness.ATPPlayerRecord_GetTimePlayToday(userId);
            }
            catch (Exception ex)
            {
                Logger.CommonLogger.PaymentLogger.Error("Lỗi lấy thoi gian choi game: " + ex);
            }

            #endregion
            rankExhange = minExchange;
            if (timePlayGameOnday >= ruleTimePlay)
            {
                rankExhange = maxEchanged;
            }
            Logger.CommonLogger.MobileLogger.Error(string.Format("han muc the: totalGoldExchanged: {0} | Time choi game: {1} | = Rule Time: {2} | totalGoldExchanged: {3} | cardAmount: {4} | rankExhange: {5}", totalGoldExchanged, timePlayGameOnday, ruleTimePlay, totalGoldExchanged, cardAmount, rankExhange));


            if ((totalGoldExchanged + cardAmount) > rankExhange)
            {
                resultReturn = true;
            }
            return resultReturn;
        }

        public Out_LogExchange_GetTotalGoldAndIsExchanged_v2_Result GetTotalGoldAndIsExchanged_V2(int userId, decimal cardAmount)
        {
            return _exchangeUser.GetTotalGoldAndIsExchanged_V2(userId, cardAmount);
        }
        #endregion

        #region lịch sử đổi thưởng

        ////Lấy Top nộp card
        ///// <summary>
        ///// .../12/2015 MinhT : Create New
        ///// Lấy lịch sử đổi thưởng
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="pageNumber"></param>
        ///// <param name="pageSize"></param>
        ///// <returns></returns>
        //public ExchargeCardHistoryResponse GetExchangeCardHistory(int userId, int pageNumber, int pageSize)
        //{
        //    if (pageSize <= 0)
        //    {
        //        pageSize = 10;
        //    }
        //    var rowStart = (pageNumber - 1) * pageSize;
        //    var rowEnd = rowStart + pageSize - 1;
        //    int totalrow;

        //    var result = _exchangeUser.GetHistory(rowStart, rowEnd, out totalrow).OrderByDescending(x => x.TransDate);
        //    var response = result.Select(x => new ExchargeCardHistoryModel
        //    {
        //        LogId = x.LogId,
        //        UserName = x.DisplayName,
        //        RowNumber = Int32.Parse(x.RowNumber.GetValueOrDefault().ToString()),
        //        CardType = x.CardType.GetValueOrDefault(),
        //        CardName = x.CardType.GetValueOrDefault().ToEnum<CardTypeEnum>().Text(),
        //        CardAmount = x.CardAmount.GetValueOrDefault(),
        //        DateCreated = x.TransDate.GetValueOrDefault().GetVnDateTimeFormat()
        //    });
        //    return new ExchargeCardHistoryResponse
        //    {
        //        TotalRows = totalrow,
        //        ListData = response.ToList()
        //    };
        //}

        /// <summary>
        ///  Lấy lịch sử đổi thưởng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        /// <history>
        /// 10/6/2016 Create by TaiNM
        /// </history>
        public ExchargeCardHistoryResponse GetExchangeCardHistory(int userId, int pageNumber, int pageSize, DateTime dtmClearCacheRedis)
        {
            if (pageSize <= 0)
                pageSize = 10;

            var rowStart = (pageNumber - 1) * pageSize;
            var rowEnd = rowStart + pageSize - 1;
            var totalrow = 0;

            var strKey = RedisKeyConstants.GetExchangeCardHistory + ":" + pageNumber + ":" + userId;
            var jsonData = _commonRedis.GetData(strKey);
            List<ExchargeCardHistoryModel> dataResults;
            if (!string.IsNullOrEmpty(jsonData)) /*Get data*/
            {
                dataResults = JsonConvert.DeserializeObject<List<ExchargeCardHistoryModel>>(jsonData);
                if (dataResults.Any())
                    dataResults = dataResults.OrderBy(t => t.RowNumber).Skip(rowStart).Take(rowEnd).ToList();
            }
            else /*Set data*/
            {
                var result = _exchangeUser.GetHistory(rowStart, rowEnd, out totalrow).OrderByDescending(x => x.TransDate);
                dataResults = result.Select(x => new ExchargeCardHistoryModel
            {
                LogId = x.LogId,
                UserName = x.DisplayName,
                RowNumber = Int32.Parse(x.RowNumber.GetValueOrDefault().ToString()),
                CardType = x.CardType.GetValueOrDefault(),
                CardName = x.CardType.GetValueOrDefault().ToEnum<CardTypeEnum>().Text(),
                CardAmount = x.CardAmount.GetValueOrDefault(),
                DateCreated = x.TransDate.GetValueOrDefault().GetVnDateTimeFormat()
            }).ToList();

                _commonRedis.SetData(strKey, dtmClearCacheRedis, JsonConvert.SerializeObject(dataResults));
            }

            return new ExchargeCardHistoryResponse
            {
                TotalRows = totalrow,
                ListData = dataResults
            };
        }

        #endregion

        #region hủy đổi thưởng
        /// <summary>
        /// ../12/2015 MinhT : Create New
        /// Hủy đổi thưởng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        public ExchangeCardCancel CancelExchangeCard(int userId, int logId)
        {
            int temResult;
            decimal temWalletNow;
            _exchangeUser.ExchangeCardCancel(userId, logId, out temResult, out temWalletNow);

            return new ExchangeCardCancel
            {
                Result = temResult,
                WalletAmount = temWalletNow
            };
        }
        #endregion

        #region Tốp đổi thưởng
        ///// <summary>
        ///// ../1/2016 MinhT: Create New
        ///// Lấy top đổi thưởng
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <returns></returns>
        //public List<Out_RankingDetail_GetTopExChangeCard_Result> GetTopExChangeCard(int start, int end)
        //{
        //    var result = _rankingDetailRepo.GetTopExChangeCard(start, end).OrderBy(x => x.OrderNo).ToList();
        //    return result;
        //}

        /// <summary>
        ///     Lấy top đổi thưởng
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        /// <history>
        /// 10/6/2016 Create by TaiNM
        /// </history>
        public List<TopChargeCardModel> GetTopExChangeCard(int pageSize, int start, int end, DateTime dtmClearCacheRedis)
        {
            #region old

            //var result = _rankingDetailRepo.GetTopExChangeCard(start, end).OrderBy(x => x.OrderNo);
            //if (result.Any())
            //    return result.Select(t => new TopChargeCardModel
            //    {
            //        Username = t.Username,
            //        RowNumber = t.OrderNo.HasValue ? t.OrderNo.Value : 0,
            //        TotalAmount = t.TotalAmount.HasValue ? (int)t.TotalAmount.Value : 0
            //    }).ToList();

            //return new List<TopChargeCardModel>();

            #endregion

            var strKey = RedisKeyConstants.GetTopExChangeCard + ":" + pageSize;
            var jsonData = _commonRedis.GetData(strKey);
            var dataResults = new List<TopChargeCardModel>();
            if (!string.IsNullOrEmpty(jsonData))/*Get data*/
                dataResults = JsonConvert.DeserializeObject<List<TopChargeCardModel>>(jsonData);
            else/*Set data*/
            {
                var result = _rankingDetailRepo.GetTopExChangeCard(0, pageSize).OrderBy(x => x.OrderNo).ToList(); ;
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
                return dataResults.OrderBy(t => t.RowNumber).Skip(start).Take(end).ToList();

            return new List<TopChargeCardModel>();
        }

        #endregion

        #region private
        /// <summary>
        /// 14/1/2016 MinhT: Create New
        /// Thực hiện đổi thẻ với đối tác
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cardTypeId"></param>
        /// <param name="serviceConfig"></param>
        /// <param name="myResult"></param>
        /// <param name="userId"></param>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        private ExchangeCardResponse DoiTheVoiDoiTac(int amount, int cardTypeId, PaymentServiceConfig serviceConfig, Out_Wallet_ExchangeCoin_ToCard_Result myResult, int userId, out UseCardModel cardInfo)
        {
            HqCardResponse hqCardResponse;
            cardInfo = new UseCardModel();

            var result = new ExchangeCardResponse();
            if (serviceConfig.IsTest)
            {
                hqCardResponse = new HqCardResponse
                {
                    e = 0,
                    Card = new HqCardItem
                    {
                        OrderID = Guid.NewGuid().ToString(),
                        DateExpired = DateTime.Now.AddDays(100).ToString(),
                        PinCode = "TestPinCode",
                        Serial = "TestSerial"
                    }
                };
            }
            else
            {
                var hqRequest = new HqCardRequest
                {
                    //OrderId = logExchangeFirst.TransId,
                    Price = amount,
                    Telco = ((CardTypeEnum)cardTypeId).Text(),
                };
                hqCardResponse = HqCardStockFactory.CardStock.CheckoutCard(hqRequest);
            }

            cardInfo.HqCardResponseE = hqCardResponse.e;

            switch (hqCardResponse.e)
            {
                case 0:
                    /*Success*/
                    if (hqCardResponse.Card != null)
                    {
                        result.Result = ExChangeCardStatus.Success;
                        result.Message = "Đổi thẻ thành công. " +
                                         "\nSerial: " + hqCardResponse.Card.Serial +
                                         "\nPincode: " + hqCardResponse.Card.PinCode +
                                         ".";
                        result.PinCode = hqCardResponse.Card.PinCode;
                        result.Serial = hqCardResponse.Card.Serial;

                        var offlineMessage = new StringBuilder();
                        offlineMessage.Append("Yêu cầu đổi thưởng thẻ ");
                        offlineMessage.Append(((CardTypeEnum)cardTypeId).Text());
                        offlineMessage.Append(" mệnh giá ");
                        offlineMessage.Append(StringCommon.FormatCurrency(amount));
                        offlineMessage.Append(" vào lúc ");
                        offlineMessage.Append(DateTime.Now.GetVnDateTimeFormat());
                        offlineMessage.Append(" thành công.");
                        offlineMessage.Append(" Thông tin thẻ cào của bạn: ");
                        offlineMessage.Append(" Serial:  ");
                        offlineMessage.Append(hqCardResponse.Card.Serial);
                        offlineMessage.Append(" Pincode:  ");
                        offlineMessage.Append(hqCardResponse.Card.PinCode);
                        offlineMessage.Append(" .");

                        cardInfo = new UseCardModel
                        {
                            Amount = amount,
                            //IsUsed = false,
                            PinCard = hqCardResponse.Card.PinCode,
                            Serial = hqCardResponse.Card.Serial,
                            PartnerTransId = hqCardResponse.Card.OrderID,
                            OfflineMessage = offlineMessage.ToString(),
                            ResponseText = hqCardResponse.ResponseText,
                            CreateDate = DateTime.Now
                        };

                        if (cardInfo.ResponseText == null)
                        {
                            cardInfo.ResponseText = "Test";
                        }
                    }
                    else
                    {
                        result.Result = ExChangeCardStatus.LoiDoiGold;
                        return result;
                    }
                    break;
                case 33:
                    result.Result = ExChangeCardStatus.Failure;
                    result.Message = "Lỗi đổi thẻ với đối tác: Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                                                           " - Amount: " + amount +
                                                           " - ValueExchange: " + myResult.Gold +
                                                           " - curCoinUser: " + myResult.curCoin +
                                                           " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse);
                    Logger.CommonLogger.DefaultLogger.Debug("Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                                                           " - Amount: " + amount +
                                                           " - ValueExchange: " + myResult.Gold +
                                                           " - curCoinUser: " + myResult.curCoin +
                                                           " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));
                    cardInfo = new UseCardModel();
                    return result;
                case 34:
                    result.Result = ExChangeCardStatus.CardOver;
                    result.Message = "Lỗi đổi thẻ với đối tác: Thẻ đã hết trong kho | UserId: " + userId +
                                     " - Amount: " + amount +
                                     " - ValueExchange: " + myResult.Gold +
                                     " - curCoinUser: " + myResult.curCoin +
                                     " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse);
                    Logger.CommonLogger.DefaultLogger.Debug("Thẻ đã hết trong kho | UserId: " + userId +
                                                           " - Amount: " + amount +
                                                           " - ValueExchange: " + myResult.Gold +
                                                           " - curCoinUser: " + myResult.curCoin +
                                                           " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));
                    cardInfo = new UseCardModel();
                    return result;
                default:
                    result.Result = ExChangeCardStatus.Failure;
                    result.Message = "Lỗi đổi thẻ với đối tác: Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                                     " - Amount: " + amount +
                                     " - ValueExchange: " + myResult.Gold +
                                     " - curCoinUser: " + myResult.curCoin +
                                     " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse);
                    Logger.CommonLogger.DefaultLogger.Debug("Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                                                           " - Amount: " + amount +
                                                           " - ValueExchange: " + myResult.Gold +
                                                           " - curCoinUser: " + myResult.curCoin +
                                                           " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));
                    cardInfo = new UseCardModel();
                    return result;
            }
            result.Result = ExChangeCardStatus.Success;
            return result;
        }

        private ExchangeCardResponse DoiTheVoiDoiTac_V2(int amount, int cardTypeId, PaymentServiceConfig serviceConfig, int userId, int logId, out UseCardModel cardInfo)
        {
            CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V2 - userId: " + userId);
            if (userId == 15454 || userId == 10010)
            {
                CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V2 - cardTypeId: " + cardTypeId);
                CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V2 - serviceConfig: " + JsonConvert.SerializeObject(serviceConfig));
                CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V2 - amount: " + amount);
            }

            HqCardResponse hqCardResponse = new HqCardResponse()
            {
                e = -1
            };
            cardInfo = new UseCardModel();

            var result = new ExchangeCardResponse();
            if (serviceConfig.IsTest)
            {
                hqCardResponse = new HqCardResponse
                {
                    e = 0,
                    Card = new HqCardItem
                    {
                        OrderID = logId.ToString(),
                        DateExpired = DateTime.Now.AddDays(100).ToString(),
                        PinCode = "TestPinCode",
                        Serial = "TestSerial"
                    }
                };
            }
            else
            {
                if (userId == 15454 || userId == 10010)
                {
                    CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V2 - cardTypeId: " + cardTypeId);
                }

                // PhatVT - 01/04/2016: Đổi cái OrderID
                var hqRequest = new HqCardRequest
                {
                    OrderId = "bc" + logId,
                    Price = amount,
                    Telco = cardTypeId.ToEnum<HqCardTypeEnum>().Text()
                };
                //CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V2 - hqRequest: " + JsonConvert.SerializeObject(hqRequest) + " | logId: " + logId);
                try
                {
                    hqCardResponse = HqCardStockFactory.CardStock.CheckoutCard(hqRequest);
                }
                catch (Exception ex)
                {
                    CommonLogger.PaymentLogger.Error("DoiTheVoiDoiTac_V2 - hqCardResponse: " + ex);
                    hqCardResponse = new HqCardResponse
                    {
                        e = -1
                    };
                }

            }
            if (hqCardResponse != null)
            {
                if (userId == 15454 || userId == 10010)
                {
                    CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V2 - hqCardResponse: " + JsonConvert.SerializeObject(hqCardResponse));
                }


                switch (hqCardResponse.e)
                {
                    case 0:
                        /*Success*/
                        if (hqCardResponse.Card != null)
                        {
                            result.Result = ExChangeCardStatus.Success;

                            result.PinCode = hqCardResponse.Card.PinCode;
                            result.Serial = hqCardResponse.Card.Serial;
                            cardInfo.HqCardResponseE = hqCardResponse.e;
                            cardInfo = new UseCardModel
                            {
                                Amount = amount,
                                //IsUsed = false,
                                PinCard = hqCardResponse.Card.PinCode,
                                Serial = hqCardResponse.Card.Serial,
                                PartnerTransId = hqCardResponse.Card.OrderID,
                                ResponseText = hqCardResponse.ResponseText + JsonConvert.SerializeObject(hqCardResponse),
                                CreateDate = DateTime.Now
                                ,
                                OfflineMessage = "Đổi thẻ thành công. " +
                                             "\nMệnh giá đổi: " + StringCommon.FormatCurrency(amount) +
                                    // " - Số gold hiện tại: " + myResult.curCoin +
                                             "\nSerial: " + hqCardResponse.Card.Serial +
                                             "\nPincode: " + hqCardResponse.Card.PinCode +
                                                     "\nLoại thẻ: " + ((CardTypeEnum)cardTypeId).Text()
                            };
                        }
                        else
                        {
                            result.Result = ExChangeCardStatus.LoiDoiGold;
                            result.Message = ExChangeCardStatus.LoiDoiGold.Text();
                            Logger.CommonLogger.DefaultLogger.Debug("Lỗi đổi thẻ với đối tác: | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));
                            return result;
                        }
                        break;
                    case 33:
                        result.Result = ExChangeCardStatus.TaiKhoanTheDaSuDungHetHanMuc;
                        Logger.CommonLogger.DefaultLogger.Debug("Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));
                        return result;
                    case 34:
                        result.Result = ExChangeCardStatus.CardOver;
                        result.Message = ExChangeCardStatus.CardOver.Text();
                        Logger.CommonLogger.DefaultLogger.Debug("Thẻ đã hết trong kho | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));
                        return result;
                    case -1:
                        result.Result = ExChangeCardStatus.LoiDoiTheTuDoiTac;
                        result.Message = ExChangeCardStatus.LoiDoiTheTuDoiTac.Text();
                        Logger.CommonLogger.DefaultLogger.Debug("loi doi tac | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));
                        return result;
                    default:
                        result.Result = ExChangeCardStatus.Failure;
                        result.Message = ExChangeCardStatus.Failure.Text();
                        //"Lỗi đổi thẻ với đối tác: Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                        //                 " - Amount: " + amount +
                        //                 " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse);
                        Logger.CommonLogger.DefaultLogger.Debug("Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));
                        return result;
                }
            }
            else
            {
                result.Result = ExChangeCardStatus.LoiKiemTraKhoTheTraVeNull;
                result.Message = ExChangeCardStatus.LoiKiemTraKhoTheTraVeNull.Text();
            }

            return result;

        }

        /// <summary>
        /// Doi the doi tac v3 - Duynd - 20/06/2016
        /// - Truong hop thanh cong va timeout van giu nguyen: Tru gold user
        /// - Nhung truong hop con lai -> hoan gold user
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cardTypeId"></param>
        /// <param name="serviceConfig"></param>
        /// <param name="userId"></param>
        /// <param name="logId"></param>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        private ExchangeCardResponse DoiTheVoiDoiTac_V4(int amount, int cardTypeId, PaymentServiceConfig serviceConfig, int userId, int logId, out UseCardModel cardInfo)
        {
            CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V4 - userId: " + userId);
            if (userId == 15454 || userId == 10010)
            {
                CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V4 - cardTypeId: " + cardTypeId);
                CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V4 - serviceConfig: " + JsonConvert.SerializeObject(serviceConfig));
                CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V4 - amount: " + amount);
            }

            HqCardResponse hqCardResponse = new HqCardResponse()
            {
                e = -1
            };
            cardInfo = new UseCardModel();

            string strApiException = string.Empty;
            var result = new ExchangeCardResponse();
            if (serviceConfig.IsTest)
            {
                hqCardResponse = new HqCardResponse
                {
                    e = 0,
                    Card = new HqCardItem
                    {
                        OrderID = logId.ToString(),
                        DateExpired = DateTime.Now.AddDays(100).ToString(),
                        PinCode = "TestPinCode",
                        Serial = "TestSerial"
                    }
                };
            }
            else
            {
                if (userId == 15454 || userId == 10010)
                {
                    CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V4 - cardTypeId: " + cardTypeId);
                }

                // PhatVT - 01/04/2016: Đổi cái OrderID
                var hqRequest = new HqCardRequest
                {
                    OrderId = "bc" + logId,
                    Price = amount,
                    Telco = cardTypeId.ToEnum<HqCardTypeEnum>().Text()
                };

                //CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V2 - hqRequest: " + JsonConvert.SerializeObject(hqRequest) + " | logId: " + logId);

                try
                {
                    hqCardResponse = HqCardStockFactory.CardStock.CheckoutCard(hqRequest);
                }
                catch (Exception ex)
                {
                    strApiException = ex.ToString();
                    CommonLogger.PaymentLogger.Error("DoiTheVoiDoiTac_V4 - hqCardResponse: " + ex);
                    hqCardResponse = new HqCardResponse
                    {
                        e = -1
                    };
                }
            }

            if (hqCardResponse != null)
            {
                if (userId == 15454 || userId == 10010)
                {
                    CommonLogger.PaymentLogger.Debug("DoiTheVoiDoiTac_V4 - hqCardResponse: " + JsonConvert.SerializeObject(hqCardResponse));
                }

                switch (hqCardResponse.e)
                {
                    case (int)ExchangeCardPartnerResult.Success:
                        if (hqCardResponse.Card != null)
                        {
                            result.Result = ExChangeCardStatus.Success;
                            result.PinCode = hqCardResponse.Card.PinCode;
                            result.Serial = hqCardResponse.Card.Serial;
                            cardInfo.HqCardResponseE = hqCardResponse.e;
                            cardInfo = new UseCardModel
                            {
                                Amount = amount,
                                //IsUsed = false,
                                PinCard = hqCardResponse.Card.PinCode,
                                Serial = hqCardResponse.Card.Serial,
                                PartnerTransId = hqCardResponse.Card.OrderID,
                                ResponseText = hqCardResponse.ResponseText + JsonConvert.SerializeObject(hqCardResponse),
                                CreateDate = DateTime.Now
                                ,
                                OfflineMessage = "Đổi thẻ thành công. " +
                                             "\nMệnh giá đổi: " + StringCommon.FormatCurrency(amount) +
                                    // " - Số gold hiện tại: " + myResult.curCoin +
                                             "\nSerial: " + hqCardResponse.Card.Serial +
                                             "\nPincode: " + hqCardResponse.Card.PinCode +
                                                     "\nLoại thẻ: " + ((CardTypeEnum)cardTypeId).Text()
                            };

                            result.PartnerResult = ExchangeCardPartnerResult.Success;
                            result.PartnerMessage = ExchangeCardPartnerResult.Success.Text();
                        }
                        else
                        {
                            result.Result = ExChangeCardStatus.LoiDoiGold;
                            result.Message = ExChangeCardStatus.LoiDoiGold.Text();
                            CommonLogger.DefaultLogger.Debug("Lỗi đổi thẻ với đối tác: | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));

                            result.PartnerResult = ExchangeCardPartnerResult.Failure;
                            result.PartnerMessage = ExchangeCardPartnerResult.Failure.Text();

                            return result;
                        }
                        break;
                    case (int)ExchangeCardPartnerResult.AppDaHetNganSachKhoThe:
                        result.Result = ExChangeCardStatus.TaiKhoanTheDaSuDungHetHanMuc;
                        CommonLogger.DefaultLogger.Debug("Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));

                        result.PartnerResult = ExchangeCardPartnerResult.AppDaHetNganSachKhoThe;
                        result.PartnerMessage = ExchangeCardPartnerResult.AppDaHetNganSachKhoThe.Text();

                        return result;
                    case (int)ExchangeCardPartnerResult.KhoDangHetThe:
                        result.Result = ExChangeCardStatus.CardOver;
                        result.Message = ExChangeCardStatus.CardOver.Text();
                        CommonLogger.DefaultLogger.Debug("Thẻ đã hết trong kho | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));

                        result.PartnerResult = ExchangeCardPartnerResult.KhoDangHetThe;
                        result.PartnerMessage = ExchangeCardPartnerResult.KhoDangHetThe.Text();

                        return result;
                    case (int)ExchangeCardPartnerResult.LoiDoiTheTuDoiTac:
                        result.Result = ExChangeCardStatus.LoiDoiTheTuDoiTac;
                        result.Message = ExChangeCardStatus.LoiDoiTheTuDoiTac.Text();
                        CommonLogger.DefaultLogger.Debug("loi doi tac | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));

                        result.PartnerResult = ExchangeCardPartnerResult.LoiDoiTheTuDoiTac;
                        result.PartnerMessage = ExchangeCardPartnerResult.LoiDoiTheTuDoiTac.Text();
                        if (!string.IsNullOrEmpty(strApiException))
                            result.PartnerMessage = ExchangeCardPartnerResult.LoiDoiTheTuDoiTac.Text() + " | " + strApiException;

                        return result;
                    default:
                        result.Result = ExChangeCardStatus.Failure;
                        result.Message = ExChangeCardStatus.Failure.Text();
                        CommonLogger.DefaultLogger.Debug("Tài khoản thẻ đã sử dụng hết hạn mức | UserId: " + userId +
                                                               " - Amount: " + amount +
                                                               " | Data doi tac: " + JsonConvert.SerializeObject(hqCardResponse));

                        result.PartnerResult = hqCardResponse.e.ToEnum<ExchangeCardPartnerResult>();
                        result.PartnerMessage = hqCardResponse.e.ToEnum<ExchangeCardPartnerResult>().Text();
                        if (!string.IsNullOrEmpty(strApiException))
                            result.PartnerMessage = hqCardResponse.e.ToEnum<ExchangeCardPartnerResult>().Text() + " | " + strApiException;

                        return result;
                }
            }
            else
            {
                result.Result = ExChangeCardStatus.LoiKiemTraKhoTheTraVeNull;
                result.Message = ExChangeCardStatus.LoiKiemTraKhoTheTraVeNull.Text();

                result.PartnerResult = ExchangeCardPartnerResult.LoiKiemTraKhoTheTraVeNull;
            }

            return result;

        }

        /// <summary>
        /// Trừ tích lũy để đổi thưởng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="goldtrans"></param>
        /// <param name="reasonCoin"></param>
        /// <returns></returns>
        /// <history>
        /// 22/3/2016 Create By MinhT
        /// </history>
        private bool UpdateStar(int userId, decimal goldtrans, int reasonCoin)
        {
            return _starRepository.UpdateStar(userId, goldtrans, reasonCoin) == 1;//thanh cong

        }

        public bool SubtractGoldExchangeCard(int userId, decimal goldTrans, out decimal subtractInNormal, out decimal subtractInCoffer)
        {
            subtractInNormal = 0;
            subtractInCoffer = 0;
            try
            {
                var value = _walletRepo.SubtractGoldExchangeCard(userId, goldTrans);
                if (value.SubtractInCoffer > 0 || value.SubtractInNormal > 0)
                {
                    subtractInNormal = value.SubtractInNormal.GetValueOrDefault();
                    subtractInCoffer = value.SubtractInCoffer.GetValueOrDefault();

                    // cap nhat gold cho user tren server game - Duynd - 05/05/2016
                    decimal decGoldTrans = -1 * goldTrans;
                    Task task = _accountBusiness.UpdateGoldUser(userId, 2, decGoldTrans);
                    //#end

                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Logger.CommonLogger.DefaultLogger.Debug(string.Format("Lỗi store Out_Wallet_SubtractGoldExchangeCard. Error Message: {0}, InnerException Message: {1}", e.Message, e.InnerException.Message));
                return false;
            }

        }

        ///// <summary>
        ///// Kiểm tra User có đủ tiền để đổi thưởng không?
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="cardTypeId"></param>
        ///// <param name="cardAmount"></param>
        ///// <param name="essentialGold"></param>
        ///// <returns></returns>
        //public bool CheckEnoughGold(int userId, int cardTypeId, int cardAmount, out decimal essentialGold)
        //{
        //    //var checkEnoughGold = _starRepository.WalletStarCheckEnoughGold(userId, cardTypeId, cardAmount);
        //    var checkEnoughGold = _walletRepo.CheckEnoughGold(userId, cardTypeId, cardAmount);
        //    essentialGold = checkEnoughGold.EssentialGold.GetValueOrDefault();
        //    return checkEnoughGold.Result == ResultCheckEnoughGold.Enough.Value();
        //}

        #region kiểm tra có phải là farmer không
        private ExchangeConfig CheckFarmer(ExchangeConfig exchangeConfig, int userId)
        {

            var checkFarmer = _farmerRepository.CheckIsFarmer(userId);
            if (!checkFarmer) return exchangeConfig;
            exchangeConfig.IsApproval = true;
            exchangeConfig.RoundId = 0;
            exchangeConfig.Result = ExChangeCardStatus.WaitApproval;
            exchangeConfig.Message = ExChangeCardStatus.WaitApproval.Text();

            return exchangeConfig;

        }
        #endregion

        #region kiểm tra vượt quá mức tối đa || đã đổi trước đó chưa
        private ExchangeConfig CheckOverAndExchangeBefore(ExchangeConfig exchangeConfig, int userId, int cardAmount, decimal maxEchanged)
        {
            decimal totalGoldExchanged;
            bool isExchanged;
            _exchangeUser.GetTotalGoldAndIsExchanged(userId, cardAmount, out totalGoldExchanged, out isExchanged);

            if (totalGoldExchanged > maxEchanged)
            {
                exchangeConfig.IsApproval = true;
                exchangeConfig.IsOverAmount = true;
                exchangeConfig.RoundId = 1;
                exchangeConfig.Result = ExChangeCardStatus.OverExchanged;
                exchangeConfig.Message = ExChangeCardStatus.OverExchanged.Text();
                return exchangeConfig;
            }

            if (isExchanged) return exchangeConfig;
            exchangeConfig.IsApproval = true;
            exchangeConfig.RoundId = 0;
            exchangeConfig.Result = ExChangeCardStatus.WaitApproval;
            exchangeConfig.Message = ExChangeCardStatus.WaitApproval.Text();
            return exchangeConfig;
        }
        #endregion

        #region kiểm tra đủ gold không
        private CheckEnoughGoldResponse CheckEnoughGold(int userId, int cardTypeId, int cardAmount)
        {
            var response = new CheckEnoughGoldResponse();
            try
            {
                var checkEnoughGold = _walletRepo.CheckEnoughGold(userId, cardTypeId, cardAmount);


                switch ((ResultCheckEnoughGold)checkEnoughGold.Result.GetValueOrDefault())
                {
                    case ResultCheckEnoughGold.CardTypeNotavailable:
                        response.ExChangeCardStatus = ExChangeCardStatus.InvalidCard;
                        response.Message = ExChangeCardStatus.InvalidCard.Text();
                        break;
                    case ResultCheckEnoughGold.NotEnoughGold:
                        response.ExChangeCardStatus = ExChangeCardStatus.NotEnoughCoin;
                        response.Message = ExChangeCardStatus.NotEnoughCoin.Text();
                        response.EssentialGold = checkEnoughGold.EssentialGold.GetValueOrDefault();
                        break;
                    case ResultCheckEnoughGold.Enough:
                        response.ExChangeCardStatus = ExChangeCardStatus.Success;
                        //response.Message = ExChangeCardStatus.Success.Text();
                        response.EssentialGold = checkEnoughGold.EssentialGold.GetValueOrDefault();
                        break;
                    default:
                        response.ExChangeCardStatus = ExChangeCardStatus.Failure;
                        response.Message = ExChangeCardStatus.Failure.Text();
                        break;
                }
                return response;

            }
            catch (Exception)
            {
                return response;
            }
        }
        #endregion
        #endregion

    }
}
