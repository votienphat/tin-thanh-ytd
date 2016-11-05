using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class FarmerRepository : DaoRepository<UserEntities, Farmer>, IFarmerRepository
    {
        public bool CheckIsFarmer(int userId)
        {
            var value =  Uow.Context.Out_Farmer_CheckIsFarmer(userId).FirstOrDefault();
            return value.GetValueOrDefault();
        }
    }
}