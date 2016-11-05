using System.Collections.Generic;
using BussinessObject.UserModule.Contract;
using DataAccess.Contract.UserModule;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.UserModule
{
    public class ItemGameBusiness : IItemGameBusiness
    {
        private readonly IItemGameReprository _itemGameReprository;
        private readonly IItemGameUserRepository _itemGameUserRepository;

        public ItemGameBusiness(IItemGameReprository itemGameReprository, IItemGameUserRepository itemGameUserRepository)
        {
            _itemGameReprository = itemGameReprository;
            _itemGameUserRepository = itemGameUserRepository;
        }

        public List<Out_ItemGame_GetItemGameList_Result> GetListItem()
        {
            return _itemGameReprository.GetListItem();
        }

        /// <summary>
        /// Lấy danh sách vật phẩm trong rương của User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 2/3/2016 Create By MinhT
        /// </history>
        public List<Out_ItemGameUser_GetList_Result> GetListItemByUserId(int userId)
        {
            return _itemGameUserRepository.GetListItemByUserId(userId);
        }
    }
}