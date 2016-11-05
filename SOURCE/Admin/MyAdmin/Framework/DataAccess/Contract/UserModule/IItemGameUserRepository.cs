using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IItemGameUserRepository : IDaoRepository<ItemGameUser>
    {
        List<Out_ItemGameUser_GetList_Result> GetListItemByUserId(int userId);

        List<Out_ItemGame_ProcessOpenGiftBox_Result> OpenGiftBox(int userId, double value1, double value2,
            long x);

        List<Out_ItemGame_ProcessOpenGiftBoxTypeCard_Result> OpenGiftBoxTypeCard(int userId, double value1,
            double value2, long x);

        List<Out_ItemGame_ProcessOpenGiftBoxTypeCard_V2_Result> OpenGiftBoxTypeCard_V2(int userId, double value1,
            double value2, long x);

        /// <summary>
        /// Mở hộp quà
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 5/5/2016 Create By TaiNM
        /// </history>
        Out_ItemGame_ProcessOpenGiftBoxTypeCard_V3_Result OpenGiftBoxTypeCard_V3(int userId);

        /// <summary>
        /// Mở hộp quà
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 13/6/2016 Create By TaiNM
        /// </history>
        Out_ItemGame_ProcessOpenGiftBoxTypeCard_V4_Result OpenGiftBoxTypeCard_V4(int userId);

        /// <summary>
        /// Cập nhật status cho ItemGameUser
        /// </summary>
        /// <param name="itemGameUserId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <history>
        /// 13/6/2016 Create By TaiNM
        /// </history>
        void ItemGameUser_UpdateStatus(int itemGameUserId, bool status);

        int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status,
            string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId);
    }
}