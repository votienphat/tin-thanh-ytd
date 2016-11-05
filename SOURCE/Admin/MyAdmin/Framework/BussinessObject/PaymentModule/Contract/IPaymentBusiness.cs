using System;
using System.Collections.Generic;
using BussinessObject.PaymentModule.Enums;
using BussinessObject.PaymentModule.Models;
using EntitiesObject.Entities.UserEntities;
using EntitiesObject.Model.Payment;
using EntitiesObject.Model.PaymentConfig;

namespace BussinessObject.PaymentModule.Contract
{
    public interface IPaymentBusiness
    {
        /// <summary>
        /// Lấy danh sách loại thẻ nạp
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 16/12/2015: Created by PhatVT
        /// </history>
        PaymentCard GetPaymentCard(string paymentCardType);

        /// <summary>
        /// Lấy thông tin cấu hình nạp thẻ qua Google IAP
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 01/12/2015 MinhT: Create New
        /// 3/6/2016 Update TaiNM - add thêm channelId
        /// </history>
        GoogleIAPModel GetGoogleIAP(int paymentType, int channelId);

        /// <summary>
        /// Nạp thẻ
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
        ChargeCardResponse ChargeCard(int userId, string serial, string pinCode, int cardTypeId,
            string iPAddress, int platformId, string hardwareId, PaymentServiceConfig serviceConfig, int defaultAdminId, int intChannelID, string strVersionGame);



        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated:21/12/2015</para>
        /// <para></para>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="depositLogGoogleResult"></param>
        /// <returns></returns>
        int LogCreditTrans_InsertDepositInApp(DepositLogGoogleModel model, ref DepositLogGoogleResultEnum depositLogGoogleResult);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/12/2015</para>
        /// <para>lay coin hien tai cua user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        decimal GetCoinUser(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        LogCreditTran GetOne_LogCreditTran(int id);

        bool Update_LogCreditTran(LogCreditTran entities);

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
        WalletExchangeStatus Wallet_AddOrSubtractGoldUser(int userId, decimal goldTrans, int reasonId, string description, int actionId,
            bool isMobile, int loginType, int clientTarget, ref int historyId, int adminId = 0);

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
        ChangeCardHistoryResponse PaymentLog_GetHistoryPagingByUserId(int userId,
            int pageNumber, int pageSize);

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
        int PaymentLog_InsertData(int userId, int paymentId, int platformId, string imei, string harwareid,
            string ipaddress, int status, int objId, decimal paymentAmount, int itemType);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>cập nhật log nạp gold</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool PaymentLog_UpdateData(int id, int status);


        #region GetTopChargeCard

        ///// <summary>
        ///// Create By: MinhT
        ///// Date Create: 24/12/2015
        ///// Lấy Top nộp card của User
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <returns></returns>
        //List<Out_RankingDetail_GetTopChargeCard_Result> GetTopChargeCard(int start, int end);

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
        List<TopChargeCardModel> GetTopChargeCard(int pageSize, int start, int end, DateTime dtmClearCacheRedis);

        #endregion

        #endregion

        CofferModel DepositOrWithdrawCoffer(int userId, decimal goldTransfer, int actionType, string ipAddress,
            int platformId, string hardwareId);

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Nap tien InAppPurchase</para>
        /// </summary>
        DepositInAppPurchaseResponseModel DepositInAppPurchase(DepositInAppPurchaseModel model);

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
        List<PaymentConfigModel> GetPaymentConfig_Channel_PaymentType(int channelId,
            int paymentTypeId);

        #endregion
    }
}
