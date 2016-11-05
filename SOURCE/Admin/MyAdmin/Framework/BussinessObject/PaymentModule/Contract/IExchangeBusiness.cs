using System;
using System.Collections.Generic;
using BussinessObject.Helper;
using BussinessObject.PaymentModule.Models;
using EntitiesObject.Entities.UserEntities;
using EntitiesObject.Model.Payment;

namespace BussinessObject.PaymentModule.Contract
{
    public interface IExchangeBusiness
    {
        /// <summary>
        /// Lấy danh sách loại thẻ đổi
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 21/12/2015: Created by PhatVT
        /// </history>
        ExchangeCard GetExchangeCard();

        ExchangeCardCancel CancelExchangeCard(int userId, int logId);
        //ExchargeCardHistoryResponse GetExchangeCardHistory(int userId, int pageNumber, int pageSize);

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
         ExchargeCardHistoryResponse GetExchangeCardHistory(int userId, int pageNumber, int pageSize,
            DateTime dtmClearCacheRedis);

        ExchangeCardResponse ExchangeCard(string token, int userId, int cardTypeId, int cardAmount, string ipAddress,
           int platfornId, string hardwareId, PaymentServiceConfig serviceConfig, decimal maxEchanged, bool isApplyRule, int defaultAdminId, SocketModel socket, int intChannelID);
        ExchangeCardResponse ExchangeCard_V2(string token, int userId, int cardTypeId, int cardAmount, string ipAddress,
          int platfornId, string hardwareId, PaymentServiceConfig serviceConfig, decimal maxEchanged, int defaultAdminId, SocketModel socket, bool isAutoExchange, int intChannelID);

        ExchangeCardResponse ExchangeCard_V3(ExchangeCardRequest request);

        //List<Out_RankingDetail_GetTopExChangeCard_Result> GetTopExChangeCard(int start, int end);

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
        List<TopChargeCardModel> GetTopExChangeCard(int pageSize, int start, int end, DateTime dtmClearCacheRedis);

        ExchangeCardResponse ExchangeCard_V4(ExchangeCardRequest request);
    }
}
