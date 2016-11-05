using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.LogManagementEntities;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface ILoginLogRepository : IDaoRepository<LoginLog>
    {
        Out_LoginLog_GetForSign_Result Get(int userId, string hardwareId, string appVersion);
    }
}
