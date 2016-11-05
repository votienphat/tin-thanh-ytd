using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessRedis.Constants;
using DataAccessRedis.Infrastructure;
using DataAccessRedis.Module.Contract;
using EntitiesObject.Entities.UserEntities;
using Logger;
using Newtonsoft.Json;

namespace DataAccessRedis.Module
{
    public class FriendRedis : IFriendRedis
    {
        #region Variables
        private IRedisRepository _redisRepository;


        public FriendRedis(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }
        #endregion

        /// <summary>
        /// Lay top ban be co level cao
        /// Duynd - 20/05/2016
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intUserId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        public List<Out_FriendRelationship_GetTopLevelFriend_Result> GetTopLevelFriend(int intUserId, int intTop, int intPageNumber, int intPageSize, DateTime dtmClearCacheRedis)
        {
            var strKey = RedisKeyConstants.GetTopLevelFriend + ":" + intUserId + ":" + intPageNumber + ":" + intPageSize;

            var lstData = new List<Out_FriendRelationship_GetTopLevelFriend_Result>();
            var objRedisData = _redisRepository.Get(strKey);
            if (!string.IsNullOrEmpty(objRedisData))
                lstData = JsonConvert.DeserializeObject<List<Out_FriendRelationship_GetTopLevelFriend_Result>>(objRedisData);

            //if (lstData == null || !lstData.Any())
            //{
            //    int intCount = 0;
            //    int intPosition = (intPageNumber - 1) * intPageSize;
            //    lstData = _friendRelationShipRepo.GetTopLevelFriend(intUserId, intTop, intPosition, intPageSize, out intCount);
            //    if (lstData.Any())
            //        _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            //}

            return lstData;
        }

        public void SetTopLevelFriend(int intUserId, int intTop, int intPageNumber, int intPageSize,
            DateTime dtmClearCacheRedis, List<Out_FriendRelationship_GetTopLevelFriend_Result> lstData)
        {
            try
            {
                var strKey = RedisKeyConstants.GetTopLevelFriend + ":" + intUserId + ":" + intPageNumber + ":" + intPageSize;
                if (lstData.Any())
                    _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("SetTopLevelFriend - Redis:", ex);
            }
        }

        /// <summary>
        /// Lay top danh sach dai gia trong nhom ban be
        /// Duynd - 20/05/2016
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intUserId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        public List<Out_FriendRelationship_GetTopRickFriend_Result> GetTopRickFriend(int intUserId, int intTop, int intPageNumber, int intPageSize, DateTime dtmClearCacheRedis)
        {
            var strKey = RedisKeyConstants.GetTopRickFriend + ":" + intUserId + ":" + intPageNumber + ":" + intPageSize;

            var lstData = new List<Out_FriendRelationship_GetTopRickFriend_Result>();
            var objRedisData = _redisRepository.Get(strKey);
            if (!string.IsNullOrEmpty(objRedisData))
                lstData = JsonConvert.DeserializeObject<List<Out_FriendRelationship_GetTopRickFriend_Result>>(objRedisData);

            //if (lstData == null || !lstData.Any())
            //{
            //    int intCount = 0;
            //    int intPosition = (intPageNumber - 1) * intPageSize;
            //    lstData = _friendRelationShipRepo.GetTopRickFriend(intUserId, intTop, intPosition, intPageSize, out intCount);

            //    if (lstData.Any())
            //        _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            //}

            return lstData;
        }


        public void SetTopRickFriend(int intUserId, int intTop, int intPageNumber, int intPageSize,
            DateTime dtmClearCacheRedis, List<Out_FriendRelationship_GetTopRickFriend_Result> lstData)
        {
            try
            {
                var strKey = RedisKeyConstants.GetTopRickFriend + ":" + intUserId + ":" + intPageNumber + ":" + intPageSize;
                if (lstData.Any())
                    _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("SetTopRickFriend - Redis:", ex);
            }
        }
    }
}
