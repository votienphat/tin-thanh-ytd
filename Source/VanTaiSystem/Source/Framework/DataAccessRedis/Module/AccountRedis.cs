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
    public class AccountRedis : IAccountRedis
    {
        #region Variables
        private readonly IRedisRepository _redisRepository;

        public AccountRedis(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }
        #endregion

        /// <summary>
        /// Lay danh sach user online
        /// Duynd - 16/05/2016
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intUserId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        public List<Out_Account_GetUsersOnline_V2_Result> GetUserOnline(int intPageSize, int intUserId, DateTime dtmClearCacheRedis)
        {
            var strKey = RedisKeyConstants.GetUsersOnline + ":" + intPageSize + ":" + intUserId; // Them userID - Duynd - 18/05/2016
            var lstData = new List<Out_Account_GetUsersOnline_V2_Result>();

            try
            {
                if (intUserId == 10010)
                {
                    CommonLogger.DefaultLogger.Debug("GetUserOnline | Start");
                }

                var objRedisData = _redisRepository.Get(strKey);
                if (!string.IsNullOrEmpty(objRedisData))
                    lstData = JsonConvert.DeserializeObject<List<Out_Account_GetUsersOnline_V2_Result>>(objRedisData);
                if (intUserId == 10010)
                {
                    CommonLogger.DefaultLogger.Debug("GetUserOnline | End");
                }

                //if (lstData == null || !lstData.Any()) // || position == 1) // Khong can kiem tra trang dau tien - Duynd - 18/05/2016
                //{
                //    lstData = _accountRepository.GetUsersOnline_V2(0, intPageSize, intUserId).ToList();
                //    if (lstData.Any())
                //        _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
                //}

                //CommonLogger.DefaultLogger.Debug(strKey);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("GetUserOnline", ex);
            }

            return lstData;
        }

        /// <summary>
        /// Nap du lieu user online
        /// Duynd - 19/05/2016
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intUserId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        public void SetUserOnline(int intPageSize, int intUserId, DateTime dtmClearCacheRedis, IEnumerable<Out_Account_GetUsersOnline_V2_Result> lstData)
        {
            try
            {
                var strKey = RedisKeyConstants.GetUsersOnline + ":" + intPageSize + ":" + intUserId;

                //var lstData = _accountRepository.GetUsersOnline_V2(0, intPageSize, intUserId).ToList();
                if (lstData.Any())
                    _redisRepository.Set(strKey, JsonConvert.SerializeObject(lstData), dtmClearCacheRedis);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("SetUserOnline", ex);
            }
        }        
    }
}
