using DataAccess.Repositories.Infrastructure;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using EntitiesObject.Entities.WebEntities;

namespace DataAccess.Repositories.Web
{
    public class ConfigRepository : DaoRepository<WebEntities, Config>, IConfigRepository
    {
        public Out_Config_GetByKey_Result ConfigGetByKey(string key)
        {
            var result = Uow.Context.Out_Config_GetByKey(key).FirstOrDefault();
            return result;
        }
        public int SaveConfigKey(string key,string value)
        {
            var result = Uow.Context.Out_Config_SaveByKey(key,value);
            return result;
        }
    }
}