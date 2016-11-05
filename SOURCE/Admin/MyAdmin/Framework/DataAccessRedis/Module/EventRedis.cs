using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessRedis.Constants;
using DataAccessRedis.Infrastructure;
using DataAccessRedis.Module.Contract;
using EntitiesObject.Entities.LogManagementEntities;
using Logger;
using Newtonsoft.Json;

namespace DataAccessRedis.Module
{
    public class EventRedis : IEventRedis
    {
        #region Variables
        private IRedisRepository _redisRepository;

        public EventRedis(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }
        #endregion

        /// <summary>
        /// Lay top level user
        /// Duynd - 16/05/2016        
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        public List<Out_LevelGameLog_GetLevelGame_Result> GetTopLevel(int top, int userId, DateTime dtmClearCacheRedis)
        {
            var lstData = new List<Out_LevelGameLog_GetLevelGame_Result>();
            var strKey = RedisKeyConstants.GetTopLevel + ":" + top + ":" + userId;
            var objRedisData = _redisRepository.Get(strKey);
            if (!string.IsNullOrEmpty(objRedisData))
                lstData = JsonConvert.DeserializeObject<List<Out_LevelGameLog_GetLevelGame_Result>>(objRedisData);

            //if (lstData == null || !lstData.Any())
            //{
            //    lstData = _leveGameLogRepository.GetTopLevel(top, userId).ToList();
            //    if (lstData.Any())
            //        _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            //}

            return lstData;
        }

        public void SetTopLevel(int top, int userId, DateTime dtmClearCacheRedis,
            List<Out_LevelGameLog_GetLevelGame_Result> lstData)
        {
            try
            {
                var strKey = RedisKeyConstants.GetTopLevel + ":" + top + ":" + userId;
                if (lstData.Any())
                    _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("SetTopLevel - Redis:", ex);
            }
        }

        /// <summary>
        /// Lay top dai gia
        /// Duynd - 19/05/2016
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        public List<Out_RichGameLog_GetData_Result> GetTopRich(int top, int userId, DateTime dtmClearCacheRedis)
        {
            var lstData = new List<Out_RichGameLog_GetData_Result>();
            var strKey = RedisKeyConstants.GetTopRich + ":" + top + ":" + userId;
            var objRedisData = _redisRepository.Get(strKey);
            if (!string.IsNullOrEmpty(objRedisData))
                lstData = JsonConvert.DeserializeObject<List<Out_RichGameLog_GetData_Result>>(objRedisData);

            //if (lstData == null || !lstData.Any())
            //{
            //    lstData = _richGameLogRepository.GetTopRich(top, userId).ToList();
            //    if (lstData.Any())
            //        _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            //}

            return lstData;
        }

        public void SetTopRich(int top, int userId, DateTime dtmClearCacheRedis,
            List<Out_RichGameLog_GetData_Result> lstData)
        {
            try
            {
                var strKey = RedisKeyConstants.GetTopRich + ":" + top + ":" + userId;
                if (lstData.Any())
                    _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("SetTopRich - Redis:", ex);
            }
        }

        public string[] GetStringArrayKey(string key)
        {
            var result =  _redisRepository.Scan(key);
            var response = result.Select(x => x.ToString());
            return response.ToArray();
        }
    }
}
