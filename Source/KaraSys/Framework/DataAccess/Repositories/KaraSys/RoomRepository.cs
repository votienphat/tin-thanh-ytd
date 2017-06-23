using DataAccess.Repositories.Infrastructure;
using DataAccess.EF;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.KaraSys;
using EntitiesObject.Entities.KaraSysEntities;

namespace DataAccess.Repositories.KaraSys
{
    public class RoomRepository : DaoRepository<KaraSysEntities, Room>, IRoomRepository
    {
        public List<Out_Room_GetAll_Result> GetAll()
        {
            return Uow.Context.Out_Room_GetAll().ToList();
        }
    }
}