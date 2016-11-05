using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.LogManagementEntities;

namespace DataAccess.Repositories.UserModule
{
    public class RichGameLogRepository : DaoRepository<LogManagementEntities, RichGameLog>, IRichGameLogRepository
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
        public List<Out_RichGameLog_GetData_Result> GetTopRich(int top, int userId)
        {
            return Uow.Context.Out_RichGameLog_GetData(top, userId).ToList();
        }
    }
}