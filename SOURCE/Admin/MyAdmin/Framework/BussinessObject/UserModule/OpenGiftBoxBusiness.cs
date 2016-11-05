using System;
using System.Collections.Generic;
using System.Linq;
using BussinessObject.Enums;
using BussinessObject.Helper.Contract;
using BussinessObject.Models;
using BussinessObject.PaymentModule.Enums;
using BussinessObject.PaymentModule.Models;
using BussinessObject.UserModule.Contract;
using DataAccess.Contract.UserModule;
using EntitiesObject.Entities.UserEntities;
using HqCardckStockApi;
using HqCardckStockApi.Model;
using Logger;
using MyUtility;
using MyUtility.Extensions;
using Newtonsoft.Json;


namespace BussinessObject.UserModule
{
    public class OpenGiftBoxBusiness : IOpenGiftBoxBusiness
    {
        #region Variables

        private readonly IItemGameUserRepository _itemGameUserRepo;
        private readonly ILogCardOpenGiftBoxRepository _logCardOpenGiftBoxRepo;
        private readonly IPaymentHelper _paymentHelper;
        private readonly ICardStockRepository _cardStockRepository;

        public OpenGiftBoxBusiness(IItemGameUserRepository itemGameUserRepo, ILogCardOpenGiftBoxRepository logCardOpenGiftBoxRepo, IPaymentHelper paymentHelper, ICardStockRepository cardStockRepository)
        {
            _itemGameUserRepo = itemGameUserRepo;
            _logCardOpenGiftBoxRepo = logCardOpenGiftBoxRepo;
            _paymentHelper = paymentHelper;
            _cardStockRepository = cardStockRepository;
        }

        #endregion

        public List<Out_ItemGame_ProcessOpenGiftBox_Result> OpenGiftBox(int userId, double value1, double value2, long x)
        {
            return _itemGameUserRepo.OpenGiftBox(userId, value1, value2, x).ToList();
        }

        public List<Out_ItemGame_ProcessOpenGiftBoxTypeCard_Result> OpenGiftBoxTypeCard(int userId, double value1, double value2, long x)
        {
            return _itemGameUserRepo.OpenGiftBoxTypeCard(userId, value1, value2, x).ToList();
        }

        public List<Out_ItemGame_ProcessOpenGiftBoxTypeCard_V2_Result> OpenGiftBoxTypeCard_V2(int userId, double value1, double value2, long x)
        {
            return _itemGameUserRepo.OpenGiftBoxTypeCard_V2(userId, value1, value2, x).ToList();
        }

        /// <summary>
        /// Mở hộp quà
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 5/5/2016 Create By TaiNM
        /// </history>
        public Out_ItemGame_ProcessOpenGiftBoxTypeCard_V3_Result OpenGiftBoxTypeCard_V3(int userId)
        {
            return _itemGameUserRepo.OpenGiftBoxTypeCard_V3(userId);
        }

        /// <summary>
        /// Mở hộp quà
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 13/6/2016 Create By TaiNM
        /// </history>
        public Out_ItemGame_ProcessOpenGiftBoxTypeCard_V4_Result OpenGiftBoxTypeCard_V4(int userId)
        {
            return _itemGameUserRepo.OpenGiftBoxTypeCard_V4(userId);
        }

        /// <summary>
        /// Cập nhật status cho ItemGameUser
        /// </summary>
        /// <param name="itemGameUserId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <history>
        /// 13/6/2016 Create By TaiNM
        /// </history>
        public void ItemGameUser_UpdateStatus(int itemGameUserId, bool status)
        {
            _itemGameUserRepo.ItemGameUser_UpdateStatus(itemGameUserId, status);
        }

        public OpenGiftBoxModel OpenGiftBox(OpenGiftBoxModel model, double param1GiftBox, double param2GiftBox, long doanhThuX, PaymentServiceConfig serviceConfig)
        {
            var store = OpenGiftBox(model.UserId, param1GiftBox, param2GiftBox, doanhThuX);
            var def = store.Single();
            if (store != null)
            {
                if (def.moneyItems == null && def.itemNameMG == null)
                {
                    model.ItemName = def.itemName;
                    model.ImagePath = def.imagePath;
                    model.MessagePopup = "Chúc mừng bạn vừa nhận được 1 " + model.ItemName;
                    model.GiftBoxEnum = GiftBoxEnum.ManhGhep.Value();
                    return model;
                }
                if (def.moneyItems != null && def.itemNameMG == null)
                {
                    model.GiftBoxEnum = GiftBoxEnum.TheCao.Value();
                }
                else
                {
                    model.ManhGhep = def.itemNameMG;
                    model.GiftBoxEnum = GiftBoxEnum.TheCaoDuocGhep.Value();
                }
                var itemGameUserId = def.itemGameUserId;
                var cardType = def.cardType;
                var cardAmout = def.moneyItems.HasValue ? def.moneyItems.Value : 0;
                serviceConfig.TestServiceString = string.Format(serviceConfig.TestServiceString, cardAmout);
                //insert vao bang log
                var insert = Insert(model.UserId, 0, itemGameUserId.HasValue ? itemGameUserId.Value : 0, cardType.HasValue ? cardType.Value : 0, cardAmout, DateTime.Now, ExChangeCardStatus.Approvaling.Value(), string.Empty);

                #region Đổi thẻ

                UseCardModel useCardModel;
                var transId = Guid.NewGuid().ToString();

                var doithe = _paymentHelper.DoiTheMoHopQua(cardAmout, transId, cardType.HasValue ? cardType.Value : 0, serviceConfig, model.UserId, out useCardModel);

                #endregion

                if (doithe.Result == ExChangeCardStatus.Success)
                {
                    //Update lại thông tin thẻ cào vừa được đổi
                    Update(insert, useCardModel.PartnerTransId, model.UserId, useCardModel.Serial, useCardModel.PinCard, doithe.Result.Value(), useCardModel.OfflineMessage, cardType.GetValueOrDefault(0), cardAmout);
                    //model.Result = model.ApiStatusCode.Success;
                    model.MessagePopup = "Bạn vừa nhận được thẻ cào. Vui lòng kiểm tra lại hộp thư";
                    model.ImagePath = def.imagePath;
                    model.ItemName = def.itemName;
                    model.MessageMail = "Phần thưởng mở hộp quà." + "\n" + useCardModel.OfflineMessage;
                }
                else
                {
                    model.MessagePopup = "Không thể tải thông tin, vui lòng liên hệ hỗ trợ trực tuyến.";
                }
            }
            return model;
        }

        public int Insert(int userId, int transId, int itemGameuserId, int cardType, int cardAmount, DateTime transDate,
            int result, string partnerMessage)
        {
            return _logCardOpenGiftBoxRepo.Insert(userId, transId, itemGameuserId, cardType, cardAmount,
                transDate, result, partnerMessage);
        }

        public int Update(int id, string transId, int userId, string serial, string pinCode, int result, string partnerMessage, int cardType, int cardAmount)
        {
            return _logCardOpenGiftBoxRepo.Update(id, transId, userId, serial, pinCode, cardType, result, partnerMessage, cardAmount);
        }

        public int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId)
        {
            return _itemGameUserRepo.SendMessage(senderId, receiverId, messageContent, messageType, status,
                ipAddress, platformId, hardwareId, isSystem, parentId);
        }

        // Đổi thẻ khi được thẻ
        public ExchangeCardResponse DoiTheMoHopQua(int amount, int cardTypeId, PaymentServiceConfig serviceConfig, int userId, out UseCardModel cardInfo)
        {
            CommonLogger.PaymentLogger.Debug("DoiTheMoHopQua - userId: " + userId);
            if (userId == 20600)
            {
                CommonLogger.PaymentLogger.Debug("DoiTheMoHopQua - cardTypeId: " + cardTypeId);
                CommonLogger.PaymentLogger.Debug("DoiTheMoHopQua - serviceConfig: " + JsonConvert.SerializeObject(serviceConfig));
                CommonLogger.PaymentLogger.Debug("DoiTheMoHopQua - amount: " + amount);
            }

            HqCardResponse hqCardResponse = new HqCardResponse()
            {
                e = -1
            };
            cardInfo = new UseCardModel();

            var orderId = Guid.NewGuid().ToString();
            var result = new ExchangeCardResponse();
            if (serviceConfig.IsTest)
            {

                hqCardResponse = new HqCardResponse
                {
                    e = 0,
                    Card = new HqCardItem
                    {
                        OrderID = orderId,
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
                    CommonLogger.PaymentLogger.Debug("DoiTheMoHopQua - cardTypeId: " + cardTypeId);
                }

                var hqRequest = new HqCardRequest
                {
                    OrderId = orderId,
                    Price = amount,
                    Telco = cardTypeId.ToEnum<HqCardTypeEnum>().Text()
                };
                try
                {
                    hqCardResponse = HqCardStockFactory.CardStock.CheckoutCard(hqRequest);
                }
                catch (Exception ex)
                {
                    CommonLogger.PaymentLogger.Error("DoiTheMoHopQua - hqCardResponse: " + ex);
                    hqCardResponse = new HqCardResponse
                    {
                        e = -1,
                        ResponseText = ex.ToString()
                    };
                }

            }
            if (hqCardResponse != null)
            {
                if (userId == 20600)
                {
                    CommonLogger.PaymentLogger.Debug("DoiTheMoHopQua - hqCardResponse: " + JsonConvert.SerializeObject(hqCardResponse));
                }


                switch (hqCardResponse.e)
                {
                    case 0:
                        /*Success*/
                        if (hqCardResponse.Card != null)
                        {
                            result.Result = ExChangeCardStatus.Success;
                            result.TransId = hqCardResponse.Card.OrderID;
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
                                CreateDate = DateTime.Now,
                                OfflineMessage = "Thẻ cào mệnh giá: " + StringCommon.FormatCurrency(amount) +
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
                        result.Result = ExChangeCardStatus.Failure;
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
                        result.Result = ExChangeCardStatus.Failure;
                        result.Message = hqCardResponse.ResponseText;
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
                result.Result = ExChangeCardStatus.Success;
            }
            else
            {
                result.Result = ExChangeCardStatus.Failure;
                result.Message = ExChangeCardStatus.Failure.Text();
            }

            return result;

        }

        /// <summary>
        /// Kiểm tra card có tồn tại không
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardAmount"></param>
        /// <returns></returns>
        /// <history>
        /// 27/4/2016 Create by TaiNM
        /// </history>
        public bool CheckCardExists(int cardType, int cardAmount)
        {
            return _cardStockRepository.CheckCardExists(cardType, cardAmount);
        }

        /// <summary>
        /// Thêm log card và cập nhật card đã sử dụng
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cardSerial"></param>
        /// <param name="cardPinCode"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        /// <history>
        /// 27/4/2016 Create by TaiNM
        /// 13/6/2016 Updated by TaiNM return amount
        /// </history>
        public int AddLogCardStock(LogCardStockModel model, out string cardSerial, out string cardPinCode, out int cardType)
        {
          return  _cardStockRepository.AddLogCardStock(model.MinValue, model.MaxValue, model.Quantity,
                model.Description, model.Message, model.Status, model.UserId, model.AdminId, model.ClientTarget,
                model.ClientIp, model.ClientAgent, model.ReasonId, model.ReasonName, out cardSerial, out cardPinCode, out cardType);
        }

        /// <summary>
        /// Thêm log card khi lấy thẻ từ đối tác
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <history>
        /// 27/4/2016 Create by TaiNM
        /// </history>
        public bool AddLogCardStock_V2(LogCardStockModel model)
        {
            return _cardStockRepository.AddLogCardStock_V2(model.TransExportId, model.CardType, model.CardAmount, model.CardSerial, model.CardPin, model.Quantity,
                model.Description, model.Message, model.Status, model.UserId, model.AdminId, model.ClientTarget,
                model.ClientIp, model.ClientAgent, model.ReasonId, model.ReasonName, model.CardTypeName);
        }
    }
}
