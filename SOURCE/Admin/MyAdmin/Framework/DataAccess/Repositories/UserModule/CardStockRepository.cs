using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class CardStockRepository : DaoRepository<UserEntities, CardStock>, ICardStockRepository
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
        public bool CheckCardExists(int cardType, int cardAmount)
        {
            var idResult = 0;
            var result = Uow.Context.Ins_CardStock_CheckExists(cardType, cardAmount).FirstOrDefault();
            if (result != null)
                int.TryParse(result.Value.ToString(), out idResult);
            return idResult > 0;
        }

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
        public int AddLogCardStock(int minValue, int maxValue, int quantity, string description, string message,
            int status, int userId, int adminId, string clientTarget, string clientIp, string clientAgent, int reasonId,
            string reasonName, out string cardSerial, out string cardPinCode, out int cardType)
        {
            cardSerial = string.Empty;
            cardPinCode = string.Empty;
            cardType = 0;
            var objPar1 = new ObjectParameter("CardSerial", cardSerial);
            var objPar2 = new ObjectParameter("CardPin", cardPinCode);
            var objPar3 = new ObjectParameter("CardType", cardType);

            var amount=Uow.Context.Ins_CardStock_LogExport_Add(minValue, maxValue, quantity, description, message,
                status, userId, adminId, clientTarget, clientIp, clientAgent, reasonId, reasonName, objPar1, objPar2, objPar3).FirstOrDefault();
            cardSerial = objPar1.Value.ToString();
            cardPinCode = objPar2.Value.ToString();
            int.TryParse(objPar3.Value.ToString(), out cardType);

            if (amount != null)
                return amount.Value;
            return 0;
        }

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
        public bool AddLogCardStock_V2(string transExportId, int cardType, int cardAmount, string cardSerial, string cardPin, int quantity, string description, string message,
            int status, int userId, int adminId, string clientTarget, string clientIp, string clientAgent, int reasonId,
            string reasonName, string cardTypeName)
        {
            int idResult;
            var result = Uow.Context.Ins_CardStock_LogExport_Add_V2(transExportId, cardType, cardAmount, cardSerial, cardPin, quantity, description, message,
                status, userId, adminId, clientTarget, clientIp, clientAgent, reasonId, reasonName, cardTypeName);
            int.TryParse(result.ToString(), out idResult);
            return idResult > 0;
        }
    }
}
