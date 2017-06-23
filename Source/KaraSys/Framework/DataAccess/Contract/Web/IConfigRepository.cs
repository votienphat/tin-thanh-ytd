using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface IConfigRepository : IDaoRepository<Config>
    {
        Out_Config_GetByKey_Result ConfigGetByKey(string key);
        int SaveConfigKey(string key,string value);
        
    }
}
