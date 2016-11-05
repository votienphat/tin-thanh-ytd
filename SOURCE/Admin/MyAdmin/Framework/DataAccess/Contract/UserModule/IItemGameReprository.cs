using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IItemGameReprository : IDaoRepository<ItemGame>
    {
        List<Out_ItemGame_GetItemGameList_Result> GetListItem();
    }
}