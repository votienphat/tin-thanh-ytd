using System;
using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IFriendRelationShipRepository : IDaoRepository<FriendRelationship>
    {
        int GetTotalFriend(int userId);

        int SaveInviteFriendRegisted(int userId, int userFriendId, string openProviderId, string description,
            string delimiter, int relationship);

        int SaveInviteExistsOpenProviderId(int userId, int userFriendId, string openProviderId, string description,
            string delimiter, int relationship);

        List<Out_FriendRelationship_GetListFriend_Result> GetListFriend(int position, int pageSize, int userId);

        List<Out_FriendRelationship_GetTopLevelFriend_Result> GetTopLevelFriend(int userId, int top, int position, int pageSize, out int count);

        List<Out_FriendRelationship_GetTopRickFriend_Result> GetTopRickFriend(int userId, int top, int position, int pageSize, out int count);

        /// <summary>
        /// Kết bạn
        /// </summary>
        /// <param name="userFriendId"></param>
        /// <returns>UserId mời kết bạn</returns>
        /// <history>
        /// 18/3/2016 Create By TaiNM
        /// </history>
        int MakeFriend(int userFriendId);

        /// <summary>
        /// Kết bạn
        /// </summary>
        /// <param name="userFriendId"></param>
        /// <returns>UserId mời kết bạn</returns>
        /// <history>
        /// 24/3/2016 Create By TaiNM
        /// </history>
        int MakeFriend_V2(int userFriendId);

        int RemoveFriend(int userId, int userFriendId);

        List<Out_FriendRelationship_GetFriendInfo_Result> GetFriendInfo(int friendId);

        /// <summary>
        /// Tặng quà cho user khác
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userFriendId"></param>
        /// <param name="coinTransfer"></param>
        /// <param name="amoutPersionOnDay"></param>
        /// <param name="dateCreate"></param>
        /// <returns></returns>
        /// <history>
        /// 1/4/2016 Updated TaiNM
        /// - 1 ngày tặng tối đa n người và mỗi người chỉ được nhận 1 lần
        /// </history>
        bool GiftCoinFriend(int userId, int userFriendId, decimal coinTransfer, int amoutPersionOnDay, out DateTime dateCreate);

        /// <summary>
        /// Tự động tìm bạn trên fb đã đăng ký trong game mà kết bạn
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="openProviderIds"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        /// <history>
        /// 24/3/2016 Create By TaiNM
        /// </history>
        bool AutoMakeFriendFb(int userId, string openProviderIds, string delimiter);

        int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status,
            string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId);

        /// <summary>
        /// Kiểm tra user có là bạn của nhau chưa
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userFriendId"></param>
        /// <returns></returns>
        bool CheckIsFriend(int userId, int userFriendId);

        int GiftCoinForFriend(int userId, int friendId, decimal goldGift, int times, int reasonSend,
            int reasonRecieve);
    }
}
