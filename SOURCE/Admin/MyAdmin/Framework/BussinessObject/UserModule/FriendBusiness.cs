using System;
using System.Collections.Generic;
using System.Linq;
using BussinessObject.UserModule.Contract;
using BussinessObject.UserModule.Enums;
using DataAccess.Contract.UserModule;
//using DataAccessRedis.Module.Contract;
using DataAccessRedis.Module.Contract;
using EntitiesObject.Entities.UserEntities;
using MyUtility.Extensions;

namespace BussinessObject.UserModule
{
    public class FriendBusiness : IFriendBusiness
    {
        #region Variables

        private readonly IFriendRelationShipRepository _friendRelationShipRepo;
        private readonly IFriendRedis _friendRedis;

        #endregion

        public FriendBusiness(IFriendRelationShipRepository friendRelationShipRepo, IFriendRedis friendRedis)
        {
            _friendRelationShipRepo = friendRelationShipRepo;
            _friendRedis = friendRedis;
        }

        /// <summary>
        /// Lấy số lượng bạn của user theo userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 17/3/2016 Create By TaiNM
        /// </history>
        public int GetTotalFriend(int userId)
        {
            return _friendRelationShipRepo.GetTotalFriend(userId);
        }

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
        public List<Out_FriendRelationship_GetListFriend_Result> GetListFriendByUserId(int position, int pageSize, int userId)
        {
            return _friendRelationShipRepo.GetListFriend(position, pageSize, userId);
        }
        /// <summary>
        /// lưu lời mời khi người được mời đã đăng kí game
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userFriendId"></param>
        /// <param name="openProviderId"></param>
        /// <param name="description"></param>
        /// <param name="delimiter"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        /// <history>
        /// create by VanNL - 17/03/2016
        /// </history>
        public int SaveInviteFriendRegisted(int userId, int userFriendId, string openProviderId, string description, string delimiter, int relationship)
        {
            return _friendRelationShipRepo.SaveInviteFriendRegisted(userId, userFriendId, openProviderId, description, delimiter, relationship); ;
        }

        /// <summary>
        /// lưu lời mời 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userFriendId"></param>
        /// <param name="openProviderId"></param>
        /// <param name="description"></param>
        /// <param name="delimiter"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        /// <history>
        /// create by VanNL - 17/03/2016
        /// </history>
        public int SaveInviteExistsOpenProviderId(int userId, int userFriendId, string openProviderId, string description, string delimiter, int relationship)
        {
            return _friendRelationShipRepo.SaveInviteExistsOpenProviderId(userId, userFriendId, openProviderId, description, delimiter, relationship); ;
        }

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
        public List<Out_FriendRelationship_GetTopLevelFriend_Result> GetTopLevelFriend(int userId, int top, int position, int pageSize, out int count)
        {
            return _friendRelationShipRepo.GetTopLevelFriend(userId, top, position, pageSize, out count);
        }

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
        public List<Out_FriendRelationship_GetTopLevelFriend_Result> GetTopLevelFriend(int userId, int top, int pageNumber, int pageSize, DateTime dtmClearCacheRedis)
        {
            var listData = _friendRedis.GetTopLevelFriend(userId, top, pageNumber, pageSize, dtmClearCacheRedis);
            if (!listData.Any())
            {
                int intCount = 0;
                int intPosition = (pageNumber - 1) * pageSize;
                listData = GetTopLevelFriend(userId, top, intPosition, pageSize, out intCount);
                _friendRedis.SetTopLevelFriend( userId,top,pageNumber,pageSize, dtmClearCacheRedis, listData);
            }
            return listData;
        }

        /// <summary>
        /// Lấy position đại gia bạn bè
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
        public List<Out_FriendRelationship_GetTopRickFriend_Result> GetTopRickFriend(int userId, int top, int position, int pageSize, out int count)
        {
            return _friendRelationShipRepo.GetTopRickFriend(userId, top, position, pageSize, out count);
        }

        /// <summary>
        /// Lay top danh sach dai gia trong nhom ban be
        /// Duynd - 20/05/2016
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="top"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        public List<Out_FriendRelationship_GetTopRickFriend_Result> GetTopRickFriend(int userId, int top, int pageNumber, int pageSize, DateTime dtmClearCacheRedis)
        {
            var listData = _friendRedis.GetTopRickFriend(userId, top, pageNumber, pageSize, dtmClearCacheRedis);
            if (!listData.Any())
            {
                int intCount = 0;
                int intPosition = (pageNumber - 1) * pageSize;
                listData = GetTopRickFriend(userId, top, intPosition, pageSize, out intCount);
                _friendRedis.SetTopRickFriend(userId, top, pageNumber, pageSize, dtmClearCacheRedis, listData);
            }
            return _friendRedis.GetTopRickFriend(userId, top, pageNumber, pageSize, dtmClearCacheRedis);
        }

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
        public int MakeFriend(int userFriendId)
        {
            return _friendRelationShipRepo.MakeFriend(userFriendId);
        }

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
        public int MakeFriend_V2(int userFriendId)
        {
            return _friendRelationShipRepo.MakeFriend_V2(userFriendId);
        }

        /// <summary>
        /// Xoa ban be
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userFriendId"></param>
        /// <returns></returns>
        /// <history>
        /// 22/3/2016 Create By VanNL
        /// </history>
        public int RemoveFriend(int userId, int userFriendId)
        {
            return _friendRelationShipRepo.RemoveFriend(userId, userFriendId);
        }

        public List<Out_FriendRelationship_GetFriendInfo_Result> GetFriendInfo(int friendId)
        {
            return _friendRelationShipRepo.GetFriendInfo(friendId);
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
            return _friendRelationShipRepo.GiftCoinFriend(userId, userFriendId, coinTransfer, amoutPersionOnDay, out dateCreate);
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
            return _friendRelationShipRepo.AutoMakeFriendFb(userId, openProviderIds, delimiter);
        }

        public int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId)
        {
            return _friendRelationShipRepo.SendMessage(senderId, receiverId, messageContent, messageType, status, ipAddress,
                platformId, hardwareId, isSystem, parentId);

        }

        /// <summary>
        /// Kiểm tra user có là bạn của nhau chưa
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userFriendId"></param>
        /// <returns></returns>
        public bool CheckIsFriend(int userId, int userFriendId)
        {
            return _friendRelationShipRepo.CheckIsFriend(userId, userFriendId);
        }


        public GiftGoldForFriendEnum GiftCoinForFriend(int userId, int friendId, decimal goldGift, int times, int reasonSend,
            int reasonRecieve)
        {
            var objResult = GiftGoldForFriendEnum.ThatBai;
            try
            {
                objResult = _friendRelationShipRepo.GiftCoinForFriend(userId, friendId, goldGift, times, reasonSend, reasonRecieve)
                    .ToEnum<GiftGoldForFriendEnum>();
            }
            catch (Exception ex)
            {
                objResult = GiftGoldForFriendEnum.LoiHeThong;
                Logger.CommonLogger.MobileLogger.Error("Lỗi hệ thống:" + ex);
            }
            return objResult;

        }
    }
}
