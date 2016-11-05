using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.LogManagementEntities;

namespace DataAccess.Contract.UserModule
{
    public interface ILeveGameLogRepository : IDaoRepository<LevelGameLog>
    {
        List<Out_LevelGameLog_GetLevelGame_Result> GetTopLevel(int top, int userId);

        List<Out_TopKillBoss_GetTop_Result> GetTopKillBoss(int top);
    }
}