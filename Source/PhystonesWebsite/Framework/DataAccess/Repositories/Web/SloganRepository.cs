using DataAccess.Repositories.Infrastructure;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using EntitiesObject.Entities.WebEntities;

namespace DataAccess.Repositories.Membership
{
    public class SloganRepository : DaoRepository<WebEntities, Slogan>, ISloganRepository
    {
        public List<Out_Slogan_Get_Result> SloganGet()
        {
            return Uow.Context.Out_Slogan_Get().ToList();
        }
    }
}