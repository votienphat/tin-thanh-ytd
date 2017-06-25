using System;

namespace DataAccessRedis.Module.Contract
{
    public interface ICommonRedis
    {
        /// <summary>
        /// Lấy data theo key truyền vào
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <history>
        /// 10/6/2016 Create by TaiNM
        /// </history>
        string GetData(string key);

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
        bool SetData(string key, DateTime dtmClearCacheRedis, string jsonData);
    }
}
