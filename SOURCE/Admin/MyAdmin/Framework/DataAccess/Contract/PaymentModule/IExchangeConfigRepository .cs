using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.PaymentModule
{
    public interface IExchangeConfigRepository : IDaoRepository<ExchangeConfig>
    {
        List<Out_ExchangeConfig_GetByType_Result> GetByType(int userId);
    }
}
