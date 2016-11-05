using DataAccess.Contract.EventModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.EventManagement;

namespace DataAccess.Repositories.EventModule
{
    public class AwardMakeFriendRepository : DaoRepository<EventManagementEntities, E1605_AwardMakeFriend>, IAwardMakeFriendRepository
    {
        public bool AwardMakeFriend_CheckExist(int userId, int userFriendId)
        {
            return Uow.Context.Out_E1605_AwardMakeFriend_CheckExist(userId, userFriendId) > 0;
        }

        public bool AwardMakeFriend_Add(int userId, int userFriendId, decimal gold, string decription)
        {
            return Uow.Context.Out_E1605_AwardMakeFriend_Add(userId, userFriendId, gold, decription) > 0;
        }
    }
}
