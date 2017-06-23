using System.Collections.Generic;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.KaraSysEntities;

namespace DataAccess.Contract.KaraSys
{
    public interface IRoomRepository : IDaoRepository<Room>
    {
        List<Out_Room_GetAll_Result> GetAll();
    }
}
