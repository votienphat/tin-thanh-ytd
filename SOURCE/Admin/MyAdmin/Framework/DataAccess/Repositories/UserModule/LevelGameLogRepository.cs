using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.LogManagementEntities;

namespace DataAccess.Repositories.UserModule
{
    public class LevelGameLogRepository : DaoRepository<LogManagementEntities, LevelGameLog>, ILeveGameLogRepository
    {
        public List<Out_LevelGameLog_GetLevelGame_Result> GetTopLevel(int top, int userId)
        {
            return Uow.Context.Out_LevelGameLog_GetLevelGame(top, userId).ToList();
        }

        public List<Out_TopKillBoss_GetTop_Result> GetTopKillBoss(int top)
        {
            return Uow.Context.Out_TopKillBoss_GetTop(top).ToList();
        }
    }
}