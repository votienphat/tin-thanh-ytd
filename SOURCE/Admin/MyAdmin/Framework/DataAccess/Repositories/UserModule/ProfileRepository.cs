using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.LogManagementEntities;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class ProfileRepository : DaoRepository<UserEntities, Profile>, IProfileRepository
    {

        public int UpdatePhone(int userId, string phone)
        {
            return Uow.Context.Out_Profile_UpdatePhoneNumber(userId, phone);
        }

        public int UpdateCMND(int userId, string CMND)
        {
            return Uow.Context.Out_Profile_UpdateCMND(userId, CMND);
        }

        public string GetPhone(int userId)
        {
            return Uow.Context.Out_Profile_GetPhoneNumber(userId).FirstOrDefault();
        }

        public string GetCMND(int userId)
        {
            return Uow.Context.Out_Profile_GetCMND(userId).FirstOrDefault();
        }
    }
}
