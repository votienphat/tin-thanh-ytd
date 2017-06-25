using System;
using System.Collections.Generic;
using EntitiesObject.Entities.UserEntities;

namespace DataAccessRedis.Module.Contract
{
    public interface IAccountRedis
    {
        /// <summary>
        /// Lay danh sach user online
        /// Duynd - 16/05/2016
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intUserId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        List<Out_Account_GetUsersOnline_V2_Result> GetUserOnline(int intPageSize, int intUserId, DateTime dtmClearCacheRedis);

        /// <summary>
        /// Nap du lieu user online
        /// Duynd - 19/05/2016
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intUserId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        void SetUserOnline(int intPageSize, int intUserId, DateTime dtmClearCacheRedis, IEnumerable<Out_Account_GetUsersOnline_V2_Result> lstData);
    }
}
