using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class FriendRelationShipRepository : DaoRepository<UserEntities, FriendRelationship>, IFriendRelationShipRepository
    {
        public int GetTotalFriend(int userId)
        {
            return Uow.Context.Out_FriendRelationship_GetTotalFriend(userId).FirstOrDefault().GetValueOrDefault(0);
        }

        public List<Out_FriendRelationship_GetListFriend_Result> GetListFriend(int position, int pageSize, int userId)
        {
            return Uow.Context.Out_FriendRelationship_GetListFriend(position, pageSize, userId).ToList();
        }


        public int SaveInviteFriendRegisted(int userId, int userFriendId, string openProviderId, string description, string delimiter, int relationship)
        {
            return Uow.Context.Out_FriendRelationship_SaveInviteFriendRegisted(userId, userFriendId, openProviderId, description, delimiter,
                relationship);
        }

        public int SaveInviteExistsOpenProviderId(int userId, int userFriendId, string openProviderId, string description, string delimiter, int relationship)
        {
            return Uow.Context.Out_FriendRelationship_SaveInviteExistsOpenProviderId(userId, userFriendId, openProviderId, description, delimiter,
                relationship);
        }

        public List<Out_FriendRelationship_GetTopLevelFriend_Result> GetTopLevelFriend(int userId, int top, int position, int pageSize, out int count)
        {
            count = 0;
            var objParam = new ObjectParameter("TotalRow", count);
            var results = Uow.Context.Out_FriendRelationship_GetTopLevelFriend(userId, top, position, pageSize, objParam).ToList();
            int.TryParse(objParam.Value.ToString(), out count);
            return results;
        }

        public List<Out_FriendRelationship_GetTopRickFriend_Result> GetTopRickFriend(int userId, int top, int position, int pageSize, out int count)
        {
            count = 0;
            var objParam = new ObjectParameter("TotalRow", count);
            var results = Uow.Context.Out_FriendRelationship_GetTopRickFriend(userId, top, position, pageSize, objParam).ToList();
            int.TryParse(objParam.Value.ToString(), out count);
            return results;
        }

        /// <summary>
        /// Kết bạn
        /// </summary>
        /// <param name="userFriendId"></param>
        /// <returns>UserId mời kết bạn</returns>
        /// <history>
        /// 18/3/2016 Create By TaiNM
        /// </history>
        public int MakeFriend(int userFriendId)
        {
            return Uow.Context.Out_FriendRelationship_MakeFriend(userFriendId);
        }

        /// <summary>
        /// Kết bạn
        /// </summary>
        /// <param name="userFriendId"></param>
        /// <returns>UserId mời kết bạn</returns>
        /// <history>
        /// 24/3/2016 Create By TaiNM
        /// </history>
        public int MakeFriend_V2(int userFriendId)
        {
            var result = 0;
            var opResult = new ObjectParameter("Result", result);
            Uow.Context.Out_FriendRelationship_MakeFriend_V2(userFriendId, opResult);
            int.TryParse(opResult.Value.ToString(), out result);
            return result;
        }

        public int RemoveFriend(int userId, int userFriendId)
        {
            return Uow.Context.Out_FriendRelationship_RemoveFriend(userId, userFriendId);
        }

        public List<Out_FriendRelationship_GetFriendInfo_Result> GetFriendInfo(int friendId)
        {
            return Uow.Context.Out_FriendRelationship_GetFriendInfo(friendId).ToList();
        }

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
        public bool GiftCoinFriend(int userId, int userFriendId, decimal coinTransfer, int amoutPersionOnDay, out DateTime dateCreate)
        {
            dateCreate = DateTime.Now;
            var objParam = new ObjectParameter("DateCreate", dateCreate);
            var result = Uow.Context.Out_Wallet_GiftCoinFriend(userId, userFriendId, coinTransfer, amoutPersionOnDay, objParam).FirstOrDefault();
            DateTime.TryParse(objParam.Value.ToString(), out dateCreate);
            return (result.HasValue ? result.Value : 0) > 0;
        }

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
        public bool AutoMakeFriendFb(int userId, string openProviderIds, string delimiter)
        {
            return Uow.Context.Out_FriendRelationShip_AutoMakeFriend(userId, openProviderIds, delimiter) > 0;
        }

        public int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId)
        {
            return Uow.Context.Out_OfflineMessage_Send(senderId, receiverId, messageContent, messageType, status,
                ipAddress, platformId, hardwareId, isSystem, parentId);
        }

        /// <summary>
        /// Kiểm tra user có là bạn của nhau chưa
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userFriendId"></param>
        /// <returns></returns>
        public bool CheckIsFriend(int userId, int userFriendId)
        {
            var result = Uow.Context.Out_FriendRelationship_CheckIsFriend(userId, userFriendId).FirstOrDefault();
            if (result != null)
                return result > 0;
            return false;
        }

        public int GiftCoinForFriend(int userId, int friendId, decimal goldGift, int times, int reasonSend,
            int reasonRecieve)
        {
            return
                Uow.Context.Out_FriendRelationship_GiftGoldForFriend(userId, friendId, goldGift, times, reasonSend,
                    reasonRecieve).FirstOrDefault().GetValueOrDefault(0);
        }
    }
}
