using DataAccess.Contract.MarketModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.MarketModule
{
    public class KiotUserRepository : DaoRepository<UserEntities, KiotUser>, IKiotUserRepository
    {
    }
}
