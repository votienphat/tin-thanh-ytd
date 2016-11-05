using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;
namespace DataAccess.Contract.PaymentModule
{
    public interface IExchangeUserRepository : IDaoRepository<LogCardTran>
    {
        void AddNew(int userId, int cardType, string cardName, decimal cardAmount, string ipAddress, int platformId, string hardwareId, int result, bool isApproval, out int logId, int aroundId, string description, int intChannelID);

        int Update(int result, string serial, string pinCode, int partnerId,
            string partnerTransId, string partnerMessage, bool serviceResult, int logId, decimal goldTransfer, int intChannelID);

        void ExchangeCardCancel(int userId, int logId, out int result,out decimal walletNow);
        IEnumerable<Out_ExchangeCard_GetHistory_Result> GetHistory(int rowStart, int rowEnd, out int totalrow);

        void GetTotalGoldAndIsExchanged(int userId, decimal cardAmount, out decimal totalGold, out bool isExchanged);

        Out_LogExchange_GetTotalGoldAndIsExchanged_v2_Result GetTotalGoldAndIsExchanged_V2(int userId, decimal cardAmount);
    }
}
