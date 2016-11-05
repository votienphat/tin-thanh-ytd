using System;
using System.Collections.Generic;
using EntitiesObject.Entities.UserEntities;

namespace DataAccessRedis.Module.Contract
{
    public interface IFriendRedis
    {
        /// <summary>
        /// Lay top ban be co level cao
        /// Duynd - 20/05/2016
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intUserId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        List<Out_FriendRelationship_GetTopLevelFriend_Result> GetTopLevelFriend(int intUserId, int intTop,
            int intPageNumber, int intPageSize, DateTime dtmClearCacheRedis);

        /// <summary>
        /// Lay top danh sach dai gia trong nhom ban be
        /// Duynd - 20/05/2016
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intUserId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        List<Out_FriendRelationship_GetTopRickFriend_Result> GetTopRickFriend(int intUserId, int intTop,
            int intPageNumber, int intPageSize, DateTime dtmClearCacheRedis);

        void SetTopLevelFriend(int intUserId, int intTop, int intPageNumber, int intPageSize,
            DateTime dtmClearCacheRedis, List<Out_FriendRelationship_GetTopLevelFriend_Result> lstData);

        void SetTopRickFriend(int intUserId, int intTop, int intPageNumber, int intPageSize,
            DateTime dtmClearCacheRedis, List<Out_FriendRelationship_GetTopRickFriend_Result> lstData);
    }
}
