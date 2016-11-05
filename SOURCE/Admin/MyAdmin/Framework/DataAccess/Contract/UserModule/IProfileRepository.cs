using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.LogManagementEntities;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IProfileRepository : IDaoRepository<Profile>
    {
        int UpdatePhone(int userId, string phone);
        int UpdateCMND(int userId, string CMND);

        string GetPhone(int userId);
        string GetCMND(int userId);
    }
}
