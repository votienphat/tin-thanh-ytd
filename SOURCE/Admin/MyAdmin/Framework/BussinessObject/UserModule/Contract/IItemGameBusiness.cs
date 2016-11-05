using System.Collections.Generic;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.UserModule.Contract
{
    public interface IItemGameBusiness
    {
        /// <summary>
        /// Lấy danh sách vật phẩm 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 27/2/2016 Create By MinhT
        /// </history>
        List<Out_ItemGame_GetItemGameList_Result> GetListItem();

        /// <summary>
        /// Lấy danh sách vật phẩm trong rương của User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 2/3/2016 Create By MinhT
        /// </history>
        List<Out_ItemGameUser_GetList_Result> GetListItemByUserId(int userId);
    }
}