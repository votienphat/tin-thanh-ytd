using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.MarketModule
{
    public interface IKiotUserRepository : IDaoRepository<KiotUser>
    {
    }
}
