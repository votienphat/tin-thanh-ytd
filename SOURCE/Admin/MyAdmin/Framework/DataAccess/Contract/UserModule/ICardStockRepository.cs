using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface ICardStockRepository : IDaoRepository<CardStock>
    {
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
         /// <param name="cardType"></param>
         /// <param name="maxValue"></param>
         /// <param name="quantity"></param>
         /// <param name="description"></param>
         /// <param name="message"></param>
         /// <param name="status"></param>
         /// <param name="userId"></param>
         /// <param name="adminId"></param>
         /// <param name="clientTarget"></param>
         /// <param name="clientIp"></param>
         /// <param name="clientAgent"></param>
         /// <param name="reasonId"></param>
         /// <param name="reasonName"></param>
         /// <param name="cardSerial"></param>
         /// <param name="cardPinCode"></param>
         /// <param name="minValue"></param>
         /// <returns></returns>
         /// <history>
         /// 27/4/2016 Create by TaiNM
         /// 13/6/2016 Updated by TaiNM - Bỏ param Amount, Thêm 2 param MinValue và MaxValue => return Amount
         /// </history>
         int AddLogCardStock(int minValue, int maxValue, int quantity, string description, string message,
             int status, int userId, int adminId, string clientTarget, string clientIp, string clientAgent, int reasonId,
             string reasonName, out string cardSerial, out string cardPinCode, out int cardType);

        /// <summary>
        /// Thêm log card khi lấy thẻ từ đối tác
        /// </summary>
        /// <param name="transExportId"></param>
        /// <param name="cardType"></param>
        /// <param name="cardAmount"></param>
        /// <param name="cardSerial"></param>
        /// <param name="cardPin"></param>
        /// <param name="quantity"></param>
        /// <param name="description"></param>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="userId"></param>
        /// <param name="adminId"></param>
        /// <param name="clientTarget"></param>
        /// <param name="clientIp"></param>
        /// <param name="clientAgent"></param>
        /// <param name="reasonId"></param>
        /// <param name="reasonName"></param>
        /// <param name="cardTypeName"></param>
        /// <returns></returns>
        /// <history>
        /// 27/4/2016 Create by TaiNM
        /// </history>
        bool AddLogCardStock_V2(string transExportId, int cardType, int cardAmount, string cardSerial, string cardPin, int quantity, string description, string message,
            int status, int userId, int adminId, string clientTarget, string clientIp, string clientAgent, int reasonId,
            string reasonName, string cardTypeName);
    }
}
