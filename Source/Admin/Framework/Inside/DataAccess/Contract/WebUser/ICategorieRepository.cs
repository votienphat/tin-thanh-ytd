using System.Collections.Generic;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.MetroUserEntities;

namespace DataAccess.Contract.WebUser
{

    public interface ICategorieRepository : IDaoRepository<Category>
    {
        IEnumerable<Out_Categories_Get_Result> GetListCategorie();
    }
}