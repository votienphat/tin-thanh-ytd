using System.Data.Entity.Core.Objects;
using System.Security;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.LogManagementEntities;
using EntitiesObject.Entities.UserEntities;
namespace DataAccess.Contract.PaymentModule
{
    public interface IExchangeLogRepository : IDaoRepository<LogExchangeCard>
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
        int AddNew(int userId, int exchangeType, int platformId, string IMEI, string hardwareId,
             string ipAddress, int status, string transId);

        int Update(int userId, int status, string transId);
    }
}
