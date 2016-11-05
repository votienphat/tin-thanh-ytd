using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.LogManagementEntities;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class ItemGameReprository : DaoRepository<UserEntities, ItemGame>, IItemGameReprository
    {

        public List<Out_ItemGame_GetItemGameList_Result> GetListItem()
        {
            return Uow.Context.Out_ItemGame_GetItemGameList().ToList();
        }
    }
}