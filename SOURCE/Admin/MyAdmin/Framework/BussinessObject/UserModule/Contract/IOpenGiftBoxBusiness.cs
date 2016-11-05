using System;
using System.Collections.Generic;
using BussinessObject.Models;
using BussinessObject.PaymentModule.Models;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.UserModule.Contract
{
    public interface IOpenGiftBoxBusiness
    {
        List<Out_ItemGame_ProcessOpenGiftBox_Result> OpenGiftBox(int userId, double value1, double value2,
            long x);

        int Insert(int userId, int transId, int itemGameuserId, int cardType, int cardAmount, DateTime transDate,
            int result, string partnerMessage);

        int Update(int id, string transId, int userId, string serial, string pinCode, int result, string partnerMessage, int cardType, int cardAmount);

        ExchangeCardResponse DoiTheMoHopQua(int amount, int cardTypeId, PaymentServiceConfig serviceConfig, int userId,
            out UseCardModel cardInfo);

        int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status,
            string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId);

        OpenGiftBoxModel OpenGiftBox(OpenGiftBoxModel model, double param1GiftBox, double param2GiftBox, long doanhThuX, PaymentServiceConfig serviceConfig);

        List<Out_ItemGame_ProcessOpenGiftBoxTypeCard_Result> OpenGiftBoxTypeCard(int userId, double value1,
            double value2, long x);

        /// <summary>
        /// Mở hộp quà
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 5/5/2016 Create By TaiNM
        /// </history>
        Out_ItemGame_ProcessOpenGiftBoxTypeCard_V3_Result OpenGiftBoxTypeCard_V3(int userId);

        /// <summary>
        /// Mở hộp quà
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 13/6/2016 Create By TaiNM
        /// </history>
        Out_ItemGame_ProcessOpenGiftBoxTypeCard_V4_Result OpenGiftBoxTypeCard_V4(int userId);

        /// <summary>
        /// Cập nhật status cho ItemGameUser
        /// </summary>
        /// <param name="itemGameUserId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <history>
        /// 13/6/2016 Create By TaiNM
        /// </history>
        void ItemGameUser_UpdateStatus(int itemGameUserId, bool status);

        List<Out_ItemGame_ProcessOpenGiftBoxTypeCard_V2_Result> OpenGiftBoxTypeCard_V2(int userId, double value1,
            double value2, long x);

        /// <summary>
        /// Kiểm tra card có tồn tại không
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardAmount"></param>
        /// <returns></returns>
        /// <history>
        /// 27/4/2016 Create by TaiNM
        /// </history>
        bool CheckCardExists(int cardType, int cardAmount);

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
        /// </history>
        int AddLogCardStock(LogCardStockModel model, out string cardSerial, out string cardPinCode, out int cardType);

        /// <summary>
        /// Thêm log card khi lấy thẻ từ đối tác
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <history>
        /// 27/4/2016 Create by TaiNM
        /// </history>
        bool AddLogCardStock_V2(LogCardStockModel model);
    }
}
