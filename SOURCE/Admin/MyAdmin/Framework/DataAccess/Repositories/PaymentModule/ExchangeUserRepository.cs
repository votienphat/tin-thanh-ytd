using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.PaymentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.PaymentModule
{
    public class ExchangeUserRepository : DaoRepository<UserEntities, LogCardTran>, IExchangeUserRepository
    {
        /// <summary>
        /// Ghi log sau khi trừ Gold User Thành công
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardType"></param>
        /// <param name="cardName"></param>
        /// <param name="cardAmount"></param>
        /// <param name="ipAddress"></param>
        /// <param name="platformId"></param>
        /// <param name="hardwareId"></param>
        /// <param name="result"></param>
        /// <param name="isApproval"></param>
        /// <param name="logId"></param>
        /// <param name="aroundId"></param>
        /// <param name="description"></param>
        /// <history>
        /// Create by MinhT 21/12/2015
        /// </history>
        public void AddNew(int userId, int cardType,string cardName, decimal cardAmount, string ipAddress, int platformId, string hardwareId, int result,bool isApproval, out int logId, int aroundId, string description, int intChannelID)
        {
            logId = 0;
            var outLogId = new ObjectParameter("LogId", logId);
            Uow.Context.Out_LogExchangeCard_AddNew(userId, cardType, cardName, cardAmount, ipAddress, platformId, hardwareId, result, isApproval, outLogId, aroundId, description, intChannelID);
            logId = outLogId.Value == null ? 0 : Int32.Parse(outLogId.Value.ToString());
        }

        /// <summary>
        /// UPdate Log Exchange Card sau khi gọi đối tác đổi thẻ
        /// </summary>
        /// <param name="result"></param>
        /// <param name="serial"></param>
        /// <param name="pinCode"></param>
        /// <param name="partnerId"></param>
        /// <param name="partnerTransId"></param>
        /// <param name="partnerMessage"></param>
        /// <param name="serviceResult"></param>
        /// <param name="logId"></param>
        /// <param name="goldTransfer"></param>
        /// <history>
        /// Create by MinhT 21/12/2015
        /// </history>
        public int Update(int result, string serial, string pinCode, int partnerId, string partnerTransId, string partnerMessage, bool serviceResult, int logId, decimal goldTransfer, int intChannelID)
        {
            return Uow.Context.Out_LogExchangeCard_Update(result, serial, pinCode, partnerId,
                partnerTransId, partnerMessage, serviceResult, logId, goldTransfer, intChannelID);

        }

        public IEnumerable<Out_ExchangeCard_GetHistory_Result> GetHistory(int rowStart, int rowEnd, out int totalrow)
        {
            totalrow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalrow);
            var result = Uow.Context.Out_ExchangeCard_GetHistory(rowStart, rowEnd, outTotalRow).ToList();
            totalrow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result.ToList();
        }


        public void ExchangeCardCancel(int userId, int logId, out int result, out decimal walletNow)
        {
            result = 0;
            walletNow = 0;
            var outResult = new ObjectParameter("Result", result);
            var outWalletNow = new ObjectParameter("WalletNow", walletNow);

            var value = Uow.Context.Out_ExchangeCard_Cancel(logId, userId, outResult, outWalletNow).FirstOrDefault();
            result = outResult.Value == null ? 0 : int.Parse(outResult.Value.ToString());
            walletNow = outWalletNow.Value == null ? 0 : decimal.Parse(outWalletNow.Value.ToString());
        }


        public void GetTotalGoldAndIsExchanged(int userId, decimal cardAmount, out decimal totalAmount, out bool isExchanged)
        {
            totalAmount = 0;
            var outTotalAmount = new ObjectParameter("TotalAmount", totalAmount);
            isExchanged = new bool();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var outisExchanged = new ObjectParameter("IsExchangedThisCardAmount",isExchanged);
            var result = Uow.Context.Out_LogExchange_GetTotalGoldAndIsExchanged(userId, cardAmount, outTotalAmount, outisExchanged);
            totalAmount = outTotalAmount.Value == null ? 0 : decimal.Parse(outTotalAmount.Value.ToString());
            isExchanged = outisExchanged.Value != null && bool.Parse(outisExchanged.Value.ToString()); 
        }

        public Out_LogExchange_GetTotalGoldAndIsExchanged_v2_Result GetTotalGoldAndIsExchanged_V2(int userId, decimal cardAmount)
        {
            
            return Uow.Context.Out_LogExchange_GetTotalGoldAndIsExchanged_v2(userId, cardAmount).FirstOrDefault();
        }
    }
}
