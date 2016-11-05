using System.Linq;
using DataAccess.Contract.PaymentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.LogManagementEntities;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.PaymentModule
{
    public class ExchangLogRepository : DaoRepository<LogManagementEntities, LogExchangeCard>, IExchangeLogRepository
    {
        /// <summary>
        /// Update Log Exchange Card sau khi gọi đối tác đổi thẻ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="exchangeType"></param>
        /// <param name="platformId"></param>
        /// <param name="IMEI"></param>
        /// <param name="hardwareId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="status"></param>
        /// <param name="transId"></param>
        /// <returns></returns>
        public int AddNew(int userId, int exchangeType, int platformId, string IMEI, string hardwareId, string ipAddress, int status, string transId)
        {
            //return Uow.Context.Out_ExchangeLog_AddNew(userId, exchangeType, platformId, IMEI, hardwareId, ipAddress, status, transId);
            return 1;
        }

        public int Update(int userId, int status, string transId)
        {
            //return Uow.Context.Out_ExchangeLog_Update(userId, status, transId);
            return 1;
        }
    }
}
