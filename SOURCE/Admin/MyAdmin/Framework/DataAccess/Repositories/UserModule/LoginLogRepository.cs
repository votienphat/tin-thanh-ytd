using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.LogManagementEntities;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class LoginLogRepository : DaoRepository<LogManagementEntities, LoginLog>, ILoginLogRepository
    {
        public Out_LoginLog_GetForSign_Result Get(int userId, string hardwareId, string appVersion)
        {
            using (var uow = new UnitOfWork<LogManagementEntities, LoginLog>())
            {
                return uow.Context.Out_LoginLog_GetForSign(hardwareId, userId, appVersion).FirstOrDefault();
            }
        }
    }
}
