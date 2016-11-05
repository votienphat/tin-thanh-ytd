using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.LogManagementEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IRichGameLogRepository : IDaoRepository<RichGameLog>
    {
        /// <summary>
        /// Lấy danh sách đại gia trong game
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 25/4/2016 Create By TaiNM
        /// </history>
        List<Out_RichGameLog_GetData_Result> GetTopRich(int top, int userId);
    }
}