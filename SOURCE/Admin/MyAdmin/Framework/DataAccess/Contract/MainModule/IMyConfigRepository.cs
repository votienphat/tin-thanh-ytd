using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.MainModule
{
    public interface IMyConfigRepository : IDaoRepository<MyConfig>
    {
        Out_MyConfig_GetCardConfig_Result Get(string key);
        List<Ins_MyConfig_GetConfigByKey_Result> GetMyConfigGetConfigByKey(string key);
        List<Out_Call_DBGame_GetServer_Result> GetIpPortServer(bool isAndroid, bool isIos);
    }
}
