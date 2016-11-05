using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.PaymentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.PaymentModule
{
    public class ExchangeConfigRepository : DaoRepository<UserEntities, ExchangeConfig>, IExchangeConfigRepository
    {
        public List<Out_ExchangeConfig_GetByType_Result> GetByType(int exchangeType)
        {
            return Uow.Context.Out_ExchangeConfig_GetByType(exchangeType).ToList();
        }
    }
}
