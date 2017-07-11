using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.Web;
using DataAccess.EF;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.WebEntities;

namespace DataAccess.Repositories.Web
{
    public class SloganRepository : DaoRepository<WebEntities, Slogan>, ISloganRepository
    {
        public List<Out_Slogan_Get_Result> SloganGet()
        {
            return Uow.Context.Out_Slogan_Get().ToList();
        }

        public Out_Slogan_GetById_Result Get(int id)
        {
            return Uow.Context.Out_Slogan_GetById(id).FirstOrDefault();
        }
    }
}