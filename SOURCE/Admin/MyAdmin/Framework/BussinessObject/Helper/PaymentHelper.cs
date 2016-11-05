using System;
using BussinessObject.Enums;
using BussinessObject.Helper.Contract;
using BussinessObject.Models;
using BussinessObject.PaymentModule.Enums;
using BussinessObject.PaymentModule.Models;
using EntitiesObject.Entities.UserEntities;
using HqCardckStockApi;
using HqCardckStockApi.Model;
using Logger;
using Microsoft.SqlServer.Server;
using MyUtility;
using MyUtility.Extensions;
using Newtonsoft.Json;

namespace BussinessObject.Helper
{
    public class PaymentHelper : IPaymentHelper
    {
        /// <summary>
        /// Gọi service gạch thẻ
        /// </summary>
        /// <param name="model"></param>
        /// <param name="serviceConfig"></param>
        /// <returns></returns>
        public UseCardResponse UseCard(UseCardModel model, PaymentServiceConfig serviceConfig)
        {
            var response = new UseCardResponse { Result = false };

            if (serviceConfig.IsTest)
            {
                // Nếu bật chế độ test thì không cần gọi đối tác
                response.Message = serviceConfig.TestServiceString;
            }
            else
            {
                var cardTypeName = GetCardTypeName(model.CardType);
                var paymentService = new PaymentService.PaymentService();
                var signature = CreateSignature(cardTypeName, model.Serial, model.PinCard, model.PartnerTransId, serviceConfig.Key);

                // Lấy chuỗi kết quả gọi service
                response.Message = paymentService.UseCard(cardTypeName, model.Serial, model.PinCard, model.PartnerTransId, signature);
            }

            // Thành công sẽ có cấu trúc như sau: 01|transactionId|Amount|0|PartnerId
            // Thất bại sẽ có cấu trúc như sau: 00|Description(transactionId)|Status đối tác|PartnerId
            // Lỗi exception sẽ có cấu trúc như sau: 00|1000|Parse json error|PartnerId
            var splitResult = response.Message.Split(new[] { '|' });
            int partnerId = 0;
            if (splitResult.Length < 1 || splitResult[0] == "00")
            {
                if (splitResult.Length > 3)
                {
                    int.TryParse(splitResult[3], out partnerId);
                }

                // Lấy Partner TransactionId
                if (splitResult[1].Contains("("))
                {
                    var tmps = splitResult[1].Split('(');
                    if (tmps.Length > 1)
                    {
                        response.PartnerTransId = tmps[1].Replace("(", "").Replace(")", "");
                    }
                }

                response.PartnerId = partnerId;
                return response;
            }

            if (splitResult.Length > 4 && splitResult[0] == "01")
            {
                int cardAmount;

                // Lấy mệnh giá thẻ nạp và partner Id
                int.TryParse(splitResult[2], out cardAmount);
                int.TryParse(splitResult[4], out partnerId);

                response.PartnerTransId = model.PartnerTransId;
                response.PartnerId = partnerId;
                response.Amount = cardAmount;
                response.Result = true;
            }

            return response;
        }

        /// <summary>
        /// Tao chu ky de goi PaymentService
        /// </summary>
        /// <param name="cardTypeName"></param>
        /// <param name="serial"></param>
        /// <param name="pinCard"></param>
        /// <param name="transId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string CreateSignature(string cardTypeName, string serial, string pinCard, string transId, string key)
        {
            var secretKey = cardTypeName + serial + pinCard + transId + key;
            var sign = MyUtility.Common.GetMd5Hash(secretKey);
            return sign;
        }

        /// <summary>
        /// Lay ra ten loai the cao
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        /// 
        private static string GetCardTypeName(CardTypeEnum cardType)
        {
            switch (cardType)
            {
                case CardTypeEnum.Mobifone:
                    return "MOBI";

                case CardTypeEnum.Vinaphone:
                    return "VINA";

                case CardTypeEnum.Viettel:
                    return "VT";

                case CardTypeEnum.Gate:
                    return "GATE";
                    case CardTypeEnum.Bit:
                    return "BIT";

                default:
                    throw new Exception("Unknown card type");
            }
        }

        public ExchangeCardResponse DoiTheMoHopQua(int amount, string transId, int cardTypeId, PaymentServiceConfig serviceConfig, int userId, out UseCardModel cardInfo)
        {
            CommonLogger.PaymentLogger.Debug("DoiThe - userId: " + userId);
            if (userId == 15454 || userId == 10010)
            {
                CommonLogger.PaymentLogger.Debug("DoiThe - cardTypeId: " + cardTypeId);
                CommonLogger.PaymentLogger.Debug("DoiThe - serviceConfig: " + JsonConvert.SerializeObject(serviceConfig));
                CommonLogger.PaymentLogger.Debug("DoiThe - amount: " + amount);
            }

            HqCardResponse hqCardResponse = new HqCardResponse()
            {
                e = -1
            };
            cardInfo = new UseCardModel();

            var orderId = transId;
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
                    CommonLogger.PaymentLogger.Debug("DoiThe - cardTypeId: " + cardTypeId);
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
                    CommonLogger.PaymentLogger.Error("DoiThe - hqCardResponse: " + ex);
                    hqCardResponse = new HqCardResponse
                    {
                        e = -1,
                        ResponseText = ex.ToString()
                    };
                }

            }
            if (hqCardResponse != null)
            {
                if (userId == 15454 || userId == 10010)
                {
                    CommonLogger.PaymentLogger.Debug("DoiThe - hqCardResponse: " + JsonConvert.SerializeObject(hqCardResponse));
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
                                CreateDate = DateTime.Now
                                ,
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

        #region Convert receipt data Apple

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2015-11-27</para>
        /// <para>Description: Kiem tra moi truong sandbox apple</para>
        /// </summary>
        /// <returns></returns>
        public bool IsSandboxApple(string receiptData)
        {
            try
            {
                var base64StringDecoded = MyUtility.Common.EncryptBase64(receiptData);
                if (!string.IsNullOrEmpty(base64StringDecoded))
                {
                    var base64StringDecodedTemp = base64StringDecoded.Replace("==", "@@")
                                                                     .Replace("=", ":")
                                                                     .Replace(";", ",")
                                                                     .Replace("@@", "==");

                    var receiptDataModel = JsonConvert.DeserializeObject<InAppPurchaseAppleReceiptModel>(base64StringDecodedTemp);
                    if (receiptDataModel != null)
                    {
                        if (receiptDataModel.Environment.ToLower().Equals("sandbox"))
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.CommonLogger.DefaultLogger.Error("ApiHelper IsSandboxApple Error", ex);
            }

            return false;
        }

        #endregion
    }
}
