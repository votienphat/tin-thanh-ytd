using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.Membership;
using DataAccess.Contract.WebUser;
using DataAccess.Entity;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.MetroUserEntities;

namespace DataAccess.Repositories.WebUser
{
    public class CategorieRepository : DaoRepository<MetroUserEntities, Category>, ICategorieRepository
    {
        public IEnumerable<Out_Categories_Get_Result> GetListCategorie()
        {
            return Uow.Context.Out_Categories_Get().ToList();
        }
    }
}
