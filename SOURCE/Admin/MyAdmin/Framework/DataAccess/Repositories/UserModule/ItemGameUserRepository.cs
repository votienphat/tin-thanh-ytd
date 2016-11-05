using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class ItemGameUserRepository : DaoRepository<UserEntities, ItemGameUser>, IItemGameUserRepository
    {

        public List<Out_ItemGameUser_GetList_Result> GetListItemByUserId(int userId)
        {
            return Uow.Context.Out_ItemGameUser_GetList(userId).ToList();
        }

        public List<Out_ItemGame_ProcessOpenGiftBox_Result> OpenGiftBox(int userId, double value1, double value2, long x)
        {
            return Uow.Context.Out_ItemGame_ProcessOpenGiftBox(userId, value1, value2, x).ToList();
        }

        public List<Out_ItemGame_ProcessOpenGiftBoxTypeCard_Result> OpenGiftBoxTypeCard(int userId, double value1, double value2, long x)
        {
            return Uow.Context.Out_ItemGame_ProcessOpenGiftBoxTypeCard(userId, value1, value2, x).ToList();
        }

        public List<Out_ItemGame_ProcessOpenGiftBoxTypeCard_V2_Result> OpenGiftBoxTypeCard_V2(int userId, double value1, double value2, long x)
        {
            return Uow.Context.Out_ItemGame_ProcessOpenGiftBoxTypeCard_V2(userId, value1, value2, x).ToList();
        }

        /// <summary>
        /// Mở hộp quà
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 5/5/2016 Create By TaiNM
        /// </history>
        public Out_ItemGame_ProcessOpenGiftBoxTypeCard_V3_Result OpenGiftBoxTypeCard_V3(int userId)
        {

            return Uow.Context.Out_ItemGame_ProcessOpenGiftBoxTypeCard_V3(userId).FirstOrDefault();
        }

        /// <summary>
        /// Mở hộp quà
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 13/6/2016 Create By TaiNM
        /// </history>
        public Out_ItemGame_ProcessOpenGiftBoxTypeCard_V4_Result OpenGiftBoxTypeCard_V4(int userId)
        {

            return Uow.Context.Out_ItemGame_ProcessOpenGiftBoxTypeCard_V4(userId).FirstOrDefault();
        }

        /// <summary>
        /// Cập nhật status cho ItemGameUser
        /// </summary>
        /// <param name="itemGameUserId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <history>
        /// 13/6/2016 Create By TaiNM
        /// </history>
        public void ItemGameUser_UpdateStatus(int itemGameUserId, bool status)
        {

            Uow.Context.Out_ItemGameUser_UpdateStatus(itemGameUserId, status);
        }

        public int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId)
        {
            return Uow.Context.Out_OfflineMessage_Send(senderId, receiverId, messageContent, messageType, status,
                ipAddress, platformId, hardwareId, isSystem, parentId);
        }

    }
}