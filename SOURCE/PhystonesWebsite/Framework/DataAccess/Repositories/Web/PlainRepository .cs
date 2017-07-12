using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.Web;
using DataAccess.EF;
using DataAccess.Repositories.Infrastructure;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.WebEntities;

namespace DataAccess.Repositories.Web
{
    public class PlainRepository : DaoRepository<WebEntities, Plain>, IPlainRepository
    {
        public List<Out_Plain_GetByType_Result> GetByType(int type)
        {
            return Uow.Context.Out_Plain_GetByType(type).ToList();
        }
    }
}