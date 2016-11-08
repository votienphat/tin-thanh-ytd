using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.MainModule;
using DataAccess.EF;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.MainModule
{
    public class MyConfigRepository : DaoRepository<UserEntities, MyConfig>, IMyConfigRepository
    {
        public Out_MyConfig_GetCardConfig_Result Get(string key)
        {
            return Uow.Context.Out_MyConfig_GetCardConfig(key).FirstOrDefault();
        }
        public List<Ins_MyConfig_GetConfigByKey_Result> GetMyConfigGetConfigByKey(string key)
        {
            return Uow.Context.Ins_MyConfig_GetConfigByKey(key).ToList();
        }

        public List<Out_Call_DBGame_GetServer_Result> GetIpPortServer(bool isAndroid, bool isIos)
        {
            return Uow.Context.Out_Call_DBGame_GetServer(isAndroid, isIos).ToList();
        }
    }
}
