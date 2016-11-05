
using System;
using System.Collections.Generic;
using System.Threading;
using BussinessObject.UserModule.Enums;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.UserModule.Contract
{
    public interface IFriendBusiness
    {
        /// <summary>
        /// Lấy số lượng bạn của user theo userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 17/3/2016 Create By TaiNM
        /// </history>
        int GetTotalFriend(int userId);

        int SaveInviteExistsOpenProviderId(int userId, int userFriendId, string openProviderId, string description,
            string delimiter, int relationship);

        int SaveInviteFriendRegisted(int userId, int userFriendId, string openProviderId, string description,
            string delimiter, int relationship);

        /// <summary>
        /// Lấy danh sách bạn bè của user
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        /// <history>
        /// 17/3/2016 Create By TaiNM
        /// </history>
        List<Out_FriendRelationship_GetListFriend_Result> GetListFriendByUserId(int position, int pageSize, int userId);

        /// <summary>
        /// Lấy position level của bạn bè
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="top"></param>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <history>
        /// Create by VanNL - 17/03/2016
        /// 22/4/2016 TaiNM Update paging
        /// </history>
        List<Out_FriendRelationship_GetTopLevelFriend_Result> GetTopLevelFriend(int userId, int top, int position, int pageSize, out int count);

        /// <summary>
        /// Lay top level cua ban be (on Redis)
        /// Duynd - 20/05/2016
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="top"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        List<Out_FriendRelationship_GetTopLevelFriend_Result> GetTopLevelFriend(int userId, int top, int pageNumber,
            int pageSize, DateTime dtmClearCacheRedis);

        List<Out_FriendRelationship_GetTopRickFriend_Result> GetTopRickFriend(int userId, int top, int position, int pageSize, out int count);

        /// <summary>
        /// Lay top danh sach dai gia trong nhom ban be (on Redis)
        /// Duynd - 20/05/2016
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="top"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        List<Out_FriendRelationship_GetTopRickFriend_Result> GetTopRickFriend(int userId, int top, int pageNumber,
            int pageSize, DateTime dtmClearCacheRedis);

        /// <summary>
        /// Kết bạn và ghi log
        /// - Update dòng người mời
        /// - Thêm dòng kết bạn
        /// </summary>
        /// <param name="userFriendId"></param>
        /// <returns>UserId mời kết bạn</returns>
        /// <history>
        /// 18/3/2016 Create By TaiNM
        /// </history>
        int MakeFriend(int userFriendId);

        /// <summary>
        /// Kết bạn và ghi log
        /// - Update danh sách người mời
        /// - Thêm danh sách bạn cho userFriendId
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

        GiftGoldForFriendEnum GiftCoinForFriend(int userId, int friendId, decimal goldGift, int times, int reasonSend,
            int reasonRecieve);
    }
}
