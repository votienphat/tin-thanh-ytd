using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IFarmerRepository : IDaoRepository<Farmer>
    {
        bool CheckIsFarmer(int userId);
    }
}