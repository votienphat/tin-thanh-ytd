using System.Collections.Generic;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface IPlainRepository : IDaoRepository<Plain>
    {
        List<Out_Plain_GetByType_Result> GetByType(int type);

    }
}
