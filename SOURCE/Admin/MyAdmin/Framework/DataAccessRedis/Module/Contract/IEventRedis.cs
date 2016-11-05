using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.LogManagementEntities;

namespace DataAccessRedis.Module.Contract
{
    public interface IEventRedis
    {
        /// <summary>
        /// Lay top level user
        /// Duynd - 16/05/2016        
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        List<Out_LevelGameLog_GetLevelGame_Result> GetTopLevel(int top, int userId, DateTime dtmClearCacheRedis);

        /// <summary>
        /// Lay top dai gia
        /// Duynd - 19/05/2016
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        List<Out_RichGameLog_GetData_Result> GetTopRich(int top, int userId, DateTime dtmClearCacheRedis);

        void SetTopLevel(int top, int userId, DateTime dtmClearCacheRedis,
            List<Out_LevelGameLog_GetLevelGame_Result> lstData);

        void SetTopRich(int top, int userId, DateTime dtmClearCacheRedis,
            List<Out_RichGameLog_GetData_Result> lstData);

        string[] GetStringArrayKey(string key);
    }
}
