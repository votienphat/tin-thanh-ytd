using System;
using DataAccessRedis.Infrastructure;
using DataAccessRedis.Module.Contract;
using Logger;

namespace DataAccessRedis.Module
{
    public class CommonRedis : ICommonRedis
    {
        private readonly IRedisRepository _redisRepository;

        public CommonRedis(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        /// <summary>
        /// Lấy data theo key truyền vào
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <history>
        /// 10/6/2016 Create by TaiNM
        /// </history>
        public string GetData(string key)
        {
            var jsonData = string.Empty;

            try
            {
                jsonData = _redisRepository.Get(key);
                return jsonData;
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("GetData - CommonRedis:", ex);
            }

            return jsonData;
        }

        /// <summary>
        /// Lưu data xuống redis với key và data truyền vào
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        /// <history>
        /// 10/6/2016 Create by TaiNM
        /// </history>
        public bool SetData(string key, DateTime dtmClearCacheRedis, string jsonData)
        {
            try
            {
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(jsonData))
                {
                    _redisRepository.Set(key, jsonData, dtmClearCacheRedis);

                    return true;
                }
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("SetData - CommonRedis:", ex);
            }

            return false;
        }
    }
}
