using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.EventManagement;

namespace DataAccess.Contract.EventModule
{
    public interface IAwardMakeFriendRepository : IDaoRepository<E1605_AwardMakeFriend>
    {
        bool AwardMakeFriend_CheckExist(int userId, int userFriendId);

        bool AwardMakeFriend_Add(int userId, int userFriendId, decimal gold, string decription);
    }
}
